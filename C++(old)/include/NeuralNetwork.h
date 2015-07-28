#ifndef NEURALNETWORK_H
#define NEURALNETWORK_H

#include <vector>

class NeuralNetwork
{
    public:
        NeuralNetwork();
        virtual ~NeuralNetwork();

        double think(std::vector<std::vector<int>> &in);
        void adjust(std::vector<std::vector<int>> &in);
    protected:
    private:
            std::vector<std::vector<double>> weight;
            double out;
};

#endif // NEURALNETWORK_H
