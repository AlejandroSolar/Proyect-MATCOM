using Fichas;

namespace Domino;

public class DominoGame : IPiece
{
    public List<DominoPiece> Box;
    public List<DominoPiece> Board;

    public List<DominoPiece> Hand;

    public bool[] AblePlay;

    public DominoGame(int max)
    {
        this.Box = new List<DominoPiece>();
        this.Board = new List<DominoPiece>();
        this.Hand = new List<DominoPiece>();
        this.AblePlay = new bool[max+1];
    }

    public void FillBox(int max)
    {
        for (int i = 0; i <= max; i++)
        {
            AblePlay[i] = true;
            for (int j = i; j <= max; j++)
            {
                Box.Add(new DominoPiece(i, j));
            }
        }
    }

    public void FillHand(int max)
    {

        for (int i = 0; i < max; i++)
        {
            Random r = new Random();
            int aux = r.Next(Box.Count);
            Hand.Add(Box[aux]);
            Box.RemoveAt(aux);
        }
    }
    public void Turn(int index)
    {
        int aux = Hand.ElementAt(index).Part1;
        Hand.ElementAt(index).Part1 = Hand.ElementAt(index).Part2;
        Hand.ElementAt(index).Part2 = aux;
    }

    public void Place(int index, string direction)
    {
        if (CanPlace(index))
        {
            if (Board.Count == 0)
            {
                Board.Add(Hand.ElementAt(index));
                Hand.RemoveAt(index);
                
            }

            else if (direction == "left")
            {
                if (Hand.ElementAt(index).Part1 == Board.First().Part1)
                {
                    Turn(index);
                    Board.Insert(0, Hand.ElementAt(index));
                    Hand.RemoveAt(index);
                }

                else if (Hand.ElementAt(index).Part2 == Board.First().Part1)
                {
                    Board.Prepend(Hand.ElementAt(index));
                    Hand.RemoveAt(index);
                }

                else
                    Console.WriteLine("You can't place it in this direction ");

            }

            else if (direction == "right")
            {
                if (Hand.ElementAt(index).Part1 == Board.Last().Part2)
                {
                    Board.Append(Hand.ElementAt(index));
                    Hand.RemoveAt(index);
                }

                else if (Hand.ElementAt(index).Part2 == Board.Last().Part2)
                {
                    Turn(index);
                    Board.Append(Hand.ElementAt(index));
                    Hand.RemoveAt(index);
                }

                else
                    Console.WriteLine("You can't place it in this direction ");
            }
        }
    }

    public bool CanPlace(int index) // esto despues lo hago pa por derecha o por izquierda y no en el place
    {
        if (AblePlay[Hand.ElementAt(index).Part1] || AblePlay[Hand.ElementAt(index).Part2])
        {
            return true;
        }
        return false;
    }
}
public class DominoPiece
{
    public int Part1 { set; get; }
    public int Part2 { set; get; }

    public DominoPiece(int part1, int part2)
    {
        this.Part1 = part1;
        this.Part2 = part2;
    }

    public override string ToString()
    {
        return ($"[{Part1}|{Part2}]");
    }
}