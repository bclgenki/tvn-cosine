﻿namespace tvn.cosine.ai.probability.bayes.impl
{
    /**
     * Default implementation of the FiniteNode interface that uses a fully
     * specified Conditional Probability Table to represent the Node's conditional
     * distribution.
     * 
     * @author Ciaran O'Reilly
     * 
     */
    public class FullCPTNode : AbstractNode, FiniteNode
    {
        private ConditionalProbabilityTable cpt = null;

        public FullCPTNode(RandomVariable var, double[] distribution)
            : this(var, distribution, (Node[])null)
        { }

        public FullCPTNode(RandomVariable var, double[] values, params Node[] parents)
            : base(var, parents)
        {
            RandomVariable[] conditionedOn = new RandomVariable[getParents().Size()];
            int i = 0;
            foreach (Node p in getParents())
            {
                conditionedOn[i++] = p.getRandomVariable();
            }

            cpt = new CPT(var, values, conditionedOn);
        }

        public override ConditionalProbabilityDistribution getCPD()
        {
            return getCPT();
        }

        public virtual ConditionalProbabilityTable getCPT()
        {
            return cpt;
        }
    }
}
