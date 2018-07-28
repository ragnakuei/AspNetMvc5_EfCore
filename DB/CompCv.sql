CREATE TABLE [dbo].[CompCv](
	[CvId] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[CountryId] [uniqueidentifier] NOT NULL,
	[CountyId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CvId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CompCV]  WITH CHECK ADD  CONSTRAINT [FK_CompCV_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([CountryId])
GO

ALTER TABLE [dbo].[CompCV] CHECK CONSTRAINT [FK_CompCV_Country]
GO

ALTER TABLE [dbo].[CompCV]  WITH CHECK ADD  CONSTRAINT [FK_CompCV_County] FOREIGN KEY([CountyId])
REFERENCES [dbo].[County] ([CountyId])
GO

ALTER TABLE [dbo].[CompCV] CHECK CONSTRAINT [FK_CompCV_County]
GO