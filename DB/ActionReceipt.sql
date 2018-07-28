CREATE TABLE [dbo].[ActionReceipt](
	[ActionReceiptId] [uniqueidentifier] NOT NULL,
	[EffectiveOn] [datetime2](7) NOT NULL,
	[CreateOn] [datetime2](7) NOT NULL,
	[UpdateOn] [datetime2](7) NOT NULL,
	[ParentActionReceiptId] [uniqueidentifier] NULL,
	[ParentEffectiveOn] [datetime2](7) NULL,
 CONSTRAINT [PK_ActionReceipt] PRIMARY KEY CLUSTERED 
(
	[ActionReceiptId] ASC,
	[EffectiveOn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO