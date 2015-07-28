//#define TRAIN
#define RUN

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
            program.letters = new byte[260][];

            program.alphabet[0] = new NeuralNetwork("A");
            program.alphabet[1] = new NeuralNetwork("B");

            int a, b;
            for(a = 0; a < 8; a++){program.letters[a] = IO.imageToBitArray($"images/A{a+1}.png");}
            for(b = 0; b < 9; b++){program.letters[1+a+b] = IO.imageToBitArray($"images/B{b+1}.png");}

#if TRAIN
            long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            double rating;
            for(int k = 0; k < 2000000; k++)
            {
                for(int i = 0; i < 8; i++)
                {
                    rating = program.alphabet[0].think(ref program.letters[i]);
                    if (Math.Round(rating - 0.5) != 1)
                        program.alphabet[0].adjust(ref program.letters[i]);
                }

                if(k%100000 == 0)
                {
                    Console.WriteLine(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - milliseconds);

                    for (int i = 0; i < 8; i++)
                    {
                        Console.WriteLine(program.alphabet[0].think(ref program.letters[i]));
                    }
                    Console.WriteLine();

                    milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                }
            }
            Console.WriteLine("save?");
            string input = Console.ReadLine();
            if(input == "y")
            {
                program.alphabet[0].writeWeights();
            }
#endif

#if RUN
        Main:
            char letter;
            Random random = new Random();
            int randomNumber = random.Next(0, a+b);
            double valA, valB;

            valA = program.alphabet[0].think(ref program.letters[randomNumber]);
            valB = program.alphabet[1].think(ref program.letters[randomNumber]);

            writeImage(program.letters[randomNumber]);

            if (valA > valB)
                letter = 'A';
            else
                letter = 'B';

            Console.WriteLine($"The input was {letter}");
            Console.WriteLine("rego?");
            string input = Console.ReadLine();
            if (input == "y")
            {
                Console.WriteLine();
                goto Main;
            }

#endif
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
