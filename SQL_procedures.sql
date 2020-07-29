
CREATE OR ALTER PROCEDURE p_SelectAllAccounts
AS
	SELECT * FROM Account;
GO

CREATE OR ALTER PROCEDURE p_SelectByUserAndPass
( @UserName     VARCHAR(30),
  @UserPassword VARCHAR(20))
AS
	SELECT * FROM Account
	WHERE UserName = @Username AND UserPassword = @UserPassword;
GO

CREATE OR ALTER PROCEDURE p_SelectByAccountID
( @AccountID UNIQUEIDENTIFIER)
AS
	SELECT * FROM Account
	WHERE AccountID = @AccountID;
GO

CREATE OR ALTER PROCEDURE p_InsertAccount 
( @AccountID    UNIQUEIDENTIFIER,
  @Email        VARCHAR(50),
  @UserName     VARCHAR(30),
  @UserPassword VARCHAR(20),
  @FileID       UNIQUEIDENTIFIER ) 
AS
BEGIN TRY
	BEGIN TRAN
		INSERT INTO Account(AccountID, Email, UserName, UserPassword, FileID)
		VALUES(@AccountID, @Email, @UserName, @UserPassword, @FileID);
		COMMIT TRAN;
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN;
	END CATCH
GO

CREATE OR ALTER PROCEDURE p_DeleteAccount
( @AccountID UNIQUEIDENTIFIER)
AS
	DELETE FROM Account WHERE AccountID = @AccountID;
GO

CREATE OR ALTER PROCEDURE p_UpdateAccount 
( @AccountID UNIQUEIDENTIFIER,
  @Email     VARCHAR(50),
  @UserName  VARCHAR(30),
  @UserPassword VARCHAR(20),
  @FileID    UNIQUEIDENTIFIER ) 
AS
BEGIN TRY
	BEGIN TRAN
		UPDATE  Account
		SET  Email = @Email, UserName = @UserName, UserPassword = @UserPassword,FileID = @FileID
		WHERE AccountID = @AccountID;
	COMMIT TRAN;
END TRY
BEGIN CATCH
	ROLLBACK TRAN;
END CATCH
GO


CREATE OR ALTER PROCEDURE p_GetMovieByName
( @Title VARCHAR(80), 
  @PageNumberStart INT,
  @PageNumberEnd INT)
AS
	SELECT  *
	FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY Title ) AS RowNum, *
			  FROM      Movie
			  WHERE     Title = @Title
			) AS RowConstrainedResult
	WHERE   RowNum >= @PageNumberStart
		AND RowNum <= @PageNumberEnd
	ORDER BY RowNum;
GO


CREATE OR ALTER PROCEDURE p_GetMovieByYear
( @YearOfProduction VARCHAR(80) )
AS
	SELECT * FROM Movie WHERE YearOfProduction = @YearOfProduction;
GO

CREATE OR ALTER PROCEDURE p_GetMovieByGenre
( @Title VARCHAR(40) )
AS
	SELECT m.MovieID, m.Title, m.YearOfProduction, m.CountryOfOrigin, m.Duration, m.PlotOutline, m.FileID
	FROM Movie m, GenreMovie gm, Genre g
	WHERE gm.MovieID = m.MovieID AND g.GenreID = gm.GenreID AND g.Title = @Title; 
GO

CREATE OR ALTER PROCEDURE p_GetMovieCastAndCrew
( @Title VARCHAR(80))
AS
	SELECT m.MovieID, m.Title, m.YearOfProduction, m.CountryOfOrigin, m.Duration, m.PlotOutline, m.FileID
	FROM Movie m, CCMovie ccm, CastAndCrew cac
	WHERE m.MovieID = ccm.MovieID AND ccm.CastID = cac.CastID AND m.Title = @Title;
GO

CREATE OR ALTER PROCEDURE p_GetByFirstName
( @FirstName VARCHAR(25) )
AS
	SELECT * FROM CastAndCrew WHERE FirstName = @FirstName;
GO

CREATE OR ALTER PROCEDURE p_GetByLastName
( @LastName VARCHAR(40) )
AS
	SELECT * FROM CastAndCrew WHERE LastName = @LastName;
GO

CREATE OR ALTER PROCEDURE p_GetByDateOfBirth
( @DateOfBirth VARCHAR(10) )
AS
	SELECT * FROM CastAndCrew WHERE DateOfBirth = CAST(@DateOfBirth AS DATE);
GO


CREATE OR ALTER PROCEDURE p_InsertReview
( @ReviewID UNIQUEIDENTIFIER,
  @NumberOfStars INT,
  @Comment VARCHAR(400),
  @DateAndTime DATETIME,
  @AccountID UNIQUEIDENTIFIER,
  @MovieID UNIQUEIDENTIFIER )
AS
BEGIN TRY
	BEGIN TRAN
		INSERT INTO Review(ReviewID, NumberOfStars, Comment, DateAndTime, AccountID, MovieID)
		VALUES(@ReviewID, @NumberOfStars, @Comment, @DateAndTime, @AccountID, @MovieID);
		COMMIT TRAN;
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN;
	END CATCH
GO

CREATE OR ALTER PROCEDURE p_UpdateReview
( @ReviewID UNIQUEIDENTIFIER,
  @NumberOfStars INT,
  @Comment VARCHAR(400),
  @DateAndTime DATETIME,
  @AccountID UNIQUEIDENTIFIER,
  @MovieID UNIQUEIDENTIFIER )
AS
BEGIN TRY
	BEGIN TRAN
		UPDATE Review
		SET NumberOfStars = @NumberOfStars, Comment = @Comment, DateAndTime = @DateAndTime, AccountID = @AccountID, MovieID = @MovieID 
		WHERE ReviewID = @ReviewID;
		COMMIT TRAN;
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN;
	END CATCH
GO

CREATE OR ALTER PROCEDURE p_DeleteReview
( @ReviewID UNIQUEIDENTIFIER)
AS
	DELETE FROM Review WHERE ReviewID = @ReviewID;
GO

CREATE OR ALTER PROCEDURE p_InsertMovieToList
( @ListID UNIQUEIDENTIFIER,
  @MovieID UNIQUEIDENTIFIER,
  @AccountID UNIQUEIDENTIFIER)
AS
BEGIN TRY
	BEGIN TRAN
		INSERT INTO MovieLists(ListID, MovieID, AccountID)
		VALUES (@ListID, @MovieID, @AccountID);
		COMMIT TRAN;
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN;
	END CATCH
GO

CREATE OR ALTER PROCEDURE p_DeleteMovieFromList
( @MovieID UNIQUEIDENTIFIER)
AS
	DELETE FROM MovieLists WHERE MovieID = @MovieID;
GO
	


INSERT INTO Movie(Title, YearOfProduction, CountryOfOrigin, Duration, PlotOutline, FileID)
VALUES ('Test', 1234, 'RVATIII', '45', 'Srp', '1180C2F6-A482-49F1-9628-5CA3D7EA6A3B' );

SELECT * FROM Movie;


SELECT m.MovieID, m.Title, m.YearOfProduction, m.CountryOfOrigin, m.Duration, m.PlotOutline, m.FileID
FROM Movie m, GenreMovie gm, Genre g
WHERE  g.Title = 'Sport' AND gm.GenreID = g.GenreID AND m.MovieID = gm.MovieID ;

CREATE OR ALTER PROCEDURE p_SelectGenreOfMovie
( @MovieID     UNIQUEIDENTIFIER)
AS
	SELECT Title 
	FROM Genre g, GenreMovie gm 
	WHERE g.GenreID = gm.GenreID AND gm.MovieID = @MovieID;
GO

SELECT * FROM Movie;

CREATE OR ALTER PROCEDURE p_InsertGenreMovie
( @MovieId    UNIQUEIDENTIFIER,
  @GenreID       UNIQUEIDENTIFIER ) 
AS
BEGIN TRY
	BEGIN TRAN
		INSERT INTO GenreMovie(MovieID, GenreId)
		VALUES(@MovieID, @GenreID);
		COMMIT TRAN;
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN;
	END CATCH
GO
