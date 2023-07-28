
GO
CREATE TABLE [Region] (
  [Id] Int not null Primary Key Identity (1,1),
  [AdminId] int FOREIGN KEY REFERENCES [User](Id),
  [Name] nvarchar(100) not null ,
  [Type] nvarchar(100) not null
);

GO
CREATE TABLE [Trail] (
  [Id] Int not null Primary Key Identity (1,1),
  [AdminId] int FOREIGN KEY REFERENCES [User](Id),
  [RegionId] int FOREIGN KEY REFERENCES [Region](Id),
  [Name] nvarchar(100) not null ,
  [Type] nvarchar(100) not null,
  [Difficulty] int not null,
  [Status] int not null,
  [LastUpdate] Datetime2
);
GO
CREATE TABLE [Post] (
  [Id] Int not null Primary Key Identity (1,1),
  [Trail Id] int FOREIGN KEY REFERENCES [Trail](Id),
  [RegionId] int FOREIGN KEY REFERENCES [Region](Id),
  [UserId] int Foreign key References [Users](Id),
  [Title] nvarchar(100) not null,
  [Type] int,
  [Content] nvarchar(500) ,
  [Date] DateTime2
);
GO
CREATE TABLE [Comment] (
  [Id] Int not null Primary Key Identity (1,1),
  [PostId] int FOREIGN KEY REFERENCES [Post](Id),
[UserId] int FOREIGN KEY REFERENCES [User](Id),
  [Content] nvarchar(100) not null
);

