namespace Engine;

/// <summary>
/// Reparte las fichas alfabéticamente a los jugadores guiandose en su nombre.
/// </summary>
public class AlphabeticalDealer : IDealer
{
    
    public void Deal(IEnumerable<Player> players, IList<Piece> box, int capacity)
    {
        /// ordena la lista de jugadores alfabéticamente.
        players = from player in players orderby player.Name select player;

        // recorre la cantidad de fichas que lleva la mano de cada jugador.
        for (int i = 0; i < capacity; i++)
        {
            // recorre los jugadores
            for (int j = 0; j < players.Count(); j++)
            {
                // añade la ficha a la mano del jugador.
                players.ElementAt(j).Hand.Add(box[0]);

                // la elimina de la caja.
                box.RemoveAt(0);
            }
        }
    }

    // representación en string del tipo de repartidor.
    public override string ToString()
    {
        return "Repartidor alfabético";
    }
}