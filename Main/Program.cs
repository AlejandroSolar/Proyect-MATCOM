using Engine;

Console.Title = "Domino Alejan2";

List<IValid> reglas = new List<IValid> { new ClassicDomino() };
List<IWinCondition> ganadas = new List<IWinCondition>
{
    new PegueWin(),
    new PointcountWinLower()
};

List<IContador> contadores = new List<IContador> { new DefaultCount(), new DoubleCount() };

List<IStopCondition> paradas = new List<IStopCondition>
{
    new Pegue(),
    new tranque()
};

List<IOrdenador> ordenadores = new List<IOrdenador> {new ParejasOrd()};

List<Player> Crew = new List<Player> { new PCPlayer("P1",new RandomST()),
 new PCPlayer("P2",new BotaDobleST()), new PCPlayer("P3",new BotagordaST()), new PCPlayer("P4",new BotagordaST())};

List<Pareja> parejas = new List<Pareja> {new Pareja(Crew[0],Crew[1]), new Pareja(Crew[2],Crew[3])};

Juego X = new Juego(Crew,3,2,reglas,new ClassicRepa(),ordenadores,contadores,false,paradas,ganadas);

X.RunGame();


