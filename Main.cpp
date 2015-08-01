#include <iostream>
#include "Neuron.h"
#include <cmath>
#include <cstdlib>

#define TRAIN 0
#define RUN 1

using namespace System;
using namespace System::Collections::Generic;

void writeImage(List<unsigned char>^ in);

int main()
{
	List<List<unsigned char>^>^ letters = gcnew List<List<unsigned char>^>();
	List<Neuron^>^ brain = gcnew List<Neuron^>();
	unsigned int count = 0;

	brain->Add(gcnew Neuron(%String("A")));
	brain->Add(gcnew Neuron(%String("B")));

	unsigned int minus = 0;
	for (unsigned char i = 0; i < brain->Count; i++)
	{
		while (true)
		{
			String^ temp = "images/" + (brain[i]->name) + (count - minus + 1) + ".png";
			if (!(IO::File::Exists(temp)))
				break;
			else
				letters->Add(Neuron::imageToBitArray(temp));
			count++;
		}
		minus = count;
	}

#if TRAIN
	double rating;

	for (unsigned int k = 0; k < 2000000; k++)
	{
		for (unsigned char i = 0; i < 8; i++)
		{
			rating = brain[0]->think(letters[i]);
			if (round(rating - 0.5) != 1)
				brain[0]->adjust(letters[i]);
		}

		if (k % 100000 == 0)
		{
			for (unsigned char i = 0; i < 8; i++)
			{
				Console::WriteLine(brain[0]->think(letters[i]));
			}
			Console::WriteLine();
		}
	}

	Console::WriteLine("save?");
	String^ input = Console::ReadLine();
	if (input == "y")
	{
		brain[0]->writeWeights();
	}
#endif

#if RUN
	MAIN:
		String^ letter;
		unsigned int randomNumber = std::rand() % (count);

		double valA = brain[0]->think(letters[randomNumber]);
		double valB = brain[1]->think(letters[randomNumber]);

		writeImage(letters[randomNumber]);

		if (abs(valA - 1) < abs(valB - 1))
			letter = "A";
		else
			letter = "B";

		Console::WriteLine("retry?");
		Console::WriteLine(letter);
		String^ input = Console::ReadLine();
		if (input == "y")
		{
			Console::WriteLine();
			goto MAIN;
		}
#endif
}

void writeImage(List<unsigned char>^ in)
{
	for (int k = 0; k < 10; k++)
	{
		for (int i = 0; i < 10; i++)
		{
			Console::Write(in[10 * i + k]);
		}
		Console::WriteLine(in[90 + k]);
	}
}
