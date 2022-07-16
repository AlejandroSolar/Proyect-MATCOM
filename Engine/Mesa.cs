using System.Text;
namespace Engine
{

    // clase que define a la mesa 
    public class Board
    {
        // lista de fichas que la representa
        private List<Ficha> table { get; set; }

        // valor actual de la mesa, acorde al valor interno de cada ficha
        public int value { get { return GetValue(); } }

        public int Count { get { return table.Count; } }

        public Ficha First { get { return table.First(); } }

        public Ficha Last { get { return table.Last(); } }

        // constructor
        public Board()
        {
            table = new List<Ficha>();
        }

        // metodo que calcula el valor de la mesa
        private int GetValue()
        {
            int valor = 0;
            foreach (var item in table)
            {
                valor += item.Valor;
            }
            return valor;
        }

        // pone la ficha a la derecha de la mesa
        public void Add(Ficha f)
        {
            table.Add(f);
        }

        // pone la ficha a la izquierda de la mesa
        public void PreAdd(Ficha f)
        {
            table.Insert(0, f);
        }

        // si se colocó una ficha magica en la mesa la actualizo
        public override string ToString()
        {
            StringBuilder X = new StringBuilder();

            foreach (var item in table)
            {
                X.Append($"-{item.ToString()}-");
            }
            return X.ToString();
        }

        // crea una nueva instancia de la mesa para evitar que se cambien los valores de la actual
        public Board Clone()
        {
           Board clone = new Board();
           clone.table = Auxiliar.CloneList(this.table);
           return clone;
        }
    }
}