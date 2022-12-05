using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Data.Dtos.User
{
    //Criação de Usuário
    public class CreateUserDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        //DataType igual senha
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        //DataType igual senha
        [DataType(DataType.Password)]
        //Confirmação da descrição de senha sendo obrigatória comparando-a com o campo Password
        [Required]
        [Compare("Password")]
        public string RePassword { get; set; }

    }
}