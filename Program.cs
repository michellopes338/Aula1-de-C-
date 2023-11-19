//Screen Sound

string appName = @"

░██████╗░█████╗░██████╗░███████╗███████╗███╗░░██╗  ░██████╗░█████╗░██╗░░░██╗███╗░░██╗██████╗░
██╔════╝██╔══██╗██╔══██╗██╔════╝██╔════╝████╗░██║  ██╔════╝██╔══██╗██║░░░██║████╗░██║██╔══██╗
╚█████╗░██║░░╚═╝██████╔╝█████╗░░█████╗░░██╔██╗██║  ╚█████╗░██║░░██║██║░░░██║██╔██╗██║██║░░██║
░╚═══██╗██║░░██╗██╔══██╗██╔══╝░░██╔══╝░░██║╚████║  ░╚═══██╗██║░░██║██║░░░██║██║╚████║██║░░██║
██████╔╝╚█████╔╝██║░░██║███████╗███████╗██║░╚███║  ██████╔╝╚█████╔╝╚██████╔╝██║░╚███║██████╔╝
╚═════╝░░╚════╝░╚═╝░░╚═╝╚══════╝╚══════╝╚═╝░░╚══╝  ╚═════╝░░╚════╝░░╚═════╝░╚═╝░░╚══╝╚═════╝░
";
string helloMessage = "Seja Bem Vindo!";

void ShowLogo()
{
    Console.WriteLine(appName);
    Console.WriteLine(helloMessage);
}

int DisplayMenu(List<MenuOption> options)
{
    Console.WriteLine();
    for (int i = 0; i < options.Count; i++)
    {
        Console.WriteLine("[{0}] {1}", i, options[i]);
    }
    System.Console.Write("\nDigite sua opção: ");
    string chosenOption = Console.ReadLine()!;

    bool canParse = int.TryParse(chosenOption, out int chosenOptionNumeric);

    if (canParse == false)
    {
        Console.Clear();
        System.Console.WriteLine("Você precisa inserir um valor valido");
        return DisplayMenu(options);
    }

    if (chosenOptionNumeric > options.Count || chosenOptionNumeric < 0)
    {
        Console.Clear();
        System.Console.WriteLine("Essa não é uma opção valida");
        return DisplayMenu(options);
    }

    return chosenOptionNumeric;
}

void HandleMenu(List<MenuOption> options)
{
    while (true)
    {
        ShowLogo();
        int chosenOptionNumeric = DisplayMenu(options);
        options[chosenOptionNumeric].Execute();
    }
}

List<MenuOption> menuOptions = new()
{
    new RegisterBand(), new ListBand(), new EvaluateBand(), new ShowBandGrade(), new ExitOption()
};
HandleMenu(menuOptions);

abstract class MenuOption
{
    public abstract void Execute();

    public override string ToString()
    {
        return "option name";
    }
}

class Band
{
    public string Name;
    public List<int> grades = new();
    public Band(string inputName)
    {
        Name = inputName;
    }

    public override string ToString()
    {
        return Name;
    }

    public void AddGrade(int newGrade)
    {
        grades.Add(newGrade);
    }
}

static class Title
{
    static public void PrintTitle(string title)
    {
        int length = title.Length;
        string symbols = string.Empty.PadLeft(length, '*');

        System.Console.WriteLine(symbols);
        System.Console.WriteLine(title);
        System.Console.WriteLine(symbols);
        System.Console.WriteLine();
    }
}

class RegisterBand : MenuOption
{
    readonly string OptionName = "Registrar Banda";
    public override void Execute()
    {
        Console.Clear();
        Title.PrintTitle(OptionName);
        Console.Write("Digite o nome da Banda: ");
        string bandName = Console.ReadLine()!;
        
        if (ListBand.AddBand(bandName))
        {
            System.Console.WriteLine($"A banda {bandName} foi registrada com sucesso!");
        } else
        {
            System.Console.WriteLine("Erro ao salvar");
        }
        Thread.Sleep(800);
        Console.Clear();
    }

    public override string ToString()
    {
        return OptionName;
    }
}

class ListBand : MenuOption
{
    public static List<Band> Bands = new();
    readonly string OptionName = "Listar Bandas";
    public override void Execute()
    {
        do
        {
            Console.Clear();
            Title.PrintTitle(OptionName);
            foreach (var item in Bands)
            {
                System.Console.WriteLine(item);
            }
        } while (Console.ReadKey().Key != ConsoleKey.Enter);
        Console.Clear();
    }

    static public bool AddBand(string band)
    {
        if (band == string.Empty)
        {
            return false;
        }
        Bands.Add(new Band(band));
        return true;
    }

    public override string ToString()
    {
        return OptionName;
    }
}

class EvaluateBand : MenuOption
{
    readonly string OptionName = "Avaliar Banda";
    public override void Execute()
    {
        Console.Clear();
        Title.PrintTitle(OptionName);
        
        System.Console.WriteLine("Qual banda você quer avaliar? ");

        for (int i = 0; i < ListBand.Bands.Count; i++)
        {
            System.Console.WriteLine($"[{i}] - {ListBand.Bands[i]}");
        }

        string requiredBand = Console.ReadLine()!;
        bool canParse = int.TryParse(requiredBand, out int requiredBandNumeric);

        if (canParse == false)
        {
            System.Console.WriteLine("Opção Inválida");
            Thread.Sleep(1000);
            Console.Clear();
            return;
        }

        if (requiredBandNumeric < 0 || requiredBandNumeric > ListBand.Bands.Count)
        {
            System.Console.WriteLine("Opção Inválida");
            Thread.Sleep(1000);
            Console.Clear();
            return;
        }

        Band band = ListBand.Bands[requiredBandNumeric];

        Console.Clear();
        System.Console.Write($"Insira uma nota para a banda {band}: ");
        string grade = Console.ReadLine()!;

        bool canParseGrade = int.TryParse(grade, out int gradeNumeric);
        if (canParseGrade == false)
        {
            System.Console.WriteLine("Opção Inválida");
            Thread.Sleep(1000);
            Console.Clear();
            return;
        }

        if (gradeNumeric < 0 || gradeNumeric > 10)
        {
            System.Console.WriteLine("Opção Inválida");
            Thread.Sleep(1000);
            return;
        }

        band.AddGrade(gradeNumeric);
        System.Console.WriteLine("Nota adicionada com sucesso");
        Thread.Sleep(1000);
        Console.Clear();
    }

    public override string ToString()
    {
        return OptionName;
    }
}

class ShowBandGrade : MenuOption
{
    readonly string OptionName = "Exibir Média da Banda";
    public override void Execute()
    {
        Console.Clear();
        Title.PrintTitle(OptionName);
        System.Console.WriteLine("Qual banda você ver a média? ");

        for (int i = 0; i < ListBand.Bands.Count; i++)
        {
            System.Console.WriteLine($"[{i}] - {ListBand.Bands[i]}");
        }

        string requiredBand = Console.ReadLine()!;
        bool canParse = int.TryParse(requiredBand, out int requiredBandNumeric);

        if (canParse == false)
        {
            System.Console.WriteLine("Opção Inválida");
            Thread.Sleep(1000);
            Console.Clear();
            return;
        }

        if (requiredBandNumeric < 0 || requiredBandNumeric > ListBand.Bands.Count)
        {
            System.Console.WriteLine("Opção Inválida");
            Thread.Sleep(1000);
            Console.Clear();
            return;
        }

        Band band = ListBand.Bands[requiredBandNumeric];

        double avgGrade = band.grades.Average();

        System.Console.WriteLine($"\nA banda {band} tem a média {avgGrade}");
        Console.ReadLine();
        
        Console.Clear();
    }

    public override string ToString()
    {
        return OptionName;
    }
}

class ExitOption : MenuOption
{
    readonly string OptionName = "Sair";
    public override void Execute()
    {
        Console.Clear();
        Console.WriteLine("Fechando o programa");
        Environment.Exit(0);
    }

    public override string ToString()
    {
        return OptionName;
    }
}
