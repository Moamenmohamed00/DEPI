create database DEDB;
use DEDB;
create table employee(
SSN int primary key identity(1,1),
Fname nvarchar(15) not null ,
Lname nvarchar(15) not null default 'unknown',
BirthOfDate date not null check(BirthOfDate<='2007'),--18 or above
Gender bit not null,
SupervisorID int foreign key references employee(SSN)
);
--drop table dependent;
create table dependent(
DepName nvarchar(20) primary key,
BirthOfDate date not null ,
Gender bit not null,
Essn int foreign key references employee(SSN) on delete cascade on update no action
);
alter table dependent
alter column Gender varchar(6);
alter table dependent
add constraint checkGender check(Gender='male' or Gender = 'female') ;

create table department(
Dnum int primary key identity(1,1),
Dname varchar(20) not null unique,
ESSn int foreign key references employee(SSN)
);
alter table employee
add Dnum int foreign key references department(Dnum);
--alter table employee
--add Dnum int,
--add constraint emp_dep_fk foreign key(Dnum) references department(Dnum)
create table dep_location(
location nvarchar(50),
Dnum int foreign key references department(Dnum),
constraint pk_dep_loc primary key(Dnum, location)--composite key becuase location may be not unique
);
create table project(
PNum int primary key identity(1,1),
Pname varchar(25) not null unique,
city varchar(25) not null default 'unknown',
Dnum int not null,
constraint proj_dep_fk foreign key (Dnum)references department(Dnum)
);
create table emp_work_proj(
Essn int foreign key references employee(SSN),
PNum int foreign key references project(PNum),
working_hours int,
constraint checkHours check(working_hours>0)
);
--------------------------
insert into employee(Fname,BirthOfDate,Gender)
values('mohsen','2001-6-23',0);
select * from employee
insert into employee(Fname,Lname,BirthOfDate,Gender)
values('hassan','ali','1999-1-2',0),
('mohamed','gmal','2003-7-19',0),
('hoda','youssef','2005-9-30',1),
('nada','moaz','1973-10-6',1)
;
--delete from department where Dname='cs';
insert into department(Dname,ESSn)
values('cs',1),
('it',2),
('mm',5)
select *from department
-------
update employee
set Dnum=3 where Gender=1;
insert into dependent(DepName,Gender,BirthOfDate,Essn)
values('hamdy','male','2020-12-20',5),
('ksr','female','2012-11-30',4)
delete from dependent
where Essn=5;
select * from employee
where dnum=3;
SELECT E.Fname, E.Lname, P.Pname,W.working_hours
FROM employee E
INNER JOIN emp_work_proj W ON E.SSN = W.Essn
INNER JOIN project P ON P.PNum = W.PNum;
