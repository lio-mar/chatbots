using System;

// Interfaces e classes base
public interface IMensagem
{
    void Enviar();
}

public abstract class MensagemBase : IMensagem
{
    public string Conteudo { get; set; }
    public DateTime DataEnvio { get; set; }

    protected MensagemBase(string conteudo)
    {
        Conteudo = conteudo;
        DataEnvio = DateTime.Now;
    }

    public abstract void Enviar();
}

// Tipos de mensagens
public class MensagemTexto : MensagemBase
{
    public MensagemTexto(string conteudo) : base(conteudo) { }

    public override void Enviar()
    {
        Console.WriteLine($"Enviando mensagem de texto: {Conteudo}");
        Console.WriteLine($"Data de envio: {DataEnvio}");
    }
}

public class MensagemVideo : MensagemBase
{
    public string Arquivo { get; set; }
    public string Formato { get; set; }
    public TimeSpan Duracao { get; set; }

    public MensagemVideo(string conteudo, string arquivo, string formato, TimeSpan duracao) 
        : base(conteudo)
    {
        Arquivo = arquivo;
        Formato = formato;
        Duracao = duracao;
    }

    public override void Enviar()
    {
        Console.WriteLine($"Enviando vídeo: {Conteudo}");
        Console.WriteLine($"Arquivo: {Arquivo}, Formato: {Formato}, Duração: {Duracao}");
        Console.WriteLine($"Data de envio: {DataEnvio}");
    }
}

public class MensagemFoto : MensagemBase
{
    public string Arquivo { get; set; }
    public string Formato { get; set; }

    public MensagemFoto(string conteudo, string arquivo, string formato) 
        : base(conteudo)
    {
        Arquivo = arquivo;
        Formato = formato;
    }

    public override void Enviar()
    {
        Console.WriteLine($"Enviando foto: {Conteudo}");
        Console.WriteLine($"Arquivo: {Arquivo}, Formato: {Formato}");
        Console.WriteLine($"Data de envio: {DataEnvio}");
    }
}

public class MensagemArquivo : MensagemBase
{
    public string Arquivo { get; set; }
    public string Formato { get; set; }

    public MensagemArquivo(string conteudo, string arquivo, string formato) 
        : base(conteudo)
    {
        Arquivo = arquivo;
        Formato = formato;
    }

    public override void Enviar()
    {
        Console.WriteLine($"Enviando arquivo: {Conteudo}");
        Console.WriteLine($"Arquivo: {Arquivo}, Formato: {Formato}");
        Console.WriteLine($"Data de envio: {DataEnvio}");
    }
}

// Canais de comunicação
public interface ICanalComunicacao
{
    void EnviarMensagem(IMensagem mensagem, string destinatario);
}

public class WhatsApp : ICanalComunicacao
{
    public void EnviarMensagem(IMensagem mensagem, string numeroTelefone)
    {
        Console.WriteLine($"Enviando para WhatsApp no número: {numeroTelefone}");
        mensagem.Enviar();
    }
}

public class Telegram : ICanalComunicacao
{
    public void EnviarMensagem(IMensagem mensagem, string destinatario)
    {
        // Telegram pode usar número ou usuário
        if (destinatario.StartsWith("@"))
        {
            Console.WriteLine($"Enviando para Telegram no usuário: {destinatario}");
        }
        else
        {
            Console.WriteLine($"Enviando para Telegram no número: {destinatario}");
        }
        mensagem.Enviar();
    }
}

public class Facebook : ICanalComunicacao
{
    public void EnviarMensagem(IMensagem mensagem, string usuario)
    {
        Console.WriteLine($"Enviando para Facebook no usuário: {usuario}");
        mensagem.Enviar();
    }
}

public class Instagram : ICanalComunicacao
{
    public void EnviarMensagem(IMensagem mensagem, string usuario)
    {
        Console.WriteLine($"Enviando para Instagram no usuário: {usuario}");
        mensagem.Enviar();
    }
}

// Classe principal para demonstrar o uso
class Program
{
    static void Main(string[] args)
    {
        // Criando canais
        var whatsapp = new WhatsApp();
        var telegram = new Telegram();
        var facebook = new Facebook();
        var instagram = new Instagram();

        // Criando mensagens
        var texto = new MensagemTexto("Olá, tudo bem?");
        var video = new MensagemVideo("Veja este vídeo", "video1.mp4", "mp4", TimeSpan.FromMinutes(2));
        var foto = new MensagemFoto("Minha foto", "foto1.jpg", "jpg");
        var arquivo = new MensagemArquivo("Documento importante", "doc.pdf", "pdf");

        // Enviando mensagens pelos canais
        whatsapp.EnviarMensagem(texto, "+5511999999999");
        Console.WriteLine();
        
        telegram.EnviarMensagem(video, "@usuario_telegram");
        Console.WriteLine();
        
        facebook.EnviarMensagem(foto, "usuario_facebook");
        Console.WriteLine();
        
        instagram.EnviarMensagem(arquivo, "usuario_instagram");
        Console.WriteLine();
        
        telegram.EnviarMensagem(texto, "+5511888888888");
    }
}
