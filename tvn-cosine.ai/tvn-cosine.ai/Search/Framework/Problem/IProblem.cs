﻿namespace tvn.cosine.ai.search.framework.problem
{
    /**
     * Artificial Intelligence A Modern Approach (3rd Edition): page 66. 
     *  
     * A problem can be defined formally by five components:  
     * <ul>
     * <li>The <b>initial state</b> that the agent starts in.</li>
     * <li>A description of the possible <b>actions</b> available to the agent.
     * Given a particular state s, ACTIONS(s) returns the set of actions that can be
     * executed in s.</li>
     * <li>A description of what each action does; the formal name for this is the
     * <b>transition model, specified by a function RESULT(s, a) that returns the
     * state that results from doing action a in state s.</b></li>
     * <li>The <b>goal test</b>, which determines whether a given state is a goal
     * state.</li>
     * <li>A <b>path cost</b> function that assigns a numeric cost to each path. The
     * problem-solving agent chooses a cost function that reflects its own
     * performance measure. The <b>step cost</b> of taking action a in state s to
     * reach state s' is denoted by c(s,a,s')</li>
     * </ul>
     *
     * This implementation provides an additional solution test. It can be used to
     * compute more than one solution or to formulate acceptance criteria for the
     * sequence of actions.
     *
     * @param <S> The type used to represent states
     * @param <A> The type of the actions to be used to navigate through the state space
     *
     * @author Ruediger Lunde
     * @author Mike Stampone
     */
    public interface IProblem<S, A> : IOnlineSearchProblem<S, A>
    {
        /**
         * Returns the description of what each action does.
         */
        S getResult(S state, A action);

        /**
         * Tests whether a node represents an acceptable solution. The default implementation
         * delegates the check to the goal test. Other implementations could make use of the additional
         * information given by the node (e.g. the sequence of actions leading to the node). A
         * solution tester implementation could for example always return false and internally collect
         * the paths of all nodes whose state passes the goal test. Search implementations should always
         * access the goal test via this method to support solution acceptance testing.
         */
        bool testSolution(Node<S, A> node);
    }

}
