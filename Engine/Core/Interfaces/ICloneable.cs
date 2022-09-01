namespace Engine;

/// <summary>
/// La interfaz <c>ICloneable</c> engloba el concepto de objeto que se puede clonar, pero que requiere
/// una implementación concreta del método Clone.
/// </summary>
public interface ICloneable<T> 
{
    /// <summary>
    /// clona el objeto.
    /// </summary>
    /// <returns>
    /// Una copia de la instancia del objeto.
    /// </returns>
    T Clone();
}