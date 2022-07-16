using Engine;

// el nombre de la consola
Console.ForegroundColor = ConsoleColor.White;
Console.Title = "Domino Alejan2";
Console.Clear();

// lista de opciones dadas al usuario en una primara fase del proograma
List<string> Options = new List<string>
{
    "Nueva Partida",
    "Nuevo Torneo",
    "Salir"
};

// ejecución del programa
while (true)
{
    // el usuario elige si nuevo juego, torneo o si salir del programa
    string Select = Auxiliar<string>.Selector("DOMINÓ ALEJAN2!", Options);

    List<Player> Crew = new List<Player>();

    if (Select == Options[2])
    {
        return;
    }

    else
    {
        // lleno la lista de jugadores
        Crew = PlayerCreator.Players();

        if (Select == Options[0])
        {
            Juego X = GameCreator.Game(Crew);
            X.RunGame();
        }

        else if (Select == Options[1])
        {
            Tournament T = TournamentCreator.Torneo(Crew);
            T.Run();
        }

        else
        {
            continue;
        }
    }
}