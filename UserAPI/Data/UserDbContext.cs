using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//Definição de contexto
namespace UserAPI.Data
{
    //recurso utilizado no IdentityDbContext
    //Nós estamos utilizando um IdentityUser, que vai possuir como identificador um inteiro,
    //e dentro do nosso sistema, ele vai ter um papel de uma role, que também vai ser como um inteiro,
    //e no final das contas a chave utilizada para identificar o IdentityDbContext no nosso sistema
    //vai ser um inteiro, então IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>.
    public class UserDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        //opções do construtor da classe referenciando UserDbContext
        public UserDbContext(DbContextOptions<UserDbContext> opt) : base(opt)
        {

        }

    }
}

//Se nós queremos ter alguma maneira de acessar um banco de dados e cadastrar essas informações
//do usuário lá, persistir de alguma maneira, nós precisamos criar um contexto de acesso ao banco.
//Então eu vou criar uma nova classe e vou chamar de UserDbContext.