create procedure Sp_insereAluno
@nome varchar(200),
@dtnasc DATETIME,
@ativo BIT
as 
begin
	insert into Aluno
	values (@nome,@dtnasc,@ativo)
	select SCOPE_IDENTITY()
	end
