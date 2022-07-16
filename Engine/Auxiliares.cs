namespace Engine
{
    // metodos auxiliares genéricos
    public static class Auxiliar<T>
    {
        // permite al usuario elegir por las distintas configuraciones usando direccionales
        public static T Selector(string Subtítulo, List<T> Options)
        {
            // tamaño de las opciones
            int Max = Options.Count;

            // posicion actual del cursor
            int Current = 0;

            // imprime las opciones y resalta la coincidente con la posición del cursor
            // hasta que el usuario eliga correctamente
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{Subtítulo}: \n");
                Console.ResetColor();

                if (Current < 0)
                {
                    Current = Max - 1;
                }

                else if (Current >= Max)
                {
                    Current = 0;
                }

                for (int i = 0; i < Max; i++)
                {
                    if (i == Current)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(Options[i]!.ToString());
                        Console.ResetColor();
                        continue;
                    }

                    Console.WriteLine(Options[i]!.ToString());
                }

                ConsoleKey input = Console.ReadKey(true).Key;

                switch (input)
                {
                    case ConsoleKey.DownArrow:
                        Current++;
                        break;

                    case ConsoleKey.UpArrow:
                        Current--;
                        break;

                    case ConsoleKey.Enter:
                        return Options[Current];

                    default:
                        break;
                }
            }
        }

       
        // Se encarga de elegir las determinadas configuraciones del jueg0 acorde con lo definido por el usuario
        // llenando las listas de Condiciones de Parada entre otras
        public static void ConfigSelector(List<T> actual, List<Type> options, string Orientación)
        {
            // lista de la configuracion con la que está trabajando
            List<T> Options = Reflection<T>.ListCreator(options);

            while (true)
            {
                // el usuario selecciona una opción
                T Select = Auxiliar<T>.Selector(Orientación, Options);

                Type x = Select!.GetType();

                // añade esa opcion a la lista de configuraciones
                actual.Add(Reflection<T>.DefaultInstanceCreator(x));

                // remueve del listado de opciones a ofrecer al usuario
                Options.Remove(Select);

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Configuraciones añadidas actualmente:");
                Console.ResetColor();
                Console.WriteLine();

                for (int i = 0; i < actual.Count; i++)
                {
                    Console.WriteLine($" -- {actual[i]!.ToString()}");
                }

                Console.WriteLine();
                Console.WriteLine("Enter para continuar");
                Console.ReadLine();

                if (Options.Count == 0)
                {
                    break;
                }

                if(Auxiliar.YesOrNo("¿Desea añadir una nueva configuración a la partida?"))
                {
                    continue;
                }

                break;

            }
        }
    }

    // metodos auxiliares no genéricos
    public class Auxiliar
    {
        // se encarga de llenar una lista de jugadas posibles de acuerdo a las reglas del juego
        // y a las fichas que tenga en su mano el jugador
        public static List<Jugada> LlenarPosibles(List<Ficha> Mano, List<IValid> reglas, Board mesa)
        {
            List<Jugada> posibles = new List<Jugada>();

            for (int i = 0; i < Mano.Count; i++)
            {
                for (int j = 0; j < reglas.Count; j++)
                {
                    if(reglas[j].Valid(new Jugada(Mano[i],false,false),mesa))
                    {
                        posibles.Add(new Jugada(Mano[i],false,false));
                    }

                    if(reglas[j].Valid(new Jugada(Mano[i],true,false),mesa))
                    {
                        posibles.Add(new Jugada(Mano[i],true,false));
                    }

                    if(reglas[j].Valid(new Jugada(Mano[i],false,true),mesa))
                    {
                        posibles.Add(new Jugada(Mano[i],false,true));
                    }

                    if(reglas[j].Valid(new Jugada(Mano[i],true,true),mesa))
                    {
                        posibles.Add(new Jugada(Mano[i],true,true));
                    }
                }
            }

            return posibles;
        }
        // pregunta al usuario por preguntas de si o no
        public static bool YesOrNo(string Question)
        {
            while (true)
            {
                string Answer = Auxiliar<string>.Selector(Question, new List<string> { "Si", "No" });

                switch (Answer)
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

        // clona listas de fichas
        public static List<Ficha> CloneList (List<Ficha> original)
        {
            List<Ficha> clon = new List<Ficha>();

            for (int i = 0; i < original.Count; i++)
            {
                clon.Add(new Ficha(original[i].PartIzq,original[i].PartDch,original[i].Valor));
            }

            return clon;
        }

        // clona la lista de jugadores
        public static List<Player> ClonePlayers (List<Player> original)
        {
            List<Player> clon = new List<Player>();

            foreach (var item in original)
            {
                Player x = item.Clone();
                clon.Add(x);
            }
            return clon;
        }

        // permite al usuario determinar un entero que representa determinadas propiedades
        // como la cantidad de dobles que va a tener en dominó entre otras
         public static int NumberSelector(string Subtítulo, int Min)
        {
            int n = Min;

            while (true)
            {
                if (n < Min)
                {
                    n = Min;
                }

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{Subtítulo}:");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine($"{n}");
                Console.WriteLine();
                Console.WriteLine("Aumente o disminuya la cifra con las flechas de dirección, pulse Enter para aceptar");


                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.RightArrow:
                        n++;
                        break;

                    case ConsoleKey.LeftArrow:
                        n--;
                        break;

                    case ConsoleKey.UpArrow:
                        n += 10;
                        break;

                    case ConsoleKey.DownArrow:
                        n -= 10;
                        break;

                    case ConsoleKey.Enter:
                        return n;

                    default:
                        break;
                }
            }
        }
    }
}