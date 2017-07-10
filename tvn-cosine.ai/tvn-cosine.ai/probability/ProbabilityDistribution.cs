﻿using System.Collections.Generic;
using tvn.cosine.ai.probability.proposition;

namespace tvn.cosine.ai.probability
{
    /**
     * Artificial Intelligence A Modern Approach (3rd Edition): page 487.<br>
     * <br>
     * A probability distribution is a function that assigns probabilities to events
     * (sets of possible worlds).<br>
     * <br>
     * <b>Note:</b> This definition is slightly different than that given in AIMA3e
     * pg. 487, which in this API corresponds to a CategoricalDistribution.
     * 
     * @see <a href="http://en.wikipedia.org/wiki/Probability_distribution"
     *      >Probability Distribution</a>
     * 
     * @author Ciaran O'Reilly
     */
    public interface ProbabilityDistribution<T>
    {
        /**
         * @return a consistent ordered Set (e.g. LinkedHashSet) of the random
         *         variables this probability distribution is for.
         */
        ISet<RandomVariable> getFor();

        /**
         * 
         * @param rv
         *            the Random Variable to be checked.
         * @return true if this Distribution is for the passed in Random Variable,
         *         false otherwise.
         */
        bool contains(RandomVariable rv);

        /**
         * Get the value for the provided set of values for the random variables
         * comprising the Distribution (ordering and size of each must equal and
         * their domains must match).
         * 
         * @param eventValues
         *            the values for the random variables comprising the
         *            Distribution
         * @return the value for the possible worlds associated with the assignments
         *         for the random variables comprising the Distribution.
         */
        double getValue(params T[] eventValues);

        /**
         * Get the value for the provided set of AssignmentPropositions for the
         * random variables comprising the Distribution (size of each must equal and
         * their random variables must match).
         * 
         * @param eventValues
         *            the assignment propositions for the random variables
         *            comprising the Distribution
         * @return the value for the possible worlds associated with the assignments
         *         for the random variables comprising the Distribution.
         */
        double getValue(params AssignmentProposition<T>[] eventValues);
    }

}