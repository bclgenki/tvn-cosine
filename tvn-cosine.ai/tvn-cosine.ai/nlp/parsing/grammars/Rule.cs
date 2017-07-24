﻿using System.Text;
using System.Text.RegularExpressions;
using tvn.cosine.ai.common.collections;
using tvn.cosine.ai.common.collections.api;

namespace tvn.cosine.ai.nlp.parsing.grammars
{
    /**
     * A derivation rule that is contained within a grammar. This rule is probabilistic, in that it 
     * has an associated probability representing the likelihood that the RHS follows from the LHS, given 
     * the presence of the LHS.
     * @author Jonathon (thundergolfer)
     *
     */
    public class Rule
    {
        public readonly float PROB;
        public readonly ICollection<string> lhs; // Left hand side of derivation rule
        public readonly ICollection<string> rhs; // Right hand side of derivation rule

        // Basic constructor
        public Rule(ICollection<string> lhs, ICollection<string> rhs, float probability)
        {
            this.lhs = lhs;
            this.rhs = rhs;
            this.PROB = validateProb(probability);
        }

        // null RHS rule constructor
        public Rule(ICollection<string> lhs, float probability)
        {
            this.lhs = lhs;
            this.rhs = null;
            this.PROB = validateProb(probability);
        }

        // string split constructor
        public Rule(string lhs, string rhs, float probability)
        {
            this.lhs = CollectionFactory.CreateQueue<string>();
            this.rhs = CollectionFactory.CreateQueue<string>();

            if (!string.IsNullOrEmpty(lhs)) 
            {
                this.lhs = CollectionFactory.CreateQueue<string>();
                foreach (string input in Regex.Split(lhs, "\\s*,\\s*"))
                {
                    if (!string.IsNullOrEmpty(input))
                    {
                        this.lhs.Add(input);
                    }
                }
            }

            if (!string.IsNullOrEmpty(rhs)) 
            {
                foreach (string input in Regex.Split(rhs, "\\s*,\\s*"))
                {
                    if (!string.IsNullOrEmpty(input))
                    {
                        this.rhs.Add(input);
                    }
                }
            }

            this.PROB = validateProb(probability);
        }

        /**
         * Currently a hack to ensure rule has a valid probablity value.
         * Don't really want to throw an exception.
         */
        private float validateProb(float prob)
        {
            if (prob >= 0.0 && prob <= 1.0)
                return prob;
            else
                return (float)0.5; // probably should throw exception
        }

        public bool derives(ICollection<string> sentForm)
        {
            if (rhs.Size() != sentForm.Size())
                return false;
            for (int i = 0; i < sentForm.Size(); ++i)
            {
                if (!rhs.Get(i).Equals(sentForm.Get(i)))
                    return false;
            }
            return true;
        }

        public bool derives(string terminal)
        {
            return rhs.Size() == 1 && rhs.Get(0).Equals(terminal);
        }


        public override string ToString()
        {
            StringBuilder output = new StringBuilder();

            foreach (string lh in lhs)
            {
                output.Append(lh);
            }

            output.Append(" -> ");

            foreach (string rh in rhs)
            {
                output.Append(rh);
            }

            output.Append(" ").Append(PROB.ToString());

            return output.ToString();
        }
    }
}
