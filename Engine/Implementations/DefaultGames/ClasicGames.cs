namespace Engine;

/// <summary>
/// La clase <c>ClassicGames</c> representa juegos de dominó tradicional, con las reglas clasicas.
/// </summary>
public abstract class ClasicGames : IDefaultGames
{
    public int SwiftTurns { get { return 0; } }

    public IDealer Dealer
    {
        get { return new RandomDealer(); }
    }

    public IEnumerable<IEvaluator> Evaluators
    {
        get { return new List<IEvaluator> { new DefaultEvaluator() }; }
    }

    public IEnumerable<IValid> Rules
    {
        get { return new List<IValid> { new ClassicValid() }; }
    }

    public IEnumerable<IStopCondition> StopConditions
    {
        get { return new List<IStopCondition> { new EmptyHandStop(), new BlockStop() }; }
    }

    public IEnumerable<IWinCondition> WinConditions
    {
        get { return new List<IWinCondition> { new EmptyHandWin(), new LowerPointsWin() }; }
    }

    public IEnumerable<ITurnOrderer> Organizers
    {
        get { return new List<ITurnOrderer> { new CouplesOrderer() }; }
    }

    public abstract int MaxDouble { get; }

    public abstract int HandCapacity { get; }
}
