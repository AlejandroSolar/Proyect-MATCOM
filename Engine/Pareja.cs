namespace Engine
{
    // entidad de pareja
    public class Pareja
    {
        // la suma de los puntos de ambos jugadores
        public int puntos { get { return Player1.Puntos + Player2.Puntos; } }

        // recibe 2 jugadores
        public Pareja(Player p1, Player p2)
        {
            this.Player1 = p1;
            this.Player2 = p2;
        }

        public Player Player1 { get;}
        public Player Player2 { get;}

    }
}