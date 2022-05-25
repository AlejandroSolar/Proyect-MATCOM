using Domino;
using Fichas;

Console.WriteLine("Bienvenido querido jugador");
Console.WriteLine("\nPulse la opcion que desee y luego presione Enter");
Console.WriteLine("\n\n[1] Nuevo Juego");
Console.WriteLine("[2] Salir");

/*int opcion = int.Parse(Console.ReadLine());
if (opcion == 1)
{*/
    DominoGame Domino = new DominoGame(6);

    Domino.FillBox(6);
    Domino.FillHand(7);


    while (true)
    {
        //Console.Clear();
        foreach (var item in Domino.Board)
        {
            Console.Write(item.ToString());
        }
        Console.WriteLine();

        foreach (var item in Domino.Hand)
        {
            Console.Write(item.ToString() + " ");
        }
        Console.WriteLine();

        //Console.WriteLine("Selecciona la ficha que deseas jugar y si la vas a colocar a la izquierda o a la derecha");
        //string[] asd = {Console.ReadLine(),Console.ReadLine()};
        int x = int.Parse(Console.ReadLine());
        string l = Console.ReadLine();
        Domino.Place(x,l);


    }
//}





