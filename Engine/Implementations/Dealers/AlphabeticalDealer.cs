namespace Engine;

/// <summary>
/// Reparte las fichas alfabéticamente a los jugadores guiandose en su nombre.
/// </summary>
public class AlphabeticalDealer : IDealer
{

    public void Deal(IEnumerable<Player> players, IList<Piece> box, int capacity)
    {
        /// ordena la lista de jugadores alfabéticamente.
        players = players.OrderBy(x => x.Name);

        // recorre la cantidad de fichas que lleva la mano de cada jugador.
        for (int i = 0; i < capacity; i++)
        {
            // recorre los jugadores
            foreach (var player in players)
            {
                // añade la ficha a la mano del jugador.
                player.Hand.Add(box[0]);

                // la elimina de la caja.
                box.RemoveAt(0);
            }
        }
    }

    // representación en string del tipo de repartidor.
    public override string ToString() => "Repartidor alfabético";
}