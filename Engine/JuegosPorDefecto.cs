namespace Engine
{
    // juegos predeterminados para facilitar la elección de juego al usuario
    public interface IDefaultGames
    {
        int Doble { get; }

        int Max { get; }

        int SwiftTurns { get; }

        IRepartidor Repartidor { get; }

        List<IEvaluador> evaluadores { get; }

        List<IValid> validadores { get; }

        List<IStopCondition> stops { get; }

        List<IWinCondition> winConditions { get; }

        List<IOrdenador> ordenadores { get; }
    }

    public abstract class ClasicGames : IDefaultGames
    {
        public int SwiftTurns { get { return 0; } }
        
        public IRepartidor Repartidor
        {
            get { return new RandomRep(); }
        }

        public List<IEvaluador> evaluadores
        {
            get { return new List<IEvaluador> { new DefaultEvl() }; }
        }

        public List<IValid> validadores
        {
            get { return new List<IValid> { new ClassicValid() }; }
        }

        public List<IStopCondition> stops
        {
            get { return new List<IStopCondition> { new PegueStop(), new TranqueStop() }; }
        }

        public List<IWinCondition> winConditions
        {
            get { return new List<IWinCondition> { new PegueWin(), new LowerWin() }; }
        }

        public List<IOrdenador> ordenadores
        {
            get { return new List<IOrdenador> { new ParejasOrd() }; }
        }

        public abstract int Doble { get; }

        public abstract int Max { get; }
    }


    public class Double6 : ClasicGames
    {
        public override int Doble { get { return 6; } }

        public override int Max { get { return 7; } }


        public override string ToString()
        {
            return "Doble 6";
        }
    }

    public class Double9 : ClasicGames
    {
        public override int Doble { get { return 9; } }

        public override int Max { get { return 10; } }


        public override string ToString()
        {
            return "Doble 9";
        }
    }
}
