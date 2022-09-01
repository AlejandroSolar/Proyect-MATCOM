namespace Engine;

/// <summary>
/// La interfaz <c>IStrategy</c> engloba el concepto de elegir una jugada para efectuar, siguiendo una estrategia.
/// </summary>
/// <remarks>
/// Va asignada a un jugador.
/// Es clonable.
/// </remarks>
public interface IStrategy : ISimpleClone<IStrategy>
{
    /// <summary>
    /// Elige la jugada que desea efectuarse de acuerdo a los objetivos de la estrategia.
    /// </summary>
    /// <returns>
    /// Un <typeparamref name="Move"/> que indica la jugada elegida por la estrategia.
    /// </returns>
    /// <param name="hand"> mano del jugador. </param>
    /// <param name="possibles"> jugadas que el jugador puede realizar. </param>
    /// <param name="table"> estado actual de la mesa. </param>
    /// <param name="register"> estado actual del registro. </param>
    /// <param name="couples"> parejas que participan en el juego. </param>
    Move Choice(IEnumerable<Piece> hand, IEnumerable<Move> possibles, Board table, Register register, IEnumerable<Couple> couples);
}
