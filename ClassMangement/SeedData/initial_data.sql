--use ClassManagementDB3

-- הכנסת מורה 
INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone, Role,UserType)
VALUES ('T1', '123456', 'Sari fridman', '1990-09-01', 'Hahavat Shalom 12', '12sr445@gmail.com', '052-7654321', 2,  'Teacher');

-- הכנסת כיתה

--INSERT INTO Classes (Password, Name,TeacherId, CountOfStudents)
--VALUES ('8214', 'A1', 'T1', 12);
SET IDENTITY_INSERT Classes ON;

INSERT INTO Classes (Id, Password, Name, TeacherId, CountOfStudents)
VALUES (1, '8214', 'A1', 'T1', 12);

SET IDENTITY_INSERT Classes OFF;



-- הכנסת כסאות

INSERT INTO Chairs (SerialNumberByClass, ClassId, IsCenteral, IsFront, IsNearTheDoor, IsNearTheWindow)
VALUES 
(1, 1, 0, 1, 1, 0),
(2, 1, 1, 1, 0, 0),
(3, 1, 1, 1, 0, 0),
(4, 1, 0, 1, 0, 1),
(5, 1, 0, 0, 0, 0),
(6, 1, 1, 0, 0, 0),
(7, 1, 1, 0, 0, 0),
(8, 1, 0, 0, 0, 1),
(9, 1, 0, 0, 0, 0),
(10, 1, 1, 0, 0, 0),
(11, 1, 1, 0, 0, 0),
(12, 1, 0, 0, 0, 1);

DECLARE @id1 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 1 AND ClassId = 1);
DECLARE @id2 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 2 AND ClassId = 1);
DECLARE @id3 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 3 AND ClassId = 1);
DECLARE @id4 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 4 AND ClassId = 1);
DECLARE @id5 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 5 AND ClassId = 1);
DECLARE @id6 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 6 AND ClassId = 1);
DECLARE @id7 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 7 AND ClassId = 1);
DECLARE @id8 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 8 AND ClassId = 1);
DECLARE @id9 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 9 AND ClassId = 1);
DECLARE @id10 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 10 AND ClassId = 1);
DECLARE @id11 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 11 AND ClassId = 1);
DECLARE @id12 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 12 AND ClassId = 1);

-- שלב 3: הכנסת קשרי שכנות (ChairNearbyChairs)
INSERT INTO ChairNearbyChairs VALUES (@id1, @id2);
INSERT INTO ChairNearbyChairs VALUES (@id1, @id5);

INSERT INTO ChairNearbyChairs VALUES (@id2, @id1);
INSERT INTO ChairNearbyChairs VALUES (@id2, @id3);
INSERT INTO ChairNearbyChairs VALUES (@id2, @id6);

INSERT INTO ChairNearbyChairs VALUES (@id3, @id2);
INSERT INTO ChairNearbyChairs VALUES (@id3, @id4);
INSERT INTO ChairNearbyChairs VALUES (@id3, @id7);

INSERT INTO ChairNearbyChairs VALUES (@id4, @id3);
INSERT INTO ChairNearbyChairs VALUES (@id4, @id8);

INSERT INTO ChairNearbyChairs VALUES (@id5, @id1);
INSERT INTO ChairNearbyChairs VALUES (@id5, @id6);
INSERT INTO ChairNearbyChairs VALUES (@id5, @id9);

INSERT INTO ChairNearbyChairs VALUES (@id6, @id2);
INSERT INTO ChairNearbyChairs VALUES (@id6, @id5);
INSERT INTO ChairNearbyChairs VALUES (@id6, @id7);
INSERT INTO ChairNearbyChairs VALUES (@id6, @id10);

INSERT INTO ChairNearbyChairs VALUES (@id7, @id3);
INSERT INTO ChairNearbyChairs VALUES (@id7, @id6);
INSERT INTO ChairNearbyChairs VALUES (@id7, @id8);
INSERT INTO ChairNearbyChairs VALUES (@id7, @id11);

INSERT INTO ChairNearbyChairs VALUES (@id8, @id4);
INSERT INTO ChairNearbyChairs VALUES (@id8, @id7);
INSERT INTO ChairNearbyChairs VALUES (@id8, @id12);

INSERT INTO ChairNearbyChairs VALUES (@id9, @id5);
INSERT INTO ChairNearbyChairs VALUES (@id9, @id10);

INSERT INTO ChairNearbyChairs VALUES (@id10, @id6);
INSERT INTO ChairNearbyChairs VALUES (@id10, @id9);
INSERT INTO ChairNearbyChairs VALUES (@id10, @id11);

INSERT INTO ChairNearbyChairs VALUES (@id11, @id10);
INSERT INTO ChairNearbyChairs VALUES (@id11, @id7);
INSERT INTO ChairNearbyChairs VALUES (@id11, @id12);

INSERT INTO ChairNearbyChairs VALUES (@id12, @id8);
INSERT INTO ChairNearbyChairs VALUES (@id12, @id11);

-- הכנסת תלמידים
--למה ליוזרס ולא לסטודנס?
INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('S1', 'pass1', 'Racheli havraham', '2004-09-01', 'Halprin 17', 'Racheli@gmail.com.com', '052-7654321', 4,  'Student',  1, NULL, 2, 2, 2, NULL,5,'[]');

INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('S2', 'pass2', 'Yeudit awat', '2004-09-01', 'Halprin 17', 'Yeudit@gmail.com.com', '052-7654321', 4,  'Student',  1, NULL, 3, 2, 2, NULL, 5,'[]');

INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('S3', 'pass3', 'Chani Heytan', '2004-09-01', 'Halprin 17', 'Chani@gmail.com.com', '052-7654321', 4,  'Student',  1, NULL, 4, 1, 2, NULL, 5,'[]');

INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('S4', 'pass4', 'Hadas Bek', '2004-09-01', 'Halprin 17', 'Hadas@gmail.com.com', '052-7654321', 4,  'Student',  1, NULL, 5, 1, 4, NULL, 5,'[]');



INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('S5', 'pass5', 'Shevi Bretler', '2004-09-01', 'Halprin 17', 'Shevi@gmail.com.com', '052-7654321', 4,  'Student',  1, NULL, 2, 3, 4, NULL, 5,'[]');

INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('S6', 'pass6', 'Rachli Hainrich', '2004-09-01', 'Halprin 17', 'Rachli@gmail.com.com', '052-7654321', 4,  'Student',  1, NULL, 2, 1, 1, NULL, 5,'[]');

INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('S7', 'pass7', 'Braci Levi', '2004-09-01', 'Halprin 17', 'Braci@gmail.com.com', '052-7654321', 4,  'Student',  1, NULL, 3, 4, 1, NULL, 5,'[]');

INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('S8', 'pass8', 'Shuli Langberg', '2004-09-01', 'Halprin 17', 'Shuli@gmail.com.com', '052-7654321', 4,  'Student',  1, NULL, 1, 1, 4, NULL, 5,'[]');


INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('S9', 'pass9', 'Zipora Pozen', '2004-09-01', 'Halprin 17', 'Zipora@gmail.com.com', '052-7654321', 4,  'Student',  1, NULL, 1, 3, 2, NULL, 5,'[]');

INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('S10', 'pass10', 'Chani Siton', '2004-09-01', 'Halprin 17', 'Chani@gmail.com.com', '052-7654321', 4,  'Student',  1, NULL, 1, 3, 1, NULL, 5,'[]');

INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('S11', 'pass11', 'Rivka Siboni', '2004-09-01', 'Halprin 17', 'Rivka@gmail.com.com', '052-7654321', 4,  'Student',  1, NULL, 2, 2, 4, NULL, 5,'[]');

INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('S12', 'pass12', 'Pnina Savyon', '2004-09-01', 'Halprin 17', 'Pnina@gmail.com.com', '052-7654321', 4,  'Student',  1, NULL, 2, 2, 2, NULL, 5,'[]');

-- הכנסת חברים מועדפים
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S1', 'S9'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S1', 'S11'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S1', 'S12'); 

INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S2', 'S1'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S2', 'S8'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S2', 'S10'); 

INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S3', 'S9'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S3', 'S11'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S3', 'S10'); 

INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S4', 'S2'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S4', 'S11'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S4', 'S8'); 

INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S5', 'S6'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S5', 'S7'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S5', 'S10'); 

INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S6', 'S3'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S6', 'S7'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S6', 'S10'); 

INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S7', 'S9'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S7', 'S6'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S7', 'S10'); 

INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S8', 'S1'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S8', 'S11'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S8', 'S2'); 

INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S9', 'S8'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S9', 'S11'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S9', 'S1'); 

INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S10', 'S1'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S10', 'S2'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S10', 'S8'); 

INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S11', 'S1'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S11', 'S2'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S11', 'S4'); 

INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S12', 'S3'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S12', 'S9'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S12', 'S7'); 

-- הכנסת חברים לא מועדפים
INSERT INTO StudentNonFavoriteFriends (StudentId, NonFriendId) VALUES ('S1', 'S2'); 
INSERT INTO StudentNonFavoriteFriends (StudentId, NonFriendId) VALUES ('S1', 'S8'); 

INSERT INTO StudentNonFavoriteFriends (StudentId, NonFriendId) VALUES ('S3', 'S5'); 
INSERT INTO StudentNonFavoriteFriends (StudentId, NonFriendId) VALUES ('S3', 'S12');

INSERT INTO StudentNonFavoriteFriends (StudentId, NonFriendId) VALUES ('S4', 'S5'); 
INSERT INTO StudentNonFavoriteFriends (StudentId, NonFriendId) VALUES ('S4', 'S12');

INSERT INTO StudentNonFavoriteFriends (StudentId, NonFriendId) VALUES ('S5', 'S2'); 
INSERT INTO StudentNonFavoriteFriends (StudentId, NonFriendId) VALUES ('S5', 'S8');

INSERT INTO StudentNonFavoriteFriends (StudentId, NonFriendId) VALUES ('S8', 'S5'); 
INSERT INTO StudentNonFavoriteFriends (StudentId, NonFriendId) VALUES ('S8', 'S12');

INSERT INTO StudentNonFavoriteFriends (StudentId, NonFriendId) VALUES ('S9', 'S12'); 

INSERT INTO StudentNonFavoriteFriends (StudentId, NonFriendId) VALUES ('S10', 'S5');

INSERT INTO StudentNonFavoriteFriends (StudentId, NonFriendId) VALUES ('S11', 'S8');