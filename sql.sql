-- Active: 1768478254505@@127.0.0.1@3306
-- Database: postgres

-- DROP DATABASE IF EXISTS postgres;

DROP TABLE IF EXISTS cliente;
DROP TABLE IF EXISTS funcionario;
DROP TABLE IF EXISTS produto;

CREATE TABLE cliente (
	nome varchar(30),
	cpf text,
	conta int,
	isvip bool
	
);
CREATE TABLE  funcionario(
	nome varchar(30),
	cpf text,
	isadmin bool,
	quantidade_atestado int,
	nascimento int
	--nascimento year
	
);
CREATE TABLE produto(
	nome varchar(30),
	codigo int,
	quantidade int,
	valor_revenda real,
	lote int 
	
)