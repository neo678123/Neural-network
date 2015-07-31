using System.IO;

namespace TextRecognition
{
    class NeuralNetwork
    {
        private double[] weights;
        public string name;

        public NeuralNetwork(string name1)
        {
            name = name1;
            weights = new double[100];

            for(int i = 0; i < 100; i++)
            {
                weights[i] = 0;
            }

            if (File.Exists($"{name}.txt"))
                readWeights();
        }

        public double think(byte[] input)
        {
            double outp = 0;
            for (int i = 0; i < 100; i++)
            {
                outp += input[i] * weights[i];
            }
            return outp;
        }

        public void adjust(byte[] input)
        {
            for (int i = 0; i < 100; i++)
            {
                 weights[i] += (input[i] == 1) ? 0.000025 : -0.000025;
            }
        }

        public void writeWeights()
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter($"{name}.txt");
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    
                    file.WriteLine(weights[10 * i + j]);
                }
            }
            file.Close();
        }

        public void readWeights()
        {
            byte count = 0;
            string line;

            System.IO.StreamReader file = new System.IO.StreamReader($"{name}.txt");
            while ((line = file.ReadLine()) != null)
            {
                weights[count] = double.Parse(line);
                count++;
            }

            file.Close();
        }
    }
}
