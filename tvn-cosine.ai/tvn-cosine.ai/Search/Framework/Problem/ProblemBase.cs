﻿using System.Collections.Generic;

namespace tvn.cosine.ai.Search.Framework.Problem
{
    public abstract class ProblemBase<S, A> : IProblem<S, A>
    {
        public abstract List<A> getActions(S state);
        public abstract S getInitialState();
        public abstract S getResult(S state, A action);
        public abstract double getStepCosts(S state, A action, S stateDelta);
        public abstract bool testGoal(S state);

        public bool testSolution(Node<S, A> node)
        {
            return testGoal(node.getState());
        }
    }
}
