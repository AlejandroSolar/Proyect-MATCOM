namespace Engine;

/// <summary>
/// La clase <c>SimpleCloneableExtensors</c> agrupa los métodos encargados de clonar objetos del tipo <typeparamref name="ISimpleClone"/> y a colecciones de estos.
/// </summary>
public static class SimpeCloneableExtensors
{
    /// <summary>
    /// Este método se encarga de clonar un <typeparamref name="ISimpleClone"/>.
    /// </summary>
    /// <returns>
    /// Un nuevo objeto del mismo tipo que el <typeparamref name="ISimpleClone"/> original.
    /// </returns>
    public static T Clone<T>(this ISimpleClone<T> obj)
    {
        // utiliza reflection para crear una nueva instancia del mismo tipo
        return Reflection<T>.DefaultInstanceCreator(obj!.GetType());
    }

    /// <summary>
    /// Este método se encarga de clonar un <typeparamref name="IEnumerable"/> de objetos <typeparamref name="ISimpleClone"/>.
    /// </summary>
    /// <returns>
    /// Un nuevo <typeparamref name="IEnumerable"/> de <typeparamref name="ISimpleClone"/> con copias de cada elemento del original.
    /// </returns>
    public static IEnumerable<T> Clone<T>(this IEnumerable<T> collection) where T : ISimpleClone<T>
    {
        return from x in collection select x.Clone();
    }
}
