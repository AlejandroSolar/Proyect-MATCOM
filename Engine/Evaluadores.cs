namespace Engine
{
    // Formas de darle valor a las fichas
    public interface IEvaluador
    {
        // criterio de evaluación de la ficha
        int Contador(int L, int R);
    }

    // evaluador clásico de dominó tradicional
    // el valor de la ficha es la suma del valor de sus partes
    public class DefaultEvl : IEvaluador
    {
        public int Contador(int L, int R)
        {
            return L + R;
        }

        public override string ToString()
        {
            return "Evaluador Clasico";
        }
    }

    // da valor solo a las fichas dobles
    // el valor es igual al valor de una de sus partes
    public class DoubleEvl : IEvaluador
    {
        public int Contador(int L, int R)
        {
            if (L == R)
            {
                return (L);
            }
            else return 0;
        }

        public override string ToString()
        {
            return "Evaluador de Dobles";
        }
    }

    // da valor solo a las fichas pares
    // igual a la suma de sus partes entre 2
    public class EvenEvl : IEvaluador
    {
        public int Contador(int L, int R)
        {
            if ((L + R) % 2 == 0)
            {
                return ((L+R)/2);
            }

            else return 0;
        }

        public override string ToString()
        {
            return "Evaluador de Pares";
        }
    }

    // da valor solo a las fichas impares
    // igual al máximo entre sus partes
    public class OddEvl : IEvaluador
    {
        public int Contador(int L, int R)
        {
            if ((L + R) % 2 != 0)
            {
                return (Math.Max(L,R));
            }

            else return 0;
        }

        public override string ToString()
        {
            return "Evaluador de Impares";
        }
    }
}