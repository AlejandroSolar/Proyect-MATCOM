namespace Engine;

/// <summary>
/// La interfaz <c>IDealer</c> engloba el concepto de repartidor de fichas.
/// </summary>
public interface IDealer
{
    /// <summary>
    /// Reparte las fichas a los jugadores de acuerdo con los parametros dados.
    /// </summary>
    /// <param name="players"> jugadores a los que repartir las fichas. </param>
    /// <param name="box"> lista de fichas que se pueden repartir. </param>
    /// <param name="capacity"> cantidad de fichas que le corresponden a cada jugador. </param>
    void Deal(IEnumerable<Player> players, IList<Piece> box, int capacity);
}
