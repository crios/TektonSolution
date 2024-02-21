/****** Object:  Table [dbo].[Product]    Script Date: 2/21/2024 5:16:04 AM ******/
DROP TABLE IF EXISTS [dbo].[Product]
GO
/****** Object:  Database [BDStoreTekton]    Script Date: 2/21/2024 5:16:04 AM ******/
DROP DATABASE IF EXISTS [BDStoreTekton]
GO
/****** Object:  Database [BDStoreTekton]    Script Date: 2/21/2024 5:16:04 AM ******/
CREATE DATABASE [BDStoreTekton]  (EDITION = 'GeneralPurpose', SERVICE_OBJECTIVE = 'GP_S_Gen5_1', MAXSIZE = 32 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS, LEDGER = OFF;
GO
ALTER DATABASE [BDStoreTekton] SET COMPATIBILITY_LEVEL = 150
GO
ALTER DATABASE [BDStoreTekton] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BDStoreTekton] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BDStoreTekton] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BDStoreTekton] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BDStoreTekton] SET ARITHABORT OFF 
GO
ALTER DATABASE [BDStoreTekton] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BDStoreTekton] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BDStoreTekton] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BDStoreTekton] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BDStoreTekton] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BDStoreTekton] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BDStoreTekton] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BDStoreTekton] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BDStoreTekton] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [BDStoreTekton] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BDStoreTekton] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [BDStoreTekton] SET  MULTI_USER 
GO
ALTER DATABASE [BDStoreTekton] SET ENCRYPTION ON
GO
ALTER DATABASE [BDStoreTekton] SET QUERY_STORE = ON
GO
ALTER DATABASE [BDStoreTekton] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/*** The scripts of database scoped configurations in Azure should be executed inside the target database connection. ***/
GO
-- ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 8;
GO
/****** Object:  Table [dbo].[Product]    Script Date: 2/21/2024 5:16:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[productId] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](200) NOT NULL,
	[status] [int] NOT NULL,
	[stock] [bit] NOT NULL,
	[description] [varchar](max) NULL,
	[price] [numeric](18, 0) NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[productId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER DATABASE [BDStoreTekton] SET  READ_WRITE 
GO
