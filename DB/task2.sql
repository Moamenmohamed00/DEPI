use StoreDB;
select[product_name],[model_year],[list_price] 
from [production].[products] where list_price>1000;

select first_name,city,state,zip_code
from sales.customers
--where state='NY'or state='CA';
where state in ('NY','CA');

select *
from sales.orders
--where order_date between'2023-1-1' and '2023-12-31';
where year(order_date)=2023;

select first_name,last_name,street,email
from sales.customers
where email like '%@gmail.com'
--where RIGHT(email,10)='@gmail.com'
--where SUBSTRING(email,LEN(email)-10,len(email))='@gmail.com' -- not work because we did't know email len

select first_name+''+last_name as FullName,active
from sales.staffs
where active=0

select top 5 product_name,model_year,list_price
from production.products
order by 1/list_price asc--=desc

select order_id,order_date,required_date,shipped_date
from sales.orders
order by order_date desc
offset 0 rows fetch next 10 rows only

select distinct(last_name),email,city
from sales.customers
order by last_name asc
offset 0 rows fetch next 3 rows only

select * 
from sales.customers
where phone is null

select *
from sales.staffs
where manager_id is not null

select category_id,count(product_id)as'num of products'--recommend select the column in group by to understand result
from production.products
group by category_id

select count(customer_id)as 'num of cust',state
from sales.customers
group by state

select cast( avg(list_price) as decimal(8,2)) as 'averge price',brand_id
from production.products
group by brand_id

select count(order_id)as 'num of orders',staff_id
from sales.orders
group by staff_id

--select *from sales.orders where customer_id=5
select customer_id,count(customer_id)as'num of orders'
from sales.orders
group by customer_id
having count(customer_id)>2

select product_name,model_year,list_price
from production.products
where list_price between '500'and'1500'

select * 
from sales.customers
where city like 's%'

select *
from sales.orders
where order_status in (2,4)

select *
from production.products
where category_id in (1,2,3)

select * 
from sales.staffs
where store_id=1 or phone is null