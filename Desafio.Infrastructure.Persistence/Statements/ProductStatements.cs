namespace Desafio.Infrastructure.Persistence.Statements;

public static class ProductStatements
{
    public const string SQL_BASE =
        @"SELECT [Id]
              ,[ProductName]
              ,[Value]
              ,[IsDeleted]
              ,[CreatedAt]
          FROM [dbo].[Products]
          WHERE IsDeleted = 0 ";

    public const string SQL_INSERT =
        @"INSERT INTO [dbo].[Products]
           ([ProductName]
           ,[Value]
           ,[IsDeleted]
           ,[CreatedAt])
         VALUES
            (@ProductName
            ,@Value
            ,@IsDeleted
            ,@CreatedAt);
        SELECT @@IDENTITY FROM [dbo].[Products] AS ID ";

    public const string SQL_UPDATE =
        @"UPDATE [dbo].[Products]
           SET [ProductName] = @ProductName
              ,[Value] = @Value
             WHERE Id = @Id";

    public const string SQL_DELETE =
        @"UPDATE [dbo].[Products]
           SET[IsDeleted] = 1
         WHERE Id = @Id";
}
