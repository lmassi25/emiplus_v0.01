﻿/*
**********************************************
************** BASE SQL CREATE ***************
**********************************************

CREATE TABLE EXAMPLE
(
	id integer generated by default as identity primary key,
	id_empresa varchar(255),
	excluir integer,
	criado timestamp,
	atualizado timestamp,
	deletado timestamp
);
*/


CREATE TABLE PESSOA
(
	id integer generated by default as identity primary key,
	id_empresa varchar(255),
	tipo varchar(255) not null,
	excluir integer,
	criado timestamp,
	atualizado timestamp,
	deletado timestamp,
	padrao integer,
	nome varchar(255),
	fantasia varchar(255),
	rg varchar(50),
	cpf varchar(50),
	aniversario date,
	pessoatipo varchar(255),
	isento integer,
	transporte_placa varchar(255),
	transporte_uf varchar(10),
	transporte_rntc varchar(255)
);

CREATE TABLE PESSOA_CONTATO
(
	id integer generated by default as identity primary key,
	id_pessoa integer not null,
	id_empresa varchar(255),
	excluir integer,
	criado timestamp,
	atualizado timestamp,
	deletado timestamp,
	contato varchar(255),
	telefone varchar(255),
	celular varchar(255),
	email varchar(255)
);

CREATE TABLE PESSOA_ENDERECO
(
	id integer generated by default as identity primary key,
	id_pessoa integer not null,
	id_empresa varchar(255),
	excluir integer,
	criado timestamp,
	atualizado timestamp,
	deletado timestamp,
	cep varchar(255),
	estado varchar(255),
	cidade varchar(255),
	rua varchar(255),
	nr varchar(255),
	bairro varchar(255),
	complemento varchar(255),
	pais varchar(255),
	ibge varchar(255)
);

CREATE TABLE PEDIDO
(
	id integer generated by default as identity primary key,
	id_empresa varchar(255),
	tipo varchar(255) not null,
	excluir integer,
	criado timestamp,
	atualizado timestamp,
	deletado timestamp,
	cliente integer,
	colaborador integer,
	total numeric(18, 4),
	desconto numeric(18, 4),
	frete numeric(18, 4),
	produtos numeric(18, 4),
	icms numeric(18, 4),
	icmsst numeric(18, 4),
	ipi numeric(18, 4),
	icmsbase numeric(18, 4),
	icmsstbase numeric(18, 4),
	pis numeric(18, 4),
	cofins numeric(18, 4)
);

CREATE TABLE PEDIDO_ITEM
(
	id integer generated by default as identity primary key,
	id_empresa varchar(255),
	tipo varchar(255) not null,
	excluir integer,
	criado timestamp,
	atualizado timestamp,
	deletado timestamp,
	pedido integer,
	item integer,
	cprod varchar(255),
	cean varchar(255),
	xprod varchar(255),
	ncm varchar(255),
	cfop varchar(255),
	origem varchar(255),
	valorcompra numeric(18,4),
	valorvenda numeric(18,4),
	quantidade numeric(18,4),
	medida varchar(255),
	total numeric(18,4),
	desconto numeric(18,4),
	descontoitem numeric(18,4),
	descontopedido numeric(18,4),
	frete numeric(18,4),
	totalcompra numeric(18,4),
	totalvenda numeric(18,4),
	icms numeric(18,4),
	icmsbase numeric(18,4),
	icmsreducaoaliq numeric(18,4),
	icmsbasecomreducao numeric(18,4),
	icmsaliq numeric(18,4),
	icmsvlr numeric(18,4),
	icmsstbase numeric(18,4),
	icmsstreducaoaliq numeric(18,4),
	icmsstbasecomreducao numeric(18,4),
	icmsstaliq numeric(18,4),
	icmsst numeric(18,4),
	icmsstvlr numeric(18, 4),
	ipi numeric(18,4),
	ipialiq numeric(18,4),
	ipivlr numeric(18,4),
	pis numeric(18,4),
	pisaliq numeric(18,4),
	pisvlr numeric(18,4),
	cofins numeric(18,4),
	cofinsaliq numeric(18,4),
	cofinsvlr numeric(18,4)
);

CREATE TABLE CATEGORIA
(
	id integer generated by default as identity primary key,
	id_empresa varchar(255),
	excluir integer,
	criado timestamp,
	atualizado timestamp,
	deletado timestamp,
	Nome varchar(255),
	tipo varchar(255)
);

CREATE TABLE ITEM
(
	id integer generated by default as identity primary key,
	id_empresa varchar(255),
	excluir integer,
	criado timestamp,
	atualizado timestamp,
	deletado timestamp,
	Nome varchar(255),
	referencia varchar(255),
	valorcompra numeric(18,4),
	valorvenda numeric(18,4),
	estoqueminimo numeric(18,4),
	estoqueatual numeric(18,4),
	categoriaid integer,
	medida varchar(255)
);

CREATE TABLE ITEM_MOV_ESTOQUE
(
	id integer generated by default as identity primary key,
	id_item integer not null,
	id_empresa varchar(255),
	id_usuario integer,
	excluir integer,
	criado timestamp,
	atualizado timestamp,
	deletado timestamp,
	quantidade numeric(18,4),
	observacao varchar(255),
	tipo varchar(10),
	local varchar(255)
);

CREATE TABLE IMPOSTO
(
    id integer generated by default as identity primary key,
	id_empresa varchar(255),
    tipo varchar(255),
    excluir integer,
    criado timestamp,
    atualizado timestamp,
    deletado timestamp,
    nome varchar(255),
    icms varchar(10),
    IcmsReducaoAliq numeric(18,4),
    IcmsAliq numeric(18,4),
    IcmsStIva numeric(18,4),
    IcmsStReducaoAliq numeric(18,4),
    IcmsStAliq numeric(18,4),
    Ipi varchar(10),
    IpiAliq numeric(18,4),
    Pis varchar(10),
    PisAliq numeric(18,4),
    Cofins varchar(10),
    CofinsAliq numeric(18,4)
	Cfop varchar(255),
	IcmsIVA numeric(18,4)
);

CREATE TABLE FORMAPGTO
(
    id integer generated by default as identity primary key,
	id_empresa varchar(255),
    excluir integer,
    criado timestamp,
    atualizado timestamp,
    deletado timestamp,
    nome varchar(255)
);

INSERT INTO FORMAPGTO (EXCLUIR, CRIADO, NOME) VALUES (0, '18.09.2019 14:59:00', 'DINHEIRO');

CREATE TABLE TITULO
(
    id integer generated by default as identity primary key,
	id_empresa varchar(255),
	tipo varchar(255),
    excluir integer,
    criado timestamp,
    atualizado timestamp,
    deletado timestamp,
    nome varchar(255),   
	emissao date,
	id_categoria integer,
	id_caixa integer,
	id_formapgto integer,
	id_pedido integer,
	id_pessoa integer,
	vencimento date,
	total numeric(18,4),
	baixa_data date,
	baixa_total numeric(18,4),
	baixa_id_formapgto numeric(18,4)
);

/* 
**************************************
************ GENERATORS **************
**************************************
*/
CREATE GENERATOR GEN_ITEM_ID;

SET TERM !! ;
CREATE TRIGGER ITEM_BI FOR ITEM
ACTIVE BEFORE INSERT POSITION 0
AS
DECLARE VARIABLE tmp DECIMAL(18,0);
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_ITEM_ID, 1);
  ELSE
  BEGIN
    tmp = GEN_ID(GEN_ITEM_ID, 0);
    if (tmp < new.ID) then
      tmp = GEN_ID(GEN_ITEM_ID, new.ID-tmp);
  END
END!!
SET TERM ; !!

 /* ---------------> Separador <--------------- */
CREATE GENERATOR GEN_ITEM_MOV_ESTOQUE_ID;

SET TERM !! ;
CREATE TRIGGER ITEM_MOV_ESTOQUE_BI FOR ITEM_MOV_ESTOQUE
ACTIVE BEFORE INSERT POSITION 0
AS
DECLARE VARIABLE tmp DECIMAL(18,0);
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_ITEM_MOV_ESTOQUE_ID, 1);
  ELSE
  BEGIN
    tmp = GEN_ID(GEN_ITEM_MOV_ESTOQUE_ID, 0);
    if (tmp < new.ID) then
      tmp = GEN_ID(GEN_ITEM_MOV_ESTOQUE_ID, new.ID-tmp);
  END
END!!
SET TERM ; !!

 /* ---------------> Separador <--------------- */
CREATE GENERATOR GEN_CATEGORIA_ID;

SET TERM !! ;
CREATE TRIGGER CATEGORIA_BI FOR CATEGORIA
ACTIVE BEFORE INSERT POSITION 0
AS
DECLARE VARIABLE tmp DECIMAL(18,0);
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_CATEGORIA_ID, 1);
  ELSE
  BEGIN
    tmp = GEN_ID(GEN_CATEGORIA_ID, 0);
    if (tmp < new.ID) then
      tmp = GEN_ID(GEN_CATEGORIA_ID, new.ID-tmp);
  END
END!!
SET TERM ; !!

 /* ---------------> Separador <--------------- */
CREATE GENERATOR GEN_IMPOSTO_ID;

SET TERM !! ;
CREATE TRIGGER IMPOSTO_BI FOR IMPOSTO
ACTIVE BEFORE INSERT POSITION 0
AS
DECLARE VARIABLE tmp DECIMAL(18,0);
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_IMPOSTO_ID, 1);
  ELSE
  BEGIN
    tmp = GEN_ID(GEN_IMPOSTO_ID, 0);
    if (tmp < new.ID) then
      tmp = GEN_ID(GEN_IMPOSTO_ID, new.ID-tmp);
  END
END!!
SET TERM ; !!

 /* ---------------> Separador <--------------- */
CREATE GENERATOR GEN_PEDIDO_ID;

SET TERM !! ;
CREATE TRIGGER PEDIDO_BI FOR PEDIDO
ACTIVE BEFORE INSERT POSITION 0
AS
DECLARE VARIABLE tmp DECIMAL(18,0);
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_PEDIDO_ID, 1);
  ELSE
  BEGIN
    tmp = GEN_ID(GEN_PEDIDO_ID, 0);
    if (tmp < new.ID) then
      tmp = GEN_ID(GEN_PEDIDO_ID, new.ID-tmp);
  END
END!!
SET TERM ; !!

 /* ---------------> Separador <--------------- */
CREATE GENERATOR GEN_PEDIDO_ITEM_ID;

SET TERM !! ;
CREATE TRIGGER PEDIDO_ITEM_BI FOR PEDIDO_ITEM
ACTIVE BEFORE INSERT POSITION 0
AS
DECLARE VARIABLE tmp DECIMAL(18,0);
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_PEDIDO_ITEM_ID, 1);
  ELSE
  BEGIN
    tmp = GEN_ID(GEN_PEDIDO_ITEM_ID, 0);
    if (tmp < new.ID) then
      tmp = GEN_ID(GEN_PEDIDO_ITEM_ID, new.ID-tmp);
  END
END!!
SET TERM ; !!

 /* ---------------> Separador <--------------- */
CREATE GENERATOR GEN_PESSOA_ID;

SET TERM !! ;
CREATE TRIGGER PESSOA_BI FOR PESSOA
ACTIVE BEFORE INSERT POSITION 0
AS
DECLARE VARIABLE tmp DECIMAL(18,0);
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_PESSOA_ID, 1);
  ELSE
  BEGIN
    tmp = GEN_ID(GEN_PESSOA_ID, 0);
    if (tmp < new.ID) then
      tmp = GEN_ID(GEN_PESSOA_ID, new.ID-tmp);
  END
END!!
SET TERM ; !!

 /* ---------------> Separador <--------------- */
CREATE GENERATOR GEN_PESSOA_CONTATO_ID;

SET TERM !! ;
CREATE TRIGGER PESSOA_CONTATO_BI FOR PESSOA_CONTATO
ACTIVE BEFORE INSERT POSITION 0
AS
DECLARE VARIABLE tmp DECIMAL(18,0);
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_PESSOA_CONTATO_ID, 1);
  ELSE
  BEGIN
    tmp = GEN_ID(GEN_PESSOA_CONTATO_ID, 0);
    if (tmp < new.ID) then
      tmp = GEN_ID(GEN_PESSOA_CONTATO_ID, new.ID-tmp);
  END
END!!
SET TERM ; !!

 /* ---------------> Separador <--------------- */
CREATE GENERATOR GEN_PESSOA_ENDERECO_ID;

SET TERM !! ;
CREATE TRIGGER PESSOA_ENDERECO_BI FOR PESSOA_ENDERECO
ACTIVE BEFORE INSERT POSITION 0
AS
DECLARE VARIABLE tmp DECIMAL(18,0);
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_PESSOA_ENDERECO_ID, 1);
  ELSE
  BEGIN
    tmp = GEN_ID(GEN_PESSOA_ENDERECO_ID, 0);
    if (tmp < new.ID) then
      tmp = GEN_ID(GEN_PESSOA_ENDERECO_ID, new.ID-tmp);
  END
END!!
SET TERM ; !!

 /* ---------------> Separador <--------------- */
 CREATE GENERATOR GEN_FORMAPGTO_ID;

SET TERM !! ;
CREATE TRIGGER FORMAPGTO_BI FOR FORMAPGTO
ACTIVE BEFORE INSERT POSITION 0
AS
DECLARE VARIABLE tmp DECIMAL(18,0);
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_FORMAPGTO_ID, 1);
  ELSE
  BEGIN
    tmp = GEN_ID(GEN_FORMAPGTO_ID, 0);
    if (tmp < new.ID) then
      tmp = GEN_ID(GEN_FORMAPGTO_ID, new.ID-tmp);
  END
END!!
SET TERM ; !!

/* ---------------> Separador <--------------- */
CREATE GENERATOR GEN_TITULO_ID;

SET TERM !! ;
CREATE TRIGGER TITULO_BI FOR TITULO
ACTIVE BEFORE INSERT POSITION 0
AS
DECLARE VARIABLE tmp DECIMAL(18,0);
BEGIN
  IF (NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_TITULO_ID, 1);
  ELSE
  BEGIN
    tmp = GEN_ID(GEN_TITULO_ID, 0);
    if (tmp < new.ID) then
      tmp = GEN_ID(GEN_TITULO_ID, new.ID-tmp);
  END
END!!
SET TERM ; !!
