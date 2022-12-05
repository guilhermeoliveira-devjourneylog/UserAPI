using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Data.Dtos;
using UserAPI.Data.Dtos.User;
using UserAPI.Data.Requests;
using UserAPI.Services;


namespace UserAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CadastroController : ControllerBase
    {
        //chamada de serviço de cadastro
        private CadastroService _cadastroService;

        public CadastroController(CadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        [HttpPost]
        public IActionResult CadastraUsuario(CreateUserDto createDto)
        {
            //controle de retorno de operação da ativação de cadastro
            Result resultado = _cadastroService.CadastraUsuario(createDto);
            if (resultado.IsFailed) return StatusCode(500);
            return Ok(resultado.Successes);
        }
        //ativação de conta
        [HttpGet("/ativa")]
        //esse AtivaContaRequest, as informações que nós estamos definindo que é o nosso
        //userId e o nosso código de ativação, nós vamos recebê-la não a partir do body em si,
        //e sim a partir de um fromQuery, que nós vamos definir.
        public IActionResult AtivaContaUsuario([FromQuery] ActiveAccountRequest request)
        {
            //chamda de resulta da requisição de ativação
            Result resultado = _cadastroService.AtivaContaUsuario(request);
            if (resultado.IsFailed) return StatusCode(500);
            return Ok(resultado.Successes);
        }
    }
}