--CRIANDO BANCO DE DADOS 
CREATE DATABASE T_Peoples;

--USADO BANCO DE DADOS
USE T_Peoples;

--CRIANDO TABELA DO BANCO DE DADOS
CREATE TABLE TipoUsuario(
	IdTipoUsuario INT PRIMARY KEY IDENTITY,
	Titulo VARCHAR(255) NOT NULL,
);

CREATE TABLE Usuario(
	IdUsuario INT PRIMARY KEY IDENTITY,
	Nome VARCHAR(100) NOT NULL,
	Sobrenome VARCHAR(100) NOT NULL,
	DataNascimento DATE,
	Email VARCHAR(100) NOT NULL,
	Senha VARCHAR(100) NOT NULL,
	IdTipoUsuario INT FOREIGN KEY REFERENCES TipoUsuario(IdTipoUsuario)
);

INSERT INTO TipoUsuario(Titulo)
VALUES('Comum'),
('Funcionario'),
('Administrador');

INSERT INTO Usuario(Nome,Sobrenome,DataNascimento,Email,Senha,IdTipoUsuario)
VALUES('Catarina','Strada','20/02/2000','catarina@gmail.com','123',2),
('Tadeu','Vitelli','12/08/1998','tadeu@gmail.com','456',2);