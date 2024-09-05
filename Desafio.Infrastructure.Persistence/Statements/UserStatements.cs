namespace Desafio.Infrastructure.Persistence.Statements;

public static class UserStatements
{
    public const string SQL_BASE =
        @"SELECT [Id]
              ,[Name]
              ,[Login]
              ,[PasswordHash]
              ,[Email]
              ,[IsDeleted]
              ,[CreatedAt]
          FROM [dbo].[Users]
          WHERE IsDeleted = 0 ";

    public const string SQL_INSERT =
        @"INSERT INTO [dbo].[Users]
           ([Name]
           ,[Login]
           ,[PasswordHash]
           ,[Email]
           ,[IsDeleted]
           ,[CreatedAt])
         VALUES
           (@Name
           ,@Login
           ,@PasswordHash
           ,@Email
           ,@IsDeleted
           ,@CreatedAt);
        SELECT IDENT_CURRENT([dbo].[Users]) AS ID ";

    public const string SQL_UPDATE =
        @"UPDATE [dbo].[Users]
           SET [Name] = @Name
              ,[Login] = @Login
              ,[PasswordHash] = @PasswordHash
              ,[Email] = @Email
            WHERE Id = @Id";

    public const string SQL_EXIST_BY_LOGIN =
         @"SELECT 1 FROM [dbo].[Users] WHERE IsDeleted = 0 AND Login Like @Login";

    public const string SQL_DELETE =
        @"UPDATE [dbo].[Users]
           SET[IsDeleted] = 1
         WHERE Id = @Id";
}
