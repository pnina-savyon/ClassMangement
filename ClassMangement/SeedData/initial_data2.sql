---use ClassManagementDB3

-- הכנסת מורה 


INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone, Role,UserType)
VALUES ('T2', '1234', 'Rav Gotlib', '1990-09-01', 'Rabi Hakiva 116', 'Gotlib@gmail.com', '0556787258', 2,  'Teacher');


SET IDENTITY_INSERT Classes ON;

-- הכנסת כיתה


INSERT INTO Classes (Id, Password, Name, TeacherId, CountOfStudents)
VALUES (2, '3693', 'B1', 'T1', 18);

SET IDENTITY_INSERT Classes OFF;

-- הכנסת כסאות


INSERT INTO Chairs (SerialNumberByClass, ClassId, IsCenteral, IsFront, IsNearTheDoor, IsNearTheWindow)
VALUES 
(1, 2, 0, 1, 1, 0),
(2, 2, 1, 1, 0, 0),
(3, 2, 1, 1, 0, 0),
(4, 2, 1, 1, 0, 0),
(5, 2, 1, 1, 0, 0),
(6, 2, 0, 1, 0, 1),
(7, 2, 0, 1, 0, 0),
(8, 2, 1, 1, 0, 0),
(9, 2, 1, 1, 0, 0),
(10, 2, 1, 1, 0, 0),
(11, 2, 1, 1, 0, 0),
(12, 2, 0, 0, 0, 1),
(13, 2, 1, 0, 0, 0),
(14, 2, 1, 0, 0, 0),
(15, 2, 1, 0, 0, 0),
(16, 2, 1, 0, 0, 0),
(17, 2, 1, 0, 0, 0),
(18, 2, 0, 0, 0, 1);


DECLARE @id1 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 1 AND ClassId = 2);
DECLARE @id2 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 2 AND ClassId = 2);
DECLARE @id3 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 3 AND ClassId = 2);
DECLARE @id4 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 4 AND ClassId = 2);
DECLARE @id5 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 5 AND ClassId = 2);
DECLARE @id6 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 6 AND ClassId = 2);
DECLARE @id7 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 7 AND ClassId = 2);
DECLARE @id8 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 8 AND ClassId = 2);
DECLARE @id9 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 9 AND ClassId = 2);
DECLARE @id10 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 10 AND ClassId = 2);
DECLARE @id11 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 11 AND ClassId = 2);
DECLARE @id12 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 12 AND ClassId = 2);
DECLARE @id13 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 13 AND ClassId = 2);
DECLARE @id14 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 14 AND ClassId = 2);
DECLARE @id15 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 15 AND ClassId = 2);
DECLARE @id16 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 16 AND ClassId = 2);
DECLARE @id17 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 17 AND ClassId = 2);
DECLARE @id18 INT = (SELECT Id FROM Chairs WHERE SerialNumberByClass = 18 AND ClassId = 2);


INSERT INTO ChairNearbyChairs VALUES (@id1, @id2);
INSERT INTO ChairNearbyChairs VALUES (@id1, @id7);

INSERT INTO ChairNearbyChairs VALUES (@id2, @id1);
INSERT INTO ChairNearbyChairs VALUES (@id2, @id3);
INSERT INTO ChairNearbyChairs VALUES (@id2, @id8);

INSERT INTO ChairNearbyChairs VALUES (@id3, @id2);
INSERT INTO ChairNearbyChairs VALUES (@id3, @id4);
INSERT INTO ChairNearbyChairs VALUES (@id3, @id9);

INSERT INTO ChairNearbyChairs VALUES (@id4, @id3);
INSERT INTO ChairNearbyChairs VALUES (@id4, @id5);
INSERT INTO ChairNearbyChairs VALUES (@id4, @id10);


INSERT INTO ChairNearbyChairs VALUES (@id5, @id4);
INSERT INTO ChairNearbyChairs VALUES (@id5, @id6);
INSERT INTO ChairNearbyChairs VALUES (@id5, @id11);

INSERT INTO ChairNearbyChairs VALUES (@id6, @id5);
INSERT INTO ChairNearbyChairs VALUES (@id6, @id12);


INSERT INTO ChairNearbyChairs VALUES (@id7, @id1);
INSERT INTO ChairNearbyChairs VALUES (@id7, @id8);
INSERT INTO ChairNearbyChairs VALUES (@id7, @id13);

INSERT INTO ChairNearbyChairs VALUES (@id8, @id2);
INSERT INTO ChairNearbyChairs VALUES (@id8, @id7);
INSERT INTO ChairNearbyChairs VALUES (@id8, @id9);
INSERT INTO ChairNearbyChairs VALUES (@id8, @id14);

INSERT INTO ChairNearbyChairs VALUES (@id9, @id3);
INSERT INTO ChairNearbyChairs VALUES (@id9, @id8);
INSERT INTO ChairNearbyChairs VALUES (@id9, @id10);
INSERT INTO ChairNearbyChairs VALUES (@id9, @id15);

INSERT INTO ChairNearbyChairs VALUES (@id10, @id4);
INSERT INTO ChairNearbyChairs VALUES (@id10, @id9);
INSERT INTO ChairNearbyChairs VALUES (@id10, @id11);
INSERT INTO ChairNearbyChairs VALUES (@id10, @id16);

INSERT INTO ChairNearbyChairs VALUES (@id11, @id5);
INSERT INTO ChairNearbyChairs VALUES (@id11, @id10);
INSERT INTO ChairNearbyChairs VALUES (@id11, @id12);
INSERT INTO ChairNearbyChairs VALUES (@id11, @id17);

INSERT INTO ChairNearbyChairs VALUES (@id12, @id6);
INSERT INTO ChairNearbyChairs VALUES (@id12, @id11);
INSERT INTO ChairNearbyChairs VALUES (@id12, @id18);


INSERT INTO ChairNearbyChairs VALUES (@id13, @id7);
INSERT INTO ChairNearbyChairs VALUES (@id13, @id14);

INSERT INTO ChairNearbyChairs VALUES (@id14, @id8);
INSERT INTO ChairNearbyChairs VALUES (@id14, @id13);
INSERT INTO ChairNearbyChairs VALUES (@id14, @id15);

INSERT INTO ChairNearbyChairs VALUES (@id15, @id9);
INSERT INTO ChairNearbyChairs VALUES (@id15, @id14);
INSERT INTO ChairNearbyChairs VALUES (@id15, @id16);

INSERT INTO ChairNearbyChairs VALUES (@id16, @id10);
INSERT INTO ChairNearbyChairs VALUES (@id16, @id15);
INSERT INTO ChairNearbyChairs VALUES (@id16, @id17);

INSERT INTO ChairNearbyChairs VALUES (@id17, @id11);
INSERT INTO ChairNearbyChairs VALUES (@id17, @id16);
INSERT INTO ChairNearbyChairs VALUES (@id17, @id18);

INSERT INTO ChairNearbyChairs VALUES (@id18, @id12);
INSERT INTO ChairNearbyChairs VALUES (@id18, @id17);

-- הכנסת תלמידים

INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('ST1', 'P1', 'Avi', '2004-09-01', 'Rev Hami 5', 'Avi@gmail.com.com', '0548415240', 4,  'Student',  2, NULL, 3, 3, 2, NULL,5,'[]');

INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('ST2', 'P2', 'Ari', '2004-09-01', 'Rev Hami 5', 'Ari@gmail.com.com', '0548415240', 4,  'Student',  2, NULL, 4, 4, 2, NULL,5,'[]');

INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('ST3', 'P3', 'Elchanan', '2004-09-01', 'Rev Hami 5', 'Elchanan@gmail.com.com', '0548415240', 4,  'Student',  2, NULL, 2, 1, 2, NULL,5,'[]');

INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('ST4', 'P4', 'Elazar', '2004-09-01', 'Rev Hami 5', 'Elazar@gmail.com.com', '0548415240', 4,  'Student',  2, NULL, 5, 4, 4, NULL,5,'[]');


INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('ST5', 'P5', 'Yeuda', '2004-09-01', 'Rev Hami 5', 'Yeuda@gmail.com.com', '0548415240', 4,  'Student',  2, NULL, 2, 2, 3, NULL,5,'[]');

INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('ST6', 'P6', 'Yedidya', '2004-09-01', 'Rev Hami 5', 'Yedidya@gmail.com.com', '0548415240', 4,  'Student',  2, NULL, 1, 1, 3, NULL,5,'[]');

INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('ST7', 'P7', 'Yonatan', '2004-09-01', 'Rev Hami 5', 'Yonatan@gmail.com.com', '0548415240', 4,  'Student',  2, NULL, 1, 2, 1, NULL,5,'[]');

INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('ST8', 'P8', 'Israel', '2004-09-01', 'Rev Hami 5', 'Israel@gmail.com.com', '0548415240', 4,  'Student',  2, NULL, 3, 2, 1, NULL,5,'[]');


INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('ST9', 'P9', 'Menachem', '2004-09-01', 'Rev Hami 5', 'Menachem@gmail.com.com', '0548415240', 4,  'Student',  2, NULL, 3, 4, 2, NULL,5,'[]');

INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('ST10', 'P10', 'Mordechay', '2004-09-01', 'Rev Hami 5', 'Mordechay@gmail.com.com', '0548415240', 4,  'Student',  2, NULL, 4, 4, 4, NULL,5,'[]');

INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('ST11', 'P11', 'Michael', '2004-09-01', 'Rev Hami 5', 'Michael@gmail.com.com', '0548415240', 4,  'Student',  2, NULL, 1, 1, 2, NULL,5,'[]');

INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('ST12', 'P12', 'Meir', '2004-09-01', 'Rev Hami 5', 'Meir@gmail.com.com', '0548415240', 4,  'Student',  2, NULL, 2, 1, 3, NULL,5,'[]');


INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('ST13', 'P13', 'Shlomo', '2004-09-01', 'Rev Hami 5', 'Shlomo@gmail.com.com', '0548415240', 4,  'Student',  2, NULL, 1, 2, 2, NULL,5,'[]');

INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('ST14', 'P14', 'Shimi', '2004-09-01', 'Rev Hami 5', 'Shimi@gmail.com.com', '0548415240', 4,  'Student',  2, NULL, 1, 2, 2, NULL,5,'[]');

INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('ST15', 'P15', 'Shmuel', '2004-09-01', 'Rev Hami 5', 'Shmuel@gmail.com.com', '0548415240', 4,  'Student',  2, NULL, 4, 4, 2, NULL,5,'[]');

INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('ST16', 'P16', 'Shaul', '2004-09-01', 'Rev Hami 5', 'Shaul@gmail.com.com', '0548415240', 4,  'Student',  2, NULL, 3, 2, 3, NULL,5,'[]');


INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('ST17', 'P17', 'David', '2004-09-01', 'Rev Hami 5', 'David@gmail.com.com', '0548415240', 4,  'Student',  2, NULL, 3, 2, 5, NULL,5,'[]');

INSERT INTO Users (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('ST18', 'P18', 'Daniel', '2004-09-01', 'Rev Hami 5', 'Daniel@gmail.com.com', '0548415240', 4,  'Student',  2, NULL, 2, 1, 1, NULL,5,'[]');

-- הכנסת חברים מועדפים

INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST1', 'ST9'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST1', 'ST15'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST1', 'ST18'); 
																   		  
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST2', 'ST9'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST2', 'ST10'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST2', 'ST15'); 
																   		  
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST3', 'ST8'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST3', 'ST14'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST3', 'ST18'); 
																   		  
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST4', 'ST1'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST4', 'ST9'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST4', 'ST15'); 
																   		  
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST5', 'ST6'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST5', 'ST13'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST5', 'ST18'); 
																   		  
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST6', 'ST7'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST6', 'ST13'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST6', 'ST16'); 
																   		  
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST7', 'ST6'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST7', 'ST11'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST7', 'ST12'); 
																   		  
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST8', 'ST3'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST8', 'ST10'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST8', 'ST18'); 
																   		  
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST9', 'ST1'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST9', 'ST10'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST9', 'ST15'); 
																   
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST10', 'ST1'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST10', 'ST9'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST10', 'ST15'); 
																   		   
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST11', 'ST7'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST11', 'ST12'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST11', 'ST14'); 
																   		   
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST12', 'ST11'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST12', 'ST3'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST12', 'ST6'); 
																   		   
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST13', 'ST6'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST13', 'ST5'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST13', 'ST17'); 
																   		   
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST14', 'ST3'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST14', 'ST8'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST14', 'ST16'); 
																   		   
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST15', 'ST1'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST15', 'ST9'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST15', 'ST10'); 
																   		   
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST16', 'ST6'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST16', 'ST7'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST16', 'ST13'); 
																   		   
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST17', 'ST5'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST17', 'ST6'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST17', 'ST13'); 
																   		   
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST18', 'ST1'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST18', 'ST5'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('ST18', 'ST8'); 

-- הכנסת חברים לא מועדפים

INSERT INTO StudentNonFavoriteFriends (StudentId, NonFriendId) VALUES ('ST3', 'ST5'); 
INSERT INTO StudentNonFavoriteFriends (StudentId, NonFriendId) VALUES ('ST3', 'ST11'); 
																		 
INSERT INTO StudentNonFavoriteFriends (StudentId, NonFriendId) VALUES ('ST8', 'ST10'); 
INSERT INTO StudentNonFavoriteFriends (StudentId, NonFriendId) VALUES ('ST8', 'ST17');
																		 
INSERT INTO StudentNonFavoriteFriends (StudentId, NonFriendId) VALUES ('9T4', 'ST5'); 
																		 
INSERT INTO StudentNonFavoriteFriends (StudentId, NonFriendId) VALUES ('ST10', 'ST3'); 
INSERT INTO StudentNonFavoriteFriends (StudentId, NonFriendId) VALUES ('ST10', 'ST8');

INSERT INTO StudentNonFavoriteFriends (StudentId, NonFriendId) VALUES ('ST12', 'ST1'); 
INSERT INTO StudentNonFavoriteFriends (StudentId, NonFriendId) VALUES ('ST12', 'ST15');

INSERT INTO StudentNonFavoriteFriends (StudentId, NonFriendId) VALUES ('ST15', 'ST4'); 
INSERT INTO StudentNonFavoriteFriends (StudentId, NonFriendId) VALUES ('ST15', 'ST8');

