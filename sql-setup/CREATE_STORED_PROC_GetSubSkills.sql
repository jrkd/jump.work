--CREATE TYPE integer_list_tbltype AS TABLE (n int NOT NULL PRIMARY KEY);


 use [JumpDB_MSSQL] 
 go
 
CREATE TYPE integer_list_tbltype AS TABLE (n int NOT NULL PRIMARY KEY);
go;

 Create PROCEDURE [dbo].[GetSubSkillIDs] 
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