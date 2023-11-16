using aplicacaoLoja.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace aplicacaoLoja.Controllers
{
    [Authorize]
    public class DadosController : Controller
    {
        private readonly Contexto contexto;

        public DadosController(Contexto context)
        {
            contexto = context;
        }

        public IActionResult Categorias()
        {
            contexto.Database.ExecuteSqlRaw("delete from categorias");
            contexto.Database.ExecuteSqlRaw("DBCC CHECKIDENT('categorias', RESEED, 0)");

            string[] vCategoria = { "Bermuda", "Calça", "Camiseta", "Moletom", "Polo", "Regata" };

            for (int i = 0; i < 6; i++)
            {

                Categoria categoria = new Categoria();

                categoria.descricao = vCategoria[i];

                contexto.Categorias.Add(categoria);
            }
            contexto.SaveChanges();

            return View(contexto.Categorias.OrderBy(o => o.descricao).ToList());
        }

        public IActionResult Clientes()
        {
            contexto.Database.ExecuteSqlRaw("delete from clientes");
            contexto.Database.ExecuteSqlRaw("DBCC CHECKIDENT('clientes', RESEED, 0)");

            string[] vNomeMas = { "Miguel", "Arthur", "Bernardo", "Heitor", "Davi", "Lorenzo", "Théo", "Pedro", "Gabriel", "Enzo", "Matheus", "Lucas", "Benjamin", "Nicolas", "Guilherme", "Rafael", "Joaquim", "Samuel", "Enzo Gabriel", "João Miguel", "Henrique", "Gustavo", "Murilo", "Pedro Henrique", "Pietro", "Lucca", "Felipe", "João Pedro", "Isaac", "Benício", "Daniel", "Anthony", "Leonardo", "Davi Lucca", "Bryan", "Eduardo", "João Lucas", "Victor", "João", "Cauã", "Antônio", "Vicente", "Caleb", "Gael", "Bento", "Caio", "Emanuel", "Vinícius", "João Guilherme", "Davi Lucas", "Noah", "João Gabriel", "João Victor", "Luiz Miguel", "Francisco", "Kaique", "Otávio", "Augusto", "Levi", "Yuri", "Enrico", "Thiago", "Ian", "Victor Hugo", "Thomas", "Henry", "Luiz Felipe", "Ryan", "Arthur Miguel", "Davi Luiz", "Nathan", "Pedro Lucas", "Davi Miguel", "Raul", "Pedro Miguel", "Luiz Henrique", "Luan", "Erick", "Martin", "Bruno", "Rodrigo", "Luiz Gustavo", "Arthur Gabriel", "Breno", "Kauê", "Enzo Miguel", "Fernando", "Arthur Henrique", "Luiz Otávio", "Carlos Eduardo", "Tomás", "Lucas Gabriel", "André", "José", "Yago", "Danilo", "Anthony Gabriel", "Ruan", "Miguel Henrique", "Oliver" };
            string[] vNomeFem = { "Alice", "Sophia", "Helena", "Valentina", "Laura", "Isabella", "Manuela", "Júlia", "Heloísa", "Luiza", "Maria Luiza", "Lorena", "Lívia", "Giovanna", "Maria Eduarda", "Beatriz", "Maria Clara", "Cecília", "Eloá", "Lara", "Maria Júlia", "Isadora", "Mariana", "Emanuelly", "Ana Júlia", "Ana Luiza", "Ana Clara", "Melissa", "Yasmin", "Maria Alice", "Isabelly", "Lavínia", "Esther", "Sarah", "Elisa", "Antonella", "Rafaela", "Maria Cecília", "Liz", "Marina", "Nicole", "Maitê", "Isis", "Alícia", "Luna", "Rebeca", "Agatha", "Letícia", "Maria-", "Gabriela", "Ana Laura", "Catarina", "Clara", "Ana Beatriz", "Vitória", "Olívia", "Maria Fernanda", "Emilly", "Maria Valentina", "Milena", "Maria Helena", "Bianca", "Larissa", "Mirella", "Maria Flor", "Allana", "Ana Sophia", "Clarice", "Pietra", "Maria Vitória", "Maya", "Laís", "Ayla", "Ana Lívia", "Eduarda", "Mariah", "Stella", "Ana", "Gabrielly", "Sophie", "Carolina", "Maria Laura", "Maria Heloísa", "Maria Sophia", "Fernanda", "Malu", "Analu", "Amanda", "Aurora", "Maria Isis", "Louise", "Heloise", "Ana Vitória", "Ana Cecília", "Ana Liz", "Joana", "Luana", "Antônia", "Isabel", "Bruna" };
            string[] vTelefone = GerarNumerosCelular(100);
            string[] vCpf = GerarCPFs(100);

            for (int i = 0; i < 100; i++)
            {

                Cliente cliente = new Cliente();

                cliente.nome = (i % 2 == 0) ? vNomeMas[i / 2] : vNomeFem[i / 2];
                cliente.telefone = vTelefone[i];
                cliente.cpf = vCpf[i];

                contexto.Clientes.Add(cliente);
            }
            contexto.SaveChanges();

            return View(contexto.Clientes.OrderBy(o => o.nome).ToList());
        }

        public IActionResult Funcionarios()
        {
            contexto.Database.ExecuteSqlRaw("delete from funcionarios");
            contexto.Database.ExecuteSqlRaw("DBCC CHECKIDENT('funcionarios', RESEED, 0)");
            Random randNum = new Random();

            string[] vNomeMas = { "Miguel", "Arthur", "Bernardo", "Heitor", "Davi", "Lorenzo", "Théo", "Pedro", "Gabriel", "Enzo", "Matheus", "Lucas", "Benjamin", "Nicolas", "Guilherme", "Rafael", "Joaquim", "Samuel", "Enzo Gabriel", "João Miguel", "Henrique", "Gustavo", "Murilo", "Pedro Henrique", "Pietro", "Lucca", "Felipe", "João Pedro", "Isaac", "Benício", "Daniel", "Anthony", "Leonardo", "Davi Lucca", "Bryan", "Eduardo", "João Lucas", "Victor", "João", "Cauã", "Antônio", "Vicente", "Caleb", "Gael", "Bento", "Caio", "Emanuel", "Vinícius", "João Guilherme", "Davi Lucas", "Noah", "João Gabriel", "João Victor", "Luiz Miguel", "Francisco", "Kaique", "Otávio", "Augusto", "Levi", "Yuri", "Enrico", "Thiago", "Ian", "Victor Hugo", "Thomas", "Henry", "Luiz Felipe", "Ryan", "Arthur Miguel", "Davi Luiz", "Nathan", "Pedro Lucas", "Davi Miguel", "Raul", "Pedro Miguel", "Luiz Henrique", "Luan", "Erick", "Martin", "Bruno", "Rodrigo", "Luiz Gustavo", "Arthur Gabriel", "Breno", "Kauê", "Enzo Miguel", "Fernando", "Arthur Henrique", "Luiz Otávio", "Carlos Eduardo", "Tomás", "Lucas Gabriel", "André", "José", "Yago", "Danilo", "Anthony Gabriel", "Ruan", "Miguel Henrique", "Oliver" };
            string[] vNomeFem = { "Alice", "Sophia", "Helena", "Valentina", "Laura", "Isabella", "Manuela", "Júlia", "Heloísa", "Luiza", "Maria Luiza", "Lorena", "Lívia", "Giovanna", "Maria Eduarda", "Beatriz", "Maria Clara", "Cecília", "Eloá", "Lara", "Maria Júlia", "Isadora", "Mariana", "Emanuelly", "Ana Júlia", "Ana Luiza", "Ana Clara", "Melissa", "Yasmin", "Maria Alice", "Isabelly", "Lavínia", "Esther", "Sarah", "Elisa", "Antonella", "Rafaela", "Maria Cecília", "Liz", "Marina", "Nicole", "Maitê", "Isis", "Alícia", "Luna", "Rebeca", "Agatha", "Letícia", "Maria-", "Gabriela", "Ana Laura", "Catarina", "Clara", "Ana Beatriz", "Vitória", "Olívia", "Maria Fernanda", "Emilly", "Maria Valentina", "Milena", "Maria Helena", "Bianca", "Larissa", "Mirella", "Maria Flor", "Allana", "Ana Sophia", "Clarice", "Pietra", "Maria Vitória", "Maya", "Laís", "Ayla", "Ana Lívia", "Eduarda", "Mariah", "Stella", "Ana", "Gabrielly", "Sophie", "Carolina", "Maria Laura", "Maria Heloísa", "Maria Sophia", "Fernanda", "Malu", "Analu", "Amanda", "Aurora", "Maria Isis", "Louise", "Heloise", "Ana Vitória", "Ana Cecília", "Ana Liz", "Joana", "Luana", "Antônia", "Isabel", "Bruna" };
            string[] vDominio = { "UOL", "Globo", "FEMA", "FEMANET", "GMAIL", "yahoo" };
            string[] vTelefone = GerarNumerosCelular(100);
            string[] vCpf = GerarCPFs(100);

            for (int i = 0; i < 100; i++)
            {

                Funcionario funcionario = new Funcionario();

                funcionario.nome = (i % 2 == 0) ? vNomeMas[i / 2] : vNomeFem[i / 2];
                funcionario.email = funcionario.nome.ToLower() + "@" + vDominio[randNum.Next() % 6].ToLower() + ".com.br";
                funcionario.telefone = vTelefone[i];
                funcionario.cpf = vCpf[i];
                funcionario.salario = randNum.Next(1350, 3000);

                contexto.Funcionarios.Add(funcionario);
            }
            contexto.SaveChanges();

            return View(contexto.Funcionarios.OrderBy(o => o.nome).ToList());
        }

        public IActionResult Fornecedores()
        {
            contexto.Database.ExecuteSqlRaw("delete from fornecedores");
            contexto.Database.ExecuteSqlRaw("DBCC CHECKIDENT('fornecedores', RESEED, 0)");

            string[] vNome = { "Adidas", "Calvin Klein", "Champion", "Gap", "Levi's", "Nike", "Puma", "Tommy Hilfiger", "Under Armour", "Vans" };
            string[] vTelefone = GerarNumerosCelular(10);
            string[] vEndereco = { "Rua A n°123", "Avenida B n°456 ", "Travessa C n°789", "Alameda D n°101", "Praça E n°202", "Estrada F n°303", "Avenida G n°404", "Rua H n°505", "Travessa I n°606", "Alameda J n°707" };
            string[] vCnpj = GerarCNPJs(10);

            for (int i = 0; i < 10; i++)
            {

                Fornecedor fornecedor = new Fornecedor();

                fornecedor.nome = vNome[i];
                fornecedor.telefone = vTelefone[i];
                fornecedor.endereco = vEndereco[i];
                fornecedor.cnpj = vCnpj[i];

                contexto.Fornecedores.Add(fornecedor);
            }
            contexto.SaveChanges();

            return View(contexto.Fornecedores.OrderBy(o => o.nome).ToList());
        }

        public IActionResult Produtos()
        {
            contexto.Database.ExecuteSqlRaw("delete from produtos");
            contexto.Database.ExecuteSqlRaw("DBCC CHECKIDENT('produtos', RESEED, 0)");
            Random randNum = new Random();

            string[] vProduto = {"Bermuda", "Calça", "Camiseta", "Moletom", "Polo", "Regata"};
            string[] vMarca = {"Adidas", "Calvin Klein", "Champion", "Gap", "Levi's", "Nike", "Puma", "Tommy Hilfiger", "Under Armour", "Vans"};

            for (int i = 0; i < 50; i++) 
            {

                Produto produto = new Produto();

                int cat = randNum.Next(1, 6);
                int forn = randNum.Next(1, 10);

                produto.descricao = vProduto[cat] + " " + vMarca[forn];
                produto.preco = randNum.Next(70, 1200);
                produto.qtdeEstoque = randNum.Next(1, 100);
                produto.categoriaID = ++cat; 
                produto.fornecedorID = ++forn;

                contexto.Produtos.Add(produto);
            }
            contexto.SaveChanges();

            return View(contexto.Produtos
                .Include(cat => cat.categoria)
                .Include(forn => forn.fornecedor)
                .OrderBy(o => o.descricao).ToList());
        }

        public IActionResult Vendas()
        {
            contexto.Database.ExecuteSqlRaw("delete from vendas");
            contexto.Database.ExecuteSqlRaw("DBCC CHECKIDENT('vendas', RESEED, 0)");
            Random randNum = new Random();

            for (int i = 0; i < 50; i++)
            {

                Venda venda = new Venda();

                venda.data = Convert.ToDateTime("01/01/2010").AddDays(randNum.Next(0, 5036));
                venda.clienteID = randNum.Next(1, 100);
                venda.funcionarioID = randNum.Next(1, 100);
                venda.produtoID = randNum.Next(1, 50);
                venda.quantidade = randNum.Next(1, 15);

                Produto produto = contexto.Produtos.Find(venda.produtoID);
                if (produto != null)
                    venda.total = produto.preco * venda.quantidade;
                else
                    venda.total = 0;
                contexto.Vendas.Add(venda);
            }
            contexto.SaveChanges();

            return View(contexto.Vendas
                .Include(cli => cli.cliente)
                .Include(func => func.funcionario)
                .Include(prod => prod.produto)
                .OrderBy(o => o.data).ToList());
        }

        public IActionResult CompraProdutos()
        {
            contexto.Database.ExecuteSqlRaw("delete from compraProdutos");
            contexto.Database.ExecuteSqlRaw("DBCC CHECKIDENT('compraProdutos', RESEED, 0)");
            Random randNum = new Random();

            for (int i = 0; i < 80; i++)
            {

                CompraProduto compraProduto = new CompraProduto();

                compraProduto.produtoID = randNum.Next(1, 50);
                compraProduto.qtde = randNum.Next(1, 40);

                contexto.CompraProdutos.Add(compraProduto);
            }
            contexto.SaveChanges();

            return View(contexto.CompraProdutos
                .Include(prod => prod.produto)
                .OrderBy(o => o.produtoID).ToList());
        }

        static string[] GerarCPFs(int quantidade)
        {
            string[] cpfs = new string[quantidade];
            Random random = new Random();

            for (int i = 0; i < quantidade; i++)
            {
                cpfs[i] = GerarCPF(random);
            }

            return cpfs;
        }

        static string GerarCPF(Random random)
        {
            int[] cpfBase = new int[9];
            for (int i = 0; i < 9; i++)
            {
                cpfBase[i] = random.Next(10);
            }

            int primeiroDigitoVerificador = CalcularDigitoVerificador(cpfBase);
            int segundoDigitoVerificador = CalcularDigitoVerificador(cpfBase.Append(primeiroDigitoVerificador).ToArray());

            string cpfFormatado = $"{cpfBase[0]}{cpfBase[1]}{cpfBase[2]}.{cpfBase[3]}{cpfBase[4]}{cpfBase[5]}.{cpfBase[6]}{cpfBase[7]}{cpfBase[8]}-{primeiroDigitoVerificador}{segundoDigitoVerificador}";

            return cpfFormatado;
        }

        static int CalcularDigitoVerificador(int[] cpfParcial)
        {
            int soma = 0;
            int peso = cpfParcial.Length + 1;

            for (int i = 0; i < cpfParcial.Length; i++)
            {
                soma += cpfParcial[i] * peso;
                peso--;
            }

            int resto = soma % 11;
            int digitoVerificador = 11 - resto;

            if (resto < 2)
            {
                digitoVerificador = 0;
            }

            return digitoVerificador;
        }

        static string[] GerarNumerosCelular(int quantidade)
        {
            string[] numerosCelular = new string[quantidade];
            Random random = new Random();

            for (int i = 0; i < quantidade; i++)
            {
                numerosCelular[i] = GerarNumeroCelular(random);
            }

            return numerosCelular;
        }

        static string GerarNumeroCelular(Random random)
        {
            string[] ddds = { "11", "21", "31", "41", "51" };
            string ddd = ddds[random.Next(ddds.Length)];

            string parteNumerica = $"{random.Next(1000):0000}-{random.Next(10000):0000}";

            string numeroCelular = $"+55 {ddd} {parteNumerica}";

            return numeroCelular;
        }

        static string[] GerarCNPJs(int quantidade)
        {
            string[] cnpjs = new string[quantidade];
            Random random = new Random();

            for (int i = 0; i < quantidade; i++)
            {
                cnpjs[i] = GerarCNPJ(random);
            }

            return cnpjs;
        }

        static string GerarCNPJ(Random random)
        {
            int[] cnpjBase = new int[8];
            for (int i = 0; i < 8; i++)
            {
                cnpjBase[i] = random.Next(10);
            }

            int[] digitosVerificadores = GerarDigitosVerificadoresCNPJ(random);

            string cnpjFormatado = $"{cnpjBase[0]}{cnpjBase[1]}.{cnpjBase[2]}{cnpjBase[3]}{cnpjBase[4]}/{cnpjBase[5]}{cnpjBase[6]}{cnpjBase[7]}-{digitosVerificadores[0]}{digitosVerificadores[1]}";

            return cnpjFormatado;
        }

        static int[] GerarDigitosVerificadoresCNPJ(Random random)
        {
            int[] digitosVerificadores = new int[2];

            digitosVerificadores[0] = random.Next(10);
            digitosVerificadores[1] = random.Next(10);

            return digitosVerificadores;
        }
    }
}
