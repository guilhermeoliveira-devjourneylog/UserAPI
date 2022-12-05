using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Data.Dtos.User;
using UserAPI.Models;

//construtor que vai fazer o mapeamento lá entre o nosso CreateUsuarioDto
//e o nosso usuário propriamente dito.
namespace UserAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
            //mapeamento de usuario para aspnetusers
            //agora que temos esse UsuarioIdentity, precisamos ter alguma maneira de cadastrá-lo
            //no banco para que ele apareça na nossa tabela e conseguirmos enviar requisição
            //e correr tudo bem.E como vamos fazer isso? Precisamos ter alguns gerenciador de
            //usuários que faça essa tarefa para nós. Vá até CadastroService para fazer a declaração do gerenciador.
            CreateMap<User, IdentityUser<int>>();
        }
    }
}