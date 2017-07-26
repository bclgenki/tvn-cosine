﻿using tvn.cosine.ai.agent.api;
using tvn.cosine.ai.common.collections.api;

namespace tvn.cosine.ai.probability.mdp.api
{ 
    /// <summary>
    /// An interface for MDP action functions.
    /// </summary>
    /// <typeparam name="S">the state type.</typeparam>
    /// <typeparam name="A">the action type.</typeparam>
    public interface IActionsFunction<S, A > where A : IAction
    { 
        /// <summary>
        /// Get the set of actions for state s.
        /// </summary>
        /// <param name="s">the state.</param>
        /// <returns>the set of actions for state s.</returns>
        ISet<A> actions(S s);
    }
}