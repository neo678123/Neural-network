#pragma once
#include <vector>
#include <string>

using namespace System;
using namespace System::Drawing;
using namespace System::Collections;
using namespace System::Collections::Generic;
using namespace System::IO;

ref class Neuron
{
public:
	String^ name;
	List<double>^ weights;

	Neuron(String^% name1);
	double think(List<unsigned char>^ in);
	void adjust(List<unsigned char>^ in);
	void writeWeights();
	void readWeights();

	static List<unsigned char>^ imageToBitArray(String^% path);
};

