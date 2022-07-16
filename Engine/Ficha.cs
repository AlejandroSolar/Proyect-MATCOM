namespace Engine
{
    // el concepto de ficha
    public class Ficha
    {
        // tiene parte izquierda y parte derecha además de un valor dado por el evaluador
        public Ficha(int Izq, int Dch, int valor)
        {
            this.PartIzq = Izq;
            this.PartDch = Dch;
            this.Valor = valor;
        }

        // parte izquierda
        public int PartIzq { get; private set; }
        
        // parte derecha
        public int PartDch { get; private set; }

        // valor de la ficha
        public int Valor { get; private set; }

        // rota la ficha
        public void Turn()
        {
            int Temp = PartIzq;
            this.PartIzq = this.PartDch;
            this.PartDch = Temp;
        }

        // representación en string de la ficha
        public override string ToString()
        {
            return $"[{PartIzq}|{PartDch}]";
        }
    }
}