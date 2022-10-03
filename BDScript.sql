create database mytestdb;
use mytestdb;

create table Department(
	DepartmentId int auto_increment,
    DepartmentName nvarchar(500),
    primary key(DepartmentId)
);

insert into Department(DepartmentName) values ('IT');
insert into Department(DepartmentName) values ('Support');
insert into Department(DepartmentName) values ('QA');

select * from Department;

create table Employee(
	EmployeeId int auto_increment,
    EmployeeName nvarchar(500),
    Department nvarchar(500),
    DateOfJoining datetime,
    PhotoFileName nvarchar(500),
    primary key(EmployeeId)
);

insert into Employee(EmployeeName, Department, DateOfJoining, PhotoFileName) 
values ('Bob','IT','2022-08-05','anonymous.pnj');

select * from Employee;