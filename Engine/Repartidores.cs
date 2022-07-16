namespace Engine
{
    // entidad encargada de repartir las fichas a los jugadores
    public interface IRepartidor
    {
        // llena las manos a los jugadores de acuerdo a un criterio
        void Repartir(List<Player> jugadores, List<Ficha> box, int cantidad);
    }

    // repartidor ¨Clasico¨, distribuye aleatoriamente las fichas
    public class RandomRep : IRepartidor
    {
        public void Repartir(List<Player> jugadores, List<Ficha> box, int cantidad)
        {
            for (int i = 0; i < jugadores.Count; i++)
            {
                for (int j = 0; j < cantidad; j++)
                {
                    Random r = new Random();
                    int aux = r.Next(box.Count);
                    jugadores[i].Mano?.Add(box[aux]);
                    box.RemoveAt(aux);
                }
            }
        }

        public override string ToString()
        {
            return "Repartidor random";
        }
    }

    // reparte las fichas en orden descendente por el valor
    public class HigherRep : IRepartidor
    {
        public void Repartir(List<Player> jugadores, List<Ficha> box, int cantidad)
        {
            var item = box.OrderByDescending(X => X.Valor).ToList();
            Random R = new Random();

            for (int i = 0; i < (jugadores.Count * cantidad); i++)
            {
                int index = R.Next(0, jugadores.Count);

                if (jugadores[index].Mano.Count == cantidad)
                {
                    i -= 1;
                    continue;
                }

                jugadores[index].Mano.Add(item[i]);
            }
        }

        public override string ToString()
        {
            return "Repartidor de tope";
        }
    }
}
