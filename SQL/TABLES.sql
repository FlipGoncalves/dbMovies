--CREATE SCHEMA Projeto;

CREATE TABLE Projeto.Users (
	username VARCHAR(256) PRIMARY KEY,
	[password] VARBINARY(128)
);
GO

CREATE TYPE FilmeAndStudio VARCHAR(256) NOT NULL;

CREATE TABLE Projeto.FilmeStudio (
	NOME VARCHAR(256) PRIMARY KEY
);
GO

CREATE TABLE Projeto.PublicoAlvo (
	Identificador INT PRIMARY KEY IDENTITY(1,1),
	Nome VARCHAR(256) UNIQUE NOT NULL DEFAULT 'EVERYONE'
);
GO

CREATE TABLE Projeto.Filme (
	StudioNome FilmeAndStudio FOREIGN KEY REFERENCES Projeto.FilmeStudio(Nome),
	FilmeNome FilmeAndStudio DEFAULT 'UNKNOWN',
	ID_Publico_Alvo INT FOREIGN KEY REFERENCES Projeto.PublicoAlvo(Identificador), --desnecessario
	Duracao INT NOT NULL,
	Ranking DECIMAL(3,1) CHECK (Ranking >= 0 AND Ranking <= 10) NOT NULL,
	Ganho INT DEFAULT 10000000,
	Ano DATE,
	PRIMARY KEY (StudioNome, FilmeNome)
);
GO

CREATE TABLE Projeto.Ve (
	Publico_Alvo_ID INT FOREIGN KEY REFERENCES Projeto.PublicoAlvo(Identificador),
	FilmeStudio_Nome FilmeAndStudio,
	Filme_Nome FilmeAndStudio,
	FOREIGN KEY (FilmeStudio_Nome, Filme_Nome) REFERENCES Projeto.Filme(StudioNome, FilmeNome),
	PRIMARY KEY (Publico_Alvo_ID, FilmeStudio_Nome, Filme_Nome)
);
GO

CREATE TABLE Projeto.Genero (
	Genero VARCHAR(256),
	FilmeNome FilmeAndStudio,
	StudioNome FilmeAndStudio,
	FOREIGN KEY (StudioNome, FilmeNome) REFERENCES Projeto.Filme(StudioNome, FilmeNome),
	PRIMARY KEY (Genero, FilmeNome, StudioNome)
);
GO

CREATE TABLE Projeto.Localizacao (
	Endereco VARCHAR(256) PRIMARY KEY
);
GO

CREATE TABLE Projeto.FilmaEm (
	FilmeNome FilmeAndStudio,
	StudioNome FilmeAndStudio,
	Endereco VARCHAR(256) FOREIGN KEY REFERENCES Projeto.Localizacao(Endereco),
	[Data] DATE NOT NULL,
	FOREIGN KEY (StudioNome, FilmeNome) REFERENCES Projeto.Filme(StudioNome, FilmeNome),
	PRIMARY KEY (FilmeNome, StudioNome, Endereco)
);
GO

CREATE TABLE Projeto.Pessoa (
	Identificador INT PRIMARY KEY IDENTITY(100,1),
	Nome VARCHAR(256) NOT NULL
);
GO

CREATE TABLE Projeto.WorksOn (
	FilmeNome FilmeAndStudio,
	StudioNome FilmeAndStudio,
	[Role] VARCHAR(256) NOT NULL,
	Prizes VARCHAR(256),
	Identificador INT FOREIGN KEY REFERENCES Projeto.Pessoa(Identificador),
	Costume VARCHAR(256) DEFAULT 'NONE',
	Interview VARCHAR(256) DEFAULT 'NONE',
	FOREIGN KEY (StudioNome, FilmeNome) REFERENCES Projeto.Filme(StudioNome, FilmeNome),
	PRIMARY KEY (FilmeNome, StudioNome, Identificador)
);
GO

CREATE TABLE Projeto.Adereco (
	Codigo INT PRIMARY KEY IDENTITY(1,1),
	Nome VARCHAR(256) DEFAULT 'UNKNOWN',
	Price INT NOT NULL 
);
GO

CREATE TABLE Projeto.UsadoEm (
	Codigo INT FOREIGN KEY REFERENCES Projeto.Adereco(Codigo),
	FilmeNome FilmeAndStudio,
	StudioNome FilmeAndStudio,
	FOREIGN KEY (StudioNome, FilmeNome) REFERENCES Projeto.Filme(StudioNome, FilmeNome),
	PRIMARY KEY (Codigo, StudioNome, FilmeNome)
);
GO

CREATE TABLE Projeto.Fornecedor (
	Codigo INT PRIMARY KEY IDENTITY (1000,1),
	Nome VARCHAR(256) NOT NULL,
	Marca VARCHAR(256) NOT NULL
);
GO

CREATE TABLE Projeto.Vende (
	Adereco INT FOREIGN KEY REFERENCES Projeto.Adereco(Codigo),
	CodigoFornecedor INT FOREIGN KEY REFERENCES Projeto.Fornecedor(Codigo),
	Quantidade INT DEFAULT 0,
	PRIMARY KEY (Adereco, CodigoFornecedor)
);
GO

CREATE TABLE Projeto.Armazem (
	Codigo INT PRIMARY KEY,
	Morada VARCHAR(256) DEFAULT 'UNKNOWN'
);
GO

CREATE TABLE Projeto.Guarda (
	CodigoAdereco INT FOREIGN KEY REFERENCES Projeto.Adereco(Codigo),
	CodigoArmazem INT FOREIGN KEY REFERENCES Projeto.Armazem(Codigo),
	Quantidade INT DEFAULT 0,
	PRIMARY KEY (CodigoAdereco, CodigoArmazem)
);
GO

CREATE TABLE Projeto.Pagamento (
	Referencia INT PRIMARY KEY IDENTITY(100000000, 1), --9 algarismos
	Quantia INT NOT NULL DEFAULT 0,
	Pessoa_ID INT FOREIGN KEY REFERENCES Projeto.Pessoa(Identificador)
);
GO

CREATE TABLE Projeto.Faz (
	PagReferencia INT FOREIGN KEY REFERENCES Projeto.Pagamento(Referencia),
	FilmeNome FilmeAndStudio,
	StudioNome FilmeAndStudio,
	FOREIGN KEY (StudioNome, FilmeNome) REFERENCES Projeto.Filme(StudioNome, FilmeNome),
	PRIMARY KEY (PagReferencia, StudioNome, FilmeNome)
);
GO


/*
DROP TABLE Projeto.Faz;
DROP TABLE Projeto.Pagamento;
DROP TABLE Projeto.Guarda;
DROP TABLE Projeto.Armazem;
DROP TABLE Projeto.Vende;
DROP TABLE Projeto.Fornecedor;
DROP TABLE Projeto.UsadoEm;
DROP TABLE Projeto.Adereco;
DROP TABLE Projeto.WorksOn;
DROP TABLE Projeto.Pessoa;
DROP TABLE Projeto.FilmaEm;
DROP TABLE Projeto.Localizacao;
DROP TABLE Projeto.Genero;
DROP TABLE Projeto.Ve;
DROP TABLE Projeto.Filme;
DROP TABLE Projeto.PublicoAlvo;
DROP TABLE Projeto.FilmeStudio;
*/
