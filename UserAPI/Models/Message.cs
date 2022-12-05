using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.Models
{
    public class Message
    {
        // o nosso destinatário não vai ser puramente uma string. Ele vai precisar 
        // ser um tipo especial, que identifica um endereço de e-mail, que um MailboxAddress
        
        public List<MailboxAddress> Destinatario { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        public Message(IEnumerable<string> destinatario, string assunto,
            int usuarioId, string codigo)
        {
            //o nosso destinatário vai ser uma nova lista de MailboxAddress, só que agora precisamos
            //adicionar essa lista de destinatários.
            //Então podemos dar um AddRange, vai adicionar um elemento a essa coleção no final. No caso,
            //nós vamos adicionar uma nova lista a essa lista, que basicamente é o nosso destinatário
            Destinatario = new List<MailboxAddress>();
            Destinatario.AddRange(destinatario.Select(d => new MailboxAddress("", d)));
            Assunto = assunto;
            Conteudo = $"http://localhost:5000/ativa?UsuarioId={usuarioId}&CodigoDeAtivacao={codigo}";
        }
    }
}
//Então, só para traduzirmos e não ficar nenhuma dúvida, a nossa lista de destinatários da nossa
//propriedade da nossa classe mensagem, nós estamos instanciando uma lista de MailboxAddress.
//Essa lista que acabou de ser instanciada, nós estamos adicionando a ela um novo elemento,
//que é um destinatário, uma string que está na nossa lista de destinatários recebida
//por parâmetro e nós precisamos converter para um MailboxAddress
//] Então a princípio a composição da nossa mensagem é feita dessa maneira, nós precisamos
//converter de string para um MaiboxAddress, para que ele consiga funcionar sem nenhum problema.