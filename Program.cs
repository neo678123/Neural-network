//#define TRAIN
#define RUN

using System;
using System.Collections.Generic;
using System.IO;

namespace TextRecognition
{
    class Program
    {
        List<byte[]> letters;
        List<NeuralNetwork> alphabet;
        static int count;

        static void Main(string[] args)
        {
            Program program = new Program();

            program.alphabet = new List<NeuralNetwork>();
            program.letters = new List<byte[]>();

            program.alphabet.Add(new NeuralNetwork("A"));
            program.alphabet.Add(new NeuralNetwork("B"));

            count = 0;
            int minus = 0;

            for(int i = 0; i < program.alphabet.Count; i++)
            {
                while (true)
                {
                    if (!(File.Exists($"images/{program.alphabet[i].name}{count - minus + 1}.png")))
                        break;

                    program.letters.Add(IO.imageToBitArray($"images/{program.alphabet[i].name}{count - minus + 1}.png"));
                    count++;
                }
                minus = count;
            }

#if TRAIN
            long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            double rating;

            for(int k = 0; k < 2000000; k++)
            {
                for(int i = 0; i < 8; i++)
                {
                    rating = program.alphabet[0].think(program.letters[i]);
                    if (Math.Round(rating - 0.5) != 1)
                        program.alphabet[0].adjust(program.letters[i]);
                }

                if(k%100000 == 0)
                {
                    Console.WriteLine(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - milliseconds);

                    for (int i = 0; i < 8; i++)
                    {
                        Console.WriteLine(program.alphabet[0].think(program.letters[i]));
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
            int randomNumber = random.Next(0, count);
            double valA, valB;

            valA = program.alphabet[0].think(program.letters[randomNumber]);
            valB = program.alphabet[1].think(program.letters[randomNumber]);

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
                for (int i = 0; i < 10; i++)
                {
                    Console.Write(b[10 * i + k]);
                }
           
                Console.WriteLine(b[90 + k]);
            }
        }
    }
}
