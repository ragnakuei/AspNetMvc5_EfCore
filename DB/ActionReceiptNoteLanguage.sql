CREATE TABLE [dbo].[ActionReceiptNoteLanguage](
	[ActionReceiptNoteLanguageId] [uniqueidentifier] NOT NULL,
	[ActionReceiptId] [uniqueidentifier] NOT NULL,
	[ActionReceiptEffectiveOn] [datetime2](7) NOT NULL,
	[Language] [varchar](10) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ActionReceiptNoteLanguage] PRIMARY KEY CLUSTERED 
(
	[ActionReceiptNoteLanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ActionReceiptNoteLanguage]  WITH CHECK ADD  CONSTRAINT [FK_ActionReceiptNoteLanguage_ActionReceipt] FOREIGN KEY([ActionReceiptId], [ActionReceiptEffectiveOn])
REFERENCES [dbo].[ActionReceipt] ([ActionReceiptId], [EffectiveOn])
GO

ALTER TABLE [dbo].[ActionReceiptNoteLanguage] CHECK CONSTRAINT [FK_ActionReceiptNoteLanguage_ActionReceipt]
GO