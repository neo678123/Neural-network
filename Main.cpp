#include <iostream>
#include "Neuron.h"
#include <cmath>
#include <cstdlib>
#include <algorithm>

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
	brain->Add(gcnew Neuron(%String("C")));

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

	for (unsigned int k = 0; k < 1000000; k++)
	{
		for (unsigned char i = 16; i < count; i++)
		{
			rating = brain[2]->think(letters[i]);
			if (round(rating - 0.5) != 1)
				brain[2]->adjust(letters[i]);
		}

		if (k % 100000 == 0)
		{
			for (unsigned char i = 17; i < count; i++)
			{
				Console::WriteLine(brain[2]->think(letters[i]));
			}
			Console::WriteLine();
		}
	}

	Console::WriteLine("save?");
	String^ input = Console::ReadLine();
	if (input == "y")
	{
		brain[2]->writeWeights();
	}
#endif

#if RUN
	MAIN:
		String^ letter;
		unsigned int randomNumber = std::rand() % (count);

		const double valA = brain[0]->think(letters[randomNumber]);
		const double valB = brain[1]->think(letters[randomNumber]);
		const double valC = brain[2]->think(letters[randomNumber]);

		writeImage(letters[randomNumber]);

		double min = std::min({abs(valA - 1), abs(valB - 1), abs(valC - 1)});

		if		(min == abs(valA - 1))
			letter = "A";
		else if (min == abs(valB - 1))
			letter = "B";
		else if (min == abs(valC - 1))
			letter = "C";
		else
			letter = "3";

		Console::WriteLine("The letter is " + letter);
		Console::WriteLine();
		Console::WriteLine("retry?");
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
