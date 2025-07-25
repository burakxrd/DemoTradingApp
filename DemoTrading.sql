USE [master]
GO
/****** Object:  Database [DemoTrading]    Script Date: 13.06.2025 16:41:37 ******/
CREATE DATABASE [DemoTrading]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DemoTrading', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\DemoTrading.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DemoTrading_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\DemoTrading_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DemoTrading] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DemoTrading].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DemoTrading] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DemoTrading] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DemoTrading] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DemoTrading] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DemoTrading] SET ARITHABORT OFF 
GO
ALTER DATABASE [DemoTrading] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DemoTrading] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DemoTrading] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DemoTrading] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DemoTrading] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DemoTrading] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DemoTrading] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DemoTrading] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DemoTrading] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DemoTrading] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DemoTrading] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DemoTrading] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DemoTrading] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DemoTrading] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DemoTrading] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DemoTrading] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DemoTrading] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DemoTrading] SET RECOVERY FULL 
GO
ALTER DATABASE [DemoTrading] SET  MULTI_USER 
GO
ALTER DATABASE [DemoTrading] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DemoTrading] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DemoTrading] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DemoTrading] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DemoTrading] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DemoTrading] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DemoTrading', N'ON'
GO
ALTER DATABASE [DemoTrading] SET QUERY_STORE = ON
GO
ALTER DATABASE [DemoTrading] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DemoTrading]
GO
/****** Object:  Table [dbo].[Assets]    Script Date: 13.06.2025 16:41:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assets](
	[asset_id] [int] IDENTITY(1,1) NOT NULL,
	[wallet_id] [int] NOT NULL,
	[amount] [decimal](18, 6) NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[asset_type_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[asset_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssetTypes]    Script Date: 13.06.2025 16:41:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssetTypes](
	[asset_type_id] [int] IDENTITY(1,1) NOT NULL,
	[asset_name] [varchar](50) NOT NULL,
	[description] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[asset_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[asset_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuditLogs]    Script Date: 13.06.2025 16:41:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditLogs](
	[audit_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[action_type] [varchar](50) NOT NULL,
	[action_details] [nvarchar](500) NULL,
	[action_date] [datetime] NULL,
	[ip_address] [varchar](45) NULL,
	[related_table] [varchar](50) NULL,
	[related_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[audit_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prices]    Script Date: 13.06.2025 16:41:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prices](
	[price_id] [int] IDENTITY(1,1) NOT NULL,
	[price] [decimal](18, 6) NOT NULL,
	[price_date] [datetime] NULL,
	[asset_type_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[price_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchasedItems]    Script Date: 13.06.2025 16:41:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchasedItems](
	[purchase_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[item_name] [nvarchar](100) NOT NULL,
	[quantity] [int] NOT NULL,
	[purchase_price] [decimal](18, 6) NOT NULL,
	[currency_asset_id] [int] NOT NULL,
	[purchase_date] [datetime] NOT NULL,
	[image_path] [nvarchar](255) NULL,
 CONSTRAINT [PK_PurchasedItems] PRIMARY KEY CLUSTERED 
(
	[purchase_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trades]    Script Date: 13.06.2025 16:41:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trades](
	[trade_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[trade_type] [char](4) NOT NULL,
	[amount] [decimal](18, 6) NOT NULL,
	[price] [decimal](18, 6) NOT NULL,
	[trade_date] [datetime] NULL,
	[asset_type_id] [int] NOT NULL,
	[total_cost] [decimal](18, 6) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[trade_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 13.06.2025 16:41:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[password_hash] [varchar](255) NOT NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wallets]    Script Date: 13.06.2025 16:41:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wallets](
	[wallet_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[wallet_name] [varchar](50) NULL,
	[balance] [decimal](18, 6) NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[wallet_type_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[wallet_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WalletTypes]    Script Date: 13.06.2025 16:41:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WalletTypes](
	[wallet_type_id] [int] IDENTITY(1,1) NOT NULL,
	[type_name] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[wallet_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[type_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [idx_asset_type]    Script Date: 13.06.2025 16:41:37 ******/
CREATE NONCLUSTERED INDEX [idx_asset_type] ON [dbo].[Prices]
(
	[asset_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Assets] ADD  DEFAULT ((0.0)) FOR [amount]
GO
ALTER TABLE [dbo].[Assets] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[Assets] ADD  DEFAULT (getdate()) FOR [updated_at]
GO
ALTER TABLE [dbo].[AuditLogs] ADD  DEFAULT (getdate()) FOR [action_date]
GO
ALTER TABLE [dbo].[Prices] ADD  DEFAULT (getdate()) FOR [price_date]
GO
ALTER TABLE [dbo].[PurchasedItems] ADD  CONSTRAINT [DF_PurchasedItems_purchase_date]  DEFAULT (getdate()) FOR [purchase_date]
GO
ALTER TABLE [dbo].[Trades] ADD  DEFAULT (getdate()) FOR [trade_date]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [updated_at]
GO
ALTER TABLE [dbo].[Wallets] ADD  DEFAULT ('Main') FOR [wallet_name]
GO
ALTER TABLE [dbo].[Wallets] ADD  DEFAULT ((0.0)) FOR [balance]
GO
ALTER TABLE [dbo].[Wallets] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[Wallets] ADD  DEFAULT (getdate()) FOR [updated_at]
GO
ALTER TABLE [dbo].[Assets]  WITH CHECK ADD FOREIGN KEY([wallet_id])
REFERENCES [dbo].[Wallets] ([wallet_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Assets]  WITH CHECK ADD  CONSTRAINT [FK_Assets_AssetTypes] FOREIGN KEY([asset_type_id])
REFERENCES [dbo].[AssetTypes] ([asset_type_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Assets] CHECK CONSTRAINT [FK_Assets_AssetTypes]
GO
ALTER TABLE [dbo].[AuditLogs]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([user_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Prices]  WITH CHECK ADD  CONSTRAINT [FK_Prices_AssetTypes] FOREIGN KEY([asset_type_id])
REFERENCES [dbo].[AssetTypes] ([asset_type_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Prices] CHECK CONSTRAINT [FK_Prices_AssetTypes]
GO
ALTER TABLE [dbo].[PurchasedItems]  WITH CHECK ADD  CONSTRAINT [FK_PurchasedItems_AssetTypes] FOREIGN KEY([currency_asset_id])
REFERENCES [dbo].[AssetTypes] ([asset_type_id])
GO
ALTER TABLE [dbo].[PurchasedItems] CHECK CONSTRAINT [FK_PurchasedItems_AssetTypes]
GO
ALTER TABLE [dbo].[PurchasedItems]  WITH CHECK ADD  CONSTRAINT [FK_PurchasedItems_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([user_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PurchasedItems] CHECK CONSTRAINT [FK_PurchasedItems_Users]
GO
ALTER TABLE [dbo].[Trades]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([user_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Trades]  WITH CHECK ADD  CONSTRAINT [FK_Trades_AssetTypes] FOREIGN KEY([asset_type_id])
REFERENCES [dbo].[AssetTypes] ([asset_type_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Trades] CHECK CONSTRAINT [FK_Trades_AssetTypes]
GO
ALTER TABLE [dbo].[Wallets]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([user_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Wallets]  WITH CHECK ADD  CONSTRAINT [FK_Wallets_WalletTypes] FOREIGN KEY([wallet_type_id])
REFERENCES [dbo].[WalletTypes] ([wallet_type_id])
GO
ALTER TABLE [dbo].[Wallets] CHECK CONSTRAINT [FK_Wallets_WalletTypes]
GO
ALTER TABLE [dbo].[Trades]  WITH CHECK ADD CHECK  (([trade_type]='SELL' OR [trade_type]='BUY'))
GO
USE [master]
GO
ALTER DATABASE [DemoTrading] SET  READ_WRITE 
GO

-- BAŞLANGIÇ VERİLERİNİ EKLEYEN KOD

USE [DemoTrading]
GO

-- Varlık Tiplerini Ekle (Nakit)
INSERT INTO [dbo].[AssetTypes] ([asset_name], [description]) VALUES ('USD', 'Amerikan Doları');
INSERT INTO [dbo].[AssetTypes] ([asset_name], [description]) VALUES ('EUR', 'Euro');
INSERT INTO [dbo].[AssetTypes] ([asset_name], [description]) VALUES ('TRY', 'Türk Lirası');
INSERT INTO [dbo].[AssetTypes] ([asset_name], [description]) VALUES ('bitcoin', 'Bitcoin');
INSERT INTO [dbo].[AssetTypes] ([asset_name], [description]) VALUES ('ethereum', 'Ethereum');
INSERT INTO [dbo].[AssetTypes] ([asset_name], [description]) VALUES ('solana', 'Solana');
INSERT INTO [dbo].[AssetTypes] ([asset_name], [description]) VALUES ('tether', 'Tether');
GO

INSERT INTO [dbo].[WalletTypes] ([type_name]) VALUES ('Kripto Para');
INSERT INTO [dbo].[WalletTypes] ([type_name]) VALUES ('Hisse Senedi');
INSERT INTO [dbo].[WalletTypes] ([type_name]) VALUES ('Döviz');
INSERT INTO [dbo].[WalletTypes] ([type_name]) VALUES ('Değerli Maden');
GO

