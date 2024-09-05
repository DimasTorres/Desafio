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
          FROM [dbo].[Order] o
          INNER JOIN [dbo].[User] u ON o.UserId = u.id
          WHERE o.IsDeleted = 0 ";

    public const string SQL_INSERT =
        @"INSERT INTO [dbo].[Order]
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
        SELECT IDENT_CURRENT([dbo].[Order]) AS ID ";
}
