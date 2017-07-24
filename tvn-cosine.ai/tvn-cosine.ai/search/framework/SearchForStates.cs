﻿using tvn.cosine.ai.common;
using tvn.cosine.ai.common.api;
using tvn.cosine.ai.search.framework.problem;

namespace tvn.cosine.ai.search.framework
{
    /**
     * Interface for all AIMA3e search algorithms which forget the exploration history and
     * return just a single state which is hopefully a goal state. This search framework expects
     * all search algorithms to provide some metrics and to actually explore the search space
     * by expanding nodes.
     *
     * @param <S> The type used to represent states
     * @param <A> The type of the actions to be used to navigate through the state space
     *
     * @author Ruediger Lunde
     */
    public interface SearchForStates<S, A>
    {
        /**
         * Returns a state which is might be but not necessary is a goal state of
         * the problem or empty.
         * 
         * @param p
         *            the search problem
         * 
         * @return a state or empty.
         */
        S findState(Problem<S, A> p);

        /**
         * Returns all the metrics of the search.
         */
        Metrics getMetrics();

        /**
         * Adds a listener to the list of node listeners. It is informed whenever a
         * node is expanded during search.
         */
        void addNodeListener(Consumer<Node<S, A>> listener);

        /**
         * Removes a listener from the list of node listeners.
         */
        bool removeNodeListener(Consumer<Node<S, A>> listener);
    } 
}
