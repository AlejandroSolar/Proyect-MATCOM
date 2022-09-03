namespace Engine;

/// <summary>
/// Organiza los turnos de forma aleatoria.
/// </summary>
public class RandomOrderer : ITurnOrderer
{
    public void Organize(IList<Player> players, IEnumerable<Couple> couples)
    {
        // índice de la lista de jugadores.
        int position = 0;
        
        // lista auxiliar donde añadir aleatoriamente
        List<Player> auxiliarList = new List<Player>();

        Random random = new Random();

        // mientras no esté vacía la lista de jugadores
        while (players.Count != 0)
        {
            // añade a la lista auxiliar los jugadores de la lista original
            // de manera aleatoria, luego los elimina de la lista original.
            position = random.Next(players.Count);
            auxiliarList.Add(players[position]);
            players.RemoveAt(position);
        }

        // llena nuevamente la lista original en el orden en que quedó la auxiliar.
        for (int i = 0; i < auxiliarList.Count; i++)
        {
            players.Add(auxiliarList[i]);
        }

    }

    // representación en string del tipo de ordenador de turnos.
    public override string ToString() => "Ordenador de turnos aleatorios";
}