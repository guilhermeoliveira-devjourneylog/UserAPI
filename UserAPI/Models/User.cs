
namespace UserAPI.Models
{
    //modelo de dados apresentados no banco
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}

//E a senha? Onde está a senha? Vamos pensar. A senha é uma informação que vamos
//armazená-la propriamente dita no nosso usuário?
//Um usuário, para ser um usuário, ele precisa ter essa informação exposta?
//Não. Até pensando em LGPD, em questão de segurança, não vamos colocar essa senha.
// A ideia é que de início, para representarmos um usuário, ele vai ter um ID, um
// Username e um e-mail. Não vamos ficar trafegando a senha dentro da nossa aplicação,
// nem trazendo isso do banco, ainda tem a questão de como vamos armazenar essa senha no banco.