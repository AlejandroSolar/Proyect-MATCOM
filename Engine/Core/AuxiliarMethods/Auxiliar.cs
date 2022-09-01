namespace Engine;

/// <summary>
/// La clase <c>Auxiliar</c> agrupa métodos que son de utilidad en la aplicación.
/// </summary>
public class Auxiliar
{
    /// <summary>
    /// Elabora una colección de <typeparamref name="Moves"/> posibles mediante los datos recibidos.
    /// </summary>
    /// <param name="hand"> mano actual del jugador. </param>
    /// <param name="rules"> reglas del juego. </param>
    /// <param name="table"> estado de la mesa. </param>
    /// <returns>
    /// Una colección de <typeparamref name="Moves"/> que pueden ser jugados.
    /// </returns>
    public static IEnumerable<Move> PossiblePlays(IEnumerable<Piece> hand, IEnumerable<IValid> rules, Board table)
    {
        IList<Move> forReturn = new List<Move>();

        //Doble for que crea todos los Move posibles a partir de cada ficha
        //y analiza cuales son válidos para, al menos, una de las reglas del juego actual
        for (int i = 0; i < hand.Count(); i++)
        {
            for (int j = 0; j < rules.Count(); j++)
            {
                if (rules.ElementAt(j).Valid(new Move(hand.ElementAt(i), Move.Position.left, false), table))
                {
                    forReturn.Add(new Move(hand.ElementAt(i), Move.Position.left, false));
                }

                if (rules.ElementAt(j).Valid(new Move(hand.ElementAt(i), Move.Position.right, false), table))
                {
                    forReturn.Add(new Move(hand.ElementAt(i), Move.Position.right, false));
                }

                if (rules.ElementAt(j).Valid(new Move(hand.ElementAt(i), Move.Position.left, true), table))
                {
                    forReturn.Add(new Move(hand.ElementAt(i), Move.Position.left, true));
                }

                if (rules.ElementAt(j).Valid(new Move(hand.ElementAt(i), Move.Position.right, true), table))
                {
                    forReturn.Add(new Move(hand.ElementAt(i), Move.Position.right, true));
                }
            }
        }
        return forReturn;
    }

    /// <summary>
    /// Hace al usuario una pregunta de si o no, dicha pregunta se recibe de parámetro.
    /// </summary>
    /// <param name="question"> pregunta hecha al usuario. </param>
    /// <returns>
    /// Un <typeparamref name= "bool"/> acorde con la elección del usuario.
    /// </returns>
    public static bool YesOrNo(string question)
    {
        while (true)
        {
            string answer = Auxiliar<string>.Selector(question, new List<string> { "Si", "No" });

            switch (answer)
            {
                case "Si":
                    return true;

                case "No":
                    return false;

                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Permite al usuario decidir por una cantidad acorde a los parámetros recibidos
    /// usando las flechas direccionales del teclado para aumentar o reducir la cantidad.
    /// </summary>
    /// <param name="subtitle"> criterio por el que se le pregunta al usuario. </param>
    /// <param name="minimalNumber"> cantidad minima aceptada de acuerdo al criterio . </param>
    /// <returns>
    /// Un <typeparamref name= "int"/> que representa la cantidad elegida por el usuario.
    /// </returns>
    public static int NumberSelector(string subtitle, int minimalNumber)
    {
        int selectedNumber = minimalNumber;

        while (true)
        {
            if (selectedNumber < minimalNumber)
            {
                selectedNumber = minimalNumber;
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{subtitle}:");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine($"{selectedNumber}");
            Console.WriteLine();
            Console.WriteLine("Aumente o disminuya la cifra con las flechas de dirección, pulse Enter para aceptar");


            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.RightArrow:
                    selectedNumber++;
                    break;

                case ConsoleKey.LeftArrow:
                    selectedNumber--;
                    break;

                case ConsoleKey.UpArrow:
                    selectedNumber += 10;
                    break;

                case ConsoleKey.DownArrow:
                    selectedNumber -= 10;
                    break;

                case ConsoleKey.Enter:
                    return selectedNumber;

                default:
                    break;
            }
        }
    }
}
