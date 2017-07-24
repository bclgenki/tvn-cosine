﻿using System.Text;
using tvn.cosine.ai.common.collections;
using tvn.cosine.ai.common.collections.api;
using tvn.cosine.ai.common.exceptions;
using tvn.cosine.ai.probability.api;
using tvn.cosine.ai.probability.domain;

namespace tvn.cosine.ai.probability.proposition
{
    public class IntegerSumProposition : AbstractDerivedProposition
    {
        private FiniteIntegerDomain sumsDomain = null;
        private ICollection<IRandomVariable> sumVars = CollectionFactory.CreateQueue<IRandomVariable>();
        //
        private string toString = null;

        public IntegerSumProposition(string name, FiniteIntegerDomain sumsDomain, params IRandomVariable[] sums)
            : base(name)
        {
            if (null == sumsDomain)
            {
                throw new IllegalArgumentException("Sum Domain must be specified.");
            }
            if (null == sums || 0 == sums.Length)
            {
                throw new IllegalArgumentException("Sum variables must be specified.");
            }
            this.sumsDomain = sumsDomain;
            foreach (IRandomVariable rv in sums)
            {
                addScope(rv);
                sumVars.Add(rv);
            }
        }
         
        public override bool holds(IMap<IRandomVariable, object> possibleWorld)
        {
            int sum = 0;

            foreach (IRandomVariable rv in sumVars)
            {
                object o = possibleWorld.Get(rv);
                if (o is int)
                {
                    sum += ((int)o);
                }
                else
                {
                    throw new IllegalArgumentException("Possible World does not contain a int value for the sum variable:" + rv);
                }
            }

            return sumsDomain.getPossibleValues().Contains(sum);
        }
         
        public override string ToString()
        {
            if (null == toString)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(getDerivedName());
                sb.Append(" = ");
                sb.Append(sumsDomain.ToString());
                toString = sb.ToString();
            }
            return toString;
        }
    }

}
