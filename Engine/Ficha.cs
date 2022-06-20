namespace Engine
{
    public class Ficha
    {
        public Ficha(int Izq, int Dch, int valor)
        {
            this.PartIzq = Izq;
            this.PartDch = Dch;
            this.Valor = valor;
        }

        public int PartIzq { get; set; }
        public int PartDch { get; set; }

        public int Valor { get; set; }

        public void Turn()
        {
            int Temp = PartIzq;
            this.PartIzq = this.PartDch;
            this.PartDch = Temp;
        }

        public override string ToString()
        {
            return $"[{PartIzq}|{PartDch}]";
        }
    }
}