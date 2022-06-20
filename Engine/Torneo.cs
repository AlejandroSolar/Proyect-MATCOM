namespace Engine;

/*public class Tournament 
{
    public Tournament(int puntos,int partidas, List<IWinTournament> condiciones, Ipremio premio,List<Player> players)
    {
        this.Puntos = puntos;
        this.Players = players;
        this.Partidas = partidas;
        this.Condiciones = condiciones;
        this.Premio = premio;
        for (int i = 0; i < players.Count; i++)
        {
            Registro.Add(players[i].Nombre,0);
        }
    }

    public int Puntos {get; set; }
    public int Partidas {get; set; }

    public List<IWinTournament> Condiciones {get; set; }
    public Ipremio Premio { get; set; }
    public Dictionary<string,int> Registro {get; set; }
    public List<Player> Players {get; set; }

    public void Run()
    {
        while (Partidas != 0)
        {
            Juego X = new Juego();

            Premio.Premio(Players,X.Ganadores,Registro);

            for (int i = 0; i < Condiciones.Count; i++)
            {
                if(Condiciones[i].Win(Registro,Puntos))
                {
                    break;
                }
            }            
        }
    }

    public void PrintRegister()
    {
        Dictionary<string,int> Auxiliar = Registro.OrderByDescending(x => x.Value).ToDictionary();
        

        Console.WriteLine("Estado del Torneo:");
        System.Console.WriteLine();
        for (int i = 0; i < Auxiliar.; i++)
        {
            
        }
    }
    
}*/