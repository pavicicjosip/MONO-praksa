DROP TABLE UserGenre;
DROP TABLE MovieLists;
DROP TABLE Review;
DROP TABLE CCMovie;
DROP TABLE GenreMovie;
DROP TABLE Movie;
DROP TABLE CastAndCrew;
DROP TABLE Genre;
DROP TABLE Account;
DROP TABLE FileStorage;
DROP TABLE AdministratorAccount

CREATE TABLE FileStorage(
	FileID UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
	ImageName VARCHAR(40) NOT NULL,
	ImagePath VARCHAR(100) NOT NULL
);

CREATE TABLE AdministratorAccount (
	AccountID UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
	Email VARCHAR(50) NOT NULL,
	UserName VARCHAR(30) NOT NULL UNIQUE,
	UserPassword VARCHAR(64) NOT NULL,
	FileID UNIQUEIDENTIFIER NOT NULL
		CONSTRAINT AdministratorAccount_fk_FileStorage
        	REFERENCES FileStorage(FileID)	
);

CREATE TABLE Account (
	AccountID UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
	Email VARCHAR(50) NOT NULL,
	UserName VARCHAR(30) NOT NULL UNIQUE,
	UserPassword VARCHAR(64) NOT NULL,
	FileID UNIQUEIDENTIFIER NOT NULL
		CONSTRAINT Account_fk_FileStorage
        	REFERENCES FileStorage(FileID)	
);

CREATE TABLE Genre(
	GenreID UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
	Title VARCHAR(40) NOT NULL
);

CREATE TABLE CastAndCrew(
	CastID UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
	FirstName VARCHAR(25) NOT NULL,
	LastName VARCHAR(40) NOT NULL,
	DateOfBirth DATE NOT NULL,
	Gender VARCHAR(10) NOT NULL,
	FileID UNIQUEIDENTIFIER NOT NULL
		CONSTRAINT CastAndCrew_fk_FileStorage
        	REFERENCES FileStorage(FileID)
);

CREATE TABLE Movie(
	MovieID UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
	Title VARCHAR(80) NOT NULL,
	YearOfProduction INT NOT NULL,
	CountryOfOrigin VARCHAR(20) NOT NULL,
	Duration VARCHAR(5) NOT NULL,
	PlotOutline VARCHAR(400) NOT NULL,
	FileID UNIQUEIDENTIFIER NOT NULL
		CONSTRAINT Movie_fk_FileStorage
        	REFERENCES FileStorage(FileID)
);

CREATE TABLE GenreMovie(
	MovieID UNIQUEIDENTIFIER NOT NULL
		CONSTRAINT GenreMovie_fk_Movie
       		REFERENCES Movie(MovieID) ON DELETE CASCADE,
   	GenreID UNIQUEIDENTIFIER NOT NULL
		CONSTRAINT GenreMovie_fk_Genre
        	REFERENCES Genre(GenreID),
    	CONSTRAINT GenreMovie_pk PRIMARY KEY (MovieID, GenreID)
);

CREATE TABLE CCMovie(
	MovieID UNIQUEIDENTIFIER NOT NULL 
		CONSTRAINT CCMovie_fk_Movie
        	REFERENCES Movie(MovieID) ON DELETE CASCADE,
   	CastID UNIQUEIDENTIFIER NOT NULL
		CONSTRAINT CCMovie_fk_CastAndCrew
        	REFERENCES CastAndCrew(CastID),
	RoleInMovie VARCHAR(30) NOT NULL,
	CONSTRAINT CCMovie_pk PRIMARY KEY (MovieID, CastID)
);

CREATE TABLE Review(
	ReviewID UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
	NumberOfStars INTEGER NOT NULL,
	Comment VARCHAR(400) NOT NULL,
	DateAndTime DATETIME NOT NULL,
	AccountID UNIQUEIDENTIFIER NULL
		CONSTRAINT Review_fk_Account
		REFERENCES Account(AccountID) ON DELETE SET null,
	MovieID UNIQUEIDENTIFIER NOT NULL
		CONSTRAINT Review_fk_Movie
		REFERENCES Movie(MovieID)
);

CREATE TABLE MovieLists(
	ListID UNIQUEIDENTIFIER default NEWID(),
	MovieID UNIQUEIDENTIFIER NOT NULL
		CONSTRAINT MovieList_fk_Movie
       		REFERENCES Movie(MovieID),
    	AccountID UNIQUEIDENTIFIER NOT NULL
		CONSTRAINT MovieList_fk_Account
		REFERENCES Account(AccountID) ON DELETE CASCADE,
    	CONSTRAINT MovieList_pk PRIMARY KEY (ListID, MovieID, AccountID)
);

CREATE TABLE UserGenre(
	AccountID UNIQUEIDENTIFIER NOT NULL
		CONSTRAINT UserGenre_fk_Account
        REFERENCES Account(AccountID) ON DELETE CASCADE,
	GenreID UNIQUEIDENTIFIER NOT NULL
		CONSTRAINT UserGenre_fk_AGenre
        REFERENCES Genre(GenreID)
	CONSTRAINT UserGenre_pk PRIMARY KEY(AccountID, GenreID)
);

INSERT INTO Genre VALUES(default, 'Action');
INSERT INTO Genre VALUES(default, 'Adventure');
INSERT INTO Genre VALUES(default, 'Animation');
INSERT INTO Genre VALUES(default, 'Biography');
INSERT INTO Genre VALUES(default, 'Comedy');
INSERT INTO Genre VALUES(default, 'Crime');
INSERT INTO Genre VALUES(default, 'Documentary');
INSERT INTO Genre VALUES(default, 'Drama');
INSERT INTO Genre VALUES(default, 'Family');
INSERT INTO Genre VALUES(default, 'Fantasy');
INSERT INTO Genre VALUES(default, 'Film Noir');
INSERT INTO Genre VALUES(default, 'History');
INSERT INTO Genre VALUES(default, 'Horor');
INSERT INTO Genre VALUES(default, 'Music');
INSERT INTO Genre VALUES(default, 'Musical');
INSERT INTO Genre VALUES(default, 'Mystery');
INSERT INTO Genre VALUES(default, 'Romance');
INSERT INTO Genre VALUES(default, 'Sci-Fi');
INSERT INTO Genre VALUES(default, 'Short Film');
INSERT INTO Genre VALUES(default, 'Sport');
INSERT INTO Genre VALUES(default, 'Superhero');
INSERT INTO Genre VALUES(default, 'Thriller');
INSERT INTO Genre VALUES(default, 'War');
INSERT INTO Genre VALUES(default, 'Western');

SELECT* FROM Genre;
