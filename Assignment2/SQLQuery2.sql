CREATE TABLE Employees (
    EmpID     INT PRIMARY KEY IDENTITY(1,1),
    Name      NVARCHAR(100) NOT NULL,
    Dept      NVARCHAR(50)  NOT NULL,
    Salary    DECIMAL(10,2) NOT NULL,
    IsActive  BIT DEFAULT 1
);

CREATE TABLE Departments (
    DeptID    INT PRIMARY KEY IDENTITY(1,1),
    DeptName  NVARCHAR(50) NOT NULL,
    Location  NVARCHAR(100)
);

-- Sample data
INSERT INTO Employees (Name, Dept, Salary, IsActive) VALUES
('Ravi Kumar',   'IT',      65000, 1),
('Priya Sharma', 'HR',      55000, 1),
('Arun Das',     'IT',      72000, 1),
('Meena Pillai', 'Finance', 68000, 1),
('Suresh Nair',  'HR',      48000, 0);

CREATE PROCEDURE usp_GetEmployee
    @EmpID INT
AS
BEGIN
    set nocount on;
    select * from Employees where EmpID=@EmpID;
END;

CREATE PROCEDURE usp_InsertEmployee
    @Name      VARCHAR(30),
    @Dept      VARCHAR(20),
    @Salary    DECIMAL(10,2),
    @NewEmpID  INT OUTPUT

AS
BEGIN
    set nocount on;
    INSERT INTO Employees (Name, Dept, Salary)
    VALUES (@Name, @Dept, @Salary);

END;


CREATE PROCEDURE usp_UpdateEmployee
    @NewSalary    DECIMAL(10,2),
    @EmpID        INT

AS
BEGIN
    set nocount on;
    update Employees 
    set    Salary = @NewSalary 
    where  EmpID = @EmpID; 

END;

CREATE PROCEDURE usp_DeleteEmployee
    @EmpID        INT

AS
BEGIN
    set nocount on;
    delete from Employees where EmpID = @EmpID;
END;

create view vw_ActiveEmployees
as
SELECT EmpID, Name, Dept, Salary FROM Employees WHERE isactive=1;

create view vw_EmployeePublic
as
select EmpID, Name, Dept from Employees;

create view vw_EmployeeDetails
as
select Employees.EmpID, Employees.Name, Employees.Dept as DepartmentName,Departments.Location as DepartmentLocation, Employees.Salary from Employees left join Departments on Employees.Dept=Departments.DeptName;

create view vw_ITDept
as
select Name, Dept, Salary from Employees where Dept='IT' with check option;

INSERT INTO vw_ITDept (Name, Dept, Salary) VALUES ('TestUser', 'HR', 50000);

create table EmployeeAudit (
    AuditID     INT PRIMARY KEY IDENTITY(1,1),
    EmpID       INT,
    Action      NVARCHAR(50),
    ChangedBy   NVARCHAR(100) default System_user,
    ChangedOn  DATETIME DEFAULT GETDATE()
);
create trigger EmpAudit_Insert on Employees after insert
as
begin
    set nocount on;
    insert into EmployeeAudit (EmpID, Action, ChangedBy, ChangedOn)
    select EmpID, 'INSERT', SYSTEM_USER, GETDATE() from inserted;
end;
INSERT INTO Employees (Name, Dept, Salary) VALUES ('Test User', 'IT', 50000);
SELECT * FROM EmployeeAudit;

alter trigger EmpAudit_Insert on Employees after insert
as
begin
    set nocount on;
    IF EXISTS (SELECT 1 FROM INSERTED) AND EXISTS (SELECT 1 FROM DELETED) 
    insert into EmployeeAudit (EmpID, Action, ChangedBy, ChangedOn)
    select inserted.EmpID, 'UPDATE', SYSTEM_USER, GETDATE() from inserted join deleted on inserted.EmpID=deleted.EmpID;
    ELSE IF EXISTS (SELECT 1 FROM INSERTED) 
    insert into EmployeeAudit (EmpID, Action, ChangedBy, ChangedOn)
    select EmpID, 'INSERT', SYSTEM_USER, GETDATE() from inserted;
    else
    insert into EmployeeAudit (EmpID, Action, ChangedBy, ChangedOn)
    select EmpID, 'DELETE', SYSTEM_USER, GETDATE() from deleted;
end;

CREATE TRIGGER trg_PreventActiveDelete ON Employees AFTER DELETE
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM DELETED
        WHERE IsActive = 1
    )
    BEGIN
        PRINT 'Cannot delete an active employee.';
        ROLLBACK TRANSACTION;
    END
END;

create table salaryAudit (
    AuditID     INT PRIMARY KEY IDENTITY(1,1),
    EmpID       INT,
    OldSalary   DECIMAL(10,2),
    NewSalary   DECIMAL(10,2),
    ChangedBy   NVARCHAR(100) default System_user,
    ChangedOn  DATETIME DEFAULT GETDATE()
);
create trigger trg_SalaryAudit ON Employees AFTER UPDATE
as
begin
    set nocount on;
    IF UPDATE(Salary)
    insert into salaryAudit (EmpID, OldSalary, NewSalary, ChangedBy, ChangedOn)
    select inserted.EmpID, deleted.Salary, inserted.Salary, SYSTEM_USER, GETDATE() from inserted join deleted on inserted.EmpID=deleted.EmpID;
end;