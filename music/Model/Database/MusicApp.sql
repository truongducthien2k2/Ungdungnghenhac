﻿CREATE DATABASE MusicApp
USE MusicApp

CREATE TABLE CLIENT(
 id INT IDENTITY(1,1) PRIMARY KEY,
 userName VARCHAR(50) UNIQUE,
 pass TEXT,
 fullName NVARCHAR(100),
 email VARCHAR(100),
 phone CHAR(10),
 isAdmin BIT,
 VIP BIT
)
select * from CLIENT
INSERT INTO CLIENT (userName, fullName, email, phone, isAdmin, VIP) VALUES ('annoy', N'Lê Thế Phúc', 'lethephuc2002@gmail.com', '0368341595', 0, 0)


CREATE TABLE VIDEO(
 id INT IDENTITY(1,1) PRIMARY KEY,
 videoName NVARCHAR(100),
 videoImage VARCHAR(500),
 videoDesc TEXT,
 topicId INT REFERENCES TOPIC(id),
 categoryId INT REFERENCES CATEGORY(id),
)

CREATE TABLE SONG(
 id INT IDENTITY(1,1) PRIMARY KEY,
 songName NVARCHAR(100),
 topicId INT REFERENCES TOPIC(id),
 categoryId INT REFERENCES CATEGORY(id),
 albumId INT REFERENCES ALBUM(id),
 singerId INT REFERENCES SINGER(id),
 lyrics TEXT,
 songImage VARCHAR(500),
 songCode VARCHAR(MAX),
 areaId CHAR(1) REFERENCES AREA(id)
)

CREATE TABLE TOPIC(
 id INT IDENTITY(1,1) PRIMARY KEY,
 topicName NVARCHAR(100),
 topicImage VARCHAR(500),
)

CREATE TABLE CATEGORY(
 id INT IDENTITY(1,1) PRIMARY KEY,
 categoryName NVARCHAR(100),
 categoryImage VARCHAR(500),
 topicId INT REFERENCES TOPIC(id)
)

CREATE TABLE ALBUM(
 id INT IDENTITY(1,1) PRIMARY KEY,
 albumName NVARCHAR(100),
 publishDate DATE,
 albumImage VARCHAR(500),
)

CREATE TABLE SINGER(
 id INT IDENTITY(1,1) PRIMARY KEY,
 singerName NVARCHAR(100),
 intro NVARCHAR(MAX),
 singerImage VARCHAR(500)
)select * from SINGER

CREATE TABLE AREA(
 id CHAR(1) PRIMARY KEY,
 areaName NVARCHAR(50)
)

CREATE TABLE PLAYLIST(
 id INT IDENTITY(1,1) PRIMARY KEY,
 playlistName NVARCHAR(100),
 clientId INT REFERENCES CLIENT(id),
 ipAddress VARCHAR(15)
)

CREATE TABLE SONG_PLAYLIST(
 id INT IDENTITY(1,1) PRIMARY KEY,
 playlistId INT REFERENCES PLAYLIST(id),
 songId INT REFERENCES SONG(id)
)

CREATE TABLE FAVORITE_LIST(
 id INT IDENTITY(1,1) PRIMARY KEY,
 clientId INT REFERENCES CLIENT(id),
)

CREATE TABLE FAVORITE_LIST_SONG(
 id INT IDENTITY(1,1) PRIMARY KEY,
 favoriteId INT REFERENCES FAVORITE_LIST(id),
 songId INT REFERENCES SONG(id)
)

CREATE TABLE CLIENT_VIEW_SONG(
 id INT IDENTITY(1,1) PRIMARY KEY,
 songId INT REFERENCES SONG(id),
 clientId INT REFERENCES CLIENT(id),
 currentViews INT,
 viewDate DATETIME
)

CREATE TABLE CLIENT_VIEW_VIDEO(
 id INT IDENTITY(1,1) PRIMARY KEY,
 videoId INT REFERENCES SONG(id),
 clientId INT REFERENCES CLIENT(id),
 currentViews INT,
 likeVideo BIT
)

CREATE TABLE CLIENT_LOVE_SONG(
 id INT IDENTITY(1,1) PRIMARY KEY,
 songId INT REFERENCES SONG(id),
 clientId INT REFERENCES CLIENT(id),
 loveDate DATETIME
)

CREATE TABLE COMMENT(
 id INT IDENTITY(1,1) PRIMARY KEY,
 content TEXT,
 commentDate DATETIME,
 songId INT REFERENCES SONG(id),
 clientId INT REFERENCES CLIENT(id)
)

CREATE TABLE ADVERTISEMENT(
 id INT IDENTITY(1,1) PRIMARY KEY,
 adImage VARCHAR(500),
 adTimeSpan INT
)