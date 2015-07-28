using System;

namespace TextRecognition
{
    class Program
    {
        byte[][] letters;

        NeuralNetwork[] alphabet;

        static void Main(string[] args)
        {
            Program program = new Program();

            program.alphabet = new NeuralNetwork[26];
            program.letters = new byte[26][];

            program.alphabet[0] = new NeuralNetwork("A");
            program.alphabet[1] = new NeuralNetwork("B");


        }

        static void writeImage(byte[] b)
        {
            for (int k = 0; k < 10; k++)
            {
                Console.Write(b[0 + 1 * k]);
                Console.Write(b[10 + 1 * k]);
                Console.Write(b[20 + 1 * k]);
                Console.Write(b[30 + 1 * k]);
                Console.Write(b[40 + 1 * k]);
                Console.Write(b[50 + 1 * k]);
                Console.Write(b[60 + 1 * k]);
                Console.Write(b[70 + 1 * k]);
                Console.Write(b[80 + 1 * k]);
                Console.WriteLine(b[90 + 1 * k]);
            }
        }
    }
}
