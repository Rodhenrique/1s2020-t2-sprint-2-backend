--CRIANDO BANCO DE DADOS 
CREATE DATABASE T_Peoples;

--USADO BANCO DE DADOS
USE T_Peoples;

--CRIANDO TABELA DO BANCO DE DADOS

CREATE TABLE Funcionarios(
	IdFuncionario INT PRIMARY KEY IDENTITY,
	Nome VARCHAR(100) NOT NULL,
	Sobrenome VARCHAR(100) NOT NULL
);