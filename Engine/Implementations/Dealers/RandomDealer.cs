namespace Engine;

/// <summary>
/// Reparte las fichas aleatoriamente (similar al dominó tradicional).
/// </summary>
public class RandomDealer : IDealer
{
    public void Deal(IEnumerable<Player> players, IList<Piece> box, int capacity)
    {
        // recorre por la cantidad de fichas que se deben repartir.
        // coincide con la cantidad de jugadores * capacidad máxima de las manos. 
        foreach (var player in players)
        {
            for (int j = 0; j < capacity; j++)
            {
                // elige un indice aleatorio 
                Random random = new Random();
                int auxiliar = random.Next(box.Count);

                // reparte la ficha de la caja en la posicion del indice aleatorio anteriormente calculado.
                player.Hand?.Add(box[auxiliar]);

                // elimina de la caja la ficha en ese indice para evitar entregar fichas repetidas.
                box.RemoveAt(auxiliar);
            }
        }
    }

    // representación en string del tipo de repartidor.
    public override string ToString() => "Repartidor random";
}