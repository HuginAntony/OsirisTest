CREATE TABLE dbo.Customer
(
	CustomerId INT NOT NULL,
	FirstName VARCHAR(255) NOT NULL,
	LastName VARCHAR(255) NOT NULL,
	EmailAddress VARCHAR(255) NOT NULL,
	DateOfBirth DATETIME NOT NULL,
	FirstDepositAmount DECIMAL(19,5) NULL,
	LastWagerAmount DECIMAL(19,5) NULL,
	LastWagerDateTime DATETIME NULL,
	InsertedDateTime DATETIME NOT NULL,
	LastUpdateDateTime DATETIME NULL,
	CONSTRAINT PK_Customer_CustomerId PRIMARY KEY(CustomerId)
)

CREATE TABLE dbo.Wager
(
	WagerId UNIQUEIDENTIFIER NOT NULL,
	CustomerId INT NOT NULL,
	Amount DECIMAL(19,5) NULL,
	WagerDateTime DATETIME NULL,
	IsValid BIT NOT NULL,
	InsertedDateTime DATETIME NOT NULL,
	CONSTRAINT PK_Wager_WagerId PRIMARY KEY(WagerId),
	CONSTRAINT FK_Wager_CustomerId FOREIGN KEY(CustomerId) REFERENCES dbo.Customer(CustomerId)
)

  CREATE VIEW WagerReport AS
  SELECT C.FirstName + ' ' + C.LastName AS 'CustomerName', W.CustomerId,
		CASE W.IsValid 
			WHEN 0 THEN 'Invalid'
			WHEN 1 THEN 'Valid'
		END AS Status, 
		COUNT(*) AS 'No. Of Wagers', SUM(W.Amount) AS 'Total Amount'
  FROM dbo.Wager W
   INNER JOIN Customer C ON C.CustomerId = W.CustomerId
  GROUP BY C.FirstName, C.LastName, W.CustomerId, IsValid
