using System.Drawing;

namespace TextRecognition
{
    class IO
    {
        public static byte[] imageToBitArray(string path)
        {
            Bitmap img = new Bitmap(Image.FromFile(path));
            byte[] output = new byte[100];

            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    if (img.GetPixel(i, j).GetBrightness() <= 0.2)
                        output[img.Width * i + j] = 1;
                    else
                        output[img.Width * i + j] = 0;
                }
            }
            return output;
        }
    }
}
