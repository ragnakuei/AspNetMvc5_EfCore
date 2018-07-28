CREATE TABLE [dbo].[CompCvCertificate](
	[CertificateId] [uniqueidentifier] NOT NULL,
	[CvId] [uniqueidentifier] NOT NULL,
	[CertificateName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CertificateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CompCvCertificate]  WITH CHECK ADD  CONSTRAINT [FK_CompCvCertificate_CompCV] FOREIGN KEY([CvId])
REFERENCES [dbo].[CompCV] ([CvId])
GO

ALTER TABLE [dbo].[CompCvCertificate] CHECK CONSTRAINT [FK_CompCvCertificate_CompCV]
GO