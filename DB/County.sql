CREATE TABLE [dbo].[County](
	[CountyId] [uniqueidentifier] NOT NULL,
	[CountryId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CountyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[County]  WITH CHECK ADD  CONSTRAINT [FK_County_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([CountryId])
GO

ALTER TABLE [dbo].[County] CHECK CONSTRAINT [FK_County_Country]
GO