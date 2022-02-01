-- create db
use ShapeUp;

-- create tables
-- exercicios
create table exercicios(
	id int primary key identity(1,1),
	exercicio varchar(100) not null,
	tipo varchar(20) not null,
	dificuldade varchar(20) not null,
	duracao varchar(10),
	autor varchar(100) default 'Admin'
);
-- receitas
create table receitas(
	id int primary key identity(1,1),
	receita varchar(100) not null,
	tipo varchar(20) not null,
	dificuldade varchar(20) not null,
	calorias int,
	autor varchar(100) default 'Admin'
);
-- user
create table utilizador(
	username varchar(50) primary key,
	name varchar(50),
	email varchar(50) not null unique,
	passw varchar(32) not null,
	dbirth date
);
-- receitas que o utilizador marcou como favorito
create table myFood(
	id int primary key identity(1,1),
	utilizador varchar(50) foreign key references utilizador(username),
	receita int foreign key references receitas(id)
);


-- exercicios que o utilizador marcou como favorito
create table myExercise(
	id int primary key identity(1,1),
	utilizador varchar(50) foreign key references utilizador(username),
	exercicio int foreign key references exercicios(id)
);
