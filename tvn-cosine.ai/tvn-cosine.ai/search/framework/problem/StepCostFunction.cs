﻿namespace tvn.cosine.ai.search.framework.problem
{
    /**
     * Artificial Intelligence A Modern Approach (3rd Edition): page 68.<br>
     * <br>
     * The <b>step cost</b> of taking action a in state s to reach state s' is
     * denoted by c(s, a, s').
     *
     * @param <S> The type used to represent states
     * @param <A> The type of the actions to be used to navigate through the state space
     *
     * @author Ruediger Lunde
     */
    public delegate double StepCostFunction<S, A>(S s, A a, S sDelta); 
}