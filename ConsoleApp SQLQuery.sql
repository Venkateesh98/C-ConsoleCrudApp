Create Database ConsoleDB;
use ConsoleDB;

Create Table Details(User_id int Primary key Identity(1,1) not null,
                     User_name varchar(50) not null,
					 User_age int not null,
					 Phone_no int not null)
select * from Details