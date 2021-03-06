USE [JumpDB_MSSQL]
GO
/****** Object:  UserDefinedTableType [dbo].[integer_list_tbltype]    Script Date: 04/07/2017 18:14:15 ******/
CREATE TYPE [dbo].[integer_list_tbltype] AS TABLE(
	[ID] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 04/07/2017 18:14:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Competitions]    Script Date: 04/07/2017 18:14:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Competitions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Year] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Competitions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[People]    Script Date: 04/07/2017 18:14:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[People](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[age] [int] NOT NULL,
 CONSTRAINT [PK_dbo.People] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PersonCompetitions]    Script Date: 04/07/2017 18:14:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonCompetitions](
	[Person_ID] [int] NOT NULL,
	[Competition_ID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.PersonCompetitions] PRIMARY KEY CLUSTERED 
(
	[Person_ID] ASC,
	[Competition_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PersonSkills]    Script Date: 04/07/2017 18:14:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonSkills](
	[Person_ID] [int] NOT NULL,
	[Skill_ID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.PersonSkills] PRIMARY KEY CLUSTERED 
(
	[Person_ID] ASC,
	[Skill_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Skills]    Script Date: 04/07/2017 18:14:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skills](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Skills] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SkillSubSkill]    Script Date: 04/07/2017 18:14:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SkillSubSkill](
	[ID] [int] NOT NULL,
	[SubSkillID] [int] NOT NULL,
 CONSTRAINT [PK_dbo.SkillSubSkill] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[SubSkillID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[PersonCompetitions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PersonCompetitions_dbo.Competitions_Competition_ID] FOREIGN KEY([Competition_ID])
REFERENCES [dbo].[Competitions] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PersonCompetitions] CHECK CONSTRAINT [FK_dbo.PersonCompetitions_dbo.Competitions_Competition_ID]
GO
ALTER TABLE [dbo].[PersonCompetitions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PersonCompetitions_dbo.People_Person_ID] FOREIGN KEY([Person_ID])
REFERENCES [dbo].[People] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PersonCompetitions] CHECK CONSTRAINT [FK_dbo.PersonCompetitions_dbo.People_Person_ID]
GO
ALTER TABLE [dbo].[PersonSkills]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PersonSkills_dbo.People_Person_ID] FOREIGN KEY([Person_ID])
REFERENCES [dbo].[People] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PersonSkills] CHECK CONSTRAINT [FK_dbo.PersonSkills_dbo.People_Person_ID]
GO
ALTER TABLE [dbo].[PersonSkills]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PersonSkills_dbo.Skills_Skill_ID] FOREIGN KEY([Skill_ID])
REFERENCES [dbo].[Skills] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PersonSkills] CHECK CONSTRAINT [FK_dbo.PersonSkills_dbo.Skills_Skill_ID]
GO
ALTER TABLE [dbo].[SkillSubSkill]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SkillSubSkill_dbo.Skills_ID] FOREIGN KEY([ID])
REFERENCES [dbo].[Skills] ([ID])
GO
ALTER TABLE [dbo].[SkillSubSkill] CHECK CONSTRAINT [FK_dbo.SkillSubSkill_dbo.Skills_ID]
GO
ALTER TABLE [dbo].[SkillSubSkill]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SkillSubSkill_dbo.Skills_SubSkillID] FOREIGN KEY([SubSkillID])
REFERENCES [dbo].[Skills] ([ID])
GO
ALTER TABLE [dbo].[SkillSubSkill] CHECK CONSTRAINT [FK_dbo.SkillSubSkill_dbo.Skills_SubSkillID]
GO
/****** Object:  StoredProcedure [dbo].[GetSubSkills]    Script Date: 04/07/2017 18:14:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 Create PROCEDURE [dbo].[GetSubSkills] 
                     -- Add the parameters for the stored procedure here
                     @ParentSkillIDs dbo.integer_list_tbltype READONLY
                    AS
                    BEGIN
                     -- SET NOCOUNT ON added to prevent extra result sets from
                     -- interfering with SELECT statements.
                     SET NOCOUNT ON;

                       ; WITH parent AS (
                            SELECT ID
                            FROM @ParentSkillIDs
                        ), tree AS (
                            SELECT x.ID, x.SubSkillID
                            FROM JumpDB_MSSQL.dbo.SkillSubSkill x
                            INNER JOIN parent ON x.ID = parent.ID
                            UNION ALL
                            SELECT y.ID, y.SubSkillID
                            FROM JumpDB_MSSQL.dbo.SkillSubSkill y
                            INNER JOIN tree t ON y.ID = t.SubSkillID
                        )
                        SELECT JumpDB_MSSQL.dbo.Skills.*
                        FROM tree inner join JumpDB_MSSQL.dbo.Skills on JumpDB_MSSQL.dbo.Skills.ID = tree.SubSkillID
                    END
GO
