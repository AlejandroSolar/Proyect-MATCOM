namespace Engine
{
    // entidad encargada de selecionar que jugada hacer de acuerdo a una cierta estrategia
    public interface IStrategy
    {
        // retorna esa jugada
        Jugada Jugada(List<Ficha> Mano, List<Jugada> posibles, Board Mesa, Register registro, List<Pareja> parejas);
    }

    // Jugador Humano. progunta al usuario por la ficha
    public class HumanST : IStrategy
    {
        public Jugada Jugada(List<Ficha> Mano, List<Jugada> posibles, Board Mesa, Register registro, List<Pareja> parejas)
        {
            Ficha ficha = Auxiliar<Ficha>.Selector($"Estado de la mesa:\n{Mesa.ToString()}\n\nElija su jugada", Mano);

            if (Mesa.Count == 0)
            {
                return new Jugada(ficha, false, false);
            }

            Jugada Final;

            List<Jugada> NewPosibles = new List<Jugada>();

            for (int i = 0; i < posibles.Count; i++)
            {
                if (ficha == posibles[i].Ficha)
                {
                    NewPosibles.Add(posibles[i]);
                }
            }

            if (NewPosibles.Count == 0)
            {
                Final = new Jugada(ficha, false, false);
            }

            else if (NewPosibles.Count == 1)
            {
                Final = NewPosibles.First();
            }

            else
            {
                if (Ambiguo(NewPosibles))
                {
                    bool pos = Askpos();

                    for (int i = NewPosibles.Count - 1; i >= 0; i--)
                    {
                        if (NewPosibles[i].Posición != pos)
                        {
                            NewPosibles.RemoveAt(i);
                        }
                    }

                    if (NewPosibles.Count == 1)
                    {
                        Final = NewPosibles.First();
                    }

                    else
                    {
                        Final = new Jugada(ficha, pos, AskTurn(ficha));
                    }
                }

                else
                {
                    Final = new Jugada(ficha, NewPosibles.First().Posición, AskTurn(ficha));
                }
            }

            return Final;
        }

        private bool Ambiguo(List<Jugada> NewPosibles)
        {
            bool izq = false;
            bool Dch = false;

            for (int i = 0; i < NewPosibles.Count; i++)
            {
                if (NewPosibles[i].Posición)
                {
                    Dch = true;
                    continue;
                }

                else
                {
                    izq = true;
                }
            }

            if (izq && Dch)
            {
                return true;
            }

            return false;
        }

        private bool Askpos()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("La Ficha que desea jugar se puede colocar en mas de una posición");
            Console.ResetColor();
            Console.WriteLine("Elija en cual desea jugarla:");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[I] ");
            Console.ResetColor();
            Console.WriteLine("Izquierda");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[D] ");
            Console.ResetColor();
            Console.WriteLine("Derecha");

            while (true)
            {
                ConsoleKey input = Console.ReadKey(true).Key;

                switch (input)
                {
                    case ConsoleKey.I:
                        return false;

                    case ConsoleKey.D:
                        return true;

                    default:
                        break;
                }
            }
        }

        private bool AskTurn(Ficha ficha)
        {
            if (ficha.PartDch == ficha.PartIzq)
            {
                return false;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("¿Desea girar la ficha antes de colocarla?");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[S] ");
            Console.ResetColor();
            Console.WriteLine("Si");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[N] ");
            Console.ResetColor();
            Console.WriteLine("No");

            while (true)
            {
                ConsoleKey input = Console.ReadKey(true).Key;

                switch (input)
                {
                    case ConsoleKey.N:
                        return false;

                    case ConsoleKey.S:
                        return true;

                    default:
                        break;
                }
            }
        }

        public override string ToString()
        {
            return "Jugador Humano";
        }
    }

    // prioriza soltar las fichas de mayor valor
    public class BotagordaST : IStrategy
    {
        public Jugada Jugada(List<Ficha> Mano, List<Jugada> posibles, Board Mesa, Register registro, List<Pareja> parejas)
        {
            int bestSum = 0;
            int index = 0;
            for (int i = 0; i < posibles.Count; i++)
            {
                int ActualSum = posibles[i].Ficha.PartIzq + posibles[i].Ficha.PartDch;
                if (bestSum < ActualSum)
                {
                    bestSum = ActualSum;
                    index = i;
                }
            }

            return posibles[index];
        }

        public override string ToString()
        {
            return "Jugador Botagorda";
        }
    }

    // elige la ficha aleatoriamente
    public class RandomST : IStrategy
    {
        public virtual Jugada Jugada(List<Ficha> Mano, List<Jugada> posibles, Board Mesa, Register registro, List<Pareja> parejas)
        {
            Random r = new Random();
            return posibles[r.Next(posibles.Count)];
        }

        public override string ToString()
        {
            return "Jugador Random";
        }
    }

    // prioriza soltar las fichas que sean dobles
    public class BotaDobleST : IStrategy
    {
        public Jugada Jugada(List<Ficha> Mano, List<Jugada> posibles, Board Mesa, Register registro, List<Pareja> parejas)
        {
            for (int i = 0; i < posibles.Count; i++)
            {
                if (posibles[i].Ficha.PartIzq == posibles[i].Ficha.PartDch)
                {
                    return posibles[i];
                }
            }
            return posibles[0];
        }

        public override string ToString()
        {
            return "Jugador Botadobles";
        }
    }

    // añade arbitrariamente jugadas no validas y elige aleatoriamente
    public class DrunkST : RandomST
    {
        public override Jugada Jugada(List<Ficha> Mano, List<Jugada> posibles, Board Mesa, Register Registro, List<Pareja> parejas)
        {
            int add = 0;

            for (int i = 0; i < Mano.Count; i++)
            {
                bool contains = false;
                for (int j = 0; j < posibles.Count; j++)
                {
                    if (Mano[i] == posibles[j].Ficha)
                    {
                        contains = true;
                        break;
                    }
                }

                if (!contains)
                {
                    posibles.Add(new Jugada(Mano[i], false, false));
                    add++;
                }

                if (add == 3)
                {
                    break;
                }
            }

            return base.Jugada(Mano, posibles, Mesa, Registro,parejas);
        }

        public override string ToString()
        {
            return "Jugador Borracho";
        }
    }

    public class CleverST : IStrategy
    {
        public Jugada Jugada(List<Ficha> Mano, List<Jugada> posibles, Board Mesa, Register Registro, List<Pareja> parejas)
        {
            // los valores que se pueden quedar como extremos con las posibles jugadas
            Dictionary<int, int> Values = new Dictionary<int, int>(); 

            // los valores por los que los demas jugadores se han pasado
            Dictionary<int, int> ValuesforPlay = new Dictionary<int, int>(); 

            // crea una mesa imaginaria donde va probando las jugadoas
            Board Imaginary = new Board();

            // recorre el registro construyendo la mesa imaginaria
            for(int i = 0; i < Registro.Count; i++)
            {
                Evento Current = Registro[i];

                // si alguien se pasó guarda por cual valor lo hizo
                if (Current.Jugada is null)
                {
                    if (Imaginary.Count == 0)
                    {
                        continue;
                    }

                    int Izq = Imaginary.First.PartIzq;
                    int Dch = Imaginary.Last.PartDch;

                    CheckValue(ValuesforPlay, Izq);
                    if (Izq != Dch)
                    {
                        CheckValue(ValuesforPlay, Dch);
                    }
                }

                else if (Current.Posicion)
                {
                    Imaginary.Add(Current.Jugada);
                }

                else
                {
                    Imaginary.PreAdd(Current.Jugada);
                }
            }

            // si nadie se ha pasado juega la primera de las posibles
            if (ValuesforPlay.Count == 0)
            {
                return posibles.First();
            }

            for (int i = 0; i < posibles.Count; i++)
            {
                int Number = 0;

                if (posibles[i].Posición)
                {
                    if (posibles[i].Girada)
                    {
                        Number = posibles[i].Ficha.PartIzq;
                    }

                    else
                    {
                        Number = posibles[i].Ficha.PartDch;
                    }
                }

                else
                {
                    if (posibles[i].Girada)
                    {
                        Number = posibles[i].Ficha.PartDch;
                    }

                    else
                    {
                        Number = posibles[i].Ficha.PartIzq;
                    }
                }

                if (!Values.ContainsKey(Number))
                {
                    Values.Add(Number, i);
                }
            }

            var B = ValuesforPlay.OrderByDescending(X => X.Value);

            // si puede dejar en la mesa el valor por el que alguien se pasó lo deja
            foreach (var item in B)
            {
                if (Values.ContainsKey((item.Key)))
                {
                    return posibles[Values[item.Key]];
                }
            }

            // si no tiene manera de forzar el pase de alguien juega la primera ficha posible
            return posibles.First();
        }

        public override string ToString()
        {
            return "jugador inteligente";
        }

        // actualiza el diccionario con los valores
        private void CheckValue(Dictionary<int, int> Values, int V)
        {
            if (!Values.ContainsKey(V))
            {
                Values.Add(V, 1);
            }

            else
            {
                Values[V] += 1;
            }
        }
    }

    // busca a su pareja si tiene y elige la jugada que no interceda con la de la pareja,
    // si no tiene pareja juega la primera jugada de las posibles
    public class ParejasST : IStrategy
    {
        private string? pareja { get; set; }

        public Jugada Jugada(List<Ficha> Mano, List<Jugada> posibles, Board Mesa, Register Registro, List<Pareja> parejas)
        {
            if (parejas.Count == 0 || Registro.Count <= 1)
            {
                return posibles.First();
            }

            if (this.pareja is null)
            {
                FindFriend(parejas);
            }

            bool FriendPlay = false;

            for (int i = Registro.Count - 1; i >= 0; i--)
            {
                if (Registro[i].Jugador == pareja)
                {
                    if (Registro[i].Jugada is null)
                    {
                        return posibles.First();
                    }

                    FriendPlay = Registro[i].Posicion;
                    break;
                }
            }

            for (int i = 0; i < posibles.Count; i++)
            {
                if (posibles[i].Posición == !FriendPlay)
                {
                    return posibles[i];
                }
            }

            return posibles.First();
        }

        private void FindFriend(List<Pareja> parejas)
        {
            for (int i = 0; i < parejas.Count; i++)
            {
                if (parejas[i].Player1.Estrategia == this)
                {
                    this.pareja = parejas[i].Player2.Nombre;
                }

                if (parejas[i].Player2.Estrategia == this)
                {
                    this.pareja = parejas[i].Player1.Nombre;
                }
            }
        }

        public override string ToString()
        {
            return "Jugador en parejas";
        }
    }
}