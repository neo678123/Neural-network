#include "Neuron.h"

Neuron::Neuron(String^% name1)
{
	name = name1;
	weights = gcnew List<double>();

	for (int i = 0; i < 100; i++)
	{
		weights->Add(0);
	}

	if (File::Exists(name + ".txt"))
	{
		readWeights();
	}
}

double Neuron::think(List<unsigned char>^ in)
{
	double out = 0;
	for (unsigned char i = 0; i < 100; i++)
	{
		out += in[i] * weights[i];
	}
	return out;
}

void Neuron::adjust(List<unsigned char>^ in)
{
	for (unsigned char i = 0; i < 100; i++)
	{
		weights[i] += (in[i] == 1) ? 0.000025 : -0.000025;
	}
}

void Neuron::writeWeights()
{
	StreamWriter^ file = gcnew StreamWriter(name + ".txt");
	for (unsigned char i = 0; i < 10; i++)
	{
		for (unsigned char j = 0; j < 10; j++)
		{

			file->WriteLine(weights[10 * i + j]);
		}
	}
	file->Close();
}

void Neuron::readWeights()
{
	unsigned char count = 0;
	String^ line;

	StreamReader^ file = gcnew StreamReader(name + ".txt");
	while ((line = file->ReadLine()) != nullptr)
	{
		weights[count] = Double::Parse(line);
		count++;
	}
}

List<unsigned char>^ Neuron::imageToBitArray(String^% path)
{
	Bitmap^ img = gcnew Bitmap(path);
	List<unsigned char>^ out = gcnew List<unsigned char>;

	for (unsigned char i = 0; i < 10; i++)
	{
		for (unsigned char j = 0; j < 10; j++)
		{
			Color^ c = img->GetPixel(i, j);
			if (c->GetBrightness() <= 0.2)
			{
				out->Add(1);
			}
			else
			{
				out->Add(0);
			}
		}
	}
	return out;
}
