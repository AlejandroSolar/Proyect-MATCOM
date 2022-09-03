namespace Engine;

/// <summary>
/// La clase estática <c>Reflection</c> agrupa los métodos que emplean reflection.
/// </summary>
public static class Reflection<T>
{
    /// <summary>
    /// Crea una nueva instancia de un objeto mediante reflection.
    /// </summary>
    /// <param name="type"> el tipo de objeto que se quiere crear. </param>
    /// <returns>
    /// Una nueva instancia del tipo recibido.
    /// </returns>
    public static T DefaultInstanceCreator(Type type)
    {
        return (T)Activator.CreateInstance(type)!;
    }

    /// <summary>
    /// Crea una lista con instancias de cada implementación de una misma interfaz o clase abstracta.
    /// </summary>
    /// <param name="types"> colección de tipos a instanciar. </param>
    /// <returns>
    /// Una <typeparamref name="IList"/> con instancias de cada tipo en la colección recibida.
    /// </returns>
    public static IEnumerable<T> CollectionCreator(IEnumerable<Type> types) => types.Select(type => DefaultInstanceCreator(type));

    /// <summary>
    /// Crea una colección que almacena todos los tipos de un ensamblado que contenga al tipo T.
    /// </summary>
    /// <returns>
    /// Un <typeparamref name="IEnumerable"/> con todos los tipos de un ensamblado.
    /// </returns>
    public static IEnumerable<Type> TypesCollectionCreator()
    {
        return typeof(T).Assembly.GetTypes().Where(x => typeof(T).IsAssignableFrom(x) && x.IsClass && !x.IsAbstract);
    }
}
