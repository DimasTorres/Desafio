namespace Desafio.Infrastructure.Persistence.Statements;

public static class OrderItemStatements
{
    public const string SQL_BASE =
        @"SELECT oi.[Id]
            ,oi.[Amount]
            ,oi.[IsDeleted]
            ,oi.[CreatedAt]
	        ,o.[Id]
	        ,p.[Id]
	        ,p.[ProductName]
	        ,p.[Value]
        FROM [dbo].[OrderItems] oi
        INNER JOIN [dbo].[Orders] o ON o.Id = oi.OrderId
        INNER JOIN [dbo].[Products] p ON p.Id = oi.ProductId
        WHERE oi.IsDeleted = 0 AND p.IsDeleted = 0 ";

    public const string SQL_INSERT =
        @"INSERT INTO [dbo].[OrderItems]
           ([OrderId]
           ,[ProductId]
           ,[Amount]
           ,[IsDeleted]
           ,[CreatedAt])
         VALUES
            (@OrderId
            ,@ProductId
            ,@Amount
            ,@IsDeleted
            ,@CreatedAt);
        SELECT @@IDENTITY FROM [dbo].[OrderItems] AS ID ";
}