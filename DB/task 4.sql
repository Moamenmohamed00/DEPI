use StoreDB;

select p.product_name,p.list_price,
case when list_price<300 then 'Economy'
when list_price between 300 and 999 then 'standard'
when list_price between 1000 and 2499 then 'premium'
else 'luxury'
end as 'price category'
from production.products p

select o.order_id,o.order_status,order_date,
case when order_status=1 then 'order received'
when order_status=2 then 'in preparation'
when order_status=3 then 'order canceeled'
else 'order delivered'
end as'order proccessing',
case
when order_status=1 and datediff(day,order_date,getdate())>5 then 'urgent'
when order_status=2 and datediff(day,order_date,getdate())>3 then 'high'
else 'normal'
end as 'pirority'
from sales.orders o

select o.staff_id,s.first_name+' '+s.last_name as 'name',count(o.order_id)as'handled_orders',
case 
when count(o.order_id)=0 then 'NEW Staff'
when count(o.order_id) between 1 and 10 then 'junior staff'
when count(o.order_id) between 11 and 25 then 'Senior staff'
else 'Expert staff'
end as 'staff category'
from sales.staffs s left join sales.orders o
on s.staff_id=o.staff_id
group by o.staff_id,s.first_name+' '+s.last_name;

select customer_id,first_name,last_name,phone,ISNULL(phone,'Phone not Available')as'missing phone',coalesce(phone,email,'NO Contact Method')as'preferred_contact',street,city,state,zip_code
from sales.customers

select coalesce(street,'NULL'),coalesce(city,'NULL'),coalesce(state,'NULL'),coalesce(zip_code,'NULL'),
coalesce(street,' ')+','+coalesce(city,' ')+','+coalesce(state,' ')+','+coalesce(zip_code,' ')
from sales.customers
-- 1 select query we can't use alise name (ugly)
select c.first_name+' '+c.last_name as'customer name',cast(sum(oi.quantity*oi.list_price*(1-oi.discount)) as decimal(8,1))as'total spend'
from sales.customers c inner join sales.orders o
on o.customer_id=c.customer_id join sales.order_items oi on oi.order_id=o.order_id
group by c.first_name+' '+c.last_name
having sum(oi.quantity*oi.list_price*(1-oi.discount))>=1500
order by sum(oi.quantity*oi.list_price*(1-oi.discount)) desc

--CTE (temporary result) can be used multiple times
with Customer_spend as(
select c.customer_id,c.first_name+' '+c.last_name as'customer name',cast(sum(oi.quantity*oi.list_price*(1-oi.discount)) as decimal(8,1))as'total spend'
from sales.customers c inner join sales.orders o
on o.customer_id=c.customer_id join sales.order_items oi on oi.order_id=o.order_id
group by c.customer_id,c.first_name+' '+c.last_name
)
select c.customer_id,cs.[customer name],c.email,c.phone,cs.[total spend]
from Customer_spend cs inner join sales.customers c
on cs.customer_id=c.customer_id
where cs.[total spend]>1500
order by cs.[total spend] desc
--What it does: Creates a temporary, named query result that can be used like a table within the main query.
--Purpose and Benefits#
--Why use it: Improve query readability, avoid repetitive subqueries, enable recursive operations
--Problems it solves:
--Complex nested queries become more readable
--Recursive operations (organizational charts, bill of materials)
--Intermediate result sets for complex calculations
--Advantages: Better readability than subqueries, can be referenced multiple times, supports recursion
--Performance: CTEs are not materialized (calculated each time referenced)


--multi CTE (Common Table Expression)
with revenue_per_Category as(
select c.category_id,c.category_name,sum(oi.quantity*oi.list_price*(1-oi.discount))as'total revenue'
from sales.order_items oi join production.products p
on oi.product_id=p.product_id join production.categories c
on c.category_id=p.category_id
group by c.category_id,c.category_name
),
avg_per_category as(
select c.category_id,c.category_name,avg(oi.quantity*oi.list_price*(1-oi.discount))as'Avege revenue'
from sales.order_items oi join production.products p
on oi.product_id=p.product_id join production.categories c
on c.category_id=p.category_id
group by c.category_id,c.category_name
)
select ac.category_id,ac.[Avege revenue],tc.[total revenue],
case
when tc.[total revenue]>50000 then 'Excellent'
when tc.[total revenue]>20000 then 'Good'
else 'Needs'
end as'combine'
from avg_per_category ac join revenue_per_Category tc
on ac.category_id=tc.category_id

