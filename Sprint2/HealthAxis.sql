create database HealthAxis;
use HealthAxis;
create table patient ( PatientId int primary key identity(1,1) not null, PatientName varchar(30) not null, DateOfBirth Date not null, Gender varchar(15) check(gender in ('Male', 'Female','Transgender','Others')) not null, PhoneNumber varchar(10) not null, Email varchar(30) null, InsuranceId varchar(7) null, RegisteredDate Date default CAST(GETDATE() AS DATE) not null);
create table doctor (DoctorId int primary key identity(1,1) not null, DoctorName varchar(30) not null, Specialisation varchar(25) check (specialisation in('GeneralPractitioner','Cardiologist','Dermatologist','Neurologist','Pediatrician','Psychiatrist','OrthopedicSurgeon','Gynecologist','Oncologist','Endocrinologist')) not null, Experience int not null, Fees int not null, IsActive bit not null);
create table appointment(AppointmentId int primary key identity(1,1) not null, PatientId int references Patient(PatientId) not null, doctorId int references Doctor(DoctorId) not null, ScheduledDate Date not null, slot varchar(10) check(slot in('09:00 AM','10:00 AM','11:00 AM','02:00 PM','03:00 PM')) not null, status varchar(10) check (status in('Pending','Cancelled','Confirmed','Completed')) not null, CancellationReason varchar(50) null);
create table HealthRecord(RecordId int primary key identity(1,1) not null, AppointmentId int references Appointment(AppointmentId) not null, PatientId int references Patient(PatientId) not null, doctorId int references Doctor(DoctorId) not null, visitdate date not null, diagnosis varchar(50) not null, Prescription varchar(100) not null, notes varchar(100) null);

use HealthAxis;

INSERT INTO Patient
(PatientName, DateOfBirth, Gender, PhoneNumber, Email, InsuranceId, RegisteredDate)
VALUES
( 'Arun Kumar', '1992-05-14 12:24:33', 'Male', '9876543210', 'arun.kumar@example.com', 'INS1001', GETDATE()),
( 'Meera Nair', '1988-09-22 22:15:30', 'Female', '9876543211', 'meera.nair@example.com', 'INS1002', GETDATE()),
( 'Rahul Menon', '2000-01-10 16:17:18', 'Male', '9876543212', 'rahul.menon@example.com', 'INS1003', GETDATE()),
( 'Anjali Thomas', '1995-12-03 01:02:03', 'Female', '9876543213', 'anjali.thomas@example.com', 'INS1004', GETDATE()),
( 'Vivek Pillai', '1983-07-19 05:06:07', 'Male', '9876543214', 'vivek.pillai@example.com', 'INS1005', GETDATE());

INSERT INTO Doctor
(DoctorName, Specialisation, Experience, Fees, IsActive)
VALUES
( 'Dr. Priya Sharma', 'Cardiologist', 12, 800, 1),
( 'Dr. Suresh Mathew', 'Dermatologist', 9, 600, 1),
( 'Dr. Neha Iyer', 'Pediatrician', 10, 700, 1),
( 'Dr. Thomas George', 'OrthopedicSurgeon', 15, 900, 1),
('Dr. Kavitha Rao', 'Neurologist', 14, 1000, 1),
( 'Dr. Mohammed Ali', 'GeneralPractitioner', 11, 500, 1),
( 'Dr. Lakshmi Menon', 'Endocrinologist', 8, 550, 1),
( 'Dr. Rajesh Nambiar', 'Oncologist', 13, 650, 1);

select * from patient;
select * from doctor;

drop table patient;
drop table doctor;
drop table appointment;
drop table healthrecord;



ALTER TABLE Patients
DROP CONSTRAINT CK__patients__Gender__06CD04F7;


ALTER TABLE Patients
ALTER COLUMN Gender INT;


UPDATE Patients SET Gender = 0 WHERE Gender = 'Male';
UPDATE Patients SET Gender = 1 WHERE Gender = 'Female';