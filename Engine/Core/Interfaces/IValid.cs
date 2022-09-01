namespace Engine;

/// <summary>
/// La interfaz <c>IValid</c> engloba el concepto de regla de juego (como puede jugarse una ficha).
/// </summary>
/// <remarks>
/// Es clonable.
/// </remarks>
public interface IValid : ISimpleClone<IValid>
{
    /// <summary>
    /// Verifica si una jugada es valida de acuerdo con una regla específica.
    /// </summary>
    /// <returns>
    /// Un <typeparamref name="bool"/> que indica si puede hacerse la jugada.
    /// </returns>
    /// <param name="move"> jugada que se desea efectuar. </param>
    /// <param name="table"> estado actual de la mesa. </param>
    bool Valid(Move move, Board table);
}
