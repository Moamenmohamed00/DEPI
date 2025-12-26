create nonclustered index IX_Customers_Email on sales.customers(email)

create nonclustered index IX_Product_Brand_Category on production.products(category_id,brand_id)

create nonclustered index IX_Order_OrderDate on sales.orders(order_date)include(customer_id,store_id,order_status)

create table sales.customer_log(
Audit_id int identity(1,1) primary key,
custmoer_id int,
action_type varchar(10),
log_message varchar(50),
changed_by varchar(50),
change_date datetime default getutcdate()
)
create or alter trigger tr_customer_log
on sales.customers
for insert
as
begin
insert into sales.customer_log(custmoer_id,action_type,log_message,changed_by)
select i.customer_id,'Insert','welcome new customer',system_user
from inserted i
end
insert into sales.customers(first_name,last_name,email)
values('moamen','ali','akm@yahoo.com')
select * from sales.customer_log

create table production.products_log(
Audit_id int identity(1,1) primary key,
product_id int,
old_price decimal(10,2),
new_price decimal(10,2),
action_type varchar(10),
changed_by varchar(50),
change_date datetime default getutcdate()
)
create or alter trigger tr_product_log
on production.products
after update
as
begin
insert into production.products_log(product_id,old_price,new_price,action_type,changed_by)
select i.product_id,d.list_price,i.list_price,'update',system_user
from inserted i join deleted d
on i.product_id=d.product_id
where i.list_price<>d.list_price-- <> <--> !=
end
update production.products
set list_price=123.45
where product_id=6
select* from production.products_log

create or alter trigger tr_category_delete
on production.categories
instead of delete
as
begin
if exists(select 1 from production.products p join deleted d on p.category_id=d.category_id)
begin
raiserror('Cannot delete category because it has associated products.',16,1)
return;
end
delete from production.categories
where category_id in(select category_id from deleted)
end
delete from production.categories
where category_id=5;

create or alter trigger tr_reduce_stock
on sales.order_items
after insert
as
begin
update s
set s.[quantity]=s.[quantity]-i.quantity
from production.stocks s join inserted i
on s.product_id=i.product_id
end
select * from [production].[stocks]where product_id=1
insert into sales.order_items (item_id,order_id, product_id, quantity, list_price)
values (6,1001, 1, 5, 1200);
select * from [production].[stocks]where product_id=1

CREATE TABLE sales.order_audit (
    audit_id INT IDENTITY(1,1) PRIMARY KEY,
    order_id INT,
    customer_id INT,
    store_id INT,
    staff_id INT,
    order_date DATE,
    audit_timestamp DATETIME DEFAULT GETDATE()
);
create or alter trigger tr_neworder
on sales.orders
after insert
as
begin
insert into sales.order_audit(order_id,customer_id,store_id,staff_id,order_date)
select i.order_id,i.customer_id,i.store_id,i.staff_id,i.order_date
from inserted i
end
insert into sales.orders
(customer_id, store_id, staff_id, order_date,order_status,required_date)
values (3, 1, 5, '2025-12-26',1,'2025-12-1');
