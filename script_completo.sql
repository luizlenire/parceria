IF EXISTS (SELECT	1 
		   FROM		master.dbo.sysdatabases AS T0 WITH(NOLOCK) 
		   WHERE	T0.name = 'Teste')
BEGIN;
	DROP DATABASE Teste;
END;
ELSE
BEGIN;
------------------------------------------------
	CREATE DATABASE Teste;
------------------------------------------------
END;
GO

------------------------------------------------------
--> START											 |
--> DROP AND CREATE TABLES, PRIMARY AND FOREIGN KEYS.|
------------------------------------------------------
USE Teste
GO

IF EXISTS (SELECT	1 
		   FROM		sys.foreign_keys	AS T0 WITH(NOLOCK)
		   WHERE	T0.object_id		= OBJECT_ID('FK_ParceriaLike_Parceria') AND 
					T0.parent_object_id	= OBJECT_ID('ParceriaLike'))
BEGIN;
------------------------------------------------
	ALTER TABLE		ParceriaLike
	DROP CONSTRAINT FK_ParceriaLike_Parceria;
------------------------------------------------
END;
GO

IF EXISTS (SELECT	1
		   FROM		sys.objects		AS T0 WITH(NOLOCK)
		   WHERE	T0.object_id	= OBJECT_ID('Parceria') AND 
					T0.type			IN ('U'))
BEGIN;
------------------------------------------------
	DROP TABLE Parceria;
------------------------------------------------
END;
GO

IF EXISTS (SELECT	1 
		   FROM		sys.objects		AS T0 WITH(NOLOCK)
		   WHERE	T0.object_id	= OBJECT_ID('ParceriaLog') AND 
					T0.type			IN ('U'))
BEGIN;
------------------------------------------------
	DROP TABLE ParceriaLog;
------------------------------------------------
END;
GO

IF EXISTS (SELECT	1 
		   FROM		sys.objects		AS T0 WITH(NOLOCK)
		   WHERE	T0.object_id	= OBJECT_ID('ParceriaLike') AND 
					T0.type			IN ('U'))
BEGIN;
------------------------------------------------
	DROP TABLE ParceriaLike;
------------------------------------------------
END;
GO

CREATE TABLE ParceriaLog
			 (Identifier				BIGINT IDENTITY(1,1)	NOT NULL,
	          Included					DATETIME				NOT NULL,
	          Description				NVARCHAR(MAX)			NOT NULL,
	          DescriptionException		NVARCHAR(MAX)			NULL,
	          DescriptionInnerException NVARCHAR(MAX)			NULL,
 CONSTRAINT [PK_Parceria_Log] PRIMARY KEY CLUSTERED 
(
	Identifier ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE ParceriaLike
			 (Codigo			INT IDENTITY(1,1)	NOT NULL,
			  CodigoParceria	INT					NOT NULL,
			  DataHoraCadastro	DATETIME			NOT NULL,
 CONSTRAINT PK_ParceriaLike PRIMARY KEY CLUSTERED 
(
	Codigo ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE Parceria
			 (Codigo			INT IDENTITY(1,1)	NOT NULL,
			  Titulo			VARCHAR(255)		NOT NULL,
			  Descricao			TEXT				NOT NULL,
			  URLPagina			VARCHAR(1000)		NULL,
			  Empresa			VARCHAR(40)			NOT NULL,
			  DataInicio		DATE				NOT NULL,
			  DataTermino		DATE				NOT NULL,
			  QtdLikes			INT					NOT NULL,
			  DataHoraCadastro	DATETIME			NOT NULL,
 CONSTRAINT PK_Parceria PRIMARY KEY CLUSTERED 
(
	Codigo ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE ParceriaLike  WITH CHECK ADD  CONSTRAINT FK_ParceriaLike_Parceria FOREIGN KEY(CodigoParceria)
REFERENCES Parceria (Codigo)
GO
ALTER TABLE ParceriaLike CHECK CONSTRAINT FK_ParceriaLike_Parceria
GO
------------------------------------------------------
--> END											     |
--> DROP AND CREATE TABLES, PRIMARY AND FOREIGN KEYS.|
------------------------------------------------------

---------------------------
--> START				  |
--> DROP AND CREATE VIEWS.|
---------------------------
IF EXISTS (SELECT	1 
		   FROM		sys.views	AS T0 WITH(NOLOCK)
		   WHERE	object_id	= OBJECT_ID('vParceria'))
BEGIN;
------------------------------------------------
	DROP VIEW vParceria;
------------------------------------------------
END;
GO

CREATE VIEW vParceria AS
	SELECT	*
	FROM	Parceria AS T0 WITH(NOLOCK);
GO

IF EXISTS (SELECT	1 
		   FROM		sys.views	AS T0 WITH(NOLOCK)
		   WHERE	object_id	= OBJECT_ID('vParceriaLike'))
BEGIN;
------------------------------------------------
	DROP VIEW vParceriaLike;
------------------------------------------------
END;
GO

CREATE VIEW vParceriaLike AS
	SELECT	*
	FROM	ParcerIaLike AS T0 WITH(NOLOCK);
GO
---------------------------
--> END 				  |
--> DROP AND CREATE VIEWS.|
---------------------------

---------------------------------
--> START						|
--> DROP AND CREATE PROCEDURES. |
---------------------------------

IF EXISTS (SELECT	1 
		   FROM		sys.objects AS T0 WITH(NOLOCK) 
		   WHERE	object_id	= OBJECT_ID('spParceria') AND 
					type		IN ('P', 'PC'))
BEGIN;
------------------------------------------------
	DROP PROCEDURE [dbo].[spParceria];
------------------------------------------------
END;
GO

CREATE PROCEDURE spParceria @Type			AS TINYINT, 
							@Codigo			AS INT,
							@Titulo			AS VARCHAR(255),
							@Descricao		AS TEXT,
							@URLPagina		AS VARCHAR(1000),
							@Empresa		AS VARCHAR(40),
							@DataInicio		AS DATE,
							@DataTermino	AS DATE,
							@QtdLikes		AS INT 
AS

DECLARE @MESSAGE AS NVARCHAR(MAX) = '';

BEGIN TRY
	IF (@Type			= 1 AND
		@Titulo			IS NOT NULL AND
		@Descricao		IS NOT NULL AND
		@Empresa		IS NOT NULL AND
		@DataInicio		IS NOT NULL AND
		@DataTermino	IS NOT NULL AND
		@QtdLikes		IS NOT NULL)
	BEGIN;
	------------------------------------------------
		INSERT INTO Parceria
					(Titulo, 
					 Descricao,
					 URLPagina,
					 Empresa,
					 DataInicio,
					 DataTermino,
					 QtdLikes,
					 DataHoraCadastro)
		VALUES		(@Titulo,
					 @Descricao,
					 @URLPagina,
					 @Empresa,
					 @DataInicio,
					 @DataTermino,
					 @QtdLikes,
					 GETDATE());	

		SET @MESSAGE = 'Registro inserido com sucesso.';
	------------------------------------------------
	END;
	ELSE IF (@Type			= 2 AND
			 @Codigo		IS NOT NULL AND
			 @Titulo		IS NOT NULL AND
			 @Descricao		IS NOT NULL AND
			 @Empresa		IS NOT NULL AND
			 @DataInicio	IS NOT NULL AND
			 @DataTermino	IS NOT NULL AND
			 @QtdLikes		IS NOT NULL)
	BEGIN;
	------------------------------------------------
		UPDATE	Parceria
		SET		Titulo		= @Titulo, 
				Descricao	= @Descricao,
				URLPagina	= @URLPagina,
				Empresa		= @Empresa,
				DataInicio	= @DataInicio,
				DataTermino = @DataTermino,
				QtdLikes	= @QtdLikes
		WHERE	Codigo		= @Codigo;

		SET @MESSAGE = 'Registro atualizado com sucesso.';
	------------------------------------------------
	END;
	ELSE IF (@Type			= 3 AND
			 @Codigo		IS NOT NULL)
	BEGIN;
	------------------------------------------------
		DELETE	ParceriaLike
		WHERE	CodigoParceria	= @Codigo;

		DELETE	Parceria
		WHERE	Codigo			= @Codigo;

		SET @MESSAGE = 'Registro removido com sucesso.';
	------------------------------------------------
	END;
	ELSE
	BEGIN;
	------------------------------------------------
		SET @MESSAGE = 'Operação não prevista.';
	------------------------------------------------
	END;
END TRY
BEGIN CATCH
------------------------------------------------
	SET @MESSAGE = 'Ocorreu uma exceção nesta operação, contatar o suporte imediatamente.';
------------------------------------------------
END CATCH;

RETURN 1;
GO

IF EXISTS (SELECT	1 
		   FROM		sys.objects		AS T0 WITH(NOLOCK) 
		   WHERE	T0.object_id	= OBJECT_ID('spParceriaLike') AND 
					T0.type			IN ('P', 'PC'))
BEGIN;
------------------------------------------------
	DROP PROCEDURE spParceriaLike;
------------------------------------------------
END;
GO

CREATE PROCEDURE spParceriaLike (@Codigo AS INT) 
AS

DECLARE @MESSAGE AS NVARCHAR(MAX) = '';

BEGIN TRY
	IF (@Codigo IS NOT NULL)
	BEGIN;
	------------------------------------------------
		IF NOT EXISTS(SELECT	1
				      FROM		Parceria AS T0 WITH(NOLOCK)
				      WHERE		T0.Codigo = @Codigo)
		BEGIN;
		------------------------------------------------
			SET @MESSAGE = 'Não existe parceria para este código.';
		------------------------------------------------
		END;
		ELSE
		BEGIN;
		------------------------------------------------
			IF NOT EXISTS (SELECT	1
						   FROM		ParceriaLike		AS T0 WITH(NOLOCK)
						   WHERE	T0.CodigoParceria	= @Codigo)
			BEGIN;
			------------------------------------------------
				INSERT INTO ParceriaLike
							(CodigoParceria,
							 DataHoraCadastro)
				VALUES		(@Codigo,
							 GETDATE());
			------------------------------------------------
			END;
			ELSE
			BEGIN
			------------------------------------------------
				UPDATE	Parceria
				SET		QtdLikes	+= 1
				WHERE	Codigo		= @Codigo;	
			------------------------------------------------
			END;

			SET @MESSAGE = 'Registro inserido com sucesso.';
		------------------------------------------------
		END;
	END;
	ELSE
	BEGIN;
	------------------------------------------------
		SET @MESSAGE = 'Operação não prevista.';
	------------------------------------------------
	END;
END TRY
BEGIN CATCH
------------------------------------------------
	SET @MESSAGE = 'Ocorreu uma exceção nesta operação, contatar o suporte imediatamente.';
------------------------------------------------
END CATCH;

RETURN 1;
GO
---------------------------------
--> END							|
--> DROP AND CREATE PROCEDURES. |
---------------------------------

-------------------------------
--> START					  |
--> DROP AND CREATE TRIGGERS. |
-------------------------------
CREATE TRIGGER TR_ParceriaLike ON ParceriaLike FOR INSERT AS
BEGIN;
------------------------------------------------
	DECLARE @RC		AS INT;
	DECLARE @Codigo AS INT = (SELECT TOP 1 CodigoParceria CodigoParceria FROm inserted);

	EXECUTE @RC = [dbo].[spParceriaLike] @Codigo;	
------------------------------------------------
END;
GO
-------------------------------
--> END						  |
--> DROP AND CREATE TRIGGERS. |
-------------------------------

-------------------------------
--> START					  |
--> TESTS.					  |
-------------------------------
DECLARE @RC				AS INT;
DECLARE @Type			AS TINYINT			= 1;
DECLARE @Codigo			AS INT				= NULL;
DECLARE @Titulo			AS VARCHAR(255)		= 'GOOGLE';
DECLARE @Descricao		AS NVARCHAR(MAX)	= 'GOOGLE GIGANTE DE BUSCA';
DECLARE @URLPagina		AS VARCHAR(1000)	= 'https://www.google.com/';
DECLARE @Empresa		AS VARCHAR(40)		= 'GOOGLE INC.';
DECLARE @DataInicio		AS DATE				= '20200924';
DECLARE @DataTermino	AS DATE				= '20201231';
DECLARE @QtdLikes		AS INT				= 0;

EXECUTE @RC = spParceria
			  @Type,
			  @Codigo,
			  @Titulo,
			  @Descricao,
			  @URLPagina,
			  @Empresa,
			  @DataInicio,
			  @DataTermino,
			  @QtdLikes;

GO

DECLARE @RC		AS INT;
DECLARE @Codigo AS INT = 1

EXECUTE @RC = spParceriaLike
			  @Codigo;
GO

SELECT 'INSERT' AS TYPE, * FROM Parceria;
SELECT 'INSERT' AS TYPE, * FROM ParceriaLike;
GO

DECLARE @RC				AS INT;
DECLARE @Type			AS TINYINT			= 2;
DECLARE @Codigo			AS INT				= 1;
DECLARE @Titulo			AS VARCHAR(255)		= 'UP GOOGLE';
DECLARE @Descricao		AS NVARCHAR(MAX)	= 'UP GOOGLE GIGANTE DE BUSCA';
DECLARE @URLPagina		AS VARCHAR(1000)	= 'UP | https://www.google.com/';
DECLARE @Empresa		AS VARCHAR(40)		= 'UP GOOGLE INC.';
DECLARE @DataInicio		AS DATE				= '20210924';
DECLARE @DataTermino	AS DATE				= '20211231';
DECLARE @QtdLikes		AS INT				= 0;

EXECUTE @RC = spParceria
			  @Type,
			  @Codigo,
			  @Titulo,
			  @Descricao,
			  @URLPagina,
			  @Empresa,
			  @DataInicio,
			  @DataTermino,
			  @QtdLikes;

GO

SELECT 'UPDATE' AS TYPE, * FROM Parceria;
SELECT 'UPDATE' AS TYPE, * FROM ParceriaLike;
GO

DECLARE @RC				AS INT;
DECLARE @Type			AS TINYINT			= 3;
DECLARE @Codigo			AS INT				= 1;
DECLARE @Titulo			AS VARCHAR(255)		= NULL;
DECLARE @Descricao		AS NVARCHAR(MAX)	= NULL;
DECLARE @URLPagina		AS VARCHAR(1000)	= NULL;
DECLARE @Empresa		AS VARCHAR(40)		= NULL;
DECLARE @DataInicio		AS DATE				= NULL;
DECLARE @DataTermino	AS DATE				= NULL;
DECLARE @QtdLikes		AS INT				= NULL;

EXECUTE @RC = spParceria
			  @Type,
			  @Codigo,
			  @Titulo,
			  @Descricao,
			  @URLPagina,
			  @Empresa,
			  @DataInicio,
			  @DataTermino,
			  @QtdLikes;

GO

SELECT 'DELETE' AS TYPE, * FROM Parceria;
SELECT 'DELETE' AS TYPE, * FROM ParceriaLike;
GO
-------------------------------
--> START					  |
--> TESTS.					  |
-------------------------------
