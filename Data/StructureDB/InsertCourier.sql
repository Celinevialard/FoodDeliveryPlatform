DECLARE @personID INT;
DECLARE @courrierID INT;
--- MArtigny = 4011, St-Maurice =4063, Sierre =4082 , Sion = 4111 
--1
INSERT INTO  Person([FirstName], [NAME], [Login], [password])
VALUES ('Jean','Dupont','jean.dupont@email.com','Dupont1234');
SELECT @personID= SCOPE_IDENTITY();
INSERT INTO Courrier([personId])
VALUES(@personID);
SELECT @courrierID= SCOPE_IDENTITY();
INSERT INTO DELIVERYZONE([courrierID],[LocationID])
SELECT @courrierID AS COURRIERID, LOCATIONID FROM LOCATION WHERE SUBSTRING(NPA, 1, 2) = '39'


--2 
INSERT INTO  Person([FirstName], [NAME], [Login], [password])
VALUES ('Brian','Saroule','brian.saroule@email.com','Saroule1234');
SELECT @personID= SCOPE_IDENTITY();
INSERT INTO Courrier([personId])
VALUES(@personID);
SELECT @courrierID= SCOPE_IDENTITY();
INSERT INTO DELIVERYZONE([courrierID],[LocationID])
SELECT @courrierID AS COURRIERID, LOCATIONID FROM LOCATION WHERE SUBSTRING(NPA, 1, 2) = '19'

--3
INSERT INTO  Person([FirstName], [NAME], [Login], [password])
VALUES ('Huber','Dumoulin','huber.dumoulin@email.com','Dumoulin1234');
SELECT @personID= SCOPE_IDENTITY();
INSERT INTO Courrier([personId])
VALUES(@personID);
SELECT @courrierID= SCOPE_IDENTITY();
INSERT INTO DELIVERYZONE([courrierID],[LocationID])
SELECT @courrierID AS COURRIERID, LOCATIONID FROM LOCATION WHERE SUBSTRING(NPA, 1, 2) = '18' OR SUBSTRING(NPA, 1, 2) = '19'