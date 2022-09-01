namespace Engine;

/// <summary>
/// Elige la jugada tratando de no perjudicar a su pareja,
/// en caso de no estar jugando por parejas, juega la primera jugada posible.
/// </summary>
public class CouplesStrategy : IStrategy
{
    // primera posición del registro que hace referencia a un evento
    // hecho por el propio jugador con esta instancia de la estrategia.
    private int GuideNumber = int.MinValue;

    // nombre de la pareja.
    private string? Couple { get; set; }

    public Move Choice(IEnumerable<Piece> hand, IEnumerable<Move> possibles, Board table, Register register, IEnumerable<Couple> couples)
    {
        // si no se está jugando por parejas juega la primera jugada posible.
        if (couples.Count() == 0)
        {
            return possibles.First();
        }

        // la estrategia no tiene conocimiento alguno del jugador que la maneja, por lo que para saber quien es
        // su pareja, primero debe saber quien es él mismo.
        // para esto se apoya en this.GuideNumber, el evento en ese indice del registro corresponde a una jugada
        // hecha por si mismo, y por dicho evento tiene acceso a su nombre, y con su nombre puede encontrar a su pareja
        // en el IEnumerable couples.

        // si aun no se ha encontrado a si mismo.
        if (GuideNumber == int.MinValue || register.Count < couples.Count() * 2)
        {
            // actualiza su posición, que se corresponde con la ultima entrada del registro +1, pues es el evento
            // que se corresponde con su jugada, que aun no ha hecho.
            GuideNumber = register.Count;
            this.Couple = null;
            // juega la primera jugada posible.
            return possibles.First();
        }

        // si aun no la ha encontrado, pero ya tiene elementos para hacerlo, busca a su pareja.
        if (this.Couple is null)
        {
            FindFriend(couples, register);
        }

        // guarda la posición por la que jugó su pareja.
        Move.Position couplePosition = Move.Position.right;

        // recorre el registro de fin a inicio.
        for (int i = register.Count - 1; i >= 0; i--)
        {
            // si el evento en el indice actual es de su pareja
            if (register[i].PlayerName == Couple)
            {
                // si su pareja se pasó juega la primera jugada posible.
                if (register[i].PlayedPiece is null)
                {
                    return possibles.First();
                }

                // si su pareja no se pasó, actualiza la posición en la que jugó.
                couplePosition = register[i].Position;
                break;
            }
        }

        // recorre la lista de jugadas posibles y retorna la primera jugada
        // que no sea por la posición en la que jugó su pareja.
        for (int i = 0; i < possibles.Count(); i++)
        {
            if (possibles.ElementAt(i).PiecePosition != couplePosition)
            {
                return possibles.ElementAt(i);
            }
        }

        // si le es imposible no perjudicar a su pareja, juega la primera jugada posible.
        return possibles.First();
    }

    /// <summary>
    /// Encuentra a la pareja del jugador.
    /// </summary>
    /// <param name="couples"> parejas que participan en el juego. </param>
    /// <param name="register"> registro actual del juego. </param>
    private void FindFriend(IEnumerable<Couple> couples, Register register)
    {
        // guarda el nombre del player que posee esta instancia de la estrategia.
        string me = register[GuideNumber].PlayerName;

        // recorre las parejas.
        for (int i = 0; i < couples.Count(); i++)
        {
            // cuando se encuentre a si mismo actualiza el nombre de la pareja.
            if (couples.ElementAt(i).Player1.Name == me)
            {
                this.Couple = couples.ElementAt(i).Player2.Name;
                return;
            }

            if (couples.ElementAt(i).Player2.Name == me)
            {
                this.Couple = couples.ElementAt(i).Player1.Name;
                return;
            }

        }
    }

    // representación en string del tipo de estrategia.
    public override string ToString()
    {
        return "Jugador en parejas";
    }
}
