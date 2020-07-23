
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
( @Title VARCHAR(80) )
AS
	SELECT * FROM Movie WHERE Title = @Title;
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
( @DateOfBirth DATE )
AS
	SELECT * FROM CastAndCrew WHERE DateOfBirth = @DateOfBirth;
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
	



INSERT INTO FileStorage
VALUES ('85e5e579-07cb-4da4-a46d-f813a8c314ef','asd','asd');

EXEC p_selectAll_account;
EXEC p_select_by_user_and_pass @UserName = 'P',
							   @UserPassword = '123';
EXEC p_insert_account @AccountID = '9E3D7E2B-7486-44BF-890E-BD00A0E3C901',
					  @Email = 'P@gmail.com',
					  @UserName = 'P',
					  @UserPassword = '123',
					  @FileID = '85e5e579-07cb-4da4-a46d-f813a8c314ef';

EXEC p_update_account @AccountID = '9E3D7E2B-7486-44BF-890E-BD00A0E3C901',
					  @Email = 'por@gmail.com',
					  @UserName = 'P',
					  @UserPassword = 'srbija',
					  @FileID = '85e5e579-07cb-4da4-a46d-f813a8c314ef';

EXEC p_delete_account @AccountID = '9E3D7E2B-7486-44BF-890E-BD00A0E3C901';

