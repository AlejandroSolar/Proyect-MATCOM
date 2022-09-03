namespace Engine;

/// <summary>
/// La clase <c>Double9</c> representa un juego de dominó tradicional de doble 9.
/// </summary>
public class Double9 : ClasicGames
{
    public override int MaxDouble { get { return 9; } }

    public override int HandCapacity { get { return 10; } }

    // representación en string del tipo de juego.
    public override string ToString()
    {
        return "Doble 9";
    }
}