# FCT
There are few step you needs to follow to make is workable

Step - 1 ==> First Create a Database or connect with existing if already exists with same schema and tables

Script for creating Database 

begin tran

Create Database FctDB;
GO

Use FctDB;
GO

CREATE TABLE [dbo].[Customer] (
    [Id]                   INT        IDENTITY (1, 1) NOT NULL,
    [Name]                 Varchar(255)        NOT NULL,
    [Email]                Varchar(255)       NOT NULL,
    [Password]             Varchar(255)   NOT NULL
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 70)
);

GO


CREATE TABLE [dbo].[Product] (
    [Id]                   INT        IDENTITY (1, 1) NOT NULL,
    [Name]                 Varchar(255)        NOT NULL,
	[Price]                MONEY    DEFAULT ((0)) NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 70)
);

GO

CREATE TABLE [dbo].[Purchase] (
    [Id]                   INT        IDENTITY (1, 1) NOT NULL,
    [UserId]               INT        NOT NULL ,
	[ProductId]            INT        NOT NULL
    CONSTRAINT [PK_Purchase] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (FILLFACTOR = 70),
	CONSTRAINT FK_Customer FOREIGN KEY (UserId)    REFERENCES Customer(Id),
	CONSTRAINT FK_Product FOREIGN KEY (ProductId)    REFERENCES Product(Id)
);

GO
Commit 

Step - 2 ==> Open Fct_WebApi project solution file and set as a startup project

a) open Fct_WebApi project and update the SQL connection string in appsettings.json file

"ConnectionStrings": {
    "ConnectionString": "Server=localhost;Database=FctDB;Trusted_Connection=True;"
  },

b) Run locally and iis express will open application url as http://localhost:59014/

Step - 3 ==> Open  FCT-UI angular project and update environment file with iis express Web Api Url.

 apiUrl: 'http://localhost:59014'
 
Step -4 ==> Run Angular Application and host or run locally (Angular 8 - JWT Authentication with API)


I have implement basic funcationality that was requested based on time constraint.

- run npm command to install required packages

npm install

- you needs to verify API url in environment file, if needed please change it to validate full functionality 

- run below command to launch angular application 

ng s -o
