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
    if (chosenOption == "")
    {
        System.Console.WriteLine("Você precisa inserir um valor");
        return DisplayMenu(options);
    }

    int chosenOptionNumeric = int.Parse(chosenOption);

    if (chosenOptionNumeric > options.Count || chosenOptionNumeric < 0)
    {
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
        System.Console.WriteLine("Você escolheu a opção '{0} - {1}'", chosenOptionNumeric, options[chosenOptionNumeric]);
        options[chosenOptionNumeric].Execute();
    }
}

ShowLogo();
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
    public Band(string inputName)
    {
        Name = inputName;
    }

    public override string ToString()
    {
        return Name;
    }
}

class RegisterBand : MenuOption
{
    public override void Execute()
    {
        Console.Clear();
        Console.WriteLine("Registro de banda");
        Console.Write("Digite o nome da Banda: ");
        string bandName = Console.ReadLine()!;
        
        if (bandName == "")
        {
            return;
        }
        
        System.Console.WriteLine($"A banda {bandName} foi registrada com sucesso!");
        ListBand.AddBand(new Band(bandName));
        Thread.Sleep(2000);
        Console.Clear();
    }

    public override string ToString()
    {
        return "Registrar Banda";
    }
}

class ListBand : MenuOption
{
    static List<Band> Bands = new();
    public override void Execute()
    {
        do
        {
            Console.Clear();
            foreach (var item in Bands)
            {
                System.Console.WriteLine(item);
            }
        } while (Console.ReadKey().Key != ConsoleKey.Enter);
        Console.Clear();
    }

    static public void AddBand(Band band)
    {
        Bands.Add(band);
    }

    public override string ToString()
    {
        return "Listar Bandas";
    }
}

class EvaluateBand : MenuOption
{
    public override void Execute()
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return "Avaliar Banda";
    }
}

class ShowBandGrade : MenuOption
{
    public override void Execute()
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return "Exibir Média da Banda";
    }
}

class ExitOption : MenuOption
{
    public override void Execute()
    {
        Console.Clear();
        Console.WriteLine("Fechando o programa");
        Environment.Exit(0);
    }

    public override string ToString()
    {
        return "Sair";
    }
}
