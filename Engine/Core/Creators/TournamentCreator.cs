namespace Engine;

/// <summary>
/// La clase <c>TournamentCreator</c> tiene como objetivo crear un torneo con las indicaciones del usuario.
/// </summary>
public static class TournamentCreator
{
    /// <value> <c>Scoring</c> contiene todos los tipos de Score implementados hasta el momento.</value>
    private static IEnumerable<Type> Scoring = Reflection<IScore>.TypesCollectionCreator();

    /// <summary>
    /// Crea un torneo con la lista de jugadores dada y mediante las especificaciones del usuario.
    /// </summary>
    /// <returns>
    /// Una nueva instancia de <typeparamref name="Tournamet"/>
    /// </returns>
    /// <param name="players"> jugadores que participan. </param>
    public static Tournament Tournament(IList<Player> players)
    {
        // cantidad de juegos que se van a jugar.
        int games = int.MaxValue;

        // cantidad de puntos a alcanzar.
        int pointLimit = int.MaxValue;

        // forma de premiar cada victoria.
        IScore score;

        // el usuario establece la cantidad maxima de partidas a jugar.
        if (Auxiliar.YesOrNo("¿Desea jugar con un Límite de Partidas?"))
        {
            games = Auxiliar.NumberSelector("¿Cuantas partidas se jugaran en el torneo?", 1);
        }

        // el usuario establece la cantidad maxima de puntos a conseguir para ganar.
        if (Auxiliar.YesOrNo("¿Desea jugar con un Límite de Puntos?"))
        {
            pointLimit = Auxiliar.NumberSelector("¿Cuanto se debe puntuar para ganar en el torneo?", 1);
        }
        // el usuario elige una forma de premiación entre las previamente implementadas.
        score = Auxiliar<IScore>.Selector("¿Como se puntuara en el Torneo?", Reflection<IScore>.CollectionCreator(Scoring));

        // devuelve una nueva instancia de torneo con los valores anteriormente obtenidos.
        return new Tournament(pointLimit, games, score, players);
    }
}