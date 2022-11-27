# UserAPI

Como acessar o banco de maneira prática para um usuário?

Podemos usar o Entity para criar as tabelas no banco?

Como validar usuário/senha para autenticação?

Como armazenar a senha de maneira segura?

Como adicionar critérios de segurança?
 
Então ficamos com as seguintes perguntas nesse momento: como vamos conseguir acessar o banco de dados de maneira prática para cadastrar um usuário? Então já conhecemos o Entity para fazer essa interface com um banco de dados, mas será que lidar com tantas questões assim específicas de autenticação vai ser prático? Vamos guardar essa pergunta.

 Então podemos utilizar o Entity, como eu acabei de falar para vocês, para essa questão de gerar as tabelas no banco? Podemos e vamos utilizar, mas para a parte de efetuar esses logins e logouts e essas características de identidade, será que vamos continuar só com ele?

 Então como conseguimos validar o usuário e senha? Dado que temos um usuário e senha cadastradas no banco, como conseguimos pegar esse usuário que estamos escrevendo atrás da requisição, ir no banco, ver se ele existe, se não existe, se existe retorna alguma informação ok, que você conseguiu fazer o login; senão retorna uma informação de que não foi autorizado, porque esse usuário não existe ou o usuário e senha está errado. São diversas lógicas com que precisamos nos preocupar para realizar essas operações.

 Uma questão importante também, como vamos armazenar essa senha de maneira segura? Porque se simplesmente recebermos uma senha de um usuário e colocar no banco direto, estamos armazenando a senha, um texto, direto. Isso não é nada seguro, porque no fim das contas, qualquer pessoa que tiver acesso ao banco, vai ter acesso a todas as credenciais de todos os usuários que tivermos e isso não é nem um pouco seguro.

 Então como conseguimos também adicionar esses critérios extras de segurança? Então dado que vamos ter um usuário cadastrado no nosso banco, como conseguimos, por exemplo, criptografar a senha, como conseguimos garantir que esse usuário pode ou não ter uma maneira de confirmar sua conta, caso realmente queira utilizar o nosso sistema?

 Então são diversas perguntas que podemos levantar nesse sentido. E para isso nós vamos utilizar o Identity.
Para conseguirmos, de maneira fácil, com o nosso cliente fazendo requisições de cadastro, de logins, de logout, com o Identity dentro do nosso projeto, dentro da nossa solução como um todo; fazendo esse intermédio e oferecendo soluções já implementadas para a parte de cadastro, login e logout, que vai facilitar muito o nosso trabalho.
