--different film studios
INSERT INTO Projeto.FilmeStudio VALUES ('Marvel');

--different targeted public
INSERT INTO Projeto.PublicoAlvo VALUES ('8+');
INSERT INTO Projeto.PublicoAlvo VALUES ('10+');
INSERT INTO Projeto.PublicoAlvo VALUES ('12+');
INSERT INTO Projeto.PublicoAlvo VALUES ('14+');
INSERT INTO Projeto.PublicoAlvo VALUES ('16+');
INSERT INTO Projeto.PublicoAlvo VALUES ('18+');

--film 1: spider-man: far from home
INSERT INTO Projeto.Filme VALUES 
	('Marvel', 'SPIDER-MAN: Far From Home', 4, 129, 7.5, 160000000, '2019-07-02');

INSERT INTO Projeto.Ve VALUES (4, 'Marvel', 'SPIDER-MAN: Far From Home');

INSERT INTO Projeto.Genero VALUES ('Sci-Fi', 'SPIDER-MAN: Far From Home', 'Marvel');
INSERT INTO Projeto.Genero VALUES ('Action', 'SPIDER-MAN: Far From Home', 'Marvel');

INSERT INTO Projeto.Localizacao VALUES ('Hertfordshire - England');
INSERT INTO Projeto.Localizacao VALUES ('Prague - Czech Republic');
INSERT INTO Projeto.Localizacao VALUES ('New York City - Unites State of America');

INSERT INTO Projeto.FilmaEm VALUES 
	('SPIDER-MAN: Far From Home', 'Marvel', 'Hertfordshire - England', '2018-07-02');
INSERT INTO Projeto.FilmaEm VALUES 
	('SPIDER-MAN: Far From Home', 'Marvel', 'Prague - Czech Republic', '2018-09-10');
INSERT INTO Projeto.FilmaEm VALUES 
	('SPIDER-MAN: Far From Home', 'Marvel', 'New York City - Unites State of America', '2018-10-18');

INSERT INTO Projeto.Pessoa (Nome) 
		VALUES ('Tom Holland');
INSERT INTO Projeto.Pessoa (Nome)
		VALUES ('Zendaya');
INSERT INTO Projeto.Pessoa (Nome)
		VALUES ('Samuel L. Jackson');
INSERT INTO Projeto.Pessoa (Nome)
		VALUES ('Jake Gyllenhaal');

INSERT INTO Projeto.WorksOn (FilmeNome, StudioNome, [Role], Identificador)
		VALUES ('SPIDER-MAN: Far From Home', 'Marvel', 'Main Actor - SpiderMan', 100);
INSERT INTO Projeto.WorksOn (FilmeNome, StudioNome, [Role], Identificador)
		VALUES ('SPIDER-MAN: Far From Home', 'Marvel', 'Secondary Actor - MJ', 101);
INSERT INTO Projeto.WorksOn (FilmeNome, StudioNome, [Role], Identificador)
		VALUES ('SPIDER-MAN: Far From Home', 'Marvel', 'Secondary Actor - Nick Fury', 102);
INSERT INTO Projeto.WorksOn (FilmeNome, StudioNome, [Role], Identificador)
		VALUES ('SPIDER-MAN: Far From Home', 'Marvel', 'Main Actor - Mysterio', 103);

INSERT INTO Projeto.Adereco VALUES ('SpiderMan Suit', 580);
INSERT INTO Projeto.Adereco VALUES ('Mysterio Suit', 400);
INSERT INTO Projeto.Adereco VALUES ('SpaceShip', 12000);
INSERT INTO Projeto.Adereco VALUES ('Tony Starks Glasses', 100);

INSERT INTO Projeto.UsadoEm VALUES (1, 'SPIDER-MAN: Far From Home', 'Marvel');
INSERT INTO Projeto.UsadoEm VALUES (2, 'SPIDER-MAN: Far From Home', 'Marvel');
INSERT INTO Projeto.UsadoEm VALUES (3, 'SPIDER-MAN: Far From Home', 'Marvel');
INSERT INTO Projeto.UsadoEm VALUES (4, 'SPIDER-MAN: Far From Home', 'Marvel');

INSERT INTO Projeto.Fornecedor VALUES ('Fornecedor1', 'Marca1');
INSERT INTO Projeto.Fornecedor VALUES ('Fornecedor2', 'Marca2');
INSERT INTO Projeto.Fornecedor VALUES ('Fornecedor3', 'Marca3');

INSERT INTO Projeto.Vende VALUES (1, 1000, 5);
INSERT INTO Projeto.Vende VALUES (2, 1001, 1);
INSERT INTO Projeto.Vende VALUES (3, 1002, 1);
INSERT INTO Projeto.Vende VALUES (4, 1001, 2);

INSERT INTO Projeto.Armazem VALUES (1, 'Newark, New Jersey');
INSERT INTO Projeto.Armazem VALUES (2, 'Hertfordshire');

INSERT INTO Projeto.Guarda VALUES (1, 1, 5);
INSERT INTO Projeto.Guarda VALUES (2, 1, 1);
INSERT INTO Projeto.Guarda VALUES (3, 2, 1);
INSERT INTO Projeto.Guarda VALUES (4, 1, 2);

INSERT INTO Projeto.Pagamento VALUES (1500000, 100); --spiderman
INSERT INTO Projeto.Pagamento VALUES (8000000, 101); --mj
INSERT INTO Projeto.Pagamento VALUES (9000000, 102); --nick
INSERT INTO Projeto.Pagamento VALUES (9000000, 103); --mysterio

INSERT INTO Projeto.Faz VALUES (100000000, 'SPIDER-MAN: Far From Home', 'Marvel');
INSERT INTO Projeto.Faz VALUES (100000001, 'SPIDER-MAN: Far From Home', 'Marvel');
INSERT INTO Projeto.Faz VALUES (100000002, 'SPIDER-MAN: Far From Home', 'Marvel');
INSERT INTO Projeto.Faz VALUES (100000003, 'SPIDER-MAN: Far From Home', 'Marvel');

--film 2: avengers: endgame
INSERT INTO Projeto.Filme VALUES 
	('Marvel', 'Avengers: Endgame', 5, 181, 8.9, 380000000, '2019-04-26');

INSERT INTO Projeto.Ve VALUES (5, 'Marvel', 'Avengers: Endgame');

INSERT INTO Projeto.Genero VALUES ('Sci-Fi', 'Avengers: Endgame', 'Marvel');
INSERT INTO Projeto.Genero VALUES ('Action', 'Avengers: Endgame', 'Marvel');
INSERT INTO Projeto.Genero VALUES ('Drama', 'Avengers: Endgame', 'Marvel');

INSERT INTO Projeto.Localizacao VALUES ('Atlanta - Unites State of America');
INSERT INTO Projeto.Localizacao VALUES ('Durham Cathedral - England');
INSERT INTO Projeto.Localizacao VALUES ('Tokyo - Japan');

INSERT INTO Projeto.FilmaEm VALUES 
	('Avengers: Endgame', 'Marvel', 'Atlanta - Unites State of America', '2018-01-04');
INSERT INTO Projeto.FilmaEm VALUES 
	('Avengers: Endgame', 'Marvel', 'Durham Cathedral - England', '2017-09-21');
INSERT INTO Projeto.FilmaEm VALUES 
	('Avengers: Endgame', 'Marvel', 'Tokyo - Japan', '2017-11-10');
INSERT INTO Projeto.FilmaEm VALUES 
	('Avengers: Endgame', 'Marvel', 'New York City - Unites State of America', '2018-02-15');

INSERT INTO Projeto.Pessoa (Nome)
		VALUES ('Robert Downey Jr.');

INSERT INTO Projeto.WorksOn (FilmeNome, StudioNome, [Role], Identificador)
		VALUES ('Avengers: Endgame', 'Marvel', 'Secondary Actor - SpiderMan', 100);
INSERT INTO Projeto.WorksOn (FilmeNome, StudioNome, [Role], Identificador)
		VALUES ('Avengers: Endgame', 'Marvel', 'Main Actor - IronMan', 104);

INSERT INTO Projeto.Adereco VALUES ('Backpack', 10);
INSERT INTO Projeto.Adereco VALUES ('Infinity Gauntlet', 140);
INSERT INTO Projeto.Adereco VALUES ('IronMan Suit', 380);

INSERT INTO Projeto.UsadoEm VALUES (5, 'Avengers: Endgame', 'Marvel');
INSERT INTO Projeto.UsadoEm VALUES (6, 'Avengers: Endgame', 'Marvel');
INSERT INTO Projeto.UsadoEm VALUES (7, 'Avengers: Endgame', 'Marvel');
INSERT INTO Projeto.UsadoEm VALUES (1, 'Avengers: Endgame', 'Marvel');
INSERT INTO Projeto.UsadoEm VALUES (3, 'Avengers: Endgame', 'Marvel');

INSERT INTO Projeto.Vende VALUES (5, 1000, 3);
INSERT INTO Projeto.Vende VALUES (6, 1001, 2);
INSERT INTO Projeto.Vende VALUES (7, 1002, 1);

INSERT INTO Projeto.Guarda VALUES (5, 1, 3);
INSERT INTO Projeto.Guarda VALUES (6, 2, 2);
INSERT INTO Projeto.Guarda VALUES (7, 2, 1);

INSERT INTO Projeto.Pagamento VALUES (7000000, 100);  --spiderman
INSERT INTO Projeto.Pagamento VALUES (15000000, 104); --ironman

INSERT INTO Projeto.Faz VALUES (100000004, 'Avengers: Endgame', 'Marvel');
INSERT INTO Projeto.Faz VALUES (100000005, 'Avengers: Endgame', 'Marvel');


--film 3: avengers: endgame
INSERT INTO Projeto.Filme VALUES 
	('Marvel', 'Avengers: Infinity War', 5, 149, 8.7, 2048000000, '2018-04-23');

INSERT INTO Projeto.Ve VALUES (5, 'Marvel', 'Avengers: Infinity War');

INSERT INTO Projeto.Genero VALUES ('Sci-Fi', 'Avengers: Infinity War', 'Marvel');
INSERT INTO Projeto.Genero VALUES ('Action', 'Avengers: Infinity War', 'Marvel');
INSERT INTO Projeto.Genero VALUES ('Drama', 'Avengers: Infinity War', 'Marvel');

INSERT INTO Projeto.Localizacao VALUES ('Downtown Atlanta - Unites State of America');
INSERT INTO Projeto.Localizacao VALUES ('Glasgow - Scotland');
INSERT INTO Projeto.Localizacao VALUES ('Queens - Unites State of America');

INSERT INTO Projeto.FilmaEm VALUES 
	('Avengers: Infinity War', 'Marvel', 'Downtown Atlanta - Unites State of America', '2018-01-04');
INSERT INTO Projeto.FilmaEm VALUES 
	('Avengers: Infinity War', 'Marvel', 'Atlanta - Unites State of America', '2018-01-04');
INSERT INTO Projeto.FilmaEm VALUES 
	('Avengers: Infinity War', 'Marvel', 'Glasgow - Scotland', '2017-09-21');
INSERT INTO Projeto.FilmaEm VALUES 
	('Avengers: Infinity War', 'Marvel', 'Queens - Unites State of America', '2017-11-10');
INSERT INTO Projeto.FilmaEm VALUES 
	('Avengers: Infinity War', 'Marvel', 'New York City - Unites State of America', '2018-02-15');

INSERT INTO Projeto.Pessoa (Nome)
		VALUES ('Chris Pratt');
INSERT INTO Projeto.Pessoa (Nome)
		VALUES ('Chris Evans');
INSERT INTO Projeto.Pessoa (Nome)
		VALUES ('Chris Hemsworth');

INSERT INTO Projeto.WorksOn (FilmeNome, StudioNome, [Role], Identificador)
		VALUES ('Avengers: Infinity War', 'Marvel', 'Secondary Actor - SpiderMan', 100);
INSERT INTO Projeto.WorksOn (FilmeNome, StudioNome, [Role], Identificador)
		VALUES ('Avengers: Infinity War', 'Marvel', 'Main Actor - IronMan', 104);
INSERT INTO Projeto.WorksOn (FilmeNome, StudioNome, [Role], Identificador)
		VALUES ('Avengers: Infinity War', 'Marvel', 'Secondary Actor - Star Lord', 105);
INSERT INTO Projeto.WorksOn (FilmeNome, StudioNome, [Role], Identificador)
		VALUES ('Avengers: Infinity War', 'Marvel', 'Main Actor - Captain America', 106);
INSERT INTO Projeto.WorksOn (FilmeNome, StudioNome, [Role], Identificador)
		VALUES ('Avengers: Infinity War', 'Marvel', 'Main Actor - Thor Odinson', 107);

INSERT INTO Projeto.Adereco VALUES ('Captain America`s Shield', 270);
INSERT INTO Projeto.Adereco VALUES ('Thor`s Hammer', 198);

INSERT INTO Projeto.UsadoEm VALUES (5, 'Avengers: Infinity War', 'Marvel');
INSERT INTO Projeto.UsadoEm VALUES (6, 'Avengers: Infinity War', 'Marvel');
INSERT INTO Projeto.UsadoEm VALUES (7, 'Avengers: Infinity War', 'Marvel');
INSERT INTO Projeto.UsadoEm VALUES (1, 'Avengers: Infinity War', 'Marvel');
INSERT INTO Projeto.UsadoEm VALUES (3, 'Avengers: Infinity War', 'Marvel');
INSERT INTO Projeto.UsadoEm VALUES (8, 'Avengers: Infinity War', 'Marvel');
INSERT INTO Projeto.UsadoEm VALUES (9, 'Avengers: Infinity War', 'Marvel');

INSERT INTO Projeto.Vende VALUES (8, 1000, 2);
INSERT INTO Projeto.Vende VALUES (9, 1001, 3);

INSERT INTO Projeto.Guarda VALUES (8, 1, 2);
INSERT INTO Projeto.Guarda VALUES (9, 2, 3);

INSERT INTO Projeto.Pagamento VALUES (8000000, 105);   --starlord
INSERT INTO Projeto.Pagamento VALUES (13000000, 106);  --cap
INSERT INTO Projeto.Pagamento VALUES (12000000, 107);  --thor
INSERT INTO Projeto.Pagamento VALUES (12000000, 100);  --spiderman 
INSERT INTO Projeto.Pagamento VALUES (12000000, 104);  --ironman

INSERT INTO Projeto.Faz VALUES (100000006, 'Avengers: Infinity War', 'Marvel');
INSERT INTO Projeto.Faz VALUES (100000007, 'Avengers: Infinity War', 'Marvel');
INSERT INTO Projeto.Faz VALUES (100000008, 'Avengers: Infinity War', 'Marvel');
INSERT INTO Projeto.Faz VALUES (100000009, 'Avengers: Infinity War', 'Marvel');
INSERT INTO Projeto.Faz VALUES (100000010, 'Avengers: Infinity War', 'Marvel');

--film 4: The Avengers
INSERT INTO Projeto.Filme VALUES 
	('Marvel', 'The Avengers', 3, 143, 7.6, 1519000000, '2012-04-11');

INSERT INTO Projeto.Ve VALUES (3, 'Marvel', 'The Avengers');

INSERT INTO Projeto.Genero VALUES ('Sci-Fi', 'The Avengers', 'Marvel');
INSERT INTO Projeto.Genero VALUES ('Action', 'The Avengers', 'Marvel');
INSERT INTO Projeto.Genero VALUES ('Fantasy', 'The Avengers', 'Marvel');

INSERT INTO Projeto.Localizacao VALUES ('Manhattan Beach - Unites State of America');
INSERT INTO Projeto.Localizacao VALUES ('Brooklin - Unites State of America');

INSERT INTO Projeto.FilmaEm VALUES 
	('The Avengers', 'Marvel', 'Manhattan Beach - Unites State of America', '2018-01-04');
INSERT INTO Projeto.FilmaEm VALUES 
	('The Avengers', 'Marvel', 'Atlanta - Unites State of America', '2018-01-04');
INSERT INTO Projeto.FilmaEm VALUES 
	('The Avengers', 'Marvel', 'Brooklin - Unites State of America', '2017-11-10');
INSERT INTO Projeto.FilmaEm VALUES 
	('The Avengers', 'Marvel', 'New York City - Unites State of America', '2018-02-15');

INSERT INTO Projeto.Pessoa (Nome)
		VALUES ('Scarlett Johansson');

INSERT INTO Projeto.WorksOn (FilmeNome, StudioNome, [Role], Identificador)
		VALUES ('The Avengers', 'Marvel', 'Main Actor - Black Widow', 108);
INSERT INTO Projeto.WorksOn (FilmeNome, StudioNome, [Role], Identificador)
		VALUES ('The Avengers', 'Marvel', 'Main Actor - IronMan', 104);
INSERT INTO Projeto.WorksOn (FilmeNome, StudioNome, [Role], Identificador)
		VALUES ('The Avengers', 'Marvel', 'Main Actor - Captain America', 106);
INSERT INTO Projeto.WorksOn (FilmeNome, StudioNome, [Role], Identificador)
		VALUES ('The Avengers', 'Marvel', 'Main Actor - Thor Odinson', 107);

INSERT INTO Projeto.UsadoEm VALUES (5, 'The Avengers', 'Marvel');
INSERT INTO Projeto.UsadoEm VALUES (6, 'The Avengers', 'Marvel');
INSERT INTO Projeto.UsadoEm VALUES (7, 'The Avengers', 'Marvel');
INSERT INTO Projeto.UsadoEm VALUES (1, 'The Avengers', 'Marvel');
INSERT INTO Projeto.UsadoEm VALUES (3, 'The Avengers', 'Marvel');
INSERT INTO Projeto.UsadoEm VALUES (8, 'The Avengers', 'Marvel');
INSERT INTO Projeto.UsadoEm VALUES (9, 'The Avengers', 'Marvel');

INSERT INTO Projeto.Pagamento VALUES (9000000, 108);   --blackwidow
INSERT INTO Projeto.Pagamento VALUES (10000000, 106);  --cap
INSERT INTO Projeto.Pagamento VALUES (10000000, 107);  --thor
INSERT INTO Projeto.Pagamento VALUES (11000000, 104);  --ironman

INSERT INTO Projeto.Faz VALUES (100000011, 'The Avengers', 'Marvel');
INSERT INTO Projeto.Faz VALUES (100000012, 'The Avengers', 'Marvel');
INSERT INTO Projeto.Faz VALUES (100000013, 'The Avengers', 'Marvel');
INSERT INTO Projeto.Faz VALUES (100000014, 'The Avengers', 'Marvel');
