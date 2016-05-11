
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 05/11/2016 16:31:18
-- Generated from EDMX file: F:\task\template\MyTemplate\MyTemplate\My.Template.Model\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Website2DB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserUser_Role]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User_Role] DROP CONSTRAINT [FK_UserUser_Role];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleUser_Role]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User_Role] DROP CONSTRAINT [FK_RoleUser_Role];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUser_Action]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User_Action] DROP CONSTRAINT [FK_UserUser_Action];
GO
IF OBJECT_ID(N'[dbo].[FK_ActionUser_Action]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User_Action] DROP CONSTRAINT [FK_ActionUser_Action];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleRole_Action]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Role_Action] DROP CONSTRAINT [FK_RoleRole_Action];
GO
IF OBJECT_ID(N'[dbo].[FK_ActionRole_Action]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Role_Action] DROP CONSTRAINT [FK_ActionRole_Action];
GO
IF OBJECT_ID(N'[dbo].[FK_ActionActionType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Action] DROP CONSTRAINT [FK_ActionActionType];
GO
IF OBJECT_ID(N'[dbo].[FK_NewsNewsType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[News] DROP CONSTRAINT [FK_NewsNewsType];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUserInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_UserUserInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_BannerBannerType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Banner] DROP CONSTRAINT [FK_BannerBannerType];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[dbo].[UserInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfo];
GO
IF OBJECT_ID(N'[dbo].[Role]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Role];
GO
IF OBJECT_ID(N'[dbo].[User_Role]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User_Role];
GO
IF OBJECT_ID(N'[dbo].[Action]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Action];
GO
IF OBJECT_ID(N'[dbo].[User_Action]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User_Action];
GO
IF OBJECT_ID(N'[dbo].[Role_Action]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Role_Action];
GO
IF OBJECT_ID(N'[dbo].[ActionType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActionType];
GO
IF OBJECT_ID(N'[dbo].[Banner]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Banner];
GO
IF OBJECT_ID(N'[dbo].[SinglePage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SinglePage];
GO
IF OBJECT_ID(N'[dbo].[News]', 'U') IS NOT NULL
    DROP TABLE [dbo].[News];
GO
IF OBJECT_ID(N'[dbo].[SiteInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SiteInfo];
GO
IF OBJECT_ID(N'[dbo].[NewsType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NewsType];
GO
IF OBJECT_ID(N'[dbo].[Videos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Videos];
GO
IF OBJECT_ID(N'[dbo].[QAInfos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QAInfos];
GO
IF OBJECT_ID(N'[dbo].[BannerType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BannerType];
GO
IF OBJECT_ID(N'[dbo].[Notice]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notice];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Account] nvarchar(32)  NOT NULL,
    [Email] nvarchar(64)  NULL,
    [Mobile] nvarchar(16)  NULL,
    [WxOpenid] nvarchar(64)  NULL,
    [SinaWBOpenid] nvarchar(64)  NULL,
    [QQNum] nvarchar(64)  NULL,
    [Pwd] nvarchar(64)  NOT NULL,
    [IsDelete] bit  NOT NULL,
    [IsFrozen] bit  NOT NULL,
    [CreateTime] datetime  NOT NULL,
    [Balance] int  NOT NULL,
    [FrozenBlance] int  NOT NULL,
    [AlipayAccount] nvarchar(max)  NULL,
    [BankNumber] nvarchar(max)  NULL,
    [BankName] nvarchar(256)  NULL,
    [UserInfo_ID] int  NOT NULL
);
GO

-- Creating table 'UserInfo'
CREATE TABLE [dbo].[UserInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FullName] nvarchar(32)  NULL,
    [NickName] nvarchar(32)  NULL,
    [HeadPic] nvarchar(512)  NULL,
    [WXHeadPic] nvarchar(512)  NULL,
    [QQHeadPic] nvarchar(512)  NULL,
    [SinaWBHeadPic] nvarchar(512)  NULL,
    [Age] int  NOT NULL,
    [City] nvarchar(64)  NULL,
    [Address] nvarchar(256)  NULL,
    [QQNum] nvarchar(16)  NULL,
    [Email] nvarchar(64)  NULL,
    [Sex] bit  NULL,
    [UserID] int  NOT NULL
);
GO

-- Creating table 'Role'
CREATE TABLE [dbo].[Role] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [RCNName] nvarchar(32)  NOT NULL,
    [REnName] nvarchar(32)  NOT NULL,
    [RRemark] nvarchar(256)  NULL,
    [IsDelete] bit  NOT NULL,
    [IsFrozen] bit  NOT NULL,
    [CreateTime] datetime  NOT NULL
);
GO

-- Creating table 'User_Role'
CREATE TABLE [dbo].[User_Role] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] int  NOT NULL,
    [RoleID] int  NOT NULL
);
GO

-- Creating table 'Action'
CREATE TABLE [dbo].[Action] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [AName] nvarchar(32)  NOT NULL,
    [AUrl] nvarchar(512)  NOT NULL,
    [AIcon] nvarchar(512)  NULL,
    [ASequence] int  NOT NULL,
    [IsDelete] bit  NOT NULL,
    [IsFrozen] bit  NOT NULL,
    [ParentID] int  NOT NULL,
    [ActionTypeID] int  NOT NULL
);
GO

-- Creating table 'User_Action'
CREATE TABLE [dbo].[User_Action] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] int  NOT NULL,
    [ActionID] int  NOT NULL
);
GO

-- Creating table 'Role_Action'
CREATE TABLE [dbo].[Role_Action] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [RoleID] int  NOT NULL,
    [ActionID] int  NOT NULL
);
GO

-- Creating table 'ActionType'
CREATE TABLE [dbo].[ActionType] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TName] nvarchar(64)  NOT NULL,
    [Remark] nvarchar(256)  NOT NULL,
    [IsDelete] bit  NOT NULL,
    [CreateTime] datetime  NOT NULL
);
GO

-- Creating table 'Banner'
CREATE TABLE [dbo].[Banner] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(64)  NOT NULL,
    [ImgPic] nvarchar(256)  NOT NULL,
    [LinkUrl] nvarchar(256)  NULL,
    [IsView] bit  NOT NULL,
    [IsDelete] bit  NOT NULL,
    [AddTime] datetime  NOT NULL,
    [Sequence] int  NOT NULL,
    [IsUse4Phone] bit  NOT NULL,
    [BannerTypeID] int  NOT NULL
);
GO

-- Creating table 'SinglePage'
CREATE TABLE [dbo].[SinglePage] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [PageName] nvarchar(64)  NOT NULL,
    [PageContent] nvarchar(max)  NOT NULL,
    [SeoKeyWord] nvarchar(128)  NULL,
    [SeoDescription] nvarchar(512)  NULL,
    [AddTime] datetime  NOT NULL,
    [IsDelete] bit  NOT NULL,
    [Sequence] int  NOT NULL,
    [IsView] bit  NOT NULL
);
GO

-- Creating table 'News'
CREATE TABLE [dbo].[News] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(256)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [IsView] bit  NOT NULL,
    [Sequence] int  NOT NULL,
    [ClickNum] int  NOT NULL,
    [AddTime] datetime  NOT NULL,
    [IsDelete] bit  NOT NULL,
    [SeoKeywords] nvarchar(128)  NULL,
    [SeoDescription] nvarchar(512)  NULL,
    [NewsTypeID] int  NOT NULL
);
GO

-- Creating table 'SiteInfo'
CREATE TABLE [dbo].[SiteInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SiteName] nvarchar(64)  NULL,
    [SiteUrl] nvarchar(256)  NULL,
    [CompanyName] nvarchar(128)  NULL,
    [Address] nvarchar(256)  NULL,
    [ZipCode] nvarchar(16)  NULL,
    [Telephone] nvarchar(32)  NULL,
    [Fax] nvarchar(32)  NULL,
    [Email] nvarchar(64)  NULL,
    [SeoKeyWords] nvarchar(256)  NULL,
    [SeoDescription] nvarchar(256)  NULL,
    [IcpNumber] nvarchar(64)  NULL,
    [IsViewInfo] bit  NOT NULL,
    [Img0] nvarchar(512)  NOT NULL,
    [Img1] nvarchar(512)  NOT NULL,
    [Img2] nvarchar(512)  NOT NULL,
    [Img3] nvarchar(512)  NOT NULL,
    [Img4] nvarchar(512)  NOT NULL,
    [Img5] nvarchar(512)  NOT NULL,
    [Img6] nvarchar(512)  NOT NULL,
    [Img7] nvarchar(512)  NOT NULL,
    [Img8] nvarchar(512)  NOT NULL
);
GO

-- Creating table 'NewsType'
CREATE TABLE [dbo].[NewsType] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TypeName] nvarchar(64)  NOT NULL,
    [ParentID] int  NOT NULL,
    [TypePath] nvarchar(256)  NOT NULL,
    [IsDelete] bit  NOT NULL,
    [AddTime] datetime  NOT NULL
);
GO

-- Creating table 'Videos'
CREATE TABLE [dbo].[Videos] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [VName] nvarchar(64)  NOT NULL,
    [ThemPic] nvarchar(512)  NOT NULL,
    [MPEG4Url] nvarchar(512)  NULL,
    [OggUrl] nvarchar(512)  NULL,
    [WebMUrl] nvarchar(512)  NULL,
    [IsView] bit  NOT NULL,
    [IsDelete] bit  NOT NULL,
    [Sequence] int  NOT NULL,
    [AddTime] datetime  NOT NULL
);
GO

-- Creating table 'QAInfos'
CREATE TABLE [dbo].[QAInfos] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Question] nvarchar(256)  NOT NULL,
    [Answer] nvarchar(max)  NOT NULL,
    [Sequence] int  NOT NULL,
    [IsView] bit  NOT NULL,
    [IsDelete] bit  NOT NULL
);
GO

-- Creating table 'BannerType'
CREATE TABLE [dbo].[BannerType] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TName] nvarchar(32)  NOT NULL,
    [Remark] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'Notice'
CREATE TABLE [dbo].[Notice] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [LinkUrl] nvarchar(max)  NOT NULL,
    [NDetails] nvarchar(max)  NOT NULL,
    [AddTime] datetime  NOT NULL,
    [ExpireTime] datetime  NOT NULL,
    [IsDelete] bit  NOT NULL,
    [Sequence] int  NOT NULL
);
GO

-- Creating table 'User_ShoppingCart'
CREATE TABLE [dbo].[User_ShoppingCart] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] int  NOT NULL,
    [GoodsInfoID] int  NOT NULL,
    [GoodsPropertyInfos] nvarchar(512)  NOT NULL,
    [Count] nvarchar(max)  NOT NULL,
    [IsDelete] bit  NOT NULL
);
GO

-- Creating table 'GoodsInfo'
CREATE TABLE [dbo].[GoodsInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SkuNumber] nvarchar(max)  NOT NULL,
    [GoodsTypeID] int  NOT NULL,
    [IsDelete] bit  NOT NULL,
    [IsView] bit  NOT NULL,
    [IsView4HomePage] bit  NOT NULL,
    [GShortName] nvarchar(64)  NOT NULL,
    [GFullName] nvarchar(256)  NOT NULL,
    [Price] int  NOT NULL,
    [ThumbPic] nvarchar(1024)  NOT NULL,
    [DetailPics] nvarchar(max)  NOT NULL,
    [Sequence] int  NOT NULL,
    [DeliveryDate] int  NOT NULL
);
GO

-- Creating table 'GoodsProperty'
CREATE TABLE [dbo].[GoodsProperty] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [GoodsInfoID] int  NOT NULL,
    [IsDelete] bit  NOT NULL,
    [GPName] nvarchar(128)  NOT NULL,
    [Sequence] int  NOT NULL
);
GO

-- Creating table 'GoodsPropertyDetail'
CREATE TABLE [dbo].[GoodsPropertyDetail] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [GoodsPropertyID] int  NOT NULL,
    [GPDName] nvarchar(128)  NOT NULL,
    [IsPic] bit  NOT NULL,
    [GPDPic] nvarchar(1024)  NULL,
    [IsDelete] bit  NOT NULL,
    [Sequence] int  NOT NULL,
    [IsOutlink] bit  NOT NULL,
    [OutLinkUrl] nvarchar(1024)  NOT NULL
);
GO

-- Creating table 'GoodsType'
CREATE TABLE [dbo].[GoodsType] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ParentID] nvarchar(max)  NOT NULL,
    [IsDelete] bit  NOT NULL,
    [GTName] nvarchar(64)  NOT NULL,
    [Sequence] int  NOT NULL,
    [IsView] bit  NOT NULL,
    [Remark] nvarchar(1024)  NULL
);
GO

-- Creating table 'Order'
CREATE TABLE [dbo].[Order] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [OrderNum] nvarchar(max)  NOT NULL,
    [DueMoney] int  NOT NULL,
    [ActualPay] int  NOT NULL,
    [BalanceChange] nvarchar(max)  NOT NULL,
    [CouponValue] int  NOT NULL,
    [OrderStatusID] int  NOT NULL,
    [RecAddressID] int  NOT NULL,
    [IsDelete] bit  NOT NULL
);
GO

-- Creating table 'OrderStatus'
CREATE TABLE [dbo].[OrderStatus] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [OSName] nvarchar(max)  NOT NULL,
    [OSDetail] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'OrderGoods'
CREATE TABLE [dbo].[OrderGoods] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [OrderID] int  NOT NULL,
    [GoodsInfoID] nvarchar(max)  NOT NULL,
    [GoodsPropertyInfos] nvarchar(512)  NOT NULL,
    [Count] int  NOT NULL,
    [CurPrice] int  NOT NULL
);
GO

-- Creating table 'RecAddress'
CREATE TABLE [dbo].[RecAddress] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [IsDelete] bit  NOT NULL,
    [IsDefault] bit  NOT NULL,
    [Title] nvarchar(128)  NOT NULL,
    [RecvicerName] nvarchar(32)  NOT NULL,
    [Mobile] nvarchar(32)  NOT NULL,
    [Province] nvarchar(32)  NOT NULL,
    [City] nvarchar(32)  NOT NULL,
    [Area] nvarchar(32)  NOT NULL,
    [Detail] nvarchar(512)  NOT NULL,
    [ZipCode] nvarchar(32)  NULL,
    [UserID] int  NOT NULL
);
GO

-- Creating table 'CityInfo'
CREATE TABLE [dbo].[CityInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [CName] nvarchar(32)  NOT NULL,
    [IsDelete] bit  NOT NULL,
    [IsView] bit  NOT NULL,
    [Sequence] int  NOT NULL
);
GO

-- Creating table 'Community'
CREATE TABLE [dbo].[Community] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [AreaInfoID] int  NOT NULL,
    [CName] nvarchar(32)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [ThumbPic] nvarchar(1024)  NOT NULL,
    [ThumbCirclePic] nvarchar(1024)  NOT NULL
);
GO

-- Creating table 'AreaInfo'
CREATE TABLE [dbo].[AreaInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [AName] nvarchar(32)  NOT NULL,
    [IsDelete] bit  NOT NULL,
    [IsView] bit  NOT NULL,
    [Sequence] int  NOT NULL,
    [CityInfoID] int  NOT NULL
);
GO

-- Creating table 'SampleHouse'
CREATE TABLE [dbo].[SampleHouse] (
    [ID] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'HouseArea'
CREATE TABLE [dbo].[HouseArea] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [AName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SpHouse_HseArea'
CREATE TABLE [dbo].[SpHouse_HseArea] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SampleHouseID] int  NOT NULL,
    [HouseAreaID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'UserInfo'
ALTER TABLE [dbo].[UserInfo]
ADD CONSTRAINT [PK_UserInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Role'
ALTER TABLE [dbo].[Role]
ADD CONSTRAINT [PK_Role]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'User_Role'
ALTER TABLE [dbo].[User_Role]
ADD CONSTRAINT [PK_User_Role]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Action'
ALTER TABLE [dbo].[Action]
ADD CONSTRAINT [PK_Action]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'User_Action'
ALTER TABLE [dbo].[User_Action]
ADD CONSTRAINT [PK_User_Action]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Role_Action'
ALTER TABLE [dbo].[Role_Action]
ADD CONSTRAINT [PK_Role_Action]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ActionType'
ALTER TABLE [dbo].[ActionType]
ADD CONSTRAINT [PK_ActionType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Banner'
ALTER TABLE [dbo].[Banner]
ADD CONSTRAINT [PK_Banner]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SinglePage'
ALTER TABLE [dbo].[SinglePage]
ADD CONSTRAINT [PK_SinglePage]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'News'
ALTER TABLE [dbo].[News]
ADD CONSTRAINT [PK_News]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SiteInfo'
ALTER TABLE [dbo].[SiteInfo]
ADD CONSTRAINT [PK_SiteInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'NewsType'
ALTER TABLE [dbo].[NewsType]
ADD CONSTRAINT [PK_NewsType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Videos'
ALTER TABLE [dbo].[Videos]
ADD CONSTRAINT [PK_Videos]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'QAInfos'
ALTER TABLE [dbo].[QAInfos]
ADD CONSTRAINT [PK_QAInfos]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'BannerType'
ALTER TABLE [dbo].[BannerType]
ADD CONSTRAINT [PK_BannerType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Notice'
ALTER TABLE [dbo].[Notice]
ADD CONSTRAINT [PK_Notice]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'User_ShoppingCart'
ALTER TABLE [dbo].[User_ShoppingCart]
ADD CONSTRAINT [PK_User_ShoppingCart]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'GoodsInfo'
ALTER TABLE [dbo].[GoodsInfo]
ADD CONSTRAINT [PK_GoodsInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'GoodsProperty'
ALTER TABLE [dbo].[GoodsProperty]
ADD CONSTRAINT [PK_GoodsProperty]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'GoodsPropertyDetail'
ALTER TABLE [dbo].[GoodsPropertyDetail]
ADD CONSTRAINT [PK_GoodsPropertyDetail]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'GoodsType'
ALTER TABLE [dbo].[GoodsType]
ADD CONSTRAINT [PK_GoodsType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Order'
ALTER TABLE [dbo].[Order]
ADD CONSTRAINT [PK_Order]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'OrderStatus'
ALTER TABLE [dbo].[OrderStatus]
ADD CONSTRAINT [PK_OrderStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'OrderGoods'
ALTER TABLE [dbo].[OrderGoods]
ADD CONSTRAINT [PK_OrderGoods]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'RecAddress'
ALTER TABLE [dbo].[RecAddress]
ADD CONSTRAINT [PK_RecAddress]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'CityInfo'
ALTER TABLE [dbo].[CityInfo]
ADD CONSTRAINT [PK_CityInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Community'
ALTER TABLE [dbo].[Community]
ADD CONSTRAINT [PK_Community]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'AreaInfo'
ALTER TABLE [dbo].[AreaInfo]
ADD CONSTRAINT [PK_AreaInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SampleHouse'
ALTER TABLE [dbo].[SampleHouse]
ADD CONSTRAINT [PK_SampleHouse]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'HouseArea'
ALTER TABLE [dbo].[HouseArea]
ADD CONSTRAINT [PK_HouseArea]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SpHouse_HseArea'
ALTER TABLE [dbo].[SpHouse_HseArea]
ADD CONSTRAINT [PK_SpHouse_HseArea]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserID] in table 'User_Role'
ALTER TABLE [dbo].[User_Role]
ADD CONSTRAINT [FK_UserUser_Role]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUser_Role'
CREATE INDEX [IX_FK_UserUser_Role]
ON [dbo].[User_Role]
    ([UserID]);
GO

-- Creating foreign key on [RoleID] in table 'User_Role'
ALTER TABLE [dbo].[User_Role]
ADD CONSTRAINT [FK_RoleUser_Role]
    FOREIGN KEY ([RoleID])
    REFERENCES [dbo].[Role]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleUser_Role'
CREATE INDEX [IX_FK_RoleUser_Role]
ON [dbo].[User_Role]
    ([RoleID]);
GO

-- Creating foreign key on [UserID] in table 'User_Action'
ALTER TABLE [dbo].[User_Action]
ADD CONSTRAINT [FK_UserUser_Action]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUser_Action'
CREATE INDEX [IX_FK_UserUser_Action]
ON [dbo].[User_Action]
    ([UserID]);
GO

-- Creating foreign key on [ActionID] in table 'User_Action'
ALTER TABLE [dbo].[User_Action]
ADD CONSTRAINT [FK_ActionUser_Action]
    FOREIGN KEY ([ActionID])
    REFERENCES [dbo].[Action]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ActionUser_Action'
CREATE INDEX [IX_FK_ActionUser_Action]
ON [dbo].[User_Action]
    ([ActionID]);
GO

-- Creating foreign key on [RoleID] in table 'Role_Action'
ALTER TABLE [dbo].[Role_Action]
ADD CONSTRAINT [FK_RoleRole_Action]
    FOREIGN KEY ([RoleID])
    REFERENCES [dbo].[Role]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleRole_Action'
CREATE INDEX [IX_FK_RoleRole_Action]
ON [dbo].[Role_Action]
    ([RoleID]);
GO

-- Creating foreign key on [ActionID] in table 'Role_Action'
ALTER TABLE [dbo].[Role_Action]
ADD CONSTRAINT [FK_ActionRole_Action]
    FOREIGN KEY ([ActionID])
    REFERENCES [dbo].[Action]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ActionRole_Action'
CREATE INDEX [IX_FK_ActionRole_Action]
ON [dbo].[Role_Action]
    ([ActionID]);
GO

-- Creating foreign key on [ActionTypeID] in table 'Action'
ALTER TABLE [dbo].[Action]
ADD CONSTRAINT [FK_ActionActionType]
    FOREIGN KEY ([ActionTypeID])
    REFERENCES [dbo].[ActionType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ActionActionType'
CREATE INDEX [IX_FK_ActionActionType]
ON [dbo].[Action]
    ([ActionTypeID]);
GO

-- Creating foreign key on [NewsTypeID] in table 'News'
ALTER TABLE [dbo].[News]
ADD CONSTRAINT [FK_NewsNewsType]
    FOREIGN KEY ([NewsTypeID])
    REFERENCES [dbo].[NewsType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_NewsNewsType'
CREATE INDEX [IX_FK_NewsNewsType]
ON [dbo].[News]
    ([NewsTypeID]);
GO

-- Creating foreign key on [UserInfo_ID] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [FK_UserUserInfo]
    FOREIGN KEY ([UserInfo_ID])
    REFERENCES [dbo].[UserInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUserInfo'
CREATE INDEX [IX_FK_UserUserInfo]
ON [dbo].[User]
    ([UserInfo_ID]);
GO

-- Creating foreign key on [BannerTypeID] in table 'Banner'
ALTER TABLE [dbo].[Banner]
ADD CONSTRAINT [FK_BannerBannerType]
    FOREIGN KEY ([BannerTypeID])
    REFERENCES [dbo].[BannerType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BannerBannerType'
CREATE INDEX [IX_FK_BannerBannerType]
ON [dbo].[Banner]
    ([BannerTypeID]);
GO

-- Creating foreign key on [UserID] in table 'User_ShoppingCart'
ALTER TABLE [dbo].[User_ShoppingCart]
ADD CONSTRAINT [FK_UserUser_ShoppingCart]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUser_ShoppingCart'
CREATE INDEX [IX_FK_UserUser_ShoppingCart]
ON [dbo].[User_ShoppingCart]
    ([UserID]);
GO

-- Creating foreign key on [GoodsInfoID] in table 'GoodsProperty'
ALTER TABLE [dbo].[GoodsProperty]
ADD CONSTRAINT [FK_GoodsInfoGoodsProperty]
    FOREIGN KEY ([GoodsInfoID])
    REFERENCES [dbo].[GoodsInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GoodsInfoGoodsProperty'
CREATE INDEX [IX_FK_GoodsInfoGoodsProperty]
ON [dbo].[GoodsProperty]
    ([GoodsInfoID]);
GO

-- Creating foreign key on [GoodsPropertyID] in table 'GoodsPropertyDetail'
ALTER TABLE [dbo].[GoodsPropertyDetail]
ADD CONSTRAINT [FK_GoodsPropertyGoodsPropertyDetail]
    FOREIGN KEY ([GoodsPropertyID])
    REFERENCES [dbo].[GoodsProperty]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GoodsPropertyGoodsPropertyDetail'
CREATE INDEX [IX_FK_GoodsPropertyGoodsPropertyDetail]
ON [dbo].[GoodsPropertyDetail]
    ([GoodsPropertyID]);
GO

-- Creating foreign key on [GoodsTypeID] in table 'GoodsInfo'
ALTER TABLE [dbo].[GoodsInfo]
ADD CONSTRAINT [FK_GoodsInfoGoodsType]
    FOREIGN KEY ([GoodsTypeID])
    REFERENCES [dbo].[GoodsType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GoodsInfoGoodsType'
CREATE INDEX [IX_FK_GoodsInfoGoodsType]
ON [dbo].[GoodsInfo]
    ([GoodsTypeID]);
GO

-- Creating foreign key on [OrderStatusID] in table 'Order'
ALTER TABLE [dbo].[Order]
ADD CONSTRAINT [FK_OrderOrderStatus]
    FOREIGN KEY ([OrderStatusID])
    REFERENCES [dbo].[OrderStatus]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderOrderStatus'
CREATE INDEX [IX_FK_OrderOrderStatus]
ON [dbo].[Order]
    ([OrderStatusID]);
GO

-- Creating foreign key on [OrderID] in table 'OrderGoods'
ALTER TABLE [dbo].[OrderGoods]
ADD CONSTRAINT [FK_OrderOrderGoods]
    FOREIGN KEY ([OrderID])
    REFERENCES [dbo].[Order]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderOrderGoods'
CREATE INDEX [IX_FK_OrderOrderGoods]
ON [dbo].[OrderGoods]
    ([OrderID]);
GO

-- Creating foreign key on [UserID] in table 'RecAddress'
ALTER TABLE [dbo].[RecAddress]
ADD CONSTRAINT [FK_UserRecAddress]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[User]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRecAddress'
CREATE INDEX [IX_FK_UserRecAddress]
ON [dbo].[RecAddress]
    ([UserID]);
GO

-- Creating foreign key on [RecAddressID] in table 'Order'
ALTER TABLE [dbo].[Order]
ADD CONSTRAINT [FK_OrderRecAddress]
    FOREIGN KEY ([RecAddressID])
    REFERENCES [dbo].[RecAddress]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderRecAddress'
CREATE INDEX [IX_FK_OrderRecAddress]
ON [dbo].[Order]
    ([RecAddressID]);
GO

-- Creating foreign key on [CityInfoID] in table 'AreaInfo'
ALTER TABLE [dbo].[AreaInfo]
ADD CONSTRAINT [FK_CityInfoAreaInfo]
    FOREIGN KEY ([CityInfoID])
    REFERENCES [dbo].[CityInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CityInfoAreaInfo'
CREATE INDEX [IX_FK_CityInfoAreaInfo]
ON [dbo].[AreaInfo]
    ([CityInfoID]);
GO

-- Creating foreign key on [AreaInfoID] in table 'Community'
ALTER TABLE [dbo].[Community]
ADD CONSTRAINT [FK_AreaInfoCommunity]
    FOREIGN KEY ([AreaInfoID])
    REFERENCES [dbo].[AreaInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AreaInfoCommunity'
CREATE INDEX [IX_FK_AreaInfoCommunity]
ON [dbo].[Community]
    ([AreaInfoID]);
GO

-- Creating foreign key on [SampleHouseID] in table 'SpHouse_HseArea'
ALTER TABLE [dbo].[SpHouse_HseArea]
ADD CONSTRAINT [FK_SampleHouseSpHouse_HseArea]
    FOREIGN KEY ([SampleHouseID])
    REFERENCES [dbo].[SampleHouse]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SampleHouseSpHouse_HseArea'
CREATE INDEX [IX_FK_SampleHouseSpHouse_HseArea]
ON [dbo].[SpHouse_HseArea]
    ([SampleHouseID]);
GO

-- Creating foreign key on [HouseAreaID] in table 'SpHouse_HseArea'
ALTER TABLE [dbo].[SpHouse_HseArea]
ADD CONSTRAINT [FK_HouseAreaSpHouse_HseArea]
    FOREIGN KEY ([HouseAreaID])
    REFERENCES [dbo].[HouseArea]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_HouseAreaSpHouse_HseArea'
CREATE INDEX [IX_FK_HouseAreaSpHouse_HseArea]
ON [dbo].[SpHouse_HseArea]
    ([HouseAreaID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------