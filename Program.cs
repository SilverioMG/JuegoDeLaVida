namespace JuegoVida
{
    using LibJuegoVida;
    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrizActual = LibJuegoVida.CargaDatos();
            LibJuegoVida.GenerarJuegodelaVida(matrizActual);
        }
    }
}
