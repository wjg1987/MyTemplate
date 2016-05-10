
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 05/10/2016 17:26:37
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
    [WxOpenod] nvarchar(64)  NULL,
    [SinaWBOpenid] nvarchar(64)  NULL,
    [QQNum] nvarchar(64)  NULL,
    [Pwd] nvarchar(64)  NOT NULL,
    [IsDelete] bit  NOT NULL,
    [IsFrozen] bit  NOT NULL,
    [CreateTime] datetime  NOT NULL,
    [Balance] decimal(18,0)  NOT NULL,
    [FrozenBlance] decimal(18,0)  NOT NULL,
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

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------