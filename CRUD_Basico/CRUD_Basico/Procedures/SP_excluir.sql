create procedure SP_Excluiraluno
@id int
as
begin
--declaracao de uma variavel
declare @nomealuno varchar(200)

--armazenando o nome do aluno na variavel
select @nomealuno = nome from Aluno where id =@id
-- deletando o aleno do banco
delete from Aluno where id = @id
-- retornando a frase de sucesso
select concat ('o(a) ',@nomealuno,'foi excluido com secesso!')
end
