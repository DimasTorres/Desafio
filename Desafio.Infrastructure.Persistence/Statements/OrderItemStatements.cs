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
        FROM [dbo].[OrderItem] oi
        INNER JOIN [dbo].[Order] o ON o.Id = oi.OrderId
        INNER JOIN [dbo].[Product] p ON p.Id = oi.ProductId
        WHERE oi.IsDeleted = 0 AND p.IsDeleted = 0 ";

    public const string SQL_INSERT =
        @"INSERT INTO [dbo].[OrderItem]
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
        SELECT IDENT_CURRENT([dbo].[OrderItem]) AS ID ";
}