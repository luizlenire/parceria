# parceria
Repositório para teste da FIAP.

A aplicação foi desenvolvida em .NET Core 5.0, inicialmente, rodando apenas em Microsoft SQL Server conforme requisitado, para a utilização da API, se faz necessário gerar um token, a API possui swagger, o mesmo irá auxiliar de uma forma bem eficaz na utilização da mesma.

Usuario/senha da API: usrfiap/pswfiap.

A API tem um healthcheck para verificar a conectividade com o banco de dados, acessar {url}/hc-ui.

As configurações para acesso a base de dados deverá ser parametrizada no appsettings.json.

O script para criação da base de dados está dividido da seguinte maneira:

1 - Criação da base de dados;

2 - Criação de tabelas, primary e foreign key;

3 - Criação de views;

4 - Criação de procedures;

5 - Criação de triggers;

6 - Testes;

Sugestões de mudanças sobre a estrutura de base de dados solicitada:

1 - Troca do tipo de primary key e foreign key int para bigint, mas, ainda sugeria a utilização de um Guid;

2 - Troca dos tipos varchar para nvarchar, para salvar dados unicode;

3 - Troca do tipo text para nvarchar(max), melhor gerenciavel via banco de dados;

4 - Troca do tipo int do campo QtdLikes para bigint, devido a ser um campo que pode estourar, pois a população mundial está em 7 bilhoes, e o campo aceita cerca de 2 bilhoes;

5 - Criação do campo "Active" do tipo bit, para gerenciar delete lógico;

6 - Para adequar as nomenclaturas, troquei "ParceiraLike" por "ParceriaLike";

7 - Utilizar o migration, eliminando a necessidade de script e assim, ter uma maior abrangência em qualquer tipo de banco de dados;