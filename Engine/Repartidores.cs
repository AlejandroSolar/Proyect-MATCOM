namespace Engine;

public interface IRepartidor
{
    public void Repartir(List<Player> jugadores, List<Ficha> box, int cantidad);

}

public class ClassicRepa : IRepartidor
{
    public void Repartir(List<Player> jugadores, List<Ficha> box, int cantidad)
    {
        for (int i = 0; i < jugadores.Count; i++)
        {
            FillHand(jugadores[i], cantidad,box);
        }
    }

    public void FillHand(Player player, int max, List<Ficha> box)
    {

        for (int i = 0; i < max; i++)
        {
            Random r = new Random();
            int aux = r.Next(box.Count);
            player.Mano?.Add(box[aux]);
            box.RemoveAt(aux);

        }
    }
}

public class MagicRepa : IRepartidor
{
    public void Repartir(List<Player> jugadores, List<Ficha> box, int cantidad)
    {
        for (int i = 0; i < jugadores.Count; i++)
        {
            for (int j = 0; j < cantidad; j++)
            {
                jugadores[i].Mano.Add(new Ficha(-1,-1,0));
            }
        }
    }
}

