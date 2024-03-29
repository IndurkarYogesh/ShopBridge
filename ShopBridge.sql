USE [master]
GO
/****** Object:  Database [ShopBridgeNew]    Script Date: 4/27/2022 8:16:39 PM ******/
CREATE DATABASE [ShopBridgeNew];
GO
USE [ShopBridgeNew]
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](100) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 4/27/2022 8:16:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [varchar](100) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCategoryMapping]    Script Date: 4/27/2022 8:16:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategoryMapping](
	[ProductCategoryMappingId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_ProductCategoryMapping] PRIMARY KEY CLUSTERED 
(
	[ProductCategoryMappingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductProperties]    Script Date: 4/27/2022 8:16:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductProperties](
	[PropertyId] [int] IDENTITY(1,1) NOT NULL,
	[PropertyName] [varchar](100) NULL,
 CONSTRAINT [PK_ProductProperties] PRIMARY KEY CLUSTERED 
(
	[PropertyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductPropertyMapping]    Script Date: 4/27/2022 8:16:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductPropertyMapping](
	[ProductPropertyMappingId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[PropertyId] [int] NOT NULL,
	[PropertyValue] [varchar](max) NULL,
 CONSTRAINT [PK_ProductPropertyMapping] PRIMARY KEY CLUSTERED 
(
	[ProductPropertyMappingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProductCategoryMapping]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategoryMapping_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductCategoryMapping] CHECK CONSTRAINT [FK_ProductCategoryMapping_Category]
GO
ALTER TABLE [dbo].[ProductCategoryMapping]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategoryMapping_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductCategoryMapping] CHECK CONSTRAINT [FK_ProductCategoryMapping_Product]
GO
ALTER TABLE [dbo].[ProductPropertyMapping]  WITH CHECK ADD  CONSTRAINT [FK_ProductPropertyMapping_ProductProperties] FOREIGN KEY([PropertyId])
REFERENCES [dbo].[ProductProperties] ([PropertyId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductPropertyMapping] CHECK CONSTRAINT [FK_ProductPropertyMapping_ProductProperties]
GO
ALTER TABLE [dbo].[ProductPropertyMapping]  WITH CHECK ADD  CONSTRAINT [FK_ProductPropertyMapping_ProductPropertyMapping] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductPropertyMapping] CHECK CONSTRAINT [FK_ProductPropertyMapping_ProductPropertyMapping]
GO
USE [master]
GO
ALTER DATABASE [ShopBridgeNew] SET  READ_WRITE 
GO


USE [ShopBridgeNew]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (1, N'Electronics')
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (2, N'Home')
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (3, N'Books')
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (4, N'Toy')
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (5, N'Appliances')
SET IDENTITY_INSERT [dbo].[Category] OFF
SET IDENTITY_INSERT [dbo].[ProductProperties] ON 

INSERT [dbo].[ProductProperties] ([PropertyId], [PropertyName]) VALUES (1, N'Description')
INSERT [dbo].[ProductProperties] ([PropertyId], [PropertyName]) VALUES (2, N'Price')
SET IDENTITY_INSERT [dbo].[ProductProperties] OFF
