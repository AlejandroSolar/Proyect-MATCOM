namespace Engine;

/// <summary>
/// Estrategia semi-inteligente que busca elegir jugadas
/// que aumenten las probabilidades de que otros jugadores se pasen.
/// </summary>
public class CleverStrategy : IStrategy
{
    public Move Choice(IEnumerable<Piece> hand, IEnumerable<Move> possibles, Board table, Register register, IEnumerable<Couple> couples)
    {
        // guarda los valores que puedan quedar como extremos con las posibles jugadas.
        // el key es el valor que se puede dejar de extremo con una jugada de las posibles
        // el value representa el índice de la primera jugada en el IEnumerable possibles que deja ese valor como extremo.
        Dictionary<int, int> possibleExtremeValues = new Dictionary<int, int>();

        // guarda los valores por los que los demás jugadores se han pasado.
        // el key es el valor por el cual se pasaron
        // el value es la recurrencia con la que se han pasado por ese valor.
        Dictionary<int, int> otherPlayersPassedValues = new Dictionary<int, int>();

        // crea una mesa auxiliar con la cual va a recrear todo el juego a partir del registro.
        Board auxiliarTable = new Board();

        // recorre el registro construyendo la mesa imaginaria.
        for (int i = 0; i < register.Count; i++)
        {
            // evento actual 
            Event currentEvent = register[i];

            // si alguien se pasó.
            if (currentEvent.PlayedPiece is null)
            {
                // si es la primera jugada que prueba, continua, ya que la mesa está vacía y no aporta información.
                if (auxiliarTable.Count == 0)
                {
                    continue;
                }

                // Sino guarda los valores de los extremos de la mesa en ese momento.
                // Porque esto significa que hay alguien que no puede jugar por esos números.
                int left = auxiliarTable.LeftPiece.LeftSide;
                int right = auxiliarTable.RightPiece.RightSide;

                // actualiza otherPlayersPassedValues con el extremo izquierdo.
                CheckValue(otherPlayersPassedValues, left);

                // si es necesario actualiza con el extremo derecho, si este es distinto al anterior.
                if (left != right)
                {
                    CheckValue(otherPlayersPassedValues, right);
                }
            }

            // si no se pasaron en este turno, actualiza la mesa auxiliar.
            else if (currentEvent.Position == Move.Position.right)
            {
                auxiliarTable.Add(currentEvent.PlayedPiece);
            }

            else
            {
                auxiliarTable.PreAdd(currentEvent.PlayedPiece);
            }
        }

        // Si llegado a este punto otherPlayersPassedValues está vacío, significa que nadie se ha pasado aún.
        // si nadie se ha pasado en todo el juego juega la primera de las posibles.
        if (otherPlayersPassedValues.Count == 0)
        {
            return possibles.First();
        }

        // indice del foreach que recorre las jugadas posibles.
        int index = 0;

        // si alguien se pasó recorre la lista de posibles jugadas.
        foreach (var move in possibles)
        {
            // valor que puede dejar la jugada actual como extremo
            int possibleExtreme = 0;
 
            // si la jugada actual es por la derecha.
            if (move.PiecePosition == Move.Position.right)
            {
                // si la ficha debe ser girada.
                if (move.IsTurned)
                {
                    // actualiza el extremo con la parte izquierda. 
                    possibleExtreme = move.Piece.LeftSide;
                }

                // si la ficha no debe ser girada.
                else
                {
                    // actualiza el extremo con la parte derecha. 
                    possibleExtreme = move.Piece.RightSide;
                }
            }

            // si la jugada actual es por la izquierda.
            else
            {
                // si la ficha debe ser girada.
                if (move.IsTurned)
                {
                    // actualiza el extremo con la parte derecha.
                    possibleExtreme = move.Piece.RightSide;
                }

                // si la ficha no debe ser girada.
                else
                {
                    // actualiza el extremo con la parte izquierda. 
                    possibleExtreme = move.Piece.LeftSide;
                }
            }

            // si los posibles extremos no contienen al extremo actual se añade a los posibles.
            // junto con el índice de la jugada en "possibles" que estábamos analizando.
            if (!possibleExtremeValues.ContainsKey(possibleExtreme))
            {
                possibleExtremeValues.Add(possibleExtreme, index);
            }

            // actualiza el indice.
            index ++;
        }

        // guarda ordenado el diccionario de los valores por los que otros jugadores se han pasado.
        // de manera que quedan en las primeras posiciones los extremos por los que más se han pasado los demás.
        var temp = otherPlayersPassedValues.OrderByDescending(X => X.Value);

        // si puede dejar de extremo en la mesa un valor por el que alguien se pasó entonces retorna ese Move.
        foreach (var item in temp)
        {
            if (possibleExtremeValues.ContainsKey((item.Key)))
            {
                return possibles.ElementAt(possibleExtremeValues[item.Key]);
            }
        }

        // si no tiene manera de tratar de forzar el pase de otro jugador
        // entonces juega la primera ficha posible.
        return possibles.First();
    }

    /// <summary>
    /// Actualiza el diccionario de valores donde se han pasado los otros jugadores.
    /// </summary>
    private void CheckValue(Dictionary<int, int> values, int value)
    {
        // si el diccionario no contenía al valor lo añade
        if (!values.ContainsKey(value))
        {
            values.Add(value, 1);
        }

        // si ya lo contenía aumenta su valor de aparición
        else
        {
            values[value] += 1;
        }
    }

    //Representación en string de la estrategia.
    public override string ToString() => "jugador inteligente";
}