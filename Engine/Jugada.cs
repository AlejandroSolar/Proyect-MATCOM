namespace Engine;

// estructura que representa una jugada
public class Jugada{

    // cual ficha se quiere jugar
    public Ficha Ficha {get;}

    // la posición donde se quiere jugar
    public bool Posición {get;}

    // si la ficha está rotada o no
    public bool Girada {get;}

    public Jugada (Ficha ficha, bool posición, bool girada)
    {
        Ficha = ficha;
        Posición = posición;
        Girada = girada;
    }
}