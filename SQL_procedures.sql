
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

CREATE OR ALTER PROCEDURE p_InsertCastAndCrew
( @FirstName    VARCHAR(25),
  @LastName     VARCHAR(40),
  @DateOfBirth  VARCHAR(10),
  @Gender       VARCHAR(10),
  @FileID       UNIQUEIDENTIFIER)
AS
	BEGIN TRY
	BEGIN TRAN
		INSERT INTO CastAndCrew(FirstName, LastName, DateOfBirth,Gender,FileID)
		VALUES (@FirstName, @LastName, @DateOfBirth, @Gender, @FileID);
	COMMIT TRAN;
END TRY
BEGIN CATCH
	ROLLBACK TRAN;
END CATCH
GO

CREATE OR ALTER PROCEDURE p_UpdateCastAndCrew
( @CastID       UNIQUEIDENTIFIER,
  @FirstName    VARCHAR(25),
  @LastName     VARCHAR(40),
  @DateOfBirth  VARCHAR(10),
  @Gender       VARCHAR(10),
  @FileID       UNIQUEIDENTIFIER)
AS
	BEGIN TRY
	BEGIN TRAN
		UPDATE  CastAndCrew
		SET  FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth,Gender = @Gender, FileID = @FileID
		WHERE CastID = @CastID;
	COMMIT TRAN;
END TRY
BEGIN CATCH
	ROLLBACK TRAN;
END CATCH
GO

CREATE OR ALTER PROCEDURE p_DeleteCastAndCrew
( @CastID       UNIQUEIDENTIFIER)
AS
	BEGIN TRY
	BEGIN TRAN
		DELETE FROM CastAndCrew
		WHERE CastID = @CastID;
	COMMIT TRAN;
END TRY
BEGIN CATCH
	ROLLBACK TRAN;
END CATCH
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
( @ListName VARCHAR(50),
  @MovieID UNIQUEIDENTIFIER,
  @AccountID UNIQUEIDENTIFIER)
AS
BEGIN TRY
	BEGIN TRAN
		INSERT INTO MovieLists(ListName, MovieID, AccountID)
		VALUES (@ListName, @MovieID, @AccountID);
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
	

CREATE OR ALTER PROCEDURE p_SelectGenreOfMovie
( @MovieID     UNIQUEIDENTIFIER)
AS
	SELECT Title 
	FROM Genre g, GenreMovie gm 
	WHERE g.GenreID = gm.GenreID AND gm.MovieID = @MovieID;
GO


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

CREATE OR ALTER PROCEDURE p_InsertUserGenre
( @AccountID    UNIQUEIDENTIFIER,
  @GenreID      UNIQUEIDENTIFIER ) 
AS
BEGIN TRY
	BEGIN TRAN
		INSERT INTO UserGenre(AccountID, GenreId)
		VALUES(@AccountID, @GenreID);
		COMMIT TRAN;
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN;
	END CATCH
GO

CREATE OR ALTER PROCEDURE p_InsertCCMovie
( @MovieID UNIQUEIDENTIFIER,
  @CastID UNIQUEIDENTIFIER,
  @RoleInMovie VARCHAR(30))
AS
BEGIN TRY
	BEGIN TRAN
		INSERT INTO CCMovie(MovieID, CastID, RoleInMovie)
		VALUES(@MovieID, @CastID, @RoleInMovie);
		COMMIT TRAN;
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN;
	END CATCH
GO

CREATE OR ALTER PROCEDURE p_HowManyCastAndCrew
AS
	SELECT COUNT(CastAndCrew.CastID)
	FROM CastAndCrew, CCMovie
	WHERE CastAndCrew.CastID = CCMovie.CastID;
GO


CREATE OR ALTER PROCEDURE p_DeleteCCMovie
( @CastID UNIQUEIDENTIFIER,
  @MovieID UNIQUEIDENTIFIER,
  @RoleInMovie VARCHAR(30))
AS
	DELETE FROM CCMovie WHERE CastID = @CastID AND MovieID = @MovieID AND RoleInMovie = @RoleInMovie;
GO

SELECT * FROM (SELECT ROW_NUMBER() OVER ( ORDER BY Title ASC, YearOfProduction DESC ) 
AS RowNum, m.MovieID, m.Title, m.YearOfProduction, m.CountryOfOrigin, m.Duration, m.PlotOutline, m.FileID
FROM CastAndCrew cac, CCMovie ccm, Movie m
WHERE m.MovieID = ccm.MovieID AND cac.CastID = '8BBAA813-3B19-4BA1-8CF6-3F2F20BBE991') AS RowConstrainedResult
WHERE   RowNum > 0 AND RowNum <= 5;


SELECT  COUNT(m.MovieID)
FROM CastAndCrew cac, CCMovie ccm, Movie m
WHERE m.MovieID = ccm.MovieID AND cac.CastID = '8BBAA813-3B19-4BA1-8CF6-3F2F20BBE991'

CREATE OR ALTER PROCEDURE p_DeleteGenreMovie
( @MovieID UNIQUEIDENTIFIER,
  @GenreID UNIQUEIDENTIFIER)
AS
	DELETE FROM GenreMovie WHERE MovieID = @MovieID AND GenreID = @GenreID;
GO

CREATE OR ALTER PROCEDURE p_DeleteAccountRole
( @AccountID UNIQUEIDENTIFIER,
  @Role VARCHAR(20))
AS
	DELETE FROM AccountRole WHERE AccountID = AccountID AND Role = @Role;
GO


CREATE OR ALTER PROCEDURE p_GetRoleById
( @AccountID UNIQUEIDENTIFIER)
AS
	SELECT Role
	FROM AccountRole
	WHERE AccountID = @AccountID; 
GO


CREATE OR ALTER PROCEDURE p_UpdateAccountRole
( @AccountID UNIQUEIDENTIFIER,
  @Role VARCHAR(20)) 
AS
BEGIN TRY
	BEGIN TRAN
		UPDATE  AccountRole
		SET  Role = Role
		WHERE AccountID = @AccountID;
	COMMIT TRAN;
END TRY
BEGIN CATCH
	ROLLBACK TRAN;
END CATCH
GO
