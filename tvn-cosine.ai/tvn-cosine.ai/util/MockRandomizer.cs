﻿using tvn.cosine.ai.common.api;
using tvn.cosine.ai.common.exceptions;

namespace tvn.cosine.ai.util
{
    /// <summary>
    /// Mock implementation of the Randomizer interface so that the set of Random
    /// numbers returned are in fact predefined.
    /// </summary>
    public class MockRandomizer : IRandom
    {
        private double[] values;
        private int index;
         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values">the set of predetermined random values to loop over.</param>
        public MockRandomizer(double[] values)
        {
            this.values = new double[values.Length];
            System.Array.Copy(values, 0, this.values, 0, values.Length);
            this.index = 0;
        }
         
        public double NextDouble()
        {
            if (index == values.Length)
            {
                index = 0;
            }

            return values[index++];
        }

        public bool NextBoolean()
        {
            throw new NotImplementedException();
        }

        public int Next()
        {
            throw new NotImplementedException();
        }

        public int Next(int minimumValue, int maximumValue)
        {
            throw new NotImplementedException();
        }

        public int Next(int maximumValue)
        {
            throw new NotImplementedException();
        } 
    }
}
