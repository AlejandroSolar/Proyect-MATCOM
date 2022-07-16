namespace Engine
{

    // entidad que decide si una ficha es valida en e juego (reglas)
    public interface IValid
    {
        // retorna verdadero si la ficha cumple con en criterio 
        bool Valid(Jugada jugada, Board mesa);
    }

    // reglas del dominó tradicional
    public class ClassicValid : IValid
    {
        public bool Valid(Jugada jugada, Board Mesa)
        {
            if (Mesa.Count == 0)
            {
                return true;
            }

            else if (!jugada.Posición)
            {
                if (!jugada.Girada)
                {
                    if (jugada.Ficha.PartDch == Mesa.First.PartIzq)
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    if (jugada.Ficha.PartIzq == Mesa.First.PartIzq)
                    {
                        return true;
                    }
                    return false;
                }
            }
            else
            {
                if (!jugada.Girada)
                {
                    if (jugada.Ficha.PartIzq == Mesa.Last.PartDch)
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    if (jugada.Ficha.PartDch == Mesa.Last.PartDch)
                    {
                        return true;
                    }
                    return false;
                }
            }

        }

        public override string ToString()
        {
            return "Reglas Clasicas";
        }

    }

    // solo se puede jugar si la ficha tiene mayor valor que la puesta en mesa
    public class MaximValid : IValid
    {
        public bool Valid(Jugada jugada, Board mesa)
        {
            if (mesa.Count == 0)
            {
                return true;
            }

            Ficha aux;
            if (!jugada.Posición)
            {
                aux = mesa.First;
            }

            else
            {
                aux = mesa.Last;
            }


            if (aux.Valor <= jugada.Ficha.Valor)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return "Regla por suma mayor";
        }
    }

    // solo se puede jugar si la cara de la ficha es mayor en valor que la cara de la ficha en mesa
    public class UpperValid : IValid
    {
        public bool Valid(Jugada jugada, Board mesa)
        {
            if (mesa.Count == 0)
            {
                return true;
            }

            Ficha aux;
            if (!jugada.Posición)
            {
                aux = mesa.First;

                if (!jugada.Girada)
                {
                    if (jugada.Ficha.PartDch > aux.PartIzq)
                    {
                        return true;
                    }
                    else return false;
                }

                if (jugada.Ficha.PartIzq > aux.PartIzq)
                {
                    return true;
                }
                else return false;


            }
            else
            {
                aux = mesa.Last;

                if (!jugada.Girada)
                {
                    if (jugada.Ficha.PartIzq > aux.PartDch)
                    {
                        return true;
                    }
                    else return false;
                }

                if (jugada.Ficha.PartDch > aux.PartDch)
                {
                    return true;
                }
                else return false;
            }

        }

        public override string ToString()
        {
            return "Regla por ficha más valiosa";
        }
    }
}