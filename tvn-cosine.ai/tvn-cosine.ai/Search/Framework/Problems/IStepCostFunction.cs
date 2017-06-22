﻿using tvn_cosine.ai.Agents;

namespace tvn_cosine.ai.Search.Framework.Problems
{
    /// <summary>
    /// Artificial Intelligence A Modern Approach (3rd Edition): page 68. 
    ///
    /// The step cost of taking action a in state s to reach state s' is denoted by c(s, a, s').
    /// </summary>
    public interface IStepCostFunction
    { 
        /// <summary>
        /// Calculate the step cost of taking action a in state s to reach state s'.
        /// </summary>
        /// <param name="s">the state from which action a is to be performed.</param>
        /// <param name="a">the action to be taken.</param>
        /// <param name="sDelta">the state reached by taking the action.</param>
        /// <returns>the cost of taking action a in state s to reach state s'.</returns>
        double c(IState s, IAction a, IState sDelta);
    }
}
