﻿using System.Collections.Generic;

namespace tvn.cosine.ai.learning.neural
{
    /**
     * @author Ravi Mohan
     * 
     */
    public class RabbitEyeDataSet : NNDataSet
    {
        public override void setTargetColumns()
        {
            // assumed that data from file has been pre processed
            // TODO this should be
            // somewhere else,in the
            // super class.
            // Type != class Aargh! I want more
            // powerful type systems
            targetColumnNumbers = new List<int>();

            targetColumnNumbers.Add(1); // using zero based indexing
        }
    }
}
