
CREATE OR ALTER PROCEDURE p_selectAll_account
AS
	SELECT * FROM Account;
GO

CREATE OR ALTER PROCEDURE p_select_by_user_and_pass
( @UserName     VARCHAR(30),
  @UserPassword VARCHAR(20))
AS
	SELECT * FROM Account
	WHERE UserName = @Username AND UserPassword = @UserPassword;
GO

CREATE OR ALTER PROCEDURE p_insert_account 
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

CREATE OR ALTER PROCEDURE p_delete_account
( @AccountID UNIQUEIDENTIFIER)
AS
	DELETE FROM Account WHERE AccountID = @AccountID;
GO

CREATE OR ALTER PROCEDURE p_update_account 
( @AccountID UNIQUEIDENTIFIER,
  @Email     VARCHAR(50),
  @UserName  VARCHAR(30),
  @UserPassword VARCHAR(20),
  @FileID    UNIQUEIDENTIFIER ) 
AS
BEGIN TRY
	BEGIN TRAN
		UPDATE  Account
		SET AccountID = @AccountID, Email = @Email, UserName = @UserName, UserPassword = @UserPassword,FileID = @FileID;
	COMMIT TRAN;
END TRY
BEGIN CATCH
	ROLLBACK TRAN;
END CATCH
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

