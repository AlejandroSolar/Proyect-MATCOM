namespace Engine
{
    // entidad encargada de controlar un turno de cada jugador actual en el juego (una vuelta a la)
    public static class Ronda
    {
        // comienza la ronda
        public static bool Run
            (
            List<Player> players,
            List<IValid> reglas,
            List<IStopCondition> conditions,
            Board mesa,
            Register registro,
            List<Pareja> parejas
            )
        {
            for (int i = 0; i < players.Count; i++)
            {
                // muestra en pantalla el estado actual del juego
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Es el turno de {players[i].Nombre}:");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("Tú Mano:");
                players[i].PrintHand();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Mesa:");
                System.Console.WriteLine(mesa.ToString());
                Console.WriteLine();

                // detiene la ronda si alguna condición de parada se cumple
                for (int j = 0; j < conditions.Count; j++)
                {
                    if (conditions[j].Stop(Auxiliar.ClonePlayers(players), mesa.Clone(), new List<IValid>(reglas)))
                    {
                        return true;
                    }

                }

                // alerta al jugador de que se pasó 
                if (Pasate(players[i], reglas, mesa))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No puedes jugar");
                    Console.ResetColor();
                    Console.ReadLine();

                    // actualiza el registro con el turno fallido del jugador
                    registro.Add(new Evento(players[i].Nombre, null!, false));
                    continue;
                }


                Jugada jugada = players[i].Play(mesa.Clone(), new List<IValid>(reglas), registro.Clone(), new List<Pareja>(parejas));

                bool Posición = CheckPosition(jugada, reglas, mesa);

                // si no se puede jugar la ficha seleccionada por el jugador 
                // no permite jugarla y vuelve a empezar su turno hasta que 
                // seleccione una ficha correcta o se pase
                if (!Posición)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Jugada no valida");
                    Thread.Sleep(500);
                    Console.ResetColor();
                    i -= 1;
                    continue;
                }

                // finalmente juega la ficha
                Play(players[i], jugada, mesa);

                // actualiza el registro
                registro.Add(new Evento(players[i].Nombre, jugada.Ficha, jugada.Posición));
                registro.PrintActual();


                Console.ReadLine();
            }
            return false;
        }

        // verifica si el jugador puede jugar al menos una ficha y si no automaticamente pasa el turno al siguiente
        public static bool Pasate(Player player, List<IValid> reglas, Board mesa)
        {
            List<Jugada> posibles = Auxiliar.LlenarPosibles(player.Mano, reglas, mesa);

            if (posibles.Count == 0)
            {
                return true;
            }
            return false;
        }

        // recibe la jugada que selecciona el jugador, verifica si es posible jugarla y la juega
        private static void Play(Player player, Jugada jugada, Board Mesa)
        {
            if (!jugada.Posición)
            {
                if (jugada.Girada)
                {
                    jugada.Ficha.Turn();
                }
                Mesa.PreAdd(jugada.Ficha);
            }

            else
            {
                if (jugada.Girada)
                {
                    jugada.Ficha.Turn();
                }
                Mesa.Add(jugada.Ficha);
            }

            player.Mano.Remove(jugada.Ficha);
        }

        // verifica si la jugada seleccionada por el jugador es valida para al menos una de las posibles reglas
        private static bool CheckPosition(Jugada jugada, List<IValid> reglas, Board mesa)
        {
            for (int i = 0; i < reglas.Count; i++)
            {
                if (reglas[i].Valid(jugada, mesa))
                {
                    return true;
                }
            }
            return false;
        }

    }
}