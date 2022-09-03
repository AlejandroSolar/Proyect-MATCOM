namespace Engine;

/// <summary>
/// Reparte las fichas priorizando las de mayor valor.
/// </summary>
public class HigherDealer : IDealer
{
    public void Deal(IEnumerable<Player> players, IList<Piece> box, int capacity)
    {
        // organiza de mayor a menor la caja por el valor de las fichas.
        var organizedBox = box.OrderByDescending(piece => piece.PieceValue).ToList();

        Random random = new Random();

        // recorre la capacidad de fichas que le corresponden a cada jugador.
        for (int i = 0; i < capacity; i++)
        {
            // crea aleatoriamente una lista de enteros que representa el orden por el cual
            // se repartiran las fichas a los jugadores.
            List<int> randomOrder = ChangeOrder(players.Count()); 

            // recorre la colección de jugadores.
            foreach (var player in players)
            {
                // añade la ficha más valorada de la caja a la mano del jugador correspondiente.
                player.Hand.Add(organizedBox[0]);

                // elimina la ficha entregada al jugador de la caja.
                box.Remove(organizedBox[0]);
                organizedBox.RemoveAt(0);
            }         
        }    
    }

    /// <summary>
    /// Determina un orden aleatorio para la repartición de fichas entre los jugadores.
    /// </summary>
    /// <returns>
    /// Una <typeparamref name="List"/> de <typeparamref name="int"/> que representa el orden a seguír.
    /// </returns>
    /// <param name="length"> tamaño de la colección de jugadores. </param>
    private List<int> ChangeOrder (int length)
    {
        // crea una lista donde se guarda el orden por defecto ascendente
        // ej: 0,1,2,3,4....
        List<int> auxiliarOrder = new List<int>();

        for (int i = 0; i < length; i++)
        {
            auxiliarOrder.Add(i);
        }

        // crea la lista que se retornara con el orden resultante.
        List<int> finalOrder = new List<int>();

        // recorre length para establecer el orden.
        for (int i = 0; i < length; i++)
        {
            Random random = new Random();

            // guarda el indice de un elemento aleatorio de la lista auxiliar.
            int index = random.Next(0,length-i);

            // añade ese elemento a la lista resultante.
            finalOrder.Add(auxiliarOrder[index]);

            // elimina el elemento seleccionado de la lista auxiliar.
            auxiliarOrder.RemoveAt(index);
        }

        // al finalizar el recorrido queda en finalOrder un orden aleatorio, luego se retorna.
        return finalOrder;
    }

    // representación en string del tipo de repartidor.
    public override string ToString() => "Repartidor de tope";
}