--INSERT INTO WORKSON
CREATE PROCEDURE addActor
		@nome VARCHAR(256),
		@role VARCHAR(256),
		@filme VARCHAR(256),
		@studio VARCHAR(256)
AS
	DECLARE @ID INT
	IF EXISTS (SELECT Identificador FROM Projeto.Pessoa WHERE @nome = Nome)
	BEGIN
		BEGIN TRANSACTION
			SELECT @ID = Identificador FROM Projeto.Pessoa WHERE @nome = Nome
			INSERT Projeto.WorksOn (FilmeNome, StudioNome, [Role], Identificador)
				   VALUES (@filme, @studio, @role, @ID);
		COMMIT
	END
	ELSE
	BEGIN
		BEGIN TRANSACTION
			INSERT Projeto.Pessoa VALUES(@nome)
			SELECT @ID = IDENT_CURRENT('Projeto.Pessoa')
			INSERT Projeto.WorksOn (FilmeNome, StudioNome, [Role], Identificador)
				   VALUES (@filme, @studio, @role, @ID);
		COMMIT
	END
GO

--INSERT INTO FILMAEM
CREATE PROCEDURE addLocation
		@endereco VARCHAR(256),
		@data VARCHAR(256),
		@filme VARCHAR(256),
		@studio VARCHAR(256)
AS
	IF EXISTS (SELECT Endereco FROM Projeto.Localizacao WHERE @endereco = Endereco)
	BEGIN
		BEGIN TRANSACTION
			INSERT Projeto.FilmaEm (FilmeNome, StudioNome, Endereco, [Data])
				   VALUES (@filme, @studio, @endereco, CONVERT(DATE, @data));
		COMMIT
	END
	ELSE
	BEGIN
		BEGIN TRANSACTION
			INSERT Projeto.Localizacao VALUES(@endereco)
			INSERT Projeto.FilmaEm (FilmeNome, StudioNome, Endereco, [Data])
				   VALUES (@filme, @studio, @endereco, CONVERT(DATE, @data));
		COMMIT
	END
GO
drop proc addlocation

--INSERT INTO ADERECO
CREATE PROCEDURE addProp
		@name VARCHAR(256),
		@price INT,
		@qnty INT,
		@filme VARCHAR(256),
		@studio VARCHAR(256)
AS
	IF EXISTS (SELECT Nome FROM Projeto.Adereco WHERE @name = Nome)
	BEGIN
	DECLARE @ID INT
		BEGIN TRANSACTION
			SELECT @ID = Codigo FROM Projeto.Adereco WHERE @name = Nome
			INSERT Projeto.UsadoEm(Codigo, FilmeNome, StudioNome)
				   VALUES (@ID, @filme, @studio);
		COMMIT
	END
	ELSE
	BEGIN
		BEGIN TRANSACTION
			INSERT Projeto.Adereco VALUES(@name, @price)
			SELECT @ID = IDENT_CURRENT('Projeto.Adereco')
			INSERT Projeto.Vende VALUES(@ID, 1000, @qnty)
			INSERT Projeto.UsadoEm(Codigo, FilmeNome, StudioNome)
				   VALUES (@ID, @filme, @studio);
		COMMIT
	END
GO

--GENRES OF A FILM
CREATE FUNCTION movie_genres (@Movie VARCHAR(256)) RETURNS TABLE
AS
	RETURN (
		SELECT Genero.Genero
		FROM Projeto.GeneroTo JOIN Projeto.Genero ON ID = GeneroTo.Genero
		WHERE GeneroTo.FilmeNome LIKE '%' + @Movie + '%'
	)
GO

--ACTORS OF A FILM
CREATE FUNCTION atores_filme (@nomefilme VARCHAR(256), @nomestudio VARCHAR(256)) RETURNS TABLE
AS
	RETURN (
		SELECT Nome, Costume, [Role] 
		FROM Projeto.Filme JOIN Projeto.Pessoa ON FilmeNome = @nomefilme AND StudioNome = @nomestudio
		JOIN Projeto.WorksOn ON Projeto.WorksOn.Identificador = Projeto.Pessoa.Identificador
	)
GO
--LOCATIONS OF A FILM
CREATE FUNCTION movie_locations (@Movie VARCHAR(256), @Studio VARCHAR(256)) RETURNS TABLE
AS
	RETURN (
		SELECT Endereco, [Data]
		FROM Projeto.FilmaEm
		WHERE Projeto.FilmaEm.FilmeNome LIKE '%' + @Movie + '%' AND Projeto.FilmaEm.StudioNome LIKE '%' + @Studio + '%'
	)
GO
--PROPS OF A FILM
CREATE FUNCTION movie_aderecos (@Movie VARCHAR(256), @Studio VARCHAR(256)) RETURNS TABLE
AS
	RETURN (
		SELECT Nome, Price, Quantidade
		FROM Projeto.UsadoEm JOIN Projeto.Adereco ON Projeto.Adereco.Codigo = Projeto.UsadoEm.Codigo
		JOIN Projeto.Vende ON Adereco = Projeto.Adereco.Codigo
		WHERE Projeto.UsadoEm.FilmeNome LIKE '%' + @Movie + '%' AND Projeto.UsadoEm.StudioNome LIKE '%' + @Studio + '%'
	)
GO


--FILTER FILM BY GENRE
CREATE FUNCTION genre_movies (@Genero VARCHAR(256) = null) RETURNS TABLE
AS
	RETURN 
	(	
		SELECT GeneroTo.FilmeNome, GeneroTo.StudioNome, Ranking, Ano, Ganho, Duracao, Nome AS 'Publico Alvo'
		FROM Projeto.Filme 
		JOIN Projeto.GeneroTo ON GeneroTo.FilmeNome = Filme.FilmeNome AND GeneroTo.StudioNome = Filme.StudioNome
		JOIN Projeto.Genero ON ID = GeneroTo.Genero
		JOIN Projeto.PublicoAlvo ON ID_Publico_Alvo = Identificador
		WHERE Genero.Genero LIKE '%' + @Genero + '%'
	)
GO

--FILTER FILM BY YEAR
CREATE FUNCTION year_movies (@Ano VARCHAR(256) = null) RETURNS TABLE
AS
	RETURN 
	(	
		SELECT Filme.FilmeNome, Filme.StudioNome, Ranking, Ano, Ganho, Duracao, Nome AS 'Publico Alvo'
		FROM Projeto.Filme JOIN Projeto.PublicoAlvo ON ID_Publico_Alvo = Identificador
		WHERE CONVERT(VARCHAR(256), DATEPART(yy, Ano)) LIKE '%' + @Ano + '%'
	)
GO
CREATE INDEX idx_movies_year
ON Projeto.Filme (Ano);
GO
--FILTER FILM BY ACTOR
CREATE FUNCTION ator_filtro (@nomeator VARCHAR(256)) RETURNS TABLE
AS
	RETURN (
		SELECT DISTINCT Filme.FilmeNome, Filme.StudioNome, Ranking, Ano, Ganho, Duracao, PublicoAlvo.Nome AS 'Publico Alvo'
		FROM Projeto.Pessoa JOIN Projeto.WorksOn 
		ON Projeto.Pessoa.Identificador = Projeto.WorksOn.Identificador
		JOIN Projeto.Filme ON Filme.FilmeNome = WorksOn.FilmeNome and Filme.StudioNome = WorksOn.StudioNome 
		JOIN Projeto.PublicoAlvo ON ID_Publico_Alvo = PublicoAlvo.Identificador
		WHERE Pessoa.Nome LIKE '%' + @nomeator + '%'
	)
GO
CREATE INDEX PessoaIdx
ON Projeto.Pessoa(Nome)
GO;
--FILTER FILM BY FILM NAME
CREATE FUNCTION filme_filtro (@nomefilme VARCHAR(256)) RETURNS TABLE
AS
	RETURN (
		SELECT Filme.FilmeNome, Filme.StudioNome, Ranking, Ano, Ganho, Duracao, Nome AS 'Publico Alvo'
		FROM Projeto.Filme 
		JOIN Projeto.PublicoAlvo ON ID_Publico_Alvo = Identificador
		WHERE Filme.FilmeNome LIKE '%' + @nomefilme + '%'
	)
GO
CREATE INDEX FilmeIdx 
ON Projeto.Filme(FilmeNome)
GO;
--FILTER FILM BY STUDIO
CREATE FUNCTION filmestudio_filtro (@nomestudio VARCHAR(256)) RETURNS TABLE
AS
	RETURN (
		SELECT Filme.FilmeNome, Filme.StudioNome, Ranking, Ano, Ganho, Duracao, PublicoAlvo.Nome AS 'Publico Alvo'
		FROM Projeto.Filme
		RIGHT OUTER JOIN Projeto.PublicoAlvo ON ID_Publico_Alvo = Identificador
		JOIN Projeto.FilmeStudio ON Filme.StudioNome = FilmeStudio.Nome 
		WHERE FilmeStudio.Nome LIKE '%' + @nomestudio + '%'
	)
GO
CREATE INDEX StudioFilmeIdx 
ON Projeto.FilmeStudio(Nome)
GO;

--FILTER ACTOR BY NAME
CREATE FUNCTION nome_ator (@Nome_ator VARCHAR(256)) RETURNS TABLE
AS
	RETURN (
		SELECT Pessoa.Identificador, Nome
		FROM Projeto.Pessoa
		WHERE Pessoa.Nome LIKE '%' + @Nome_ator + '%'
	)
GO
--FILTER ACTOR BY ROLE
CREATE FUNCTION role_ator (@Role_ator VARCHAR(256)) RETURNS TABLE
AS
	RETURN (
		SELECT Pessoa.Identificador, Nome
		FROM Projeto.Pessoa JOIN Projeto.WorksOn ON WorksOn.Identificador = Pessoa.Identificador
		WHERE WorksOn.[Role] LIKE '%' + @Role_ator + '%'
	)
GO
--FILTER ACTOR BY FILM NAME
CREATE FUNCTION filme_ator (@Filme_ator VARCHAR(256)) RETURNS TABLE
AS
	RETURN (
		SELECT Pessoa.Identificador, Pessoa.Nome
		FROM Projeto.Filme JOIN Projeto.WorksOn ON Filme.FilmeNome = WorksOn.FilmeNome
		JOIN Projeto.Pessoa ON Projeto.WorksOn.Identificador = Projeto.Pessoa.Identificador
		WHERE Filme.FilmeNome LIKE '%' + @Filme_ator + '%'
	)
GO
--FILTER ACTOR BY PRIZE
CREATE FUNCTION prizes_ator (@Prizes_ator VARCHAR(256)) RETURNS TABLE
AS
	RETURN (
		SELECT Pessoa.Identificador, Pessoa.Nome
		FROM Projeto.PrizesTo JOIN Projeto.Pessoa ON PrizesTo.Identificador = Pessoa.Identificador
		JOIN Projeto.Prizes ON Prizes.ID = PrizesTo.ID
		WHERE Prizes.Prize LIKE '%' + @Prizes_ator + '%'
	)
GO

--ALL MOVIES
CREATE VIEW all_movies AS
		SELECT Filme.FilmeNome, Filme.StudioNome, Ranking, Ano, Ganho, Duracao, Nome AS 'Publico Alvo'
		FROM Projeto.Filme JOIN Projeto.PublicoAlvo ON ID_Publico_Alvo = Identificador
GO

--ALL STATS OF FILM
CREATE PROCEDURE filme_geral
	@nomefilme VARCHAR(256),
	@nomestudio VARCHAR(256),
	@ano DATE OUTPUT,
	@dur INT OUTPUT,
	@publico VARCHAR(256) OUTPUT,
	@rank INT OUTPUT,
	@ganho INT OUTPUT
AS
	SELECT @ano = Ano, @dur = Duracao, @publico = Nome, @ganho = Ganho, @rank = Ranking 
	FROM Projeto.Filme JOIN Projeto.PublicoAlvo ON ID_Publico_Alvo = Identificador
	WHERE FilmeNome LIKE '%'+@nomefilme+'%' AND StudioNome LIKE '%'+@nomestudio+'%';
GO

--TRIGGER SO WE DONT INSERT PRIZE WITH NO KNOWLEDGE OF ROLE
CREATE TRIGGER role_prize ON Projeto.PrizesTo
AFTER INSERT, UPDATE
AS
	SET NOCOUNT ON;
	DECLARE @role AS VARCHAR(256)
	DECLARE @prize_id AS INT
	DECLARE @filme AS VARCHAR(256)
	DECLARE @studio AS VARCHAR(256)
	DECLARE @id AS INT
	SELECT @prize_id = ID, @studio = StudioNome, @filme = FilmeNome, @id = Identificador FROM inserted;
	IF @prize_id IS NOT NULL
	BEGIN
		DECLARE @prize VARCHAR(256)
		SELECT @prize = Prize FROM Projeto.Prizes WHERE ID = @prize_id
		IF @role LIKE '%Main Actor%' AND @prize NOT LIKE '%Main Actor%'
		BEGIN
			RAISERROR ('Erro no prize', 16,1);
			ROLLBACK TRAN; -- Anula a inserção
		END
		ELSE IF @role LIKE '%Secondary Actor%' AND @prize NOT LIKE '%Secondary Actor%'
		BEGIN
			RAISERROR ('Erro no prize', 16, 1);
			ROLLBACK TRAN; -- Anula a inserção
		END
	END
GO
drop trigger role_prize

--TOTAL SPENT - 2 CURSORS (1 FOR PROPS, 1 FOR ACTOR PAYMENT)
CREATE FUNCTION money_movie (@filme VARCHAR(256), @studio VARCHAR(256)) RETURNS @Money TABLE
(
	StudioNome VARCHAR(256), 
	FilmeNome VARCHAR(256), 
	Gasto INT, 
	Ganho_filme INT, 
	Diferenca INT
)  
AS 
BEGIN 
	DECLARE @price_aderecos AS INT, 
			@quantidade AS INT, 
			@quantia AS INT, 
			@total_budget AS INT,
			@temp AS INT,
			@Gasto INT,
			@Ganho_filme INT,
			@Diferenca INT;
 
    DECLARE c_aderecos CURSOR 
    FAST_FORWARD 
    FOR SELECT Price, Quantidade 
        FROM (Projeto.Adereco JOIN Projeto.Vende ON Adereco=Codigo)
		JOIN Projeto.UsadoEm ON Projeto.Adereco.Codigo = Projeto.UsadoEm.Codigo
        WHERE Projeto.UsadoEm.FilmeNome = @filme AND Projeto.UsadoEm.StudioNome = @studio;
 
	OPEN c_aderecos; 
	FETCH c_aderecos INTO @price_aderecos, @quantidade;
	SELECT @total_budget = 0;
	SELECT @Ganho_filme = Ganho FROM Projeto.Filme 
	WHERE @filme = FilmeNome AND @studio = StudioNome
	INSERT INTO @money VALUES(@studio, @filme, @total_budget, @Ganho_filme, @Diferenca);

	WHILE @@fetch_status = 0 
	BEGIN
		UPDATE @money SET Gasto += @total_budget; 
		SET @total_budget += @price_aderecos * @quantidade;
		FETCH c_aderecos INTO @price_aderecos, @quantidade; 
	END 
	CLOSE c_aderecos; 
	DEALLOCATE c_aderecos;

	DECLARE c_atores CURSOR 
    FAST_FORWARD 
    FOR SELECT Quantia
        FROM Projeto.Pagamento JOIN Projeto.Faz ON Referencia = PagReferencia
		WHERE FilmeNome = @filme AND StudioNome = @studio; --nome filme + studio nome
 
	OPEN c_atores; 
	FETCH c_atores INTO @quantia;
	SET @total_budget = 0
	WHILE @@fetch_status = 0 
	BEGIN
		UPDATE @money SET Gasto += @total_budget;
		SET @total_budget += @quantia;
		FETCH c_atores INTO @quantia;
	END
	CLOSE c_atores; 
	DEALLOCATE c_atores;

	UPDATE @money SET Diferenca = Ganho_filme - Gasto;

	RETURN; 
END
GO

--VERIFICATION OF LOGIN
CREATE PROCEDURE ValidationUser 
		@user VARCHAR(256),
		@pass VARCHAR(256)
AS
	DECLARE @A INT
	IF EXISTS (
		SELECT * FROM Projeto.Users 
			WHERE @user = username AND DECRYPTBYPASSPHRASE('ThePassPhrase', [password]) = @pass
		)
	BEGIN
		SET @A = 1
	END
	ELSE
	BEGIN
		SET @A = 0
	END
	RETURN @A
GO

--EDIT ACTOR
CREATE PROCEDURE EditActor
	@id INT,
	@nome VARCHAR(256),
	@role VARCHAR(256),
	@filme VARCHAR(256),
	@studio VARCHAR(256),
	@pay INT,
	@prize VARCHAR(256)
AS
BEGIN TRANSACTION
	BEGIN TRY
		UPDATE Projeto.Pessoa SET Nome = @nome WHERE Identificador = @id
		UPDATE Projeto.WorksOn SET [Role] = @role 
			WHERE Identificador = @id AND @filme = FilmeNome AND @studio = StudioNome
		DECLARE @prizes INT
		SELECT @prizes = Prizes.ID FROM Projeto.Prizes JOIN Projeto.PrizesTo ON Prizes.ID = PrizesTo.ID WHERE Prize LIKE '%' + @prize + '%'
		UPDATE Projeto.PrizesTo SET ID = @prizes 
			WHERE Identificador = @id AND @filme = FilmeNome AND @studio = StudioNome
		DECLARE @ref INT
		SELECT @ref = Referencia FROM Projeto.Pagamento JOIN Projeto.Faz ON Referencia = PagReferencia
			WHERE Pessoa_ID = @id AND @filme = FilmeNome AND @studio = StudioNome
		UPDATE Projeto.Pagamento SET Quantia = @pay 
			WHERE @ref = Referencia
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH
COMMIT
GO
drop proc editactor

--GET ROLE AND PRIZE
CREATE PROCEDURE WhichRole
	@filme VARCHAR(256),
	@studio VARCHAR(256),
	@ID INT,
	@Role VARCHAR(256) OUTPUT,
	@Prize VARCHAR(256) OUTPUT,
	@Pay INT OUTPUT
AS
	SELECT @role = [Role], @pay = Quantia FROM Projeto.WorksOn JOIN Projeto.Faz ON Faz.FilmeNome = WorksOn.FilmeNome AND Faz.StudioNome = WorksOn.StudioNome
	JOIN Projeto.Pagamento ON Referencia = PagReferencia AND Pessoa_ID = Identificador
	WHERE @filme = WorksOn.FilmeNome AND @studio = WorksOn.StudioNome AND @id = WorksOn.Identificador

	SELECT @prize = Prize FROM Projeto.WorksOn JOIN Projeto.PrizesTo 
	ON PrizesTo.Identificador = WorksOn.Identificador AND PrizesTo.FilmeNome = WorksOn.FilmeNome 
	AND PrizesTo.StudioNome = WorksOn.StudioNome
	JOIN Projeto.Prizes ON Prizes.ID = PrizesTo.ID
	WHERE @filme = WorksOn.FilmeNome AND @studio = WorksOn.StudioNome AND @id = WorksOn.Identificador
GO

--ADD PRIZE AND PAY TO ACTOR
CREATE PROCEDURE addprizepay
		@nome VARCHAR(256),
		@prize VARCHAR(256),
		@pay INT,
		@filme VARCHAR(256),
		@studio VARCHAR(256)
AS
	DECLARE @ID INT
	DECLARE @prize_id INT
	IF EXISTS (SELECT Identificador FROM Projeto.Pessoa WHERE @nome = Nome)
	BEGIN
		SELECT @ID = Identificador FROM Projeto.Pessoa WHERE @nome = Nome
		IF @prize != ''
		BEGIN
			SELECT @prize_id = ID FROM Projeto.Prizes WHERE Prize LIKE '%'+@prize+'%'
			IF @prize_id IS NOT NULL
			BEGIN
				INSERT Projeto.PrizesTo VALUES (@filme, @studio, @ID, @prize_id)
			END
		END
		INSERT Projeto.Pagamento VALUES (@pay, @ID)
		SELECT @ID = IDENT_CURRENT('Projeto.Pagamento')
		INSERT Projeto.Faz VALUES (@ID, @filme, @studio)
	END
	ELSE
	BEGIN
		INSERT Projeto.Pessoa VALUES(@nome)
		SELECT @ID = IDENT_CURRENT('Projeto.Pessoa')
		IF @prize != ''
		BEGIN
			SELECT @prize_id = ID FROM Projeto.Prizes WHERE Prize LIKE '%'+@prize+'%'
			IF @prize_id IS NOT NULL
			BEGIN
				INSERT Projeto.PrizesTo VALUES (@filme, @studio, @ID, @prize_id)
			END
		END
		INSERT Projeto.Pagamento VALUES (@pay, @ID)
		SELECT @ID = IDENT_CURRENT('Projeto.Pagamento')
		INSERT Projeto.Faz VALUES (@ID, @filme, @studio)
	END
GO
drop proc addprizepay
--ADD GENRE TO MOVIE
CREATE PROCEDURE addgenre
		@Genero VARCHAR(256),
		@filme VARCHAR(256),
		@studio VARCHAR(256)
AS
	DECLARE @ID INT
	SELECT @ID = ID FROM Projeto.Genero WHERE Genero = @Genero
	BEGIN TRY
		INSERT INTO Projeto.GeneroTo VALUES(@ID, @filme, @studio)
		END TRY
	BEGIN CATCH
		RAISERROR ('Erro add Genre', 16, 1)
	END CATCH
GO

--SEE AVG AND COUNT OF PAYMENT BY STUDIO
CREATE FUNCTION avgStudio (
	@studio VARCHAR(256)
) RETURNS TABLE
AS
	RETURN (
		SELECT AVG(Quantia) AS 'AVG Quantia', COUNT(*) AS 'Numero Pagamentos' 
		FROM  Projeto.Faz JOIN Projeto.Pagamento ON Referencia = PagReferencia
		WHERE Faz.StudioNome = @studio
	)
GO
