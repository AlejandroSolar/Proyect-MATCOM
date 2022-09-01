namespace Engine;

/// <summary>
/// La interfaz <c>IDefaultGames</c> establece los campos necesarios de un juego previamente configurado.
/// </summary>
public interface IDefaultGames
{
    /// <value> <c>MaxValue</c> representa el doble máximo que tienen las fichas del juego creado. </value>
    int MaxDouble { get; }

    /// <value> <c>HandCapacity</c> representa la capacidad de la mano de los jugadores del juego creado.</value>
    int HandCapacity { get; }

    /// <value> <c>SwiftTurns</c> representa la cantidad de turnos que deben pasar para cambiar de ordenador de turnos.</value>
    int SwiftTurns { get; }

    /// <value> <c>Dealer</c> representa al repartidor de fichas del juego creado.</value>
    IDealer Dealer { get; }

    /// <value> <c>Evaluator</c> representa a los evaluadores de fichas del juego creado.</value>
    IEnumerable <IEvaluator> Evaluators { get; }

    /// <value> <c>Rules</c> representa las reglas a seguir en el juego creado.</value>
    IEnumerable<IValid> Rules { get; }

    /// <value> <c>StopConditions</c> representa las condiciones de parada del juego creado.</value>
    IEnumerable<IStopCondition> StopConditions { get; }

    /// <value> <c>WinConditions</c> representa las condiciones de victoria del juego creado.</value>
    IEnumerable<IWinCondition> WinConditions { get; }

    /// <value> <c>Organizers</c> representa los organizadores de turnos del juego creado.</value>
    IEnumerable<ITurnOrderer> Organizers { get; }
}
