create or alter proc sp_GetCustomerOrderHistory (@customerid int,@start int=2020 ,@end int=2020)
as
begin
select o.order_date,sum(oi.list_price*oi.quantity*(1-oi.discount))as'total yearly' from sales.orders o join sales.customers c
on c.customer_id=o.customer_id join sales.order_items oi
on oi.order_id=o.order_id
where year(order_date) between @start and @end or c.customer_id=@customerid
group by order_date
end

exec sp_GetCustomerOrderHistory 5,2019,2024
exec sp_GetCustomerOrderHistory 6
select* from sales.orders where customer_id=5

create or alter proc sp_RestockProduct ( @productid int,@requantity int,@storeid int,@oldquatity int output,@newquantity int output,@status varchar(15) output)
as
begin
set @oldquatity=0
set @newquantity=0
set @status='bad'
if @productid in (select product_id from production.products where product_id=@productid)
begin
select @oldquatity=quantity
from production.stocks
update production.stocks
set quantity=quantity+@requantity,@newquantity=quantity,@status='good'
where product_id=@productid and store_id=@storeid
end
--select @oldquatity as'old',@newquantity as'new',@status as'status' from production.stocks where product_id=@productid and store_id=@storeid
end
select* from production.stocks
declare @old int
declare @new int
declare @status varchar(15)
exec sp_RestockProduct  1,5,1,@old output,@new output,@status output
select @old,@new,@status
exec sp_RestockProduct  1,5,0,@old output,@new output,@status output
select @old,@new,@status


--15
--مش عارف احله
--

create or alter proc sp_SearchProducts (@name varchar(25)=null,@categoryid int=null,@min decimal(10,2)=null,@max decimal(10,2)=null,@sortby varchar(25)= null)
as
begin
select *from production.products
where category_id=@categoryid or product_name like @name or list_price between @min and @max
--order by @sortby
order by
case when @sortby='name' then product_name end, 
case when @sortby='price' then list_price end
end
--exec sp_SearchProducts ,,,,'price'
exec sp_SearchProducts @min=10,@max=50,@sortby='price'

declare @bounce1 decimal(10,2)=.05;
declare @bounce2 decimal(10,2)=.1;
declare @bounce decimal(10,2)=.15;
with staff as(
select s.staff_id,first_name+' '+last_name as'name',sum(oi.quantity*oi.list_price*(1-oi.discount)) as total
from sales.staffs s join
sales.orders o on o.staff_id=s.staff_id join sales.order_items oi
on oi.order_id=o.order_id
group by s.staff_id,first_name+' '+last_name
)
select name,total,
case
when total>5000 then @bounce*total
when total>2000 then @bounce1*total
when total>1000 then @bounce2*total
else 0
end 'bounce based on sales'
from staff 

select p.product_name,c.category_name,s.store_id,ISNULL(s.quantity, 0) AS current_stock,
case 
when c.category_id = 1 then 
case 
when ISNULL(s.quantity, 0) <= 2 then 'Critical - Order 5'
when ISNULL(s.quantity, 0) <= 5 then 'Low - Order 2'
else 'Sufficient'
end
when c.category_id = 2 then 
        case 
        when ISNULL(s.quantity, 0) = 0 then 'Out - Order 50'
        when ISNULL(s.quantity, 0) < 20 then 'Restock - Order 20'
        else 'Sufficient'
        end
        else 
        case 
        when ISNULL(s.quantity, 0) < 10 THEN 'Restock - Order 10'
        else 'Sufficient'
        end
end as restock_status,
case 
when c.category_id = 1 and ISNULL(s.quantity, 0) <= 2 then 5
when c.category_id = 1 and ISNULL(s.quantity, 0) <= 5 then 2
when c.category_id = 2 and ISNULL(s.quantity, 0) = 0 then 50
when c.category_id = 2 and ISNULL(s.quantity, 0) < 20 then 20
when c.category_id not in (1,2) and ISNULL(s.quantity, 0) < 10 then 10
else 0
end as quantity_to_order
from production.stocks s
join production.products p on s.product_id = p.product_id
join production.categories c on p.category_id = c.category_id
order by quantity_to_order desc, current_stock asc;


with customer_totals as (
select c.customer_id,c.first_name,c.last_name,c.email,sum(isnull(oi.quantity * oi.list_price * (1 - oi.discount), 0)) as total_spent
from sales.customers c left join sales.orders o on c.customer_id = o.customer_id
left join sales.order_items oi on o.order_id = oi.order_id
group by c.customer_id,c.first_name,c.last_name,c.email)
select customer_id,first_name,last_name,email,cast(total_spent as decimal(10,2)) as total_spent,
    case 
     when total_spent = 0 then 'no orders yet'
     when total_spent < 1000 then 'bronze'
     when total_spent >= 1000 and total_spent < 5000 then 'silver'
     when total_spent >= 5000 and total_spent < 10000 then 'gold'
     else 'platinum'
    end as loyalty_tier
from customer_totals
order by total_spent desc;

create or alter proc production.sp_discontinue_product ( @product_id int, @replacement_product_id int = null)
as
begin
if exists (select 1 
from sales.order_items oi join sales.orders o 
on oi.order_id = o.order_id 
where oi.product_id = @product_id and o.order_status in (1, 2)
)
begin
if @replacement_product_id is null
begin
print 'cannot discontinue: pending orders exist and no replacement provided'
return
end
update oi
set product_id = @replacement_product_id
from sales.order_items oi
join sales.orders o on oi.order_id = o.order_id
where oi.product_id = @product_id and o.order_status in (1, 2)
end
delete from production.stocks 
where product_id = @product_id
print 'product discontinued successfully'
end