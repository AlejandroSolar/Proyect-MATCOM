namespace Engine;

public static class Ronda
{
    public static bool Run(List<Player> players, List<IValid> reglas, List<IStopCondition> conditions, List<Ficha> mesa, bool magic)
    {
        for (int i = 0; i < players.Count; i++)
        {
            Console.Clear();
            Console.WriteLine($"Es el turno de {players[i].Nombre}:");
            Console.WriteLine();
            players[i].PrintHand();
            Console.WriteLine();
            printMesa(mesa);
            Console.WriteLine();

            for (int j = 0; j < conditions.Count; j++)
            {
                if (conditions[j].Stop(players, mesa, reglas))
                {
                    return true;
                }

            }

            if (Pasate(players[i], reglas, mesa))
            {
                Console.WriteLine("No puedes jugar");
                Thread.Sleep(1000);
                continue;
            }

            Ficha f = players[i].Play(mesa,reglas);
            Play(players[i],f,mesa,reglas);

            if (magic)
            {
                checkMagic(mesa);
            }
            Thread.Sleep(1000);
        }
        return false;

    }

    public static void printMesa(List<Ficha> Mesa)
    {
        for (int i = 0; i < Mesa.Count; i++)
        {
            Console.Write($"-{Mesa[i]}-");
        }
    }

    public static bool Pasate(Player player, List<IValid> reglas, List<Ficha> mesa)
    {
        foreach (var cosa in player.Mano)
        {
            foreach (var item in reglas)
            {
                if (item.Valid(cosa, mesa) != -1)
                {
                    return false;
                }
            }

        }
        return true;
    }

    public static void checkMagic(List<Ficha> Mesa)
    {
        for (int i = 0; i < Mesa.Count; i++)
        {
            if (Mesa[i].PartIzq == -1 && Mesa[i].PartDch == -1)
            {
                if (Auxiliar.Validpos(i + 1, Mesa))
                {
                    Mesa[i].PartDch = Mesa[i + 1].PartIzq;
                    Mesa[i].PartIzq = Mesa[i + 1].PartIzq;
                }

                else if (Auxiliar.Validpos(i - 1, Mesa))
                {
                    Mesa[i].PartDch = Mesa[i - 1].PartDch;
                    Mesa[i].PartIzq = Mesa[i - 1].PartDch;
                }

                else
                {
                    Mesa[i].PartDch = 0;
                    Mesa[i].PartIzq = 0;
                }
            }
        }


    }

    public static void Play(Player player, Ficha ficha, List<Ficha> Mesa, List<IValid> Reglas)
    {
        if (Mesa.Count == 0)
        {
            Mesa.Add(ficha);
            player.Mano?.Remove(ficha);
            return;
        }

        int posición = CheckPosition(ficha,Mesa,Reglas);

        while (true)
        {
            switch (posición)
            {
                case 0:
                    if (ficha.PartDch != Mesa[0].PartIzq)
                    {
                        ficha.Turn();
                    }
                    Mesa.Insert(0, ficha);
                    break;

                case 1:
                    if (ficha.PartIzq != Mesa[Mesa.Count - 1].PartDch)
                    {
                        ficha.Turn();
                    }
                    Mesa.Add(ficha);
                    break;

                case 2:
                    posición = player.AskPosition();
                    continue;



            }
            player.Mano?.Remove(ficha);
            break;
        }



    }
    public static int CheckPosition(Ficha ficha, List<Ficha> Mesa, List<IValid> Reglas)
    {
        bool cero = false;
        bool uno = false;
        bool dos = false;

        for (int i = 0; i < Reglas.Count; i++)
        {
            if (Reglas[i].Valid(ficha, Mesa) == 0)
            {
                cero = true;
            }

            if (Reglas[i].Valid(ficha, Mesa) == 1)
            {
                uno = true;
            }

            if (Reglas[i].Valid(ficha, Mesa) == 2)
            {
                dos = true;
                break;
            }

        }

        if ((cero && uno) || dos)
        {
            return 2;
        }

        else if (cero)
        {
            return 0;
        }

        else return 1;
    }


}