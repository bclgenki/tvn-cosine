﻿using tvn.cosine.collections;
using tvn.cosine.collections.api;
using tvn.cosine.ai.logic.fol.kb.data;
using tvn.cosine.ai.logic.fol.parsing.ast;

namespace tvn.cosine.ai.logic.fol.inference.otter.defaultimpl
{
    public class DefaultClauseSimplifier : ClauseSimplifier
    {
        private Demodulation demodulation = new Demodulation();
        private ICollection<TermEquality> rewrites = CollectionFactory.CreateQueue<TermEquality>();

        public DefaultClauseSimplifier()
        { }

        public DefaultClauseSimplifier(ICollection<TermEquality> rewrites)
        {
            this.rewrites.AddAll(rewrites);
        }
         
        public Clause simplify(Clause c)
        {
            Clause simplified = c;

            // Apply each of the rewrite rules to the clause
            foreach (TermEquality te in rewrites)
            {
                Clause dc = simplified;
                // Keep applying the rewrite as many times as it
                // can be applied before moving on to the next one.
                while (null != (dc = demodulation.apply(te, dc)))
                {
                    simplified = dc;
                }
            }

            return simplified;
        }
    }
}
