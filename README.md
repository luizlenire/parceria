# parceria
Repositório para teste da FIAP.

Alterações no script da base de dados:

1 - Troca do tipo de primary key e foreign key int para bigint, mas, ainda sugeria a utilização de um Guid;
2 - Troca dos tipos varchar para nvarchar, para salvar dados unicode;
3 - Troca do tipo text para nvarchar(max), melhor gerenciavel via banco de dados;
4 - Troca do tipo int do campo QtdLikes para bigint, devido a ser um campo que pode estourar, pois a população mundial está em 7 bilhoes, e o campo aceita cerca de 2 bilhoes;
5 - Criação do campo "Active" do tipo bit, para gerenciar delete lógico;
6 - Para adequar as nomenclaturas, troquei "ParceiraLike" por "ParceriaLike";
