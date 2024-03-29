namespace Engine;

/// <summary>
/// Organiza los turnos de la manera clásica del juego por parejas,
/// primero todos los jugadores primarios de las parejas y luego sus parejas
/// en el mismo orden.
/// </summary>
public class CouplesOrderer : ITurnOrderer
{
    public void Organize(IList<Player> players, IEnumerable<Couple> couples)
    {
        // si no hay parejas deja el orden como estaba.
        if (couples.Count() == 0)
        {
            return;
        }

        // limpia la lista de jugadores para volverla a organizar.
        players.Clear();

        // recorre las parejas y añade los player1.
        foreach (var couple in couples)
        {
            players.Add(couple.Player1);
        }
        
        // vuelve a recorrer las parajas para añadir los player2
        foreach (var couple in couples)
        {
            players.Add(couple.Player2);
        }
    }

    // representación en string del tipo de ordenador de turnos.
    public override string ToString() => "Ordenador de turnos por pareja";
}