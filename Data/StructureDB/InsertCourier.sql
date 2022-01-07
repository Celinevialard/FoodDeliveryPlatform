DECLARE @personID INT;
DECLARE @courrierID INT;
--- MArtigny = 4011, St-Maurice =4063, Sierre =4082 , Sion = 4111 
--1
INSERT INTO  Person([FirstName], [NAME], [Login], [password])
VALUES ('Jean','Dupont','Jean.Dupont','Dupont1234');
SELECT @personID= SCOPE_IDENTITY();
INSERT INTO Courrier([personId])
VALUES(@personID);
SELECT @courrierID= SCOPE_IDENTITY();
INSERT INTO DELEVERYZONE([courrierID],[LocationID])
VALUES (@courrierID, 4011),
		(@courrierID, 4063),
		(@courrierID, 4111);

--2 
INSERT INTO  Person([FirstName], [NAME], [Login], [password])
VALUES ('Brian','Saroule','Brian.Saroule','Saroule1234');
SELECT @personID= SCOPE_IDENTITY();
INSERT INTO Courrier([personId])
VALUES(@personID);
SELECT @courrierID= SCOPE_IDENTITY();
INSERT INTO DELEVERYZONE([courrierID],[LocationID])
VALUES (@courrierID, 4082),
		(@courrierID, 4111);