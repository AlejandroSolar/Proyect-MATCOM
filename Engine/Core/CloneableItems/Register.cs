namespace Engine;

/// <summary>
/// La clase <c>Register</c> representa un compendio de todos los movimientos del juego.
/// </summary>
/// <remarks> Es clonable. </remarks>
public class Register : ICloneable<Register>
{
    /// <value> <c>Turn</c> indica que turno es en el juego.</value>
    private int Turn { get; set; }

    /// <value> <c>Count</c> indica cuantos movimientos se han hecho.</value>
    public int Count { get { return GameRegister.Count; } }

    /// <returns>
    /// devuelve el evento en el indice dado.
    /// </returns>
    public Event this[int i]
    {
        get { return GameRegister[i]; }
    }

    /// <value> <c>GameRegister</c> es una lista que almacena los eventos del juego. </value>
    private IList<Event> GameRegister { get; set; }

    /// <summary>
    /// Crea un nuevo registro e inicia en el turno 0.
    /// </summary>
    public Register()
    {
        GameRegister = new List<Event>();
        Turn = 0;
    }

    /// <summary>
    /// Añade un evento al registro y actualiza el contador de turnos.
    /// </summary>
    public void Add(Event x)
    {
        GameRegister.Add(x);
        Turn++;
    }

    /// <summary>
    /// Reprenta gráficamente la ultima jugada que se hizo.
    /// </summary>
    public void PrintActual()
    {
        // bool que indica si es la primera jugada del juego.
        bool first = false;
        
        if (Turn == 1)
        {
            first = true;
        }
        Console.Write($"Turno {Turn} : ");
        GameRegister.Last().PrintEvent(first);
    }

     /// <summary>
    /// Reprenta gráficamente todo lo sucedido en el juego.
    /// </summary>
    public void PrintRegister()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Resumen del Juego:");
        Console.WriteLine("------------------");
        Console.ResetColor();
        Console.WriteLine();

        // recorre e imprime cada evento.
        for (int i = 0; i < GameRegister.Count; i++)
        {
            bool first = false;

            if (i == 0)
            {
                first = true;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"{i + 1}:");
            GameRegister[i].PrintEvent(first);
        }
    }

    /// <summary>
    /// Clona el registro.
    /// </summary>
    /// <returns>
    /// Un nuevo <typeparamref name="Register"/> con una copia de cada evento del original.
    /// </returns>
    public Register Clone() => new Register() { GameRegister = this.GameRegister.Clone().ToList()};
}