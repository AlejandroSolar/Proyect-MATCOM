namespace Engine;

/// <summary>
/// La clase <c>Couple</c> representa a una pareja en el juego (2 jugadores).
/// </summary>
/// <remarks> es clonable </remarks>
public class Couple : ICloneable<Couple>
{
    /// <summary>
    /// Crea una nueva pareja con los 2 jugadores que se indican
    /// </summary>
    /// <param name="p1"> primer jugador </param>
    /// <param name="p2"> segundo jugador </param>
    public Couple(Player p1, Player p2)
    {
        this.Player1 = p1;
        this.Player2 = p2;
    }

    /// <value> <c>Player1</c> representa al primer jugador de la pareja </value>
    public Player Player1 { get; }

    /// <value> <c>Player2</c> representa al segundo jugador de la pareja </value>
    public Player Player2 { get; }

    /// <summary>
    /// Clona la pareja.
    /// </summary>
    /// <returns>
    /// Una nueva <typeparamref name="Couple"/> con copias de los jugadores de la pareja original
    /// </returns>
    public Couple Clone()
    {
        return new Couple(this.Player1.Clone(), this.Player2.Clone());
    }
}
