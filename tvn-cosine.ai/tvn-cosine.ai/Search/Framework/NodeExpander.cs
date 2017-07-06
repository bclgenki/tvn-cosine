﻿using System;
using System.Collections.Generic;
using tvn.cosine.ai.search.framework.problem;

namespace tvn.cosine.ai.search.framework
{
    /**
     * Instances of this class are responsible for node creation and expansion. They
     * compute path costs, support progress tracking, and count the number of
     * {@link #expand(Node, Problem)} calls.
     *
     * @param <S> The type used to represent states
     * @param <A> The type of the actions to be used to navigate through the state space
     *
     * @author Ruediger Lunde
     */
    public class NodeExpander<S, A>
    {
        protected bool useParentLinks = true;

        /**
         * Modifies {@link #useParentLinks} and returns this node expander. When
         * using local search to search for states, parent links are not needed and
         * lead to unnecessary memory consumption.
         */
        public NodeExpander<S, A> UseParentLinks(bool s)
        {
            useParentLinks = s;
            return this;
        }

        ///////////////////////////////////////////////////////////////////////
        // expanding nodes

        /**
         * Factory method, which creates a root node for the specified state.
         */
        public Node<S, A> createRootNode(S state)
        {
            return new Node<S, A>(state);
        }

        /**
         * Computes the path cost for getting from the root node state via the
         * parent node state to the specified state, creates a new node for the
         * specified state, adds it as child of the provided parent (if
         * {@link #useParentLinks} is true), and returns it.
         */
        public Node<S, A> createNode(S state, Node<S, A> parent, A action, double stepCost)
        {
            Node<S, A> p = useParentLinks ? parent : null;
            return new Node<S, A>(state, p, action, parent.getPathCost() + stepCost);
        }

        /**
         * Returns the children obtained from expanding the specified node in the
         * specified problem.
         * 
         * @param node
         *            the node to expand
         * @param problem
         *            the problem the specified node is within.
         * 
         * @return the children obtained from expanding the specified node in the
         *         specified problem.
         */
        public List<Node<S, A>> expand(Node<S, A> node, IProblem<S, A> problem)
        {
            List<Node<S, A>> successors = new List<Node<S, A>>();

            foreach (A action in problem.getActions(node.getState()))
            {
                S successorState = problem.getResult(node.getState(), action);

                double stepCost = problem.getStepCosts(node.getState(), action, successorState);
                successors.Add(createNode(successorState, node, action, stepCost));
            }
            notifyNodeListeners(node);
            return successors;
        }

        ///////////////////////////////////////////////////////////////////////
        // progress tracking

        /**
         * All node listeners added to this list get informed whenever a node is
         * expanded.
         */
        private List<Action<Node<S, A>>> nodeListeners = new List<Action<Node<S, A>>>();

        /**
         * Adds a listener to the list of node listeners. It is informed whenever a
         * node is expanded during search.
         */
        public void addNodeListener(Action<Node<S, A>> listener)
        {
            nodeListeners.Add(listener);
        }

        /**
         * Removes a listener from the list of node listeners.
         */
        public bool removeNodeListener(Action<Node<S, A>> listener)
        {
            return nodeListeners.Remove(listener);
        }

        protected void notifyNodeListeners(Node<S, A> node)
        {
            foreach (Action<Node<S, A>> listener in nodeListeners)
                listener(node);
        }
    }
}