using AutoMapper;
using FluentResults;
using K4os.Compression.LZ4.Internal;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using UserAPI.Data.Dtos;
using UserAPI.Data.Dtos.User;
using UserAPI.Data.Requests;
using UserAPI.Models;


namespace UserAPI.Services
{
    public class CadastroService
    {
        private IMapper _mapper;
        //Declaração de gerenciador de usuário
        //Esse gerenciador vai gerar, a princípio, um resultado do Identity.
        //Então um resultado do Identity vai ser uma operação que vamos realizar a partir
        //desse userManager, que vai ser para criar de uma maneira assíncrona esse usuário,
        //que vai ser o nosso UsuarioIdentity, que acabamos de mapear, e ele vai ter uma senha.
        //Onde está a senha? Na nossa requisição, no nosso DTO, então CreateDto.password.
        private UserManager<IdentityUser<int>> _userManager;
        private EmailService _emailService;
        // Inicialização com os atributos via construtor.
        public CadastroService(IMapper mapper,
        UserManager<IdentityUser<int>> userManager,
        EmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public Result CadastraUsuario(CreateUserDto createDto)
        {
            //conversão de createDto para User mais mapeamento de User para tabela aspnetusers
            //aspnetusers já contém  a senha de usuário, mas não a senha puramente em texto e
            //sim um hash para essa senha. Então já estamos guardando de maneira segura,
            //não estamos armazenando em nenhum modelo dentro da nossa aplicação,
            //só direto no banco e hasheada, criptografada.
            User usuario = _mapper.Map<User>(createDto);
            IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);
            Task<IdentityResult> resultadoIdentity = _userManager
                .CreateAsync(usuarioIdentity, createDto.Password);
            if (resultadoIdentity.Result.Succeeded)
            {
                //Gerando código de ativação 
                //utilizando o próprio UserManager conseguimos ter a chamada do método
                //generateEmailConfirmationTokenAsync
                //E para ele nós precisamos passar o nosso usuarioIdentity.Só que nesse caso nós
                //já temos a declaração, o mapeamento em cima, então não precisamos fazer como
                //fizemos no nosso loginService, a parte de recuperá - lo através do nosso Manager.
                //Porque já o temos dentro.
                //Então passamos o nosso usuarioIdentity. Com tudo isso sendo feito, estamos gerando
                //o nosso código de confirmação do nosso e-mail. E agora, é aquela velha história: como
                //estamos com esse async, que retorna, se dermos uma olhada no que está escrito, ele retorna
                //uma task desse resultado de uma string, que vai ser no nosso código de ativação, nós
                //precisamos aguardar esse código ser preenchido para conseguirmos retornar.
                var code = _userManager
                    .GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result;
                //evitando problemas caracteristicas da conversão de caracteres sem encode
                var encodedCode = HttpUtility.UrlEncode(code);
                //operação positiva
                //Parâmetros destinatário de envio, assunto do e-mail, Id que compoe a mensagem, código de ativação
                _emailService.EnviarEmail(new[] { usuarioIdentity.Email },
                    "Link de Ativação", usuarioIdentity.Id, encodedCode);

                return Result.Ok().WithSuccess(code);
            }
            //operação negativa
            return Result.Fail("Falha ao cadastrar usuário");

        }

        public Result AtivaContaUsuario(ActiveAccountRequest request)
        {
            //recuperando identityUser
            //, dentro nós temos o nosso UserManager. E já vimos como se usa o
            //UserManager para recuperar esse tipo de informação, então
            //var IdentityUser = _userManager.Users.FirstOrDefault.
            //E agora, eu quero recuperar um usuário qualquer, então um u já bastaria,
            //e esse usuário qualquer, o identificador dele tem que ser igual ao identificador
            //do AtivaContaRequest desse request que eu estou recebendo
            var identityUser = _userManager
                .Users
                .FirstOrDefault(u => u.Id == request.UsuarioId);
            //Então, por isso que nós estamos fazendo essa declaração dessa maneira. Por isso que
            //nós estamos precisando do nosso ActiveAccountRequest ter esse usuarioId dentro da definição dele.
            //Nós precisamos desse usuarioId, porque dentro do nosso CadastroService precisamos de
            //alguma maneira ter essa chamada ao nosso IdentityUser para recuperá-lo e conseguirmos
            //efetuar o cadastro, a confirmação da conta.
            var identityResult = _userManager
                .ConfirmEmailAsync(identityUser, request.CodigoDeAtivacao).Result;
            if (identityResult.Succeeded)
            {
                return Result.Ok();
            }
            return Result.Fail("Falha ao ativar conta de usuário");
        }
    }
}
