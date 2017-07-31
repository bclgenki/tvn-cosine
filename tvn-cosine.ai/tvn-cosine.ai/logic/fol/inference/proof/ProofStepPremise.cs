﻿using tvn.cosine.collections;
using tvn.cosine.collections.api;

namespace tvn.cosine.ai.logic.fol.inference.proof
{
    public class ProofStepPremise : AbstractProofStep
    { 
        private static readonly ICollection<ProofStep> _noPredecessors = CollectionFactory.CreateQueue<ProofStep>();
        //
        private object proof = "";

        public ProofStepPremise(object proof)
        {
            this.proof = proof;
        }
         
        public override ICollection<ProofStep> getPredecessorSteps()
        {
            return CollectionFactory.CreateReadOnlyQueue<ProofStep>(_noPredecessors);
        }
         
        public override string getProof()
        {
            return proof.ToString();
        }
         
        public override string getJustification()
        {
            return "Premise";
        } 
    }
}
