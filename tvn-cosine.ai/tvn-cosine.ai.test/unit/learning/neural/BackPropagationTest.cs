﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using tvn.cosine.ai.learning.framework;
using tvn.cosine.ai.learning.neural;
using tvn.cosine.ai.learning.neural.api;
using tvn.cosine.ai.learning.neural.examples;
using tvn.cosine.ai.util.math;

namespace tvn_cosine.ai.test.unit.learning.neural
{
    [TestClass]
    public class BackPropagationTest
    {

        [TestMethod]
        public void testFeedForwardAndBAckLoopWorks()
        {
            // example 11.14 of Neural Network Design by Hagan, Demuth and Beale
            Matrix hiddenLayerWeightMatrix = new Matrix(2, 1);
            hiddenLayerWeightMatrix.set(0, 0, -0.27);
            hiddenLayerWeightMatrix.set(1, 0, -0.41);

            Vector hiddenLayerBiasVector = new Vector(2);
            hiddenLayerBiasVector.setValue(0, -0.48);
            hiddenLayerBiasVector.setValue(1, -0.13);

            Vector input = new Vector(1);
            input.setValue(0, 1);

            Matrix outputLayerWeightMatrix = new Matrix(1, 2);
            outputLayerWeightMatrix.set(0, 0, 0.09);
            outputLayerWeightMatrix.set(0, 1, -0.17);

            Vector outputLayerBiasVector = new Vector(1);
            outputLayerBiasVector.setValue(0, 0.48);

            Vector error = new Vector(1);
            error.setValue(0, 1.261);

            double learningRate = 0.1;
            double momentumFactor = 0.0;
            FeedForwardNeuralNetwork ffnn = new FeedForwardNeuralNetwork(
                    hiddenLayerWeightMatrix, hiddenLayerBiasVector,
                    outputLayerWeightMatrix, outputLayerBiasVector);
            ffnn.SetTrainingScheme(new BackPropagationLearning(learningRate,
                    momentumFactor));
            ffnn.ProcessInput(input);
            ffnn.ProcessError(error);

            Matrix finalHiddenLayerWeights = ffnn.GetHiddenLayerWeights();
            Assert.AreEqual(-0.265, finalHiddenLayerWeights.get(0, 0), 0.001);
            Assert.AreEqual(-0.419, finalHiddenLayerWeights.get(1, 0), 0.001);

            Vector hiddenLayerBias = ffnn.GetHiddenLayerBias();
            Assert.AreEqual(-0.475, hiddenLayerBias.getValue(0), 0.001);
            Assert.AreEqual(-0.1399, hiddenLayerBias.getValue(1), 0.001);

            Matrix finalOutputLayerWeights = ffnn.GetOutputLayerWeights();
            Assert.AreEqual(0.171, finalOutputLayerWeights.get(0, 0), 0.001);
            Assert.AreEqual(-0.0772, finalOutputLayerWeights.get(0, 1), 0.001);

            Vector outputLayerBias = ffnn.GetOutputLayerBias();
            Assert.AreEqual(0.7322, outputLayerBias.getValue(0), 0.001);
        }

        [TestMethod]
        public void testFeedForwardAndBAckLoopWorksWithMomentum()
        {
            // example 11.14 of Neural Network Design by Hagan, Demuth and Beale
            Matrix hiddenLayerWeightMatrix = new Matrix(2, 1);
            hiddenLayerWeightMatrix.set(0, 0, -0.27);
            hiddenLayerWeightMatrix.set(1, 0, -0.41);

            Vector hiddenLayerBiasVector = new Vector(2);
            hiddenLayerBiasVector.setValue(0, -0.48);
            hiddenLayerBiasVector.setValue(1, -0.13);

            Vector input = new Vector(1);
            input.setValue(0, 1);

            Matrix outputLayerWeightMatrix = new Matrix(1, 2);
            outputLayerWeightMatrix.set(0, 0, 0.09);
            outputLayerWeightMatrix.set(0, 1, -0.17);

            Vector outputLayerBiasVector = new Vector(1);
            outputLayerBiasVector.setValue(0, 0.48);

            Vector error = new Vector(1);
            error.setValue(0, 1.261);

            double learningRate = 0.1;
            double momentumFactor = 0.5;
            FeedForwardNeuralNetwork ffnn = new FeedForwardNeuralNetwork(
                    hiddenLayerWeightMatrix, hiddenLayerBiasVector,
                    outputLayerWeightMatrix, outputLayerBiasVector);

            ffnn.SetTrainingScheme(new BackPropagationLearning(learningRate,
                    momentumFactor));
            ffnn.ProcessInput(input);
            ffnn.ProcessError(error);

            Matrix finalHiddenLayerWeights = ffnn.GetHiddenLayerWeights();
            Assert.AreEqual(-0.2675, finalHiddenLayerWeights.get(0, 0), 0.001);
            Assert.AreEqual(-0.4149, finalHiddenLayerWeights.get(1, 0), 0.001);

            Vector hiddenLayerBias = ffnn.GetHiddenLayerBias();
            Assert.AreEqual(-0.4775, hiddenLayerBias.getValue(0), 0.001);
            Assert.AreEqual(-0.1349, hiddenLayerBias.getValue(1), 0.001);

            Matrix finalOutputLayerWeights = ffnn.GetOutputLayerWeights();
            Assert.AreEqual(0.1304, finalOutputLayerWeights.get(0, 0), 0.001);
            Assert.AreEqual(-0.1235, finalOutputLayerWeights.get(0, 1), 0.001);

            Vector outputLayerBias = ffnn.GetOutputLayerBias();
            Assert.AreEqual(0.6061, outputLayerBias.getValue(0), 0.001);
        }

        [TestMethod]
        public void testDataSetPopulation()
        {
            DataSet irisDataSet = DataSetFactory.getIrisDataSet();
            INumerizer numerizer = new IrisDataSetNumerizer();
            NeuralNetworkDataSet innds = new IrisNeuralNetworkDataSet();

            innds.CreateExamplesFromDataSet(irisDataSet, numerizer);

            NeuralNetworkConfig config = new NeuralNetworkConfig();
            config.SetConfig(FeedForwardNeuralNetwork.NUMBER_OF_INPUTS, 4);
            config.SetConfig(FeedForwardNeuralNetwork.NUMBER_OF_OUTPUTS, 3);
            config.SetConfig(FeedForwardNeuralNetwork.NUMBER_OF_HIDDEN_NEURONS, 6);
            config.SetConfig(FeedForwardNeuralNetwork.LOWER_LIMIT_WEIGHTS, -2.0);
            config.SetConfig(FeedForwardNeuralNetwork.UPPER_LIMIT_WEIGHTS, 2.0);

            FeedForwardNeuralNetwork ffnn = new FeedForwardNeuralNetwork(config);
            ffnn.SetTrainingScheme(new BackPropagationLearning(0.1, 0.9));

            ffnn.TrainOn(innds, 10);

            innds.RefreshDataset();
            ffnn.TestOnDataSet(innds);
        }

        [TestMethod]
        public void testPerceptron()
        {
            DataSet irisDataSet = DataSetFactory.getIrisDataSet();
            INumerizer numerizer = new IrisDataSetNumerizer();
            NeuralNetworkDataSet innds = new IrisNeuralNetworkDataSet();

            innds.CreateExamplesFromDataSet(irisDataSet, numerizer);

            Perceptron perc = new Perceptron(3, 4);

            perc.TrainOn(innds, 10);

            innds.RefreshDataset();
            perc.TestOnDataSet(innds);
        }
    } 
}
