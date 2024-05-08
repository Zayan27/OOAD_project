use OOADloginform

select * from Login_new;

create table Branch_info(
B_id int primary key identity(100,1),
B_name nvarchar(20),
)

insert into Branch_info (B_name) values ('Karsaz'),('Gulshan');

select * from Branch_info

drop table Branch_info;

CREATE TABLE User_Info (
    U_id INT PRIMARY KEY IDENTITY,
    B_id INT FOREIGN KEY REFERENCES Branch_info(B_id), -- Correct foreign key constraint
    Uname NVARCHAR(20),
    Uage INT,
    Umail NVARCHAR(20) UNIQUE
);

select * from User_Info
Alter table User_Info add Ugender varchar(20)