CREATE DATABASE UST_Shop;
USE UST_Shop;
CREATE TABLE Categories (     CategoryID    INT          IDENTITY(1,1) PRIMARY KEY,     CategoryName  NVARCHAR(50) NOT NULL UNIQUE );
CREATE TABLE Products (     ProductID    INT           IDENTITY(1,1) PRIMARY KEY,     CategoryID   INT           NOT NULL,     ProductName  NVARCHAR(100) NOT NULL,     Price        DECIMAL(8,2)  NOT NULL CHECK (Price > 0),     StockQty     INT           NOT NULL DEFAULT 0,     IsActive     BIT           NOT NULL DEFAULT 1,     CONSTRAINT FK_Products_Categories         FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID) );
CREATE TABLE Customers (     CustomerID  INT           IDENTITY(1,1) PRIMARY KEY,     FullName    NVARCHAR(100) NOT NULL,     Email       NVARCHAR(150) NOT NULL UNIQUE,     City        NVARCHAR(50)  NOT NULL );
CREATE TABLE Orders (     OrderID     INT           IDENTITY(1,1) PRIMARY KEY,     CustomerID  INT           NOT NULL,     OrderDate   DATE          NOT NULL DEFAULT GETDATE(),     Status      NVARCHAR(20)  NOT NULL DEFAULT 'Pending'                 CHECK (Status IN ('Pending','Shipped','Delivered','Cancelled')),     Total       DECIMAL(10,2) NOT NULL DEFAULT 0,     CONSTRAINT FK_Orders_Customers         FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID) );
CREATE TABLE OrderItems (     OrderID    INT           NOT NULL,     ProductID  INT           NOT NULL,     Quantity   INT           NOT NULL CHECK (Quantity > 0),     UnitPrice  DECIMAL(8,2)  NOT NULL,     CONSTRAINT PK_OrderItems PRIMARY KEY (OrderID, ProductID),     CONSTRAINT FK_OI_Orders   FOREIGN KEY (OrderID)   REFERENCES Orders(OrderID),     CONSTRAINT FK_OI_Products FOREIGN KEY (ProductID) REFERENCES Products(ProductID) );
INSERT INTO Categories (CategoryName) VALUES     ('Electronics'), ('Books'), ('Clothing'), ('Sports');
INSERT INTO Products (CategoryID, ProductName, Price, StockQty) VALUES     (1, 'Samsung Galaxy S24',  74999, 50),     (1, 'Apple iPhone 15',     89999, 30),     (1, 'Sony Headphones',     29999, 75),     (1, 'Dell Laptop',         65999, 20),     (2, 'Clean Code',            599, 200),     (2, 'The Pragmatic Programmer', 699, 150),     (2, 'Design Patterns',       899, 100),     (3, 'Nike T-Shirt',         1999, 300),     (3, 'Levi Jeans',           3499, 200),     (4, 'Yoga Mat',             1299, 180),     (4, 'Fitbit Charge 6',     14999,  40);
INSERT INTO Customers (FullName, Email, City) VALUES     ('Arjun Sharma',   'arjun@email.com',   'Chennai'),     ('Priya Nair',     'priya@email.com',   'Mumbai'),     ('Rahul Verma',    'rahul@email.com',   'Delhi'),     ('Sneha Iyer',     'sneha@email.com',   'Bangalore'),     ('Karthik Reddy',  'karthik@email.com', 'Hyderabad'),     ('Deepa Menon',    'deepa@email.com',   'Chennai'),     ('Vijay Kumar',    'vijay@email.com',   'Mumbai');
INSERT INTO Orders (CustomerID, OrderDate, Status, Total) VALUES     (1, '2024-01-10', 'Delivered',  74999),     (2, '2024-01-15', 'Delivered',  31998),     (3, '2024-02-01', 'Shipped',    65999),     (4, '2024-02-10', 'Pending',     5997),     (5, '2024-02-14', 'Delivered',  89999),     (1, '2024-03-15', 'Delivered',  29999),     (2, '2024-04-01', 'Cancelled',   1299),     (6, '2024-04-10', 'Pending',    14999);
INSERT INTO OrderItems (OrderID, ProductID, Quantity, UnitPrice) VALUES     (1, 1, 1, 74999), (2, 3, 1, 29999), (2, 8, 1, 1999),     (3, 4, 1, 65999), (4, 5, 3,   599), (4,10, 3, 1299),     (5, 2, 1, 89999), (6, 3, 1, 29999), (7,10, 1, 1299),     (8,11, 1,14999);
PRINT 'Setup complete!'; 

CREATE TABLE Reviews(ReviewID INT PRIMARY KEY  IDENTITY(1,1), CustomerID INT REFERENCES Customers(CustomerID), ProductID INT REFERENCES Products(ProductID), Rating INT CHECK(Rating BETWEEN 1 AND 5), ReviewText VARCHAR(100) null, ReviewDate DATE DEFAULT GETDATE());
ALTER TABLE Products add Description nvarchar(500) null;
alter table Products alter column Price decimal(8,2);
alter table Products drop column Description;

insert into Customers(FullName, Email, City) values ('Ravi Shankar','ravi@email.com', 'Kolkata');
insert into Products (CategoryID, ProductName, Price, StockQty) values (1,'OnePlus 12R', 39999,45),(4,'Resistance Bands' ,1999, 90);
update Customers set City='Chennai' where CustomerID=5;
update Products set price=price*0.9 where CategoryID=2;
begin transaction;
select count(*) as cancelled from Orders where Status='Cancelled';
delete from OrderItems where OrderID in (select OrderID from Orders where status='Cancelled');
delete from Orders where Status='Cancelled';
commit;
insert into Orders (CustomerID,Status,Total) values (3,'Pending',1298);
select SCOPE_IDENTITY() as NewOrderID;

select * from Products where IsActive=1 and StockQty>50;
select ProductName, Price from Products where Price between 1000 and 10000 order by Price asc;
select top 3 ProductName, Price from Products order by Price desc;
select * from Customers where City in ('Chennai', 'Mumbai');
select * from Products where ProductName like '%Pro%';
select OrderId, CustomerId, Status, Total from Orders where status not in ('Cancelled' , 'Pending') order by OrderDate desc;

select count(*) as TotalProducts, avg(Price) as AveragePrice, min(Price) as MinimumPrice, MAX(Price) as MaximumPrice from Products;
select sum(total) as TotalRevenue from Orders where Status='Delivered';
select City, count(*) CustomerCount from Customers group by City order by CustomerCount desc;
select CategoryID,Count(*) as ProductCount from Products group by CategoryID;
select CategoryID, count(*)as ProductCount from Products group by CategoryID having count(*)>2; 
select CustomerID, count(*) as OrderCount from Orders group by CustomerID having count(OrderID)>1;

select FullName, City from Orders inner join Customers on Orders.CustomerID=Customers.CustomerID;
select Customers.FullName, Count(Orders.OrderID) as OrderCount from Customers left join Orders on Orders.CustomerID=Customers.CustomerID group by FullName;
select OrderItems.OrderID, Products.ProductName, Categories.CategoryName, Quantity, UnitPrice from OrderItems join Orders on Orders.OrderID=OrderItems.OrderID join Products on Products.ProductID=OrderItems.ProductID join Categories on Products.CategoryID=Categories.CategoryID;
select * from Customers left join Orders on Customers.CustomerID=Orders.CustomerID where Orders.OrderID is null;

select ProductName, Price from Products where Price>(select avg(Price) from Products);
select * from Customers where CustomerID in (select CustomerID from orders where total>50000); 
select * from Products where ProductID not in (select ProductID from OrderItems);

select Customers.FullName, Count(Orders.OrderID), Count(Orders.Total), 