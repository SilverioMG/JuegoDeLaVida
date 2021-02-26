using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibJuegoVida
{
    /// <summary>
    /// Esta clase contiene métodos estáticos para realizar el "Juego de la Vida".
    /// Code by Silverio Martinez Garcia (https://github.com/SilverioMG).
    /// </summary>
    public class LibJuegoVida
    {
        //Matrices con las figuras que se pueden añadir a la matriz:
        static private int[,] spin_hor = { { 1, 1, 1 } };
        static private int[,] spin_ver = { { 1 }, { 1 }, { 1 } };
        static private int[,] barco = { { 1, 1, 0 }, { 1, 0, 1 }, { 0, 1, 0 } };
        static private int[,] sapo = { { 0, 1, 1, 1 }, { 1, 1, 1, 0 } };
        static private int[,] planeador = { { 1, 1, 1 }, { 1, 0, 0 }, { 0, 1, 0 } };
        static private int[,] nave = { { 0, 1, 0, 0, 1 }, { 1, 0, 0, 0, 0 }, { 1, 0, 0, 0, 1 }, { 1, 1, 1, 1, 0 } };
        static private int[,] diehard = { { 0, 0, 0, 0, 0, 0, 1, 0 }, { 1, 1, 0, 0, 0, 0, 0, 0 }, { 0, 1, 0, 0, 0, 1, 1, 1 } };
        static private int[,] acorn = { { 0, 1, 0, 0, 0, 0, 0 }, { 0, 0, 0, 1, 0, 0, 0 }, { 1, 1, 0, 0, 1, 1, 1 } };

        /// <summary>
        /// Esta función carga los datos para la matriz del juego de la vida.
        /// <para>Excepciones:</para>
        /// El tamaño de la matriz es de 20x20.
        /// Se muestra un menú donde se puede introducir vida en las celdas, muerte o
        /// figuras predefinidas (spin, nave espacial...). Con los cursores nos desplazamos
        /// por las celdas de la matriz.
        /// </summary>
        /// <returns>Una matriz de enteros con los datos cargados por teclado.</returns>
        public static int[,] CargaDatos()
        {
            int fila = 0, columna = 0, tam;
            int[,] matrizactual;
            ConsoleKey valor; //Variable de enumeracion "ConsoleKey" con el valor de la tecla pulsada.
            int sw = 0;

            tam = 20; //Para esta versión tamaño fijo de 20x20 para la matriz.
            //Creo el objeto matriz.
            matrizactual = new int[tam, tam];

            //INTRODUCCIÓN DE DATOS:
            Console.Clear();
            VisualizarMarco();
            do
            {
                VisualizarMatriz(matrizactual, false);

                Console.WriteLine("\nMenú Juego de La Vida:");
                Console.WriteLine("-------------------");
                Console.WriteLine("0.- Borrar Celda");
                Console.WriteLine("1.- Vida en Celda");
                Console.WriteLine("2.- Spin Horizontal");
                Console.WriteLine("3.- Spin Vertical");
                Console.WriteLine("4.- Barco");
                Console.WriteLine("5.- Sapo");
                Console.WriteLine("6.- Planeador");
                Console.WriteLine("7.- Nave Espacial");
                Console.WriteLine("8.- DieHard");
                Console.WriteLine("9.- Acorn");
                Console.WriteLine("-Use las flechas del teclado para moverse por la tabla-");
                Console.WriteLine("-Pulse escape para salir y generar Juego-");

                //Vuelvo a posicionar el cursor en la fila y columna de la matriz:
                Console.SetCursorPosition((20 + columna), (1 + fila));

                do
                {//Bucle para moverse por la matriz con las flechas del teclado.
                    sw = 0;
                    valor = Console.ReadKey(true).Key; //Espera hasta que se pulse una tecla.
                    switch (valor)
                    {
                        case ConsoleKey.UpArrow:
                            if (fila > 0) { fila = fila - 1; }
                            break;
                        case ConsoleKey.DownArrow:
                            if (fila < tam - 1) { fila = fila + 1; }
                            break;
                        case ConsoleKey.RightArrow:
                            if (columna < tam - 1) { columna = columna + 1; }
                            break;
                        case ConsoleKey.LeftArrow:
                            if (columna > 0) { columna = columna - 1; }
                            break;
                        default:
                            sw = 1; //Se sale del bucle si se pulsa otra tecla que no sea flecha.
                            break;
                    }
                    Console.SetCursorPosition((20 + columna), (1 + fila));
                }
                while (sw == 0);

                //Comprobar valores de la tecla pulsada que no es flecha:                                
                if ((valor == ConsoleKey.D0) || (valor == ConsoleKey.NumPad0))
                { matrizactual[fila, columna] = 0; }

                if ((valor == ConsoleKey.D1) || (valor == ConsoleKey.NumPad1))
                { matrizactual[fila, columna] = 1; }

                if ((valor == ConsoleKey.D2) || (valor == ConsoleKey.NumPad2))
                { CopiarFigura(matrizactual, spin_hor, fila, columna); }

                if ((valor == ConsoleKey.D3) || (valor == ConsoleKey.NumPad3))
                { CopiarFigura(matrizactual, spin_ver, fila, columna); }

                if ((valor == ConsoleKey.D4) || (valor == ConsoleKey.NumPad4))
                { CopiarFigura(matrizactual, barco, fila, columna); }

                if ((valor == ConsoleKey.D5) || (valor == ConsoleKey.NumPad5))
                { CopiarFigura(matrizactual, sapo, fila, columna); }

                if ((valor == ConsoleKey.D6) || (valor == ConsoleKey.NumPad6))
                { CopiarFigura(matrizactual, planeador, fila, columna); }

                if ((valor == ConsoleKey.D7) || (valor == ConsoleKey.NumPad7))
                { CopiarFigura(matrizactual, nave, fila, columna); }

                if ((valor == ConsoleKey.D8) || (valor == ConsoleKey.NumPad8))
                { CopiarFigura(matrizactual, diehard, fila, columna); }

                if ((valor == ConsoleKey.D9) || (valor == ConsoleKey.NumPad9))
                { CopiarFigura(matrizactual, acorn, fila, columna); }
            }
            while (valor != ConsoleKey.Escape);

            return matrizactual;
        }

        /// <summary>
        /// Esta función recibe como parámetro la matriz de enteros con los datos cargados.
        /// Y genera el juego de la vida.
        /// </summary>
        /// <param name="matrizactual"></param>
        public static void GenerarJuegodelaVida(int[,] matrizactual)
        {
            int fila, columna;
            int tam = matrizactual.GetLength(0);
            int[,] matrizfutura = new int[tam, tam];
            //GENERAR JUEGO:
            Console.Clear();
            VisualizarMarco();

            do
            {
                for (fila = 0; fila < tam; fila++)
                {
                    for (columna = 0; columna < tam; columna++)
                    {
                        GenerarJuego(fila, columna, matrizactual, matrizfutura);
                    }
                }

                CopiaDeMatriz(matrizfutura, matrizactual);
                VisualizarMatriz(matrizactual, false);

                Console.WriteLine("Pulse una tecla para salir...");

                System.Threading.Thread.Sleep(500);
            }
            while (!Console.KeyAvailable);
        }


        /// <summary>
        /// Visualiza el contenido de la matriz recibida como parámetro.
        /// </summary>
        /// <param name="matriz"></param>
        /// <param name="clean_src">Parámetro que indica si se debe borrar la pantalla o no.
        /// Si no se borra la pantalla, en su lugar se situa el cursor en la posicion (0,0)
        /// antes de visualizar la matriz para evitar parpadeos. Si no se recibe valor
        /// para este parámetro, por defecto el valor es "true".</param>
        static void VisualizarMatriz(int[,] matriz, bool clean_src = true)
        {
            int fila, columna;
            int tam = matriz.GetLength(0);
            if (clean_src) { Console.Clear(); }

            for (fila = 0; fila < tam; fila++)
            {
                Console.SetCursorPosition(20, (1 + fila));
                for (columna = 0; columna < tam; columna++)
                {
                    if (matriz[fila, columna] == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write(" ");
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(15, fila + 3);
        }

        /// <summary>
        /// Este método visualiza el marco con caracteres Ascii que contiene la matriz 
        /// en pantalla.
        /// </summary>
        static void VisualizarMarco()
        {
            int columna;

            for (columna = 0; columna < 21; columna++)
            {
                Console.SetCursorPosition(20 + columna, 0);
                Console.Write('═');
                Console.SetCursorPosition(20 + columna, 21);
                Console.Write('═');
                Console.SetCursorPosition(19, columna);
                Console.Write('║');
                Console.SetCursorPosition(40, columna);
                Console.Write('║');
            }
            Console.SetCursorPosition(19, 0);
            Console.Write('╔');
            Console.SetCursorPosition(40, 0);
            Console.Write('╗');
            Console.SetCursorPosition(19, 21);
            Console.Write('╚');
            Console.SetCursorPosition(40, 21);
            Console.Write('╝');
        }


        /// <summary>
        /// Esta función visualiza el nº de celdas vecinas vivas de todas las celdas de la matriz
        //  que tienen al menos 1 celda vecina viva. Es una función de depuración.
        /// </summary>
        /// <param name="matriz"></param>
        static void mostrarvecinas(int[,] matriz)
        {
            int vecinas = 0;
            for (int fila = 0; fila < 10; fila++)
            {
                for (int columna = 0; columna < 10; columna++)
                {
                    vecinas = ContabilizarVecinas(fila, columna, matriz);
                    if (vecinas > 0)
                    {
                        Console.WriteLine("Vecinas [{0}][{1}]={2}", fila, columna, vecinas);
                    }
                }
            }
            Console.Read();
        }

        /// <summary>
        /// Esta función Lee un nº entero por teclado y lo devuelve. Si se introducen letras muestra
        //  mensaje de error.
        /// </summary>
        /// <returns></returns>
        static int LeerNum()
        {
            bool error;
            int num;

            do
            {
                error = !int.TryParse(Console.ReadLine(), out num);
                if (error) { Console.WriteLine("Solo números!\n\n"); }
            }
            while (error);

            return num;
        }

        /// <summary>
        /// Se genera la matriz futura a partir de la matriz actual.
        //  La matriz futura siempre se sobreescribe en cada llamada, no hace falta borrarla si se llama
        //  para todas sus posiciones.
        /// </summary>
        /// <param name="fila"></param>
        /// <param name="columna"></param>
        /// <param name="matrizact"></param>
        /// <param name="matrizfut"></param>
        static void GenerarJuego(int fila, int columna, int[,] matrizact, int[,] matrizfut)
        {
            int vecinas;

            vecinas = ContabilizarVecinas(fila, columna, matrizact);
            if (matrizact[fila, columna] == 1)
            {//Si la posición actual tiene vida:
                if ((vecinas < 2) || (vecinas > 3)) { matrizfut[fila, columna] = 0; }
                else { matrizfut[fila, columna] = 1; }
            }
            else
            {//Si la posición actual no tiene vida:
                if (vecinas == 3)
                { matrizfut[fila, columna] = 1; }
                else { matrizfut[fila, columna] = 0; }
            }
        }


        /// <summary>
        /// Esta función devuelve en número de vecinas vivas que tiene la posición indicada por
        //  "fila" y "columna" en la "matriz" recibida como parámetro.
        /// </summary>
        /// <param name="fila"></param>
        /// <param name="columna"></param>
        /// <param name="matriz"></param>
        /// <returns></returns>
        static int ContabilizarVecinas(int fila, int columna, int[,] matriz)
        {
            int filav, columnav;
            int acum = 0;
            int tam = matriz.GetLength(0);

            fila = fila - 1;
            columna = columna - 1;
            for (filav = fila; filav < fila + 3; filav++)
            {
                for (columnav = columna; columnav < columna + 3; columnav++)
                {
                    if ((filav >= 0 && filav < tam) && (columnav >= 0 && columnav < tam) && ((filav - fila) != 1 || (columnav - columna) != 1))
                    {
                        acum = acum + matriz[filav, columnav];
                    }
                }
            }
            return acum;
        }

        /// <summary>
        /// Se copia el contenido de la "matriz_origen" en la "matriz_destino":
        /// </summary>
        /// <param name="matriz_origen"></param>
        /// <param name="matriz_destino"></param>
        static void CopiaDeMatriz(int[,] matriz_origen, int[,] matriz_destino)
        {
            int fila, columna;

            for (fila = 0; fila < matriz_origen.GetLength(0); fila++)
            {
                for (columna = 0; columna < matriz_origen.GetLength(1); columna++)
                {
                    matriz_destino[fila, columna] = matriz_origen[fila, columna];
                }
            }
        }

        /// <summary>
        /// Este  método copia una matriz que contiene una figura en otra matriz.
        /// Se copia en la posicion fila/columna indicada como parámetro.  Dicha posición
        /// indica el centro donde se dibujará la figura sobre la matriz principal.
        /// </summary>
        /// <param name="matriz">La matriz del juego de la vida</param>
        /// <param name="figura">La matriz que contiene la figura</param>
        /// <param name="y">Fila de la matriz donde se dibujará la figura. Apunta al centro de la figura.</param>
        /// <param name="x">Columna de la matriz donde se dibujará la figura. Apunta al centro de la figura.</param>
        static void CopiarFigura(int[,] matriz, int[,] figura, int y, int x)
        {
            int fila, columna;
            int ancho_fig = figura.GetLength(1);
            int alto_fig = figura.GetLength(0);
            int ancho_matriz = matriz.GetLength(1);
            int alto_matriz = matriz.GetLength(0);
            int posx, posy;

            x = x - (ancho_fig / 2);
            y = y - (alto_fig / 2);

            for (fila = 0; fila < alto_fig; fila++)
            {
                for (columna = 0; columna < ancho_fig; columna++)
                {
                    posy = y + fila;
                    posx = x + columna;
                    if ((posy >= 0) && (posy < alto_matriz) && (posx >= 0) && (posx < ancho_matriz))
                    {
                        matriz[posy, posx] = figura[fila, columna];
                    }
                }
            }
        }


    }

}
