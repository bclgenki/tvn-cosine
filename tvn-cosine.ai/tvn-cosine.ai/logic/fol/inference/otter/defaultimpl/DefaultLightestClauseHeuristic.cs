﻿using tvn.cosine.ai.common;
using tvn.cosine.ai.common.collections;
using tvn.cosine.ai.logic.fol.kb.data;
using tvn.cosine.ai.util;

namespace tvn.cosine.ai.logic.fol.inference.otter.defaultimpl
{
    public class DefaultLightestClauseHeuristic : LightestClauseHeuristic
    {
        private LightestClauseSorter c;
        private IQueue<Clause> sos;

        public DefaultLightestClauseHeuristic()
        {
            c = new LightestClauseSorter();
            sos = Factory.CreatePriorityQueue<Clause>(c);
        }

        //
        // START-LightestClauseHeuristic
        public Clause getLightestClause()
        {
            Clause lightest = null;

            if (sos.Size() > 0)
            {
                lightest = Util.first(sos);
            }

            return lightest;
        }

        public void initialSOS(ISet<Clause> clauses)
        {
            sos.Clear();
            sos.AddAll(clauses);
        }

        public void addedClauseToSOS(Clause clause)
        {
            sos.Add(clause);
        }

        public void removedClauseFromSOS(Clause clause)
        {
            sos.Remove(clause);
        }
    }

    class LightestClauseSorter : IComparer<Clause>
    {
        public int Compare(Clause c1, Clause c2)
        {
            if (c1 == c2)
            {
                return 0;
            }
            int c1Val = c1.getNumberLiterals();
            int c2Val = c2.getNumberLiterals();
            return (c1Val < c2Val ? -1
                    : (c1Val == c2Val ? (compareEqualityIdentities(c1, c2)) : 1));
        }

        private int compareEqualityIdentities(Clause c1, Clause c2)
        {
            int c1Len = c1.getEqualityIdentity().Length;
            int c2Len = c2.getEqualityIdentity().Length;

            return (c1Len < c2Len ? -1
                : (c1Len == c2Len ? c1.getEqualityIdentity().CompareTo(c2.getEqualityIdentity()) : 1));
        }
    }
}
