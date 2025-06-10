create database DLMS_Assignment
go
use DLMS_Assignment
go
-------------------------------------------------------
--RULE-->  Role -> Account -> Profile
--		   Category -> Product -> Order | Store
--====================================================--
--QUERY: CREATE
--*1st flood*--
create table Role(
	[ID] int identity(1,1) primary key,
	[role_id] varchar(1) not null,
	[role_name] nvarchar(10) not null,
	CONSTRAINT U_key0 UNIQUE (role_ID)
);
go
create table Category(
	[ID] int identity(1,1) primary key,
	[cat_id] varchar(1) not null,
	[cat_name] nvarchar(50)not null,
	CONSTRAINT U_key1 UNIQUE (cat_id)
);
go
--*2nd flood*--
create table Account(
	[ID] int identity(1,1) primary key,
	[account_name] varchar(50) not null,		--FOREIGN KEY
	[password] varchar(50) not null,
	[role_id] varchar(1) not null,				
	[isActive] bit default 1,					--mac dinh account dang kha dung
	CONSTRAINT U_key2 UNIQUE (account_name),
	CONSTRAINT F_key0 FOREIGN KEY (role_id) REFERENCES Role(role_id) 
		ON DELETE CASCADE ON UPDATE CASCADE
);
go
create table Products(
	[ID] int identity(1,1) primary key,
	[product_name] nvarchar(50) not null,
	[product_id] varchar(5) not null,
	[author] nvarchar(50) not null,
	[URL_image] varchar(100) not null,
	[date_of_insert] date not null,
	[number_product] int not null default 0,	--mac dinh khong co san pham
	[cat_id] varchar(1) not null,
	[isAvailable] bit default 0,				--0:disable;1:enable
	CONSTRAINT U_key3 UNIQUE (product_id),
	CONSTRAINT F_key1 FOREIGN KEY (cat_id) REFERENCES Category(cat_id) 
		ON DELETE CASCADE ON UPDATE CASCADE
);
go
--*3rd flood*--
create table Profile(
	[ID] int identity(1,1) primary key,
	[account_name] varchar(50) not null,		--FOREIGN KEY | UNIQUE KEY
	[full_name] nvarchar(50) null,
	[address] varchar(50) null,
	[date_of_birth] date null,
	[email] varchar(50) null,
	[number_phone] varchar(20) null,			
	CONSTRAINT U_key4 UNIQUE (account_name),
	CONSTRAINT F_key2 FOREIGN KEY (account_name) REFERENCES Account(account_name) 
		ON DELETE CASCADE ON UPDATE CASCADE
);
go
create table Orders(
	[ID] int identity(1,1) primary key,
	[product_id] varchar(5) not null,
	[account_name] varchar(50) not null,
	[date_of_order] date not null,
	[number_order] int not null default 1,	--mac dinh order 1 san pham
	[wait_time] date null,
	[deadline] date not null,
	[isSended] bit default 0,				--0:No;1:Yes
	[isLate] bit default 0,					--0:No;1:Yes
	CONSTRAINT F_key3 FOREIGN KEY (account_name) REFERENCES Account(account_name) 
		ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT F_key4 FOREIGN KEY (product_id) REFERENCES Products(product_id) 
		ON DELETE CASCADE ON UPDATE CASCADE
);
go
create table Stores(
	[ID] int identity(1,1) primary key,
	[product_id] varchar(5) not null,
	[account_name] varchar(50) not null,
	[isOrder] bit default 0,				--0:No;1:Yes
	CONSTRAINT F_key5 FOREIGN KEY (account_name) REFERENCES Account(account_name) 
		ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT F_key6 FOREIGN KEY (product_id) REFERENCES Products(product_id)
		ON DELETE CASCADE ON UPDATE CASCADE
);
go
--====================================================--
--QUERY: INSERT
--*insert Role*--
insert Role ([role_id],[role_name]) values
('1',N'admin'),
('2',N'staff'),
('3',N'user');
go
--*insert Category*--
insert Category ([cat_id],[cat_name]) values
('1',N'C'),
('2',N'C++'),
('3',N'C#'),
('4',N'Java'),
('5',N'Typhon'),
('6',N'PHP'),
('7',N'JavaScript');
go
--*insert Account*--
insert Account ([account_name],[password],[role_id]) values
('admin','admin@123','1'),
('staff','staff@123','2'),
('user01','user1@123','3'),
('nam01','nam1@123','3')
go
--*insert Products*--
insert Products ([product_name],[product_id],[author],[URL_image],[date_of_insert],[cat_id]) values
(N'The C Programming Language',		  'A0001',N'Brian W. Kernighan & Dennis M. Ritchie',			'https://m.media-amazon.com/images/I/51EyaJeebHL._AC_UF1000,1000_QL80_.jpg',			'2025-01-01','1'),
(N'C Programming: A Modern Approach', 'A0002',N'K.N.King',											'https://m.media-amazon.com/images/I/71YNXYuwPGL.jpg',									'2025-01-01','1'),
(N'Effective C++',					  'B0001',N'Scott Meyers',										'https://m.media-amazon.com/images/I/712Qlq3iHcL.jpg',									'2025-01-01','2'),
(N'C++ Primer',						  'B0002',N'Stanley B. Lippman, Josée Lajoie, Barbara E. Moo',  'https://images-na.ssl-images-amazon.com/images/I/61f+mezprVL._AC_UL210_SR210,210_.jpg','2025-01-01','2'),
(N'C# in Depth',					  'C0001',N'Jon Skeet',											'https://m.media-amazon.com/images/I/614ylT+3i1L._AC_UF1000,1000_QL80_.jpg',			'2025-01-01','3'),
(N'Pro C# 7: With .NET and .NET Core','C0002',N'Andrew Troelsen, Philip Japikse',					'https://m.media-amazon.com/images/I/61II3EhHknL.jpg',									'2025-01-01','3'),
(N'Effective Java',					  'D0001',N'Joshua Bloch',										'https://m.media-amazon.com/images/I/71JAVv3TW4L.jpg',									'2025-01-01','4'),
(N'Head First Java',				  'D0002',N'Kathy Sierra, Bert Bates',							'https://m.media-amazon.com/images/I/51zTrLnLLDL._AC_SY200_QL15_.jpg',					'2025-01-01','4'),
(N'Delphi Programming Projects',	  'E0001',N'William Duarte',									'https://m.media-amazon.com/images/I/711QaMoHYtL._AC_UF1000,1000_QL80_.jpg',			'2025-01-01','5'),
(N'Mastering Delphi',				  'E0002',N'Marco Cantù',										'https://m.media-amazon.com/images/I/61DFp3wsTGL._AC_UF1000,1000_QL80_.jpg',			'2025-01-01','5'),
(N'PHP and MySQL Web Development',	  'F0001',N'Luke Welling, Laura Thomson',						'https://m.media-amazon.com/images/I/51se-6XKeaL._AC_UF1000,1000_QL80_.jpg',			'2025-01-01','6'),
(N'Modern PHP',						  'F0002',N'Josh Lockhart',										'https://m.media-amazon.com/images/I/91i-OAVlljL.jpg',									'2025-01-01','6'),
(N'You Don’t Know JS" (series)',	  'G0001',N'Kyle Simpson',										'https://m.media-amazon.com/images/I/71mKvD89oEL.jpg',									'2025-01-01','7'),
(N'Eloquent JavaScript',			  'G0002',N'Marijn Haverbeke',									'https://m.media-amazon.com/images/I/81HqVRRwp3L._AC_UF1000,1000_QL80_.jpg',			'2025-01-01','7')
go
--*insert Profile*--
insert Profile ([account_name],[full_name],[address],[date_of_birth],[email],[number_phone]) values
('admin',	N'admin',		'123 HN',				'1990-10-20','admin@gmail.com',			'0123456789'),
('staff',	N'staff',		'124 HN',				'1995-11-21','staff@gmail.com',			'0213467895'),
('user01',	N'user',		'125 HN',				'2000-12-22','user@gmail.com',			'0337246798'),
('nam01',	N'Doi Hoai Nam','Hoa Lac Thach That HN','2001-01-19','namdhhe153102@fpt.edu.vn','0375727539')
go
--*insert Orders*--
insert Orders ([product_id],[account_name],[date_of_order],[wait_time],[deadline]) values
('C0001','user01','2025-06-05','2025-06-07','2025-06-11'),
('C0002','user01','2025-06-05',null,'2025-06-11'),
('C0001','nam01','2025-06-05','2025-06-07','2025-06-11'),
('C0002','nam01','2025-06-05',null,'2025-06-11')
go
--*insert Stores*--
insert Stores ([product_id],[account_name]) values
('F0001','user01'),
('F0001','nam01')
go
--====================================================--
--QUERY: SELECT
select * from Role
go
select * from Category
go
select * from Account
go
select * from Products
go
select * from Profile
go
select * from Orders
go
select * from Stores
go
--====================================================--
--Customization
-- Entity:Category --
select * from Category
go
--Entity: Account --
--Entity: Product --
--*GET*--
select p.product_name, p.product_id, p.author, p.URL_image, p.date_of_insert, p.number_product, p.isAvailable,
		c.cat_id, c.cat_name
from Products p
JOIN Category c 
on p.cat_id = c.cat_id
go
--*ADD*--
--*UPDATE*--
--*DELET*--
--*SEARCH*--
--Entity: Orders --
--*GET*--
--*ADD*--
--*UPDATE*--
--*DELET*--
--*SEARCH*--
--Entity: Cart --
--*GET*--
--*ADD*--
--*UPDATE*--
--*DELET*--
--*SEARCH*--