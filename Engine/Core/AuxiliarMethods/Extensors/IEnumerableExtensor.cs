namespace Engine;

/// <summary>
/// La clase <c>IEnumerableExtensors</c> agrupa a los métodos extensores de <typeparamref name="IEnumerable"/>.
/// </summary>
public static class IEnumerableExtensors
{
    /// <summary> 
    /// Este método se encarga de clonar un <typeparamref name="IEnumerable"/> de objetos <typeparamref name="ICloneables"/>.
    /// </summary>
    /// <returns>
    /// Un nuevo <typeparamref name="IEnumerable"/> con copias de cada elemento del original de sus elementos.
    /// </returns>
    public static IEnumerable<T> Clone<T>(this IEnumerable<T> collection) where T : ICloneable<T>
    {
        return from x in collection select x.Clone();
    }
}
