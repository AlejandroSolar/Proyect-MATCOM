using Engine;

// el nombre de la consola
Console.Title = "Dominó Alejan2";
Console.ForegroundColor = ConsoleColor.White;
Console.Clear();

// lista de opciones dadas al usuario en una primera fase del programa
List<string> Options = new List<string>
{
    "Nueva Partida",
    "Nuevo Torneo",
    "Salir"
};

// ejecución del programa
while (true)
{
    // el usuario elige si empezar un nuevo juego, torneo o si salir del programa
    string Select = Auxiliar<string>.Selector("DOMINÓ ALEJAN2!", Options);

    IList<Player> Crew;

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
            Game newGame = GameCreator.Game(Crew);
            newGame.RunGame();
        }

        else if (Select == Options[1])
        {
            Tournament T = TournamentCreator.Tournament(Crew);
            T.Run();
        }

        else
        {
            continue;
        }
    }
}