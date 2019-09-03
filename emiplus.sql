/*-------------------------------------------------------------------------------------*/

CREATE TABLE ITEM
(
    id integer not null primary key,
    tipo varchar(20),
    excluir integer not null,
    datainserido date,
    dataatualizado date,
    datadeletado date,
    empresaid integer,
    nome varchar(200),
    referencia varchar(50),
    valorcompra numeric(18,4),
    valorvenda numeric(18,4),
    estoqueatual numeric(18,4)
);

CREATE GENERATOR GEN_ITEM_ID;

SET TERM !! ;
CREATE TRIGGER ITEM_BI FOR ITEM
ACTIVE BEFORE INSERT POSITION 0
AS
DECLARE VARIABLE tmp DECIMAL(18,0);
BEGIN
    IF(NEW.ID IS NULL) THEN
    NEW.ID = GEN_ID(GEN_ITEM_ID, 1);
ELSE
BEGIN
    tmp = GEN_ID(GEN_ITEM_ID, 0);
    if (tmp< new.ID) then
        tmp = GEN_ID(GEN_ITEM_ID, new.ID - tmp);
END
END!!
SET TERM; !!

/*-------------------------------------------------------------------------------------*/

CREATE TABLE CATEGORIA
(
    id integer not null primary key,
    tipo varchar(20),
    excluir integer not null,
    datainserido date,
    dataatualizado date,
    datadeletado date,
    empresaid integer,
    nome varchar(200)
);

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

/*-------------------------------------------------------------------------------*/

CREATE TABLE PESSOA
(
    id integer not null primary key,
    tipo varchar(20),
    excluir integer not null,
    datainserido date,
    dataatualizado date,
    datadeletado date,
    empresaid integer,
    nome varchar(200)
);

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

/*-------------------------------------------------------------------------------*/

CREATE TABLE PESSOA_CONTATO
(
    id integer not null primary key,
    tipo varchar(20),
    excluir integer not null,
    datainserido date,
    dataatualizado date,
    datadeletado date,
    empresaid integer,
    contato varchar(200),
    email varchar(200),
    telefone varchar(200),
    celular varchar(200)    
);

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

/*-------------------------------------------------------------------------------*/

CREATE TABLE PESSOA_ENDERECO
(
    id integer not null primary key,
    tipo varchar(20),
    excluir integer not null,
    datainserido date,
    dataatualizado date,
    datadeletado date,
    empresaid integer,
    cep varchar(50),
    estado varchar(50),
    cidade varchar(200),
    ibge varchar(50),
    rua varchar(200),
    n varchar(200),
    bairro varchar(200),
    complemento varchar(200)
);

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

/*-------------------------------------------------------------------------------*/


PEDIDO 
CAIXA
TITULO



/*-------------------------------------------------------------------------------*/