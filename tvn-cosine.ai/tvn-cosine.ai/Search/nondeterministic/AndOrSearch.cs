﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tvn.cosine.ai.search.nondeterministic
{
    /**
     * Artificial Intelligence A Modern Approach (3rd Edition): Figure 4.11, page
     * 136.<br>
     * <br>
     * 
     * <pre>
     * <code>
     * function AND-OR-GRAPH-SEARCH(problem) returns a conditional plan, or failure
     *   OR-SEARCH(problem.INITIAL-STATE, problem, [])
     * 
     * ---------------------------------------------------------------------------------
     * 
     * function OR-SEARCH(state, problem, path) returns a conditional plan, or failure
     *   if problem.GOAL-TEST(state) then return the empty plan
     *   if state is on path then return failure
     *   for each action in problem.ACTIONS(state) do
     *       plan <- AND-SEARCH(RESULTS(state, action), problem, [state | path])
     *       if plan != failure then return [action | plan]
     *   return failure
     * 
     * ---------------------------------------------------------------------------------
     * 
     * function AND-SEARCH(states, problem, path) returns a conditional plan, or failure
     *   for each s<sub>i</sub> in states do
     *      plan<sub>i</sub> <- OR-SEARCH(s<sub>i</sub>, problem, path)
     *      if plan<sub>i</sub> = failure then return failure
     *   return [if s<sub>1</sub> then plan<sub>1</sub> else if s<sub>2</sub> then plan<sub>2</sub> else ... if s<sub>n-1</sub> then plan<sub>n-1</sub> else plan<sub>n</sub>]
     * </code>
     * </pre>
     * 
     * Figure 4.11 An algorithm for searching AND-OR graphs generated by
     * nondeterministic environments. It returns a conditional plan that reaches a
     * goal state in all circumstances. (The notation [x | l] refers to the list
     * formed by adding object x to the front of the list l.)<br>
     * <br>
     * Note: Unfortunately, this class cannot implement the interface SearchForActions
     * (core.search.framework.SearchForActions) because SearchForActions.search() returns a list of
     * Actions to perform, whereas a nondeterministic search must return a Plan.
     * 
     * @author Andrew Brown
     * @author Ruediger Lunde
     */
    public class AndOrSearch<S, A>
    {
        protected int expandedNodes;

        /**
         * Searches through state space and returns a conditional plan for the given
         * problem. The conditional plan is a list of either an action or an if-then
         * construct (consisting of a list of states and consequent actions). The
         * final product, when printed, resembles the contingency plan on page 134.
         * 
         * This function is equivalent to the following on page 136:
         * 
         * <pre>
         * <code>
         * function AND-OR-GRAPH-SEARCH(problem) returns a conditional plan, or failure
         *   OR-SEARCH(problem.INITIAL-STATE, problem, [])
         * </code>
         * </pre>
         *
         * @return a conditional plan or null on failure
         */
        public Plan search(NondeterministicProblem<S, A> problem)
        {
            expandedNodes = 0;
            // OR-SEARCH(problem.INITIAL-STATE, problem, [])
            return orSearch(problem.getInitialState(), problem, new Path());
        }

        /**
         * Returns a conditional plan or null on failure; this function is
         * equivalent to the following on page 136:
         * 
         * <pre>
         * <code>
         * function OR-SEARCH(state, problem, path) returns a conditional plan, or failure
         *   if problem.GOAL-TEST(state) then return the empty plan
         *   if state is on path then return failure
         *   for each action in problem.ACTIONS(state) do
         *       plan <- AND-SEARCH(RESULTS(state, action), problem, [state | path])
         *       if plan != failure then return [action | plan]
         *   return failure
         * </code>
         * </pre>
         * 
         * @param state
         * @param problem
         * @param path
         * @return a conditional plan or null on failure
         */
        public Plan orSearch(S state, NondeterministicProblem<S, A> problem,
                Path path)
        {
            // do metrics
            this.expandedNodes++;
            // if problem.GOAL-TEST(state) then return the empty plan
            if (problem.testGoal(state))
            {
                return new Plan();
            }
            // if state is on path then return failure
            if (path.Contains(state))
            {
                return null;
            }
            // for each action in problem.ACTIONS(state) do
            foreach (A action in problem.getActions(state))
            {
                // plan <- AND-SEARCH(RESULTS(state, action), problem, [state|path])
                Plan plan = andSearch(
                        problem.getResults(state, action),
                        problem, path.prepend(state));
                // if plan != failure then return [action|plan]
                if (plan != null)
                {
                    return plan.prepend(action);
                }
            }
            // return failure
            return null;
        }

        /**
         * Returns a conditional plan or null on failure; this function is
         * equivalent to the following on page 136:
         * 
         * <pre>
         * <code>
         * function AND-SEARCH(states, problem, path) returns a conditional plan, or failure
         *   for each s<sub>i</sub> in states do
         *      plan<sub>i</sub> <- OR-SEARCH(s<sub>i</sub>, problem, path)
         *      if plan<sub>i</sub> = failure then return failure
         *   return [if s<sub>1</sub> then plan<sub>1</sub> else if s<sub>2</sub> then plan<sub>2</sub> else ... if s<sub>n-1</sub> then plan<sub>n-1</sub> else plan<sub>n</sub>]
         * </code>
         * </pre>
         * 
         * @param states
         * @param problem
         * @param path
         * @return a conditional plan or null on failure
         */
        public Plan andSearch(List<S> states, NondeterministicProblem<S, A> problem, Path path)
        {
            // do metrics, setup
            this.expandedNodes++;
            S[] _states = states.ToArray();
            Plan[] plans = new Plan[_states.Length];
            // for each s_i in states do
            for (int i = 0; i < _states.Length; i++)
            {
                // plan_i <- OR-SEARCH(s_i, problem, path)
                plans[i] = orSearch(_states[i], problem, path);
                // if plan_i = failure then return failure
                if (plans[i] == null)
                {
                    return null;
                }
            }
            // return [if s_1 then plan_1 else ... if s_n-1 then plan_n-1 else
            // plan_n]
            object[] steps = new object[plans.Length];
            if (plans.Length > 0)
            {
                for (int i = 0; i < plans.Length - 1; i++)
                {
                    steps[i] = new IfStateThenPlan<S>(_states[i], plans[i]);
                }
                steps[steps.Length - 1] = plans[plans.Length - 1];
            }

            return new Plan(steps);
        }

        /**
         * Returns all the metrics of the node expander.
         * 
         * @return all the metrics of the node expander.
         */
        public IDictionary<string, double> getMetrics()
        {
            IDictionary<string, double> result = new Dictionary<string, double>();
            result["expandedNodes"] = expandedNodes;
            return result;
        }
    }

}
