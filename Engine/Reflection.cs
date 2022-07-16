using System.Reflection;

namespace Engine;

public static class Reflection<T>
{
    // crea una instancia de un type específico
    public static T DefaultInstanceCreator(Type t)
    {
        return (T)Activator.CreateInstance(t)!;
    }

    // crea una lista de instancias a partir de una lista de types
    public static List<T> ListCreator(List<Type> tipos)
    {
        List<T> resultado = new List<T>();
        foreach (var tipo in tipos)
        {
            resultado.Add(DefaultInstanceCreator(tipo));
        }
        return resultado;
    }

    // devuelve una lista con todos los types de un ensamblado
    public static List<Type> TypesListCreator()
    {
        return typeof(T).Assembly.GetTypes().Where(x => typeof(T).IsAssignableFrom(x) && x.IsClass && !x.IsAbstract).ToList();
    }

    /*private static object[] AskParameters (IEnumerable<ParameterInfo> parametros)
    {
        object[] par = new object[parametros.Count()];

        for (int i = 0; i < par.Length; i++)
        {
            System.Console.WriteLine("Dime el valor del parámetro {0}",parametros.ElementAt(i).Name);
            par[i] = Console.ReadLine()!;

        }
        return par;
    }
    
    public static T ParameterInstanceCreator(Type t, object[] parametros)
    {
        return (T)Activator.CreateInstance(t,parametros)!;
    }
    public static T ParamsCreator(Type t)
    {
        IEnumerable<ParameterInfo> parametros = t.GetConstructors().SelectMany(x => x.GetParameters());
        object[] par = new object[t.GetConstructors().SelectMany(x=> x.GetParameters()).Count()];

        if (par.Length != 0)
        {
            par = AskParameters(parametros);
            return ParameterInstanceCreator(t,par);
        }

        else return DefaultInstanceCreator(t);
    }*/




}
