INSERT INTO IncomeCategories (Name) VALUES 
( N'Зарплата')
	GO

INSERT INTO CostCategories (Name, MonthLimit) VALUES 
	(N'Еда', 100),
	(N'Коммуналка', 50),
	(N'Интернет', 10),
	(N'Одежда', 50),
	(N'Кредит', 500)
	GO

	SELECT * from IncomeCategories
	go

	select *from CostCategories
	go

INSERT INTO Incomes (Id, IncomeCategoryId, Date, Amount, [Comment]) 
VALUES ('EA0B0DBA-3CE5-4B7F-8726-79009100CBE8', 1, GETUTCDATE(), 1000, null)
go

INSERT INTO Costs (Id, CostCategoryId, Date, Amount, Comment) 
VALUES ('A4BF377F-E965-4201-9C7B-0151F7ED503B', 2, GETUTCDATE(), 10, null),
		('56397655-AC12-43C8-AF80-FD9718FD7166', 1, GETUTCDATE(), 10, N'Prostor')
go
