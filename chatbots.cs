using System;
using System.Collections.Generic;

abstract class Mensagem
{
    public string Conteudo { get; set; }
    public DateTime DataEnvio { get; set; }

    public Mensagem(string conteudo)
    {
        Conteudo = conteudo;
        DataEnvio = DateTime.Now;
    }

    public abstract void Exibir();
}

class MensagemTexto : Mensagem
{
    public MensagemTexto(string conteudo) : base(conteudo) { }

    public override void Exibir()
    {
        Console.WriteLine($"[Texto] Conteúdo: {Conteudo}, Enviado em: {DataEnvio}");
    }
}

abstract class MensagemArquivo : Mensagem
{
    public string Arquivo { get; set; }
    public string Formato { get; set; }

    public MensagemArquivo(string conteudo, string arquivo, string formato)
        : base(conteudo)
    {
        Arquivo = arquivo;
        Formato = formato;
    }
}

class MensagemFoto : MensagemArquivo
{
    public MensagemFoto(string conteudo, string arquivo, string formato)
        : base(conteudo, arquivo, formato) { }

    public override void Exibir()
    {
        Console.WriteLine($"[Foto] Conteúdo: {Conteudo}, Arquivo: {Arquivo}, Formato: {Formato}, Enviado em: {DataEnvio}");
    }
}

class MensagemVideo : MensagemArquivo
{
    public int Duracao { get; set; }

    public MensagemVideo(string conteudo, string arquivo, string formato, int duracao)
        : base(conteudo, arquivo, formato)
    {
        Duracao = duracao;
    }

    public override void Exibir()
    {
        Console.WriteLine($"[Vídeo] Conteúdo: {Conteudo}, Arquivo: {Arquivo}, Formato: {Formato}, Duração: {Duracao}s, Enviado em: {DataEnvio}");
    }
}

class MensagemGenericaArquivo : MensagemArquivo
{
    public MensagemGenericaArquivo(string conteudo, string arquivo, string formato)
        : base(conteudo, arquivo, formato) { }

    public override void Exibir()
    {
        Console.WriteLine($"[Arquivo] Conteúdo: {Conteudo}, Arquivo: {Arquivo}, Formato: {Formato}, Enviado em: {DataEnvio}");
    }
}

interface ICanal
{
    void EnviarMensagem(Mensagem mensagem, string destino);
}

class WhatsApp : ICanal
{
    public void EnviarMensagem(Mensagem mensagem, string numeroTelefone)
    {
        Console.WriteLine($"Enviando via WhatsApp para {numeroTelefone}:");
        mensagem.Exibir();
    }
}

class Telegram : ICanal
{
    public void EnviarMensagem(Mensagem mensagem, string identificador)
    {
        Console.WriteLine($"Enviando via Telegram para {identificador}:");
        mensagem.Exibir();
    }
}

class Facebook : ICanal
{
    public void EnviarMensagem(Mensagem mensagem, string usuario)
    {
        Console.WriteLine($"Enviando via Facebook para {usuario}:");
        mensagem.Exibir();
    }
}

class Instagram : ICanal
{
    public void EnviarMensagem(Mensagem mensagem, string usuario)
    {
        Console.WriteLine($"Enviando via Instagram para {usuario}:");
        mensagem.Exibir();
    }
}

class Program
{
    static void Main()
    {
        Mensagem texto = new MensagemTexto("Olá, tudo bem?");
        Mensagem video = new MensagemVideo("Confira esse vídeo", "video.mp4", "mp4", 120);
        Mensagem foto = new MensagemFoto("Veja essa imagem", "foto.jpg", "jpg");
        Mensagem arquivo = new MensagemGenericaArquivo("Segue o relatório", "relatorio.pdf", "pdf");

        List<(ICanal canal, string destino)> canais = new List<(ICanal, string)>
        {
            (new WhatsApp(), "+5511999999999"),
            (new Telegram(), "@usuario_telegram"),
            (new Facebook(), "usuario_facebook"),
            (new Instagram(), "usuario_instagram")
        };

        foreach (var (canal, destino) in canais)
        {
            canal.EnviarMensagem(texto, destino);
            canal.EnviarMensagem(video, destino);
            canal.EnviarMensagem(foto, destino);
            canal.EnviarMensagem(arquivo, destino);
            Console.WriteLine();
        }
    }
}
