USE [master]
GO
/****** Object:  Database [Pos_db]    Script Date: 10/12/2024 10:30:25 pm ******/
CREATE DATABASE [Pos_db]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'POS_db', FILENAME = N'C:\Users\Ganotice\OneDrive\Desktop\BSCSAPPDEV\2ndyr\1st_sem\oop\projectmaking\MSSQL16.SQLEXPRESS01\MSSQL\DATA\POS_db.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'POS_db_log', FILENAME = N'C:\Users\Ganotice\OneDrive\Desktop\BSCSAPPDEV\2ndyr\1st_sem\oop\projectmaking\MSSQL16.SQLEXPRESS01\MSSQL\DATA\POS_db_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Pos_db] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Pos_db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Pos_db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Pos_db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Pos_db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Pos_db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Pos_db] SET ARITHABORT OFF 
GO
ALTER DATABASE [Pos_db] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Pos_db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Pos_db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Pos_db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Pos_db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Pos_db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Pos_db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Pos_db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Pos_db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Pos_db] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Pos_db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Pos_db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Pos_db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Pos_db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Pos_db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Pos_db] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Pos_db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Pos_db] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Pos_db] SET  MULTI_USER 
GO
ALTER DATABASE [Pos_db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Pos_db] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Pos_db] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Pos_db] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Pos_db] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Pos_db] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Pos_db] SET QUERY_STORE = ON
GO
ALTER DATABASE [Pos_db] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Pos_db]
GO
/****** Object:  Table [dbo].[tbl_products]    Script Date: 10/12/2024 10:30:26 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_products](
	[product_id] [int] IDENTITY(1,1) NOT NULL,
	[product_name] [varchar](255) NOT NULL,
	[category] [varchar](100) NULL,
	[brand] [varchar](100) NULL,
	[model_number] [varchar](50) NULL,
	[sku] [varchar](50) NULL,
	[description] [text] NULL,
	[stock_quantity] [int] NULL,
	[unit_cost] [decimal](10, 2) NULL,
	[selling_price] [decimal](10, 2) NULL,
	[min_stock_level] [int] NULL,
	[is_archived] [bit] NULL,
	[image_path] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_PurchaseOrderDetails]    Script Date: 10/12/2024 10:30:26 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_PurchaseOrderDetails](
	[OrderDetailID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_PurchaseOrders]    Script Date: 10/12/2024 10:30:26 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_PurchaseOrders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[SupplierID] [int] NOT NULL,
	[OrderDate] [date] NOT NULL,
	[Status] [nvarchar](50) NULL,
	[TotalAmount] [decimal](10, 2) NULL,
	[ShippingFee] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_RefundedItems]    Script Date: 10/12/2024 10:30:26 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_RefundedItems](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[sale_item_id] [int] NOT NULL,
	[refund_quantity] [int] NOT NULL,
	[refund_datetime] [datetime] NOT NULL,
	[refund_amount] [decimal](10, 2) NOT NULL,
	[refund_reason] [nvarchar](255) NOT NULL,
	[product_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Sale_Items]    Script Date: 10/12/2024 10:30:26 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Sale_Items](
	[sale_item_id] [int] IDENTITY(1,1) NOT NULL,
	[sale_id] [int] NOT NULL,
	[product_id] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[item_total_price] [decimal](10, 2) NOT NULL,
	[is_refunded] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[sale_item_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_sales]    Script Date: 10/12/2024 10:30:26 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_sales](
	[sale_id] [int] IDENTITY(1,1) NOT NULL,
	[date_time] [datetime] NULL,
	[cashier_id] [int] NULL,
	[total_amount] [decimal](10, 2) NOT NULL,
	[payment_method] [varchar](50) NULL,
	[customer_payment] [decimal](10, 2) NOT NULL,
	[change_given] [decimal](10, 2) NOT NULL,
	[is_void] [bit] NOT NULL,
	[VoidReason] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[sale_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Suppliers]    Script Date: 10/12/2024 10:30:26 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Suppliers](
	[SupplierID] [int] IDENTITY(1,1) NOT NULL,
	[SupplierName] [nvarchar](255) NOT NULL,
	[ContactName] [nvarchar](255) NULL,
	[Phone] [nvarchar](50) NULL,
	[Email] [nvarchar](255) NULL,
	[Address] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_users]    Script Date: 10/12/2024 10:30:26 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password] [varchar](255) NOT NULL,
	[role] [varchar](7) NULL,
	[created_at] [datetime] NULL,
	[salt] [nvarchar](256) NULL,
	[is_logged_in] [bit] NOT NULL,
	[last_login_time] [datetime] NULL,
	[last_logout_time] [datetime] NULL,
	[fullname] [varchar](255) NULL,
	[user_image] [varchar](255) NULL,
	[status] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tbl_products] ADD  CONSTRAINT [DF_products_sku]  DEFAULT ('N/A') FOR [sku]
GO
ALTER TABLE [dbo].[tbl_products] ADD  DEFAULT ((0)) FOR [stock_quantity]
GO
ALTER TABLE [dbo].[tbl_products] ADD  DEFAULT ((0)) FOR [min_stock_level]
GO
ALTER TABLE [dbo].[tbl_products] ADD  DEFAULT ((0)) FOR [is_archived]
GO
ALTER TABLE [dbo].[tbl_PurchaseOrders] ADD  DEFAULT ('Pending') FOR [Status]
GO
ALTER TABLE [dbo].[tbl_PurchaseOrders] ADD  DEFAULT ((0.00)) FOR [TotalAmount]
GO
ALTER TABLE [dbo].[tbl_PurchaseOrders] ADD  DEFAULT ((0.00)) FOR [ShippingFee]
GO
ALTER TABLE [dbo].[tbl_Sale_Items] ADD  DEFAULT ((1)) FOR [quantity]
GO
ALTER TABLE [dbo].[tbl_Sale_Items] ADD  DEFAULT ((0)) FOR [is_refunded]
GO
ALTER TABLE [dbo].[tbl_sales] ADD  DEFAULT (getdate()) FOR [date_time]
GO
ALTER TABLE [dbo].[tbl_sales] ADD  DEFAULT ((0)) FOR [is_void]
GO
ALTER TABLE [dbo].[tbl_users] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[tbl_users] ADD  DEFAULT ((0)) FOR [is_logged_in]
GO
ALTER TABLE [dbo].[tbl_users] ADD  DEFAULT ('Active') FOR [status]
GO
ALTER TABLE [dbo].[tbl_PurchaseOrderDetails]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [dbo].[tbl_PurchaseOrders] ([OrderID])
GO
ALTER TABLE [dbo].[tbl_PurchaseOrderDetails]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [dbo].[tbl_PurchaseOrders] ([OrderID])
GO
ALTER TABLE [dbo].[tbl_PurchaseOrderDetails]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[tbl_products] ([product_id])
GO
ALTER TABLE [dbo].[tbl_PurchaseOrderDetails]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[tbl_products] ([product_id])
GO
ALTER TABLE [dbo].[tbl_PurchaseOrders]  WITH CHECK ADD FOREIGN KEY([SupplierID])
REFERENCES [dbo].[tbl_Suppliers] ([SupplierID])
GO
ALTER TABLE [dbo].[tbl_PurchaseOrders]  WITH CHECK ADD FOREIGN KEY([SupplierID])
REFERENCES [dbo].[tbl_Suppliers] ([SupplierID])
GO
ALTER TABLE [dbo].[tbl_RefundedItems]  WITH CHECK ADD FOREIGN KEY([sale_item_id])
REFERENCES [dbo].[tbl_Sale_Items] ([sale_item_id])
GO
ALTER TABLE [dbo].[tbl_Sale_Items]  WITH CHECK ADD  CONSTRAINT [FK_SaleItems_Products] FOREIGN KEY([product_id])
REFERENCES [dbo].[tbl_products] ([product_id])
GO
ALTER TABLE [dbo].[tbl_Sale_Items] CHECK CONSTRAINT [FK_SaleItems_Products]
GO
ALTER TABLE [dbo].[tbl_Sale_Items]  WITH CHECK ADD  CONSTRAINT [FK_SaleItems_Sales] FOREIGN KEY([sale_id])
REFERENCES [dbo].[tbl_sales] ([sale_id])
GO
ALTER TABLE [dbo].[tbl_Sale_Items] CHECK CONSTRAINT [FK_SaleItems_Sales]
GO
ALTER TABLE [dbo].[tbl_sales]  WITH CHECK ADD  CONSTRAINT [FK__sales__cashier_i__45F365D3] FOREIGN KEY([cashier_id])
REFERENCES [dbo].[tbl_users] ([user_id])
GO
ALTER TABLE [dbo].[tbl_sales] CHECK CONSTRAINT [FK__sales__cashier_i__45F365D3]
GO
ALTER TABLE [dbo].[tbl_sales]  WITH CHECK ADD FOREIGN KEY([cashier_id])
REFERENCES [dbo].[tbl_users] ([user_id])
GO
ALTER TABLE [dbo].[tbl_sales]  WITH CHECK ADD FOREIGN KEY([cashier_id])
REFERENCES [dbo].[tbl_users] ([user_id])
GO
ALTER TABLE [dbo].[tbl_sales]  WITH CHECK ADD  CONSTRAINT [FK__sales__cashier_i__4CA06362] FOREIGN KEY([cashier_id])
REFERENCES [dbo].[tbl_users] ([user_id])
GO
ALTER TABLE [dbo].[tbl_sales] CHECK CONSTRAINT [FK__sales__cashier_i__4CA06362]
GO
ALTER TABLE [dbo].[tbl_sales]  WITH CHECK ADD  CONSTRAINT [CK__sales__payment_m__46E78A0C] CHECK  (([payment_method]='CARD' OR [payment_method]='CASH'))
GO
ALTER TABLE [dbo].[tbl_sales] CHECK CONSTRAINT [CK__sales__payment_m__46E78A0C]
GO
ALTER TABLE [dbo].[tbl_sales]  WITH CHECK ADD CHECK  (([payment_method]='CARD' OR [payment_method]='CASH'))
GO
ALTER TABLE [dbo].[tbl_sales]  WITH CHECK ADD CHECK  (([payment_method]='CARD' OR [payment_method]='CASH'))
GO
ALTER TABLE [dbo].[tbl_sales]  WITH CHECK ADD  CONSTRAINT [CK__sales__payment_m__4D94879B] CHECK  (([payment_method]='CARD' OR [payment_method]='CASH'))
GO
ALTER TABLE [dbo].[tbl_sales] CHECK CONSTRAINT [CK__sales__payment_m__4D94879B]
GO
ALTER TABLE [dbo].[tbl_users]  WITH CHECK ADD  CONSTRAINT [CK__users__role__47DBAE45] CHECK  (([role]='CASHIER' OR [role]='ADMIN'))
GO
ALTER TABLE [dbo].[tbl_users] CHECK CONSTRAINT [CK__users__role__47DBAE45]
GO
ALTER TABLE [dbo].[tbl_users]  WITH CHECK ADD CHECK  (([role]='CASHIER' OR [role]='ADMIN'))
GO
ALTER TABLE [dbo].[tbl_users]  WITH CHECK ADD CHECK  (([role]='CASHIER' OR [role]='ADMIN'))
GO
ALTER TABLE [dbo].[tbl_users]  WITH CHECK ADD  CONSTRAINT [CK__users__role__4E88ABD4] CHECK  (([role]='CASHIER' OR [role]='ADMIN'))
GO
ALTER TABLE [dbo].[tbl_users] CHECK CONSTRAINT [CK__users__role__4E88ABD4]
GO
USE [master]
GO
ALTER DATABASE [Pos_db] SET  READ_WRITE 
GO
