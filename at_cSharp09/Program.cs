using System;
using System.IO;

class Program
{
    static void Main()
    {
        string arquivo = "estoque.txt";
        while (true)  
        {
            Console.WriteLine("Menu de opções:");
            Console.WriteLine("1) Inserir Produto");
            Console.WriteLine("2) Listar Produtos");
            Console.WriteLine("3) Sair");
            Console.Write("Escolha uma opção: ");
            
            string opcao = Console.ReadLine();
            if (opcao == "1")
            {
                AddProdutos(arquivo);
            }else if (opcao == "2")
            {
                ListaProdutos(arquivo);
            }else if (opcao == "3")
            {
                Console.WriteLine("Encerrado");
                break;
            }else
            {
                Console.WriteLine("Opção inválida. Tente novamente");
            }
        }
    }

    static void AddProdutos(string arquivo)
    {
        string[] linhas = File.Exists(arquivo) ? File.ReadAllLines(arquivo) : new string[0]; 

        if (linhas.Length >= 5)  
        {
            Console.WriteLine("Limite de produtos");
            return;
        }
        


        Console.Write("Nome: ");
        string nome = Console.ReadLine();


        Console.Write("Quantidade em estoque: ");
        if (!int.TryParse(Console.ReadLine(), out int quantidade))
        {
            Console.WriteLine("Quantidade inválida!");
            return;
        }


        Console.Write("Preço unitário: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal preco))
        {
            Console.WriteLine("Preço inválido!");
            return;
        }
        


        try
        {
            using (StreamWriter sw = new StreamWriter(arquivo, true))
            {
                sw.WriteLine($"{nome}, {quantidade}, {preco}");
            }
            Console.WriteLine("Produto cadastrado com sucesso!");
        }
        
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar produto: {ex.Message}");
        }
    }

    static void ListaProdutos(string arquivo)
    {
        if (!File.Exists(arquivo) || new FileInfo(arquivo).Length == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado");
            return;
        }
        
        try
        {
            using (StreamReader sr = new StreamReader(arquivo))
            {
                string linha;
                while ((linha = sr.ReadLine()) != null)
                {
                    string[] dados = linha.Split(',');
                    if (dados.Length != 3 || !int.TryParse(dados[1], out int quantidade) || !decimal.TryParse(dados[2], out decimal preco))
                    {
                        Console.WriteLine("Erro ao ler um produto. formato incorreto");
                        continue;
                    }
                    Console.WriteLine($"Produto: {dados[0]} | Quantidade: {quantidade} | Preço: R$ {preco:0.00}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao ler o arquivo: {ex.Message}");
        }
    }
}