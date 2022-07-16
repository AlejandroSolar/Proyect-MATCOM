using System.Text;

namespace Engine
{
    // eventos por los cuales está conformado el registro,
    public class Evento
    {
        // nombre del jugador que jugó la ficha
        public string Jugador { get; private set; }

        // cual fue su jugada
        public Ficha Jugada { get; private set; }

        // en que lado de la mesa jugó
        public bool Posicion { get; private set; }

        public Evento(string nombre, Ficha jugada, bool posicion)
        {
            Jugador = nombre;
            Jugada = jugada;
            Posicion = posicion;
        }

        // imprime el evento
        public void PrintEvent(bool first)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(Jugador);
            Console.ResetColor();

            if (Jugada is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Se pasó");
                Console.ResetColor();
                return;
            }

            Console.Write(" Jugó la ficha ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(Jugada.ToString());
            Console.ResetColor();
            if (first)
            {
                Console.WriteLine();
            }

            if (!first)
            {
                string pos = "";
                switch (Posicion)
                {
                    case false: pos = "izquierda"; break;
                    case true: pos = "derecha"; break;
                }
                Console.Write(" a la ");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(pos);
                Console.ResetColor();
            }

        }
    }

    // entidad encargada de llevar un registro del juego actual
    public class Register
    {
        // el turno en el que se jugó la ficha
        private int Turno { get; set; }

        // cuantos turnos se han jugado
        public int Count { get { return Registro.Count; } }

        // permite indexar por el turno
        public Evento this[int i]
        {
            get { return Registro[i]; }
        }

        // almacena los eventos
        private List<Evento> Registro { get; set; }

        public Register()
        {
            Registro = new List<Evento>();
            Turno = 0;
        }

        // añade el evento a la lista para actualizarla
        public void Add(Evento x)
        {
            Registro.Add(x);
            Turno++;
        }

        // imprime la ultima jugada
        public void PrintActual()
        {
            bool first = false;

            if (Turno == 1)
            {
                first = true;
            }
            Console.Write($"Turno {Turno} : ");
            Registro.Last().PrintEvent(first);
        }

        // imprime el registro entero
        public void PrintRegister()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Resumen del Juego:");
            Console.WriteLine("------------------");
            Console.ResetColor();
            Console.WriteLine();

            for (int i = 0; i < Registro.Count; i++)
            {
                bool first = false;

                if (i == 0)
                {
                    first = true;
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{i + 1}:");
                Registro[i].PrintEvent(first);
            }
        }

        // crea una copia del registro
        public Register Clone()
        {
            Register clon = new Register();
            clon.Registro = new List<Evento>(this.Registro);
            return clon;
        }

    }
}

