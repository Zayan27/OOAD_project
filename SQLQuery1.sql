create database LoginInfo

use LoginInfo

create table Login_val(
username varchar(50),
pass varchar(50)
);

insert into Login_val (username, pass) values ('Admin','admin123')

select * from Login_val