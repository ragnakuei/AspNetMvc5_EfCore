CREATE TABLE [dbo].[CountyLanguage](
	[CountyLanguageId] [uniqueidentifier] NOT NULL,
	[Language] [varchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CountyId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_CountyLanguage] PRIMARY KEY CLUSTERED 
(
	[CountyLanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CountyLanguage]  WITH CHECK ADD  CONSTRAINT [FK_CountyLanguage_County] FOREIGN KEY([CountyId])
REFERENCES [dbo].[County] ([CountyId])
GO

ALTER TABLE [dbo].[CountyLanguage] CHECK CONSTRAINT [FK_CountyLanguage_County]
GO