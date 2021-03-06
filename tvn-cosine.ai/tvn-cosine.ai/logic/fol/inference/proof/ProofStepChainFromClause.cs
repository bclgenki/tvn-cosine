﻿using tvn.cosine.collections;
using tvn.cosine.collections.api;
using tvn.cosine.ai.logic.fol.kb.data;

namespace tvn.cosine.ai.logic.fol.inference.proof
{
    public class ProofStepChainFromClause : AbstractProofStep
    {
        private ICollection<ProofStep> predecessors = CollectionFactory.CreateQueue<ProofStep>();
        private Chain chain = null;
        private Clause fromClause = null;

        public ProofStepChainFromClause(Chain chain, Clause fromClause)
        {
            this.chain = chain;
            this.fromClause = fromClause;
            this.predecessors.Add(fromClause.getProofStep());
        }
         
        public override ICollection<ProofStep> getPredecessorSteps()
        {
            return CollectionFactory.CreateReadOnlyQueue<ProofStep>(predecessors);
        }
         
        public override string getProof()
        {
            return chain.ToString();
        }
         
        public override string getJustification()
        {
            return "Chain from Clause: "
                    + fromClause.getProofStep().getStepNumber();
        } 
    }
}
