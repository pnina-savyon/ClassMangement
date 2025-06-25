use ClassManagementDB

-- הכנסת מורה 
INSERT INTO [User] (Id, Password, Name, DateOfBirth, Address, Email, Phone, Role,UserType)
VALUES ('T1', '123456', 'Sari fridman', '1990-09-01', 'Hahavat Shalom 12', '12sr445@gmail.com', '052-7654321', 2,  'Teacher');

-- הכנסת כיתה

INSERT INTO Classes (Password, Name,TeacherId, CountOfStudents)
VALUES ('8214', 'A1', 'T1', 12);

-- הכנסת כסאות
INSERT INTO Chairs (SerialNumberByClass,ClassId,  IsCenteral, IsFront, IsNearTheDoor, IsNearTheWindow)
VALUES (1, 1, 0, 1, 1, 0);

INSERT INTO Chairs (SerialNumberByClass,ClassId,  IsCenteral, IsFront, IsNearTheDoor, IsNearTheWindow)
VALUES (2, 1, 1, 1, 0, 0);

INSERT INTO Chairs (SerialNumberByClass,ClassId,  IsCenteral, IsFront, IsNearTheDoor, IsNearTheWindow)
VALUES (3, 1, 1, 1, 0, 0);

INSERT INTO Chairs (SerialNumberByClass,ClassId,  IsCenteral, IsFront, IsNearTheDoor, IsNearTheWindow)
VALUES (4, 1, 0, 1, 0, 1);


INSERT INTO Chairs (SerialNumberByClass,ClassId,  IsCenteral, IsFront, IsNearTheDoor, IsNearTheWindow)
VALUES (5, 1, 0, 0, 0, 0);

INSERT INTO Chairs (SerialNumberByClass,ClassId,  IsCenteral, IsFront, IsNearTheDoor, IsNearTheWindow)
VALUES (6, 1, 1, 0, 0, 0);

INSERT INTO Chairs (SerialNumberByClass,ClassId,  IsCenteral, IsFront, IsNearTheDoor, IsNearTheWindow)
VALUES (7, 1, 1, 0, 0, 0);

INSERT INTO Chairs (SerialNumberByClass,ClassId,  IsCenteral, IsFront, IsNearTheDoor, IsNearTheWindow)
VALUES (8, 1, 0, 0, 0, 1);


INSERT INTO Chairs (SerialNumberByClass,ClassId,  IsCenteral, IsFront, IsNearTheDoor, IsNearTheWindow)
VALUES (9, 1, 0, 0, 0, 0);

INSERT INTO Chairs (SerialNumberByClass,ClassId,  IsCenteral, IsFront, IsNearTheDoor, IsNearTheWindow)
VALUES (10, 1, 1, 0, 0, 0);

INSERT INTO Chairs (SerialNumberByClass,ClassId,  IsCenteral, IsFront, IsNearTheDoor, IsNearTheWindow)
VALUES (11, 1, 1, 0, 0, 0);

INSERT INTO Chairs (SerialNumberByClass,ClassId,  IsCenteral, IsFront, IsNearTheDoor, IsNearTheWindow)
VALUES (12, 1, 0, 0, 0, 1);

--הכנסת כיסאות שכנים
INSERT INTO ChairNearbyChairs VALUES (1, 2);
INSERT INTO ChairNearbyChairs VALUES (1, 5);

INSERT INTO ChairNearbyChairs VALUES (2, 1);
INSERT INTO ChairNearbyChairs VALUES (2, 3);
INSERT INTO ChairNearbyChairs VALUES (2, 6);

INSERT INTO ChairNearbyChairs VALUES (3, 2);
INSERT INTO ChairNearbyChairs VALUES (3, 4);
INSERT INTO ChairNearbyChairs VALUES (3, 7);

INSERT INTO ChairNearbyChairs VALUES (4, 3);
INSERT INTO ChairNearbyChairs VALUES (4, 8);

INSERT INTO ChairNearbyChairs VALUES (5, 1);
INSERT INTO ChairNearbyChairs VALUES (5, 6);
INSERT INTO ChairNearbyChairs VALUES (5, 9);

INSERT INTO ChairNearbyChairs VALUES (6, 2);
INSERT INTO ChairNearbyChairs VALUES (6, 5);
INSERT INTO ChairNearbyChairs VALUES (6, 7);
INSERT INTO ChairNearbyChairs VALUES (6, 10);

INSERT INTO ChairNearbyChairs VALUES (7, 3);
INSERT INTO ChairNearbyChairs VALUES (7, 6);
INSERT INTO ChairNearbyChairs VALUES (7, 8);
INSERT INTO ChairNearbyChairs VALUES (7, 11);

INSERT INTO ChairNearbyChairs VALUES (8, 4);
INSERT INTO ChairNearbyChairs VALUES (8, 7);
INSERT INTO ChairNearbyChairs VALUES (8, 12);

INSERT INTO ChairNearbyChairs VALUES (9, 5);
INSERT INTO ChairNearbyChairs VALUES (9, 10);

INSERT INTO ChairNearbyChairs VALUES (10, 6);
INSERT INTO ChairNearbyChairs VALUES (10, 9);
INSERT INTO ChairNearbyChairs VALUES (10, 11);

INSERT INTO ChairNearbyChairs VALUES (11, 10);
INSERT INTO ChairNearbyChairs VALUES (11, 7);
INSERT INTO ChairNearbyChairs VALUES (11, 12);

INSERT INTO ChairNearbyChairs VALUES (12, 8);
INSERT INTO ChairNearbyChairs VALUES (12, 11); 

-- הכנסת תלמידים
INSERT INTO [User] (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('S1', 'pass1', 'Racheli havraham', '2004-09-01', 'Halprin 17', 'Racheli@gmail.com.com', '052-7654321', 4,  'Student',  1, NULL, 2, 2, 2, NULL, 50,'[]');

INSERT INTO [User] (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('S2', 'pass2', 'Yeudit awat', '2004-09-01', 'Halprin 17', 'Yeudit@gmail.com.com', '052-7654321', 4,  'Student',  1, NULL, 3, 2, 2, NULL, 50,'[]');

INSERT INTO [User] (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('S3', 'pass3', 'Chani Heytan', '2004-09-01', 'Halprin 17', 'Chani@gmail.com.com', '052-7654321', 4,  'Student',  1, NULL, 4, 1, 2, NULL, 50,'[]');

INSERT INTO [User] (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('S4', 'pass4', 'Hadas Bek', '2004-09-01', 'Halprin 17', 'Hadas@gmail.com.com', '052-7654321', 4,  'Student',  1, NULL, 5, 1, 4, NULL, 50,'[]');



INSERT INTO [User] (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('S5', 'pass5', 'Shevi Bretler', '2004-09-01', 'Halprin 17', 'Shevi@gmail.com.com', '052-7654321', 4,  'Student',  1, NULL, 2, 3, 4, NULL, 50,'[]');

INSERT INTO [User] (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('S6', 'pass6', 'Rachli Hainrich', '2004-09-01', 'Halprin 17', 'Rachli@gmail.com.com', '052-7654321', 4,  'Student',  1, NULL, 2, 1, 1, NULL, 50,'[]');

INSERT INTO [User] (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('S7', 'pass7', 'Braci Levi', '2004-09-01', 'Halprin 17', 'Braci@gmail.com.com', '052-7654321', 4,  'Student',  1, NULL, 3, 4, 1, NULL, 50,'[]');

INSERT INTO [User] (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('S8', 'pass8', 'Shuli Langberg', '2004-09-01', 'Halprin 17', 'Shuli@gmail.com.com', '052-7654321', 4,  'Student',  1, NULL, 1, 1, 4, NULL, 50,'[]');


INSERT INTO [User] (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('S9', 'pass9', 'Zipora Pozen', '2004-09-01', 'Halprin 17', 'Zipora@gmail.com.com', '052-7654321', 4,  'Student',  1, NULL, 1, 3, 2, NULL, 50,'[]');

INSERT INTO [User] (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('S10', 'pass10', 'Chani Siton', '2004-09-01', 'Halprin 17', 'Chani@gmail.com.com', '052-7654321', 4,  'Student',  1, NULL, 1, 3, 1, NULL, 50,'[]');

INSERT INTO [User] (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('S11', 'pass11', 'Rivka Siboni', '2004-09-01', 'Halprin 17', 'Rivka@gmail.com.com', '052-7654321', 4,  'Student',  1, NULL, 2, 2, 4, NULL, 50,'[]');

INSERT INTO [User] (Id, Password, Name, DateOfBirth, Address, Email, Phone,Role,  UserType, ClassId, ChairId, MoralLevel, StatusSocial,  AttentionLevel, ImageUrl, Priority,HistoryChairsJson)
VALUES ('S12', 'pass12', 'Pnina Savyon', '2004-09-01', 'Halprin 17', 'Pnina@gmail.com.com', '052-7654321', 4,  'Student',  1, NULL, 2, 2, 2, NULL, 50,'[]');

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

INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S10', 'S3'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S10', 'S9'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S10', 'S7'); 

INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S11', 'S1'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S11', 'S2'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S11', 'S4'); 

INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S12', 'S9'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S12', 'S11'); 
INSERT INTO StudentFavoriteFriends (StudentId, FriendId) VALUES ('S12', 'S12'); 

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