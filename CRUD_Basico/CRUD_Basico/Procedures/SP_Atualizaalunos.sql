create procedure SP_Atualizaraluno
@nome varchar(200),@Dtnascimento datetime,
@ativo bit, @id int
as
begin
	update Aluno
	
	set nome = @nome, DtNascimento =@Dtnascimento,Ativo = @ativo
	
	where id = @id
	
	
	select CONCAT('o(a) aluno (a) de Id ', @id ,', foi alterado com sucesso')

end
