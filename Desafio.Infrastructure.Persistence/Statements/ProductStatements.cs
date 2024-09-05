namespace Desafio.Infrastructure.Persistence.Statements;

public static class ProductStatements
{
    public const string SQL_BASE =
        @"SELECT [Id]
              ,[ProductName]
              ,[Value]
              ,[IsDeleted]
              ,[CreatedAt]
          FROM [dbo].[Product]
          WHERE IsDeleted = 0 ";

    public const string SQL_INSERT =
        @"INSERT INTO [dbo].[Product]
           ([ProductName]
           ,[Value]
           ,[IsDeleted]
           ,[CreatedAt])
         VALUES
            (@ProductName
            ,@Value
            ,@IsDeleted
            ,@CreatedAt);
        SELECT IDENT_CURRENT([dbo].[OrderItem]) AS ID ";

    public const string SQL_UPDATE =
        @"UPDATE [dbo].[Product]
           SET [ProductName] = @ProductName
              ,[Value] = @Value
             WHERE Id = @Id";

    public const string SQL_DELETE =
        @"UPDATE [dbo].[Product]
           SET[IsDeleted] = 1
         WHERE Id = @Id";
}
