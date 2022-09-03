namespace Engine;

/// <summary>
/// La clase <c>Auxiliar{T}</c> agrupa métodos que devuelven tipos genéricos y son de utilidad en la aplicación.
/// </summary>
public static class Auxiliar<T>
{
    /// <summary>
    /// Muestra al usuario una lista de opciones para que seleccione mediante
    /// las flechas direccionales una de estas.
    /// </summary>
    /// <param name="subtitle"> funcionalidad de las opciones a escoger. </param>
    /// <param name="options"> colección con las diferentes opciones a elegir. </param>
    /// <returns>
    /// El elemento seleccionado por el usuario entre las opciones dadas.
    /// </returns>
    public static T Selector(string subtitle, IEnumerable<T> options)
    {
        // opcion elegida por el usuario
        T selectedOption = default(T)!;

        // tamaño de las opciones
        int max = options.Count();

        // posicion actual del cursor
        int current = 0;

        // mueve el cursor hasta que el usuario selecciona una opción y la devuelve
        while (true)
        {
            //indice del foreach que recorre options.
            int index = 0;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{subtitle}: \n");
            Console.ResetColor();

            if (current < 0)
            {
                current = max - 1;
            }

            else if (current >= max)
            {
                current = 0;

            }

            foreach (var option in options)
            {
                if (index == current)
                {
                    Console.ForegroundColor = ConsoleColor.Green;

                    // actualiza la opcion resaltada por el usuario.
                    selectedOption = option;

                    Console.WriteLine(option!.ToString());
                    Console.ResetColor();
                    index++;
                    continue;
                }

                Console.WriteLine(option!.ToString());

                // actualiza el indice.
                index++;
            }

            ConsoleKey input = Console.ReadKey(true).Key;

            switch (input)
            {
                case ConsoleKey.DownArrow:
                    current++;
                    break;

                case ConsoleKey.UpArrow:
                    current--;
                    break;

                case ConsoleKey.Enter:
                    return selectedOption;

                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Mediante este método el usuario configura un aspecto personalizable del juego añadiendo
    /// opciones a una lista invocando al método <c>Selector</c> para elegirlas una a una.
    /// </summary>
    /// <param name="actual"> lista de opciones que el usuario va añadiendo. </param>
    /// <param name="typeCollection"> colección con las diferentes opciones a elegir. </param>
    /// <param name="orientation"> descripcion del aspecto a personalizar </param>
    public static void ConfigSelector(IList<T> actual, IEnumerable<Type> typeCollection, string orientation)
    {
        // crea una lista de opciones mediante reflection 
        IList<T> options = Reflection<T>.ListCreator(typeCollection);

        while (true)
        {
            // el usuario selecciona una opción
            T selectedOption = Auxiliar<T>.Selector(orientation, options);

            // guarda el tipo de variable de la opción seleccionada
            Type selectedType = selectedOption!.GetType();

            // añade una instancia de esta opción a la lista de configuraciones usando reflection
            actual.Add(Reflection<T>.DefaultInstanceCreator(selectedType));

            // remueve del listado de opciones la seleccionada por el usuario, para que no se vuelva elegir
            options.Remove(selectedOption);

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Configuraciones añadidas actualmente:");
            Console.ResetColor();
            Console.WriteLine();

            // imprime las configuraciones que se han seleccionado hasta ahora.
            foreach (var option in actual)
            {
                Console.WriteLine(option!.ToString());
            }

            Console.WriteLine();
            Console.WriteLine("Enter para continuar");
            Console.ReadLine();

            // detiene el ciclo si no quedan opciones por añadir
            if (options.Count == 0)
            {
                break;
            }

            // detiene el ciclo si el usuario ha terminado de elegir configuraciones de este aspecto del juego.
            if (Auxiliar.YesOrNo("¿Desea añadir una nueva configuración a la partida?"))
            {
                continue;
            }

            break;

        }
    }
}
