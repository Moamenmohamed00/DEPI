use StoreDB
--select take more time than print
declare @customerid int=1
declare @lasrcutomer int=500--(select max(customer_id)from sales.customers)
declare @total decimal(10,2)=0
while @customerid<=@lasrcutomer
begin
select @total =isnull(sum(oi.quantity*oi.list_price),0)
from sales.customers c inner join sales.orders o
on c.customer_id=o.customer_id inner join sales.order_items oi
on oi.order_id=o.order_id
where c.customer_id=@customerid
if @total>=5000
begin print 'ID '+cast(@customerid as varchar(10))+' Buy ' +convert(varchar(10),@total)+' Status '+'vip'--select customer_id,@total,'vip customer'as'status' from sales.customers where customer_id=@customerid
end
else
begin print 'ID '+cast(@customerid as varchar(10))+' Total '+convert(varchar(10),@total)+' status '+'regular' --select customer_id,total,'Regular customer'as'status' from sales.customers where customer_id=@customerid 
end
print '-------------------------'
set @customerid=@customerid+1
end
--Result Sets < Message Buffer

--whithout loop
declare @count int
declare @all int=(select count(*)from production.products)
declare @Tprice int=1500

set @count=(select count(list_price) from production.products where list_price>@Tprice)
print 'number of product above price '+cast(@count as varchar(10))+' number of product below price ' +cast(@all-@count as varchar(10))
--with loop
declare @productid int=(select min(product_id)from production.products)
declare @lastproductid int=(select max(product_id)from production.products)
declare @thprice decimal(10,2)=1500
declare @counter int=0
declare @price decimal(10,3)
declare @status varchar(25)
while @productid<=@lastproductid
begin
select @price= list_price
from production.products
where product_id=@productid
if @price>@thprice
begin
set @counter=@counter+1
set @status='above'
print 'ID '+cast(@productid as varchar(10))+' price '+cast(@price as varchar(10))+' status '+@status
end
else
set @status='below'
begin print 'ID '+cast(@productid as varchar(10))+' price '+cast(@price as varchar(10))+' status '+@status end
set @productid=@productid+1
end
print 'number of product above price '+cast(@counter as varchar(10))+' number of product below price ' +cast(@lastproductid-@counter as varchar(10))

declare @id int=(select min(staff_id)from sales.staffs)
declare @last int =(select max(staff_id) from sales.staffs)
declare @total2 decimal(10,2)
declare @year int=(select min(year(order_date)) from sales.orders)
while @id<=@last and @year<=2024
begin
select @year=YEAR(o.order_date),@total2=sum(oi.list_price*oi.quantity)
from sales.staffs s join sales.orders o
on s.staff_id=o.staff_id join sales.order_items oi
on o.order_id=oi.order_id
where s.staff_id=@id and year(o.order_date)=@year
group by YEAR(o.order_date)
print 'ID '+cast(@id as varchar(10))+' yaer '+cast(@year as varchar(5))+' total sales '+cast(@total2 as varchar(10))
set @id=@id+1
if @id=@last
begin
set @year=@year+1
set @id=(select min(staff_id)from sales.staffs)
end
end

select @@SERVERNAME,@@VERSION,cast(@@IDENTITY as varchar(5))

declare @quantity int
declare @storeid int=1
declare @producid int=1
while @storeid<=(select max(store_id)from sales.stores)
begin
select @quantity=st.quantity
from production.stocks st join sales.stores s
on st.store_id=s.store_id join production.products p
on p.product_id=st.product_id
where p.product_id=@producid and s.store_id=@storeid
print 'product id '+cast(@producid as varchar(5))+' store id '+cast(@storeid as varchar(5))+' quantity '+cast(@quantity as varchar(10))
if @quantity<10
begin print' Low stock - reorder needed' end
else if @quantity between 10 and 20
begin print' Moderate stock' end
else
print' well stocked'
set @producid=@producid+1
if(@producid=(select max(product_id) from production.products))
begin set @storeid=@storeid+1 end
end

--declare @batch int=0
--declare @countrow int=0
--declare @lowid int=(select min(p.product_id) from production.stocks st join production.products p on p.product_id=st.product_id)
--declare @lastlowid int=(select max(p.product_id) from production.stocks st join production.products p on p.product_id=st.product_id)
--while @lowid<=@lastlowid
--begin
--with lowQuantity as(
--select p.product_id,st.quantity
--from production.stocks st join production.products p
--on st.product_id=p.product_id
--where st.quantity<5 and p.product_id=@lowid)
--update production.stocks set quantity=quantity+10 where product_id=@lowid--can't update cte
--set @countrow=@countrow+1
--print 'batch '+cast(@batch as varchar(5))+' row '+cast(@countrow as varchar(5))+' updated low-stock items by +10 units'
--if @countrow=3
--begin
--set @batch=@batch+1
--set @countrow=0
--end
--set @lowid=@lowid+1
--end
--print 'total batch '+cast(@batch as varchar(5))

--chat solution becaude mine didn't work
declare @batch int = 0;
while 1 = 1
begin
    with lowQuantity as (
        select top (3)
            st.product_id,
            st.store_id,
            st.quantity
        from production.stocks st
        join production.products p
            ON st.product_id = p.product_id
        WHERE st.quantity < 5
        ORDER BY st.quantity ASC
    )
    update st
    set st.quantity = st.quantity + 10
    FROM production.stocks st
    JOIN lowQuantity lq
        ON st.product_id = lq.product_id
       AND st.store_id = lq.store_id;
    -- Stop if no rows updated
    if @@ROWCOUNT = 0 
        break;
    set @batch = @batch + 1;
    print 'Batch ' + cast(@batch as varchar(10)) 
        + ': Updated 3 low-stock items by +10 units';
end;
print 'Total batches updated: ' + cast(@batch as varchar(10));

select product_name,list_price,
case 
when list_price<300 then 'budget'
when list_price between 300 and 800 then'mid-range'
when list_price between 801 and 2000 then 'premium'
else 
'luxury'
end as'categorizes'
from production.products

declare @custid int=5
declare @order int
if(@custid in (select customer_id from sales.customers))
begin
select @order= count(o.customer_id)
from sales.customers c join sales.orders o
on c.customer_id=o.customer_id 
where c.customer_id=@custid
print'customer '+cast(@custid as varchar(5))+' total orders '+cast(@order as varchar(5))
end
else
print 'not exist'

create or alter function CalculateShipping(@total decimal(10,2))
returns decimal(10,2)
as
begin
--one return in end
--if @total>100
--return @total;
--else if @total>50
--return @total-5.99;
--else
--return @total -12.99;
declare @shipcost decimal(10,2)
if @total>100
set @shipcost=0
else if @total between 50 and 99
set @shipcost=5.99
else
set @shipcost=12.99
return @total-@shipcost 
end
select dbo.CalculateShipping(120.6)
select dbo.CalculateShipping(69.31)

create or alter function GetProductsByPriceRange(@min decimal,@max decimal)
returns table
as
return(
select p.product_name,p.model_year,b.brand_name,c.category_name from production.products p join production.categories c
on p.category_id=c.category_id join production.brands b
on b.brand_id=p.brand_id
where p.list_price between @min and @max)

select *from dbo.GetProductsByPriceRange(12.6,100.5)

create or alter function GetCustomerYearlySummary(@customerid int)
returns @table table(
years int,
total_orders int,
total_spend decimal(10,2),
avergeorder_peryear decimal(10,2)
)
as
begin
insert into @table(years,total_orders,total_spend,avergeorder_peryear)
select year(o.order_date),count(o.order_id),sum(oi.quantity*oi.list_price*(1-oi.discount)),avg(oi.quantity*oi.list_price*(1-oi.discount))
from  sales.customers c join sales.orders o
on c.customer_id=o.customer_id join sales.order_items oi
on oi.order_id=o.order_id
where c.customer_id=@customerid
group by YEAR(order_date)
return
end
select * from dbo.GetCustomerYearlySummary(1)
select * from dbo.GetCustomerYearlySummary(6)

create or alter function CalculateBulkDiscount(@qunatity int)
returns decimal(10,2)
as
begin
declare @dicount decimal(10,2)
if @qunatity in (1,2)
set @dicount=0
else if @qunatity between 3 and 5
set @dicount=.05
else if @qunatity between 6 and 9
set @dicount=.1
else
set @dicount=.15
return @dicount
end
select dbo.CalculateBulkDiscount(20)
select dbo.CalculateBulkDiscount(5)
select dbo.CalculateBulkDiscount(2)