namespace Desafio.Infrastructure.Persistence.Statements;

public static class OrderStatements
{
    public const string SQL_BASE =
        @"SELECT o.Id
              ,o.IsDeleted
              ,o.CreatedAt
              ,o.ClientName
              ,o.ClientEmail              
              ,o.IsPaid
              ,u.Id
              ,u.Name
          FROM [dbo].[Orders] o
          INNER JOIN [dbo].[Users] u ON o.UserId = u.id
          WHERE o.IsDeleted = 0 ";

    public const string SQL_INSERT =
        @"INSERT INTO [dbo].[Orders]
           ([ClientName]
           ,[ClientEmail]
           ,[IsPaid]
           ,[UserId]
           ,[IsDeleted]
           ,[CreatedAt])
        VALUES
            (@ClientName
            ,@ClientEmail
            ,@IsPaid
            ,@UserId
            ,@IsDeleted
            ,@CreatedAt);
        SELECT @@IDENTITY FROM [dbo].[Orders] AS ID ";
}
