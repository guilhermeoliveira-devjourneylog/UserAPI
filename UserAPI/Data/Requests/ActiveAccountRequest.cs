using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

//request de ativação de conta para o método AtivaContaUsuario do CadastroController
namespace UserAPI.Data.Requests
{
    public class ActiveAccountRequest
    {
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public string CodigoDeAtivacao { get; set; }
    }
}