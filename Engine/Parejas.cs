namespace Engine;

public class Pareja
{
    int puntos {get { return Player1.Puntos() + Player2.Puntos();}}

    public Pareja (Player p1, Player p2)
    {
        this.Player1 = p1;
        this.Player2 = p2;
    }

    public Player Player1 {get; set; }
    public Player Player2 {get; set; }
    
}
