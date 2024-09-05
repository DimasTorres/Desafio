using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
          FROM [dbo].[User]
          WHERE IsDeleted = 0 ";

    public const string SQL_INSERT =
        @"INSERT INTO [dbo].[User]
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
        SELECT IDENT_CURRENT([dbo].[OrderItem]) AS ID ";

    public const string SQL_UPDATE =
        @"UPDATE [dbo].[User]
           SET [Name] = @Name
              ,[Login] = @Login
              ,[PasswordHash] = @PasswordHash
              ,[Email] = @Email
            WHERE Id = @Id";

    public const string SQL_EXIST_BY_LOGIN =
         @"SELECT 1 FROM [dbo].[User] WHERE IsDeleted = 0 AND Login Like @Login";

    public const string SQL_DELETE =
        @"UPDATE [dbo].[User]
           SET[IsDeleted] = 1
         WHERE Id = @Id";
}
