namespace Engine;

/// <summary>
/// La clase <c>PlayerCreator</c> encierra al método necesario para la creación de la colección de jugadores.
/// </summary>
public static class PlayerCreator
{
    /// <value> <c>Strategies</c> contiene una instancia de cada tipo de estrategia que esté implementada</value>
    private static IEnumerable<IStrategy> Strategies = Reflection<IStrategy>.ListCreator(Reflection<IStrategy>.TypesCollectionCreator());

    /// <summary>
    /// Crea la lista de jugadores de acuerdo con las indicaciones del usuario.
    /// </summary>
    /// <returns>
    /// Una colección de <typeparamref name="Player"/> con los jugadores creados.
    /// </returns>
    public static IList<Player> Players()
    {
        // lista a la que se van a ir añadiendo los jugadores.
        IList<Player> players = new List<Player>();

        // el usuario elige cuantos jugadores van a participar.
        int numberOfPlayers = Auxiliar.NumberSelector("Elija la cantidad de jugadores", 1);

        // asigna a cada jugador un nombre y una estrategia.
        for (int i = 0; i < numberOfPlayers; i++)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Diga el nombre del jugador {0}", i + 1);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            string playerName = Console.ReadLine()!;

            if (string.IsNullOrEmpty(playerName))
            {
                playerName = $"Player {i + 1}";
            }

            // representa si hay algun nombre que se repita entre los jugadores 
            bool repited = false;

            // actualiza repited.
            for (int j = 0; j < players.Count; j++)
            {
                if (playerName == players[j].Name)
                {
                    repited = true;
                    break;
                }
            }
            // no permite que el usuario le ponga el mismo nombre a varios jugadores
            if (repited)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nombre ya existente!");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(2000);
                i -= 1;
                continue;
            }

            // el usuario elige la estrategia
            IStrategy strategy = Auxiliar<IStrategy>.Selector($"Elige la estrategia que usara {playerName}", Strategies);

            Type typeOfStrategy = strategy.GetType();

            // añade a la lista el jugador con su nombre y estrategias correspondientes
            players.Add(new Player(playerName, Reflection<IStrategy>.DefaultInstanceCreator(typeOfStrategy)));
        }
        return players;
    }
}
