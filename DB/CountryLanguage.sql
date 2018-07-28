CREATE TABLE [dbo].[CountryLanguage](
	[CountryLanguageId] [uniqueidentifier] NOT NULL,
	[Language] [varchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CountryId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_CountryLanguage] PRIMARY KEY CLUSTERED 
(
	[CountryLanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CountryLanguage]  WITH CHECK ADD  CONSTRAINT [FK_CountryLanguage_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([CountryId])
GO

ALTER TABLE [dbo].[CountryLanguage] CHECK CONSTRAINT [FK_CountryLanguage_Country]
GO