using System.Security.Cryptography.X509Certificates;

class Cliente 
{
    private string nome;

    public Cliente()
    { }

    public Cliente(string nome)
    {
        this.nome = nome;
    }

    public String Nome
    {
        get { return nome; }
        set { nome = value; }
    }
}


// Classe PessoaFisica herda ("Nome") de Cliente
class PessoaFisica : Cliente
{
    private String cpf;

    public PessoaFisica()
    {
    }

    public PessoaFisica(string nome, String cpf) : base(nome)
    {
        this.cpf = cpf;
    }

    public string Cpf
    {
        get { return cpf; }
        set { cpf = value; }
    }
}


// Classe PessoaJuridica herda ("Nome") de Cliente
class PessoaJuridica : Cliente
{
    private String cnpj;

    public PessoaJuridica()
    {
    }

    public PessoaJuridica(string nome) : base(nome)
    {
    }

    public string Cnpj
    {
        get { return Cnpj; }
        set { Cnpj = value; }
    }
}


// Classe Cabecalho
class Cabecalho
{
    private Guid numeroNota;
    public Cliente Cliente { get; set; } = new Cliente();

    public Cabecalho()
    {
    }

    public Cabecalho(Guid numeroNota, Cliente cliente)
    {
        this.numeroNota = numeroNota;
        Cliente = cliente;
    }

    public Guid NumeroNota
    {
        get { return numeroNota; }
        set { numeroNota = value; }
    }

}

// Classe Rodape
class Rodape
{
    private decimal valorTotal;

    public Rodape()
    {
    }

    public Rodape(decimal valorTotal)
    {
        this.valorTotal = valorTotal;
    }

    public decimal ValorTotal
    {
        get { return valorTotal; }
        set { valorTotal = value; }
    }
}


// Classe Produto
class Produto
{
    private string nome;
    private decimal preco;
    private int quantidade;

    public Produto(string nome, decimal preco, int quantidade)
    {
        this.nome = nome;
        this.preco = preco;
        this.quantidade = quantidade;
    }

    public string Nome
    {
        get { return nome;}
        set { nome = value; }
    }

    public decimal Preco
    {
        get { return preco; }
        set { preco = value; }
    }

    public int Quantidade
    {
        get { return quantidade; }
        set { quantidade = value;  }
    }

}


class NotaFiscal
{
    public Cabecalho Cabecalho { get; set; } = new Cabecalho();
    public List<Produto> produtos { get; set; } = new List<Produto>();
    public Rodape Rodape { get; set; } = new Rodape();

    public NotaFiscal()
    {
    }
    public NotaFiscal(Cabecalho cabecalho, List<Produto> produtos, Rodape rodape)
    {
        Cabecalho = cabecalho;
        this.produtos = produtos;
        Rodape = rodape;
    }

    public void Imprimir()
    {
        Console.WriteLine("**************************** NOTA FISCAL DE SAÍDA ****************************");
        Console.WriteLine();
        Console.WriteLine();


        Console.WriteLine("Número: " + Cabecalho.NumeroNota);
        Console.WriteLine("Cliente: " + Cabecalho.Cliente.Nome);
        Console.WriteLine("Cpf: 11122233344");
        
        
        Console.WriteLine();
        Console.WriteLine();

        foreach(Produto produto in produtos)
        {
            Console.WriteLine("Produto                     Quantidade                      Preco");
            Console.WriteLine(produto.Nome + "                           " + produto.Quantidade + "                           " + produto.Preco);
        }

        Console.WriteLine();
        Console.WriteLine();

        Console.WriteLine("Total da Nota Fiscal: " + produtos.Sum(produto => produto.Preco * produto.Quantidade));

        Console.WriteLine();
        Console.WriteLine();

        Console.WriteLine("******************************************************************************");

    }

}


// Classe principal Program
class Program
{
    static void Main(String[] args)
    {
        List<Produto> p = new List<Produto>();
        //Produto produto = new Produto("Teste", 10000, 1);
        Rodape rodape = new Rodape();


        Console.WriteLine("Nome do Cliente: ");
        String nome = Console.ReadLine();
        Console.WriteLine("Digite o Cpf do CLiente: ");
        String cpf = Console.ReadLine();

        Cabecalho myCb = new Cabecalho(Guid.NewGuid(), new PessoaFisica(nome, cpf));


        string n = "";
        while(n != "s")
        {

            Console.WriteLine("Nome do Produto: ");
            string pNome = Console.ReadLine();
            Console.WriteLine("Quandidade do Produto: ");
            int qtd = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Preço do Pordudo: ");
            decimal preco = Convert.ToDecimal(Console.ReadLine());

            Produto produto = new Produto(pNome, preco, qtd);
            p.Add(produto);

            Console.WriteLine("Deseja sair (S/N)");
            n = Console.ReadLine();

            if (p.Count <= 1 && n == "s")
            {
                n = "n";
                Console.WriteLine("Registre no minimo 2 produtos");
            }
           

        }




        NotaFiscal nota = new NotaFiscal(myCb, p , rodape);

        nota.Imprimir();
    }
}