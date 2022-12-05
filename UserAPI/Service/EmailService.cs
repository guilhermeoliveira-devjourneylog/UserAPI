using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAPI.Models;

// lógica responsável por compor uma Mensagem que será enviada
namespace UserAPI.Services
{
    public class EmailService
    {
        //Carregando informações de appsettings para estabelecer a conexão e enviar o e-mail
        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Parâmetros da classe 
        public void EnviarEmail(string[] destinatario, string assunto,
            int usuarioId, string code)
        {
            //composição da mensagem
            Message mensagem = new Message(destinatario,
                assunto, usuarioId, code);
            var mensagemDeEmail = CriaCorpoDoEmail(mensagem);
            Enviar(mensagemDeEmail);
        }

        //lógica de envio
        private void Enviar(MimeMessage mensagemDeEmail)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    //coletando config do SmtpServer da Porta para conexão
                    //1º parâmetro
                    client.Connect(_configuration.GetValue<string>("EmailSettings:SmtpServer"),
                    //a porta que ele espera receber não é uma string e sim o número inteiro,
                    //então eu coloco int
                    //2º parâmetro + 3º contrato SSL igual a true
                        _configuration.GetValue<int>("EmailSettings:Port"), true);
                    //definindo mecanismo de autenticação
                    client.AuthenticationMechanisms.Remove("XOUATH2");
                    //autenticação com client e-mail, remetente e senha
                    //1º parâmetro
                    client.Authenticate(_configuration.GetValue<string>("EmailSettings:From"),
                    //2º parâmetro
                        _configuration.GetValue<string>("EmailSettings:Password"));
                    //por fim o envio
                    client.Send(mensagemDeEmail);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    //E no fim das contas, independente de termos conseguido fazer ou não o envio,
                    //nós precisamos desconectar do nosso servidor de e-mail, vamos colocar client.
                    //Disconnect(true) e vamos liberar com o Dispose, esse é o papel dele, liberar
                    //os recursos do nosso cliente, para não ficarmos ocupando recursos desnecessários
                    //com a nossa aplicação, então client.Dispose()
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
        //conversão da mensagem em si em realmente um e-mail
        //método para receber a mensagem e fazer a conversão
        private MimeMessage CriaCorpoDoEmail(Message mensagem)
        {
            //definição de uma mensagem
            var mensagemDeEmail = new MimeMessage();
            //adicionando remetente
            mensagemDeEmail.From.Add(new MailboxAddress(
                _configuration.GetValue<string>("EmailSettings:From")));
            //to 
            mensagemDeEmail.To.AddRange(mensagem.Destinatario);
            //Assunto
            mensagemDeEmail.Subject = mensagem.Assunto;
            //Corpo de e-mail
            mensagemDeEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = mensagem.Conteudo
            };

            return mensagemDeEmail;
        }
    }
}
//Como queremos isolar o código responsável por gerar o corpo da nossa mensagem que será enviada,
//iremos criar a classe Mensagem e construiremos seu conteúdo através do construtor. Dentro da pasta
//Models, crie a classe Mensagem inicializando os parâmetros: