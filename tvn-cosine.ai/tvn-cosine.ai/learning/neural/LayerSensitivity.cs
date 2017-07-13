﻿using System.Collections.Generic;
using tvn.cosine.ai.util.math;

namespace tvn.cosine.ai.learning.neural
{
    /**
     * @author Ravi Mohan
     * 
     */
    public class LayerSensitivity
    {
        /*
         * contains sensitivity matrices and related calculations for each layer.
         * Used for backprop learning
         */

        private Matrix sensitivityMatrix;
        private readonly Layer layer;

        public LayerSensitivity(Layer layer)
        {
            Matrix weightMatrix = layer.getWeightMatrix();
            this.sensitivityMatrix = new Matrix(weightMatrix.getRowDimension(), weightMatrix.getColumnDimension());
            this.layer = layer;

        }

        public Matrix getSensitivityMatrix()
        {
            return sensitivityMatrix;
        }

        public Matrix sensitivityMatrixFromErrorMatrix(Vector errorVector)
        {
            Matrix derivativeMatrix = createDerivativeMatrix(layer .getLastInducedField());
            Matrix calculatedSensitivityMatrix = derivativeMatrix .times(errorVector).times(-2.0);
            sensitivityMatrix = calculatedSensitivityMatrix.copy();
            return calculatedSensitivityMatrix;
        }

        public Matrix sensitivityMatrixFromSucceedingLayer(LayerSensitivity nextLayerSensitivity)
        {
            Layer nextLayer = nextLayerSensitivity.getLayer();
            Matrix derivativeMatrix = createDerivativeMatrix(layer.getLastInducedField());
            Matrix weightTranspose = nextLayer.getWeightMatrix().transpose();
            Matrix calculatedSensitivityMatrix = derivativeMatrix.times(
                    weightTranspose).times(
                    nextLayerSensitivity.getSensitivityMatrix());
            sensitivityMatrix = calculatedSensitivityMatrix.copy();
            return sensitivityMatrix;
        }

        public Layer getLayer()
        {
            return layer;
        }

        //
        // PRIVATE METHODS
        //

        private Matrix createDerivativeMatrix(Vector lastInducedField)
        {
            IList<double> lst = new List<double>();
            for (int i = 0; i < lastInducedField.size(); i++)
            {
                lst.Add(layer.getActivationFunction().deriv(lastInducedField.getValue(i)));
            }
            return Matrix.createDiagonalMatrix(lst);
        }
    } 
}
