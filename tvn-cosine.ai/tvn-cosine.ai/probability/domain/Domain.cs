﻿namespace tvn.cosine.ai.probability.domain
{
    /**
     * Artificial Intelligence A Modern Approach (3rd Edition): page 486.<br>
     * <br>
     * Every random variable has a <b>domain</b> - the set of possible values it can
     * take on. The domain of <i>Total</i> for two dice is the set {2,...,12} and
     * the domain of Die<sub>1</sub> is {1,...,6}. A bool random variable has the
     * domain {true, false}.
     * 
     * @author Ciaran O'Reilly
     */
    public interface Domain
    {

        /**
         * 
         * @return true if the Domain is finite, false otherwise (i.e. discrete
         *         (like the integers) or continuous (like the reals)).
         */
        bool isFinite();

        /**
         * 
         * @return !isFinite().
         */
        bool isInfinite();

        /**
         * 
         * @return the size of the Domain, only applicable if isFinite() == true.
         */
        int size();

        /**
         * 
         * @return true if the domain is ordered, false otherwise. i.e. you can
         *         specify 1 object from the domain is < or = another object in the
         *         domain.
         */
        bool isOrdered();
    }

}
