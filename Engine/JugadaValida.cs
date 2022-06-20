namespace Engine;


public interface IValid
{
    public int Valid(Ficha ficha, List<Ficha> mesa);
}

public class ClassicDomino : IValid
{
    public int Valid(Ficha Puesta, List<Ficha> Mesa)
    {
        if (Mesa.Count == 0)
        {
            return 1;
        }

        bool Izq1 = Puesta.PartDch == Mesa[0].PartIzq;
        bool Izq2 = Puesta.PartIzq == Mesa[0].PartIzq;
        bool Der1 = Puesta.PartIzq == Mesa[Mesa.Count - 1].PartDch;
        bool Der2 = Puesta.PartDch == Mesa[Mesa.Count - 1].PartDch;

        if ((Izq1 || Izq2) && (Der1 || Der2))
        {
            return 2;
        }

        else if (Izq1 || Izq2)
        {
            return 0;
        }

        else if (Der1 || Der2)
        {
            return 1;
        }

        else
        {
            return -1;
        }
    }

}

public class MagicValid : IValid
{
    public int Valid(Ficha magica, List<Ficha> mesa)
    {
        if (mesa.Count == 0)
        {
            return -1;
        }
        if(magica.PartDch == -1 && magica.PartIzq == -1)
        {
            return 2;
        }
        else return -1;
    }
}