﻿using tvn.cosine.ai.common.collections;

namespace tvn.cosine.ai.probability.domain
{
    /**
     * A Domain over a countable/discrete and finite set of objects.
     * 
     * @author Ciaran O'Reilly
     * 
     */
    public interface FiniteDomain : DiscreteDomain
    {
        /**
         * 
         * @return a consistent ordered Set (e.g. LinkedHashSet) of the possible
         *         values this domain can take on.
         */
        ISet<T> getPossibleValues<T>();

        /**
         * The possible values for a finite domain are to have a consistent ordering
         * (whether they are actually ordered by value or not). This will return an
         * offset into that ordering.
         * 
         * @param value
         *            a value from the domain.
         * @return an offset (starting from 0) into the consistent order of the set
         *         of possible values.
         * @exception IllegalArgumentException
         *                if the value is not valid for the domain.
         */
        int getOffset(object value);

        /**
         * 
         * @param offset
         *            an offset into the consistent ordering for this domain.
         * @return the object at the specified offset in this domains consistent
         *         ordered set of values. null if the offset does not index the
         *         domain correctly.
         */
        object getValueAt(int offset);
    }

}
