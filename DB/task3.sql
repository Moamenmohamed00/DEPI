use StoreDB

select count(*)as'total number' from production.products

select MIN(list_price)as'minimum',AVG(list_price)as'averge',MAX(list_price)as 'maximum' from production.products

select count(p.product_id)as'num product per category',c.category_name
from production.products p inner join production.categories c
on p.category_id=c.category_id
group by c.category_name

select count(o.order_id)as'total number',s.store_name
from sales.orders o inner join sales.stores s
on o.store_id=s.store_id
group by s.store_name

select UPPER(first_name)as'fname',LOWER(last_name)as'lname'
from sales.customers
order by first_name asc
offset 0 row fetch next 10 rows only 

select top 10 product_name,LEN(product_name)as'length'
from production.products
order by product_name asc

select top 15 concat('(', left(phone,3),')')as 'area code'from sales.customers
--where customer_id between 1 and 15

select top(10) GETUTCDATE()as'now',YEAR(order_date)as'year',MONTH(order_date)as'month' from sales.orders

select p.product_name as' product name',c.category_name as'category name'
from production.products p inner join production.categories c
on p.category_id=c.category_id
order by p.product_name asc
offset 0 row fetch next 10 rows only

select CONCAT(c.first_name,' ',c.last_name)as name,o.order_date
from sales.customers c inner join sales.orders o
on o.customer_id=c.customer_id

select p.product_name,ISNULL(b.brand_name,'No Brand')as'brand name'
from production.products p left join production.brands b
on p.brand_id=b.brand_id

select product_name,list_price
from production.products
where list_price>(select AVG(list_price)from production.products)

--can solve by inner join
select customer_id,CONCAT(first_name,' ',last_name) from sales.customers
where customer_id in(select customer_id from sales.orders)
--14 tricky
select c.first_name,c.last_name,(select COUNT(*)as'num ordes' from sales.orders o where o.customer_id=c.customer_id )as'orders'--لازم where
from sales.customers 

create view easy_product_list
as
select p.product_name as'product',c.category_name as'category',p.list_price as'price'
from production.products p inner join production.categories c
on p.category_id=c.category_id

select * from easy_product_list
where list_price>100;

create view customer_info
as
select c.customer_id,CONCAT(c.first_name,' ',c.last_name)as'full name',c.email,CONCAT( c.city,'(',c.state,')')as'city_state' from [sales].[customers] c

drop view customer_info
select * from customer_info where city_state like '%(CA)%'

select product_name,list_price
from production.products
where list_price between 50 and 200
order by list_price asc

select count(*)as'count',state from sales.customers
group by state
order by count desc

select top 1 p.product_name,c.category_name,p.list_price
from production.products p inner join production.categories c
on p.category_id=c.category_id
order by p.list_price desc

select s.store_name,s.city,count(o.store_id) as'total orders per store'
from sales.stores s inner join sales.orders o
on o.store_id=s.store_id
group by s.store_name,s.city
