SET IDENTITY_INSERT Genre ON;

-- ########### Genre ###########
INSERT INTO [dbo].[Genre](ID, G_Name) VALUES(1000, 'Action');
INSERT INTO [dbo].[Genre](ID, G_Name) VALUES(1001,'Scial');
INSERT INTO [dbo].[Genre](ID, G_Name) VALUES(1002,'Sport');
INSERT INTO [dbo].[Genre](ID, G_Name) VALUES(1003,'History');
INSERT INTO [dbo].[Genre](ID, G_Name) VALUES(1004,'General');

SET IDENTITY_INSERT Genre OFF;

SET IDENTITY_INSERT Users ON;
-- ########### Users ###########
INSERT INTO [dbo].[Users](ID, Name, Username, Password) VALUES(2000, 'Sami', 'Jameel', 11234);
INSERT INTO [dbo].[Users](ID, Name, Username, Password) VALUES(2001, 'Nano', 'Camel', 21234);
INSERT INTO [dbo].[Users](ID, Name, Username, Password) VALUES(2002, 'Hadi', 'Dodi', 31234);
INSERT INTO [dbo].[Users](ID, Name, Username, Password) VALUES(2003, 'Mahd', 'Cyber', 41234);
INSERT INTO [dbo].[Users](ID, Name, Username, Password) VALUES(2004, 'Dhahb', 'Short', 51234);
SET IDENTITY_INSERT Users OFF;


-- ########### Headline ###########
INSERT INTO [dbo].[Headline](GenreID, UserID, Title, Body, Comment, PDate) VALUES(1000, 2000, '14 people died', ' ', ' ', '01/12/2017');
INSERT INTO [dbo].[Headline](GenreID, UserID, Title, Body, Comment, PDate) VALUES(1002, 2001, 'Happy wife happy life', ' ', ' ', '01/12/2017');
INSERT INTO [dbo].[Headline](GenreID, UserID, Title, Body, Comment, PDate) VALUES(1001, 2002, 'No way to get away', ' ', ' ', '01/12/2017');
INSERT INTO [dbo].[Headline](GenreID, UserID, Title, Body, Comment, PDate) VALUES(1003, 2003, 'Winter is comming', ' ', ' ', '01/12/2017');
INSERT INTO [dbo].[Headline](GenreID, UserID, Title, Body, Comment, PDate) VALUES(1002, 2004, 'Alhilal is the best in Asia', ' ', ' ', '01/12/2017');
INSERT INTO [dbo].[Headline](GenreID, UserID, Title, Body, Comment, PDate) VALUES(1002, 2000, 'Real Madrid is the best in Europe', ' ', ' ', '01/12/2017');
INSERT INTO [dbo].[Headline](GenreID, UserID, Title, Body, Comment, PDate) VALUES(1004, 2002, 'Next week a suroeise', ' ', ' ', '01/12/2017');
INSERT INTO [dbo].[Headline](GenreID, UserID, Title, Body, Comment, PDate) VALUES(1003, 2001, 'Who did invente it', ' ', ' ', '01/12/2017');
