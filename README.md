# Eralp-Software
 Back-end .net core 7 solution for a task

MSSQL
For DB tables,
CREATE TABLE [dbo].[tblUser] (
    [id]        INT           IDENTITY (1, 1) NOT NULL,
    [username]  VARCHAR (50)  NOT NULL,
    [password]  VARCHAR (255) NOT NULL,
    [email]     VARCHAR (50)  NOT NULL,
    [firstname] VARCHAR (50)  NOT NULL,
    [lastname]  VARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_tblUser_1] PRIMARY KEY CLUSTERED ([username] ASC)
);



design of product table 
CREATE TABLE [dbo].[tblProduct] (
    [id]          INT             IDENTITY (1, 1) NOT NULL,
    [name]        VARCHAR (50)    NOT NULL,
    [description] VARCHAR (50)    NOT NULL,
    [price]       DECIMAL (10, 2) NOT NULL,
    [instock]     BIT             NOT NULL,
    [userid]      INT             NOT NULL,
    CONSTRAINT [PK_tblProduct] PRIMARY KEY CLUSTERED ([id] ASC)
);
