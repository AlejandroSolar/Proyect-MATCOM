namespace Fichas;

public interface IPiece
{
    public void Turn (int index);

    public virtual void Place (int index, string direction)
    {
        Console.WriteLine("Te jodi");
    }

    public void FillBox (int Max);

    public bool CanPlace(int index);
}

    




