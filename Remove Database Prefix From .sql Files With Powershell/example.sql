Select * from Invoice.dbo.Invoices

Select * from Purchases.dbo.Purchases

Delete From Purchases.dbo.BulkPurchases

Update Invoice.dbo.RecentInvoices Set ModifiedDate = GETDATE() Where ID > 20