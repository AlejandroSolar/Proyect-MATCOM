namespace Engine;

/// <summary>
/// La clase <c>Double6</c> representa un juego de dominó tradicional de doble 6.
/// </summary>
public class Double6 : ClasicGames
{
    public override int MaxDouble { get { return 6; } }

    public override int HandCapacity { get { return 7; } }

    // representación en string del tipo de juego.
    public override string ToString() => "Doble 6";
}