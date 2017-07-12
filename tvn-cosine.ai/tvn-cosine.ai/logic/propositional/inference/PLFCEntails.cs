﻿using System;
using System.Collections.Generic;
using System.Linq;
using tvn.cosine.ai.logic.propositional.kb;
using tvn.cosine.ai.logic.propositional.kb.data;
using tvn.cosine.ai.logic.propositional.parsing.ast;
using tvn.cosine.ai.logic.propositional.visitors;
using tvn.cosine.ai.search.framework.qsearch;

namespace tvn.cosine.ai.logic.propositional.inference
{
    /**
     * Artificial Intelligence A Modern Approach (3rd Edition): page 258. 
     *  
     * 
     * <pre>
     * <code>
     * function PL-FC-ENTAILS?(KB, q) returns true or false
     *   inputs: KB, the knowledge base, a set of propositional definite clauses
     *           q, the query, a proposition symbol
     *   count &larr; a table, where count[c] is the number of symbols in c's premise
     *   inferred &larr; a table, where inferred[s] is initially false for all symbols
     *   agenda &larr; a queue of symbols, initially symbols known to be true in KB
     *   
     *   while agenda is not empty do
     *     p &larr; Pop(agenda)
     *     if p = q then return true
     *     if inferred[p] = false then
     *        inferred[p] &larr; true
     *        for each clause c in KB where p is in c.PREMISE do
     *            decrement count[c]
     *            if count[c] = 0 then add c.CONCLUSION to agenda
     *   return false
     * </code>
     * </pre>
     * 
     * Figure 7.15 the forward-chaining algorithm for propositional logic. The
     * <i>agenda</i> keeps track of symbols known to be true but not yet
     * "processed". The <i>count</i> table keeps track of how many premises of each
     * implication are as yet unknown. Whenever a new symbol p from the agenda is
     * processed, the count is reduced by one for each implication in whose premise
     * p appears (easily identified in constant time with appropriate indexing.) If
     * a count reaches zero, all the premises of the implication are known, so its
     * conclusion can be added to the agenda. Finally, we need to keep track of
     * which symbols have been processed; a symbol that is already in the set of
     * inferred symbols need not be added to the agenda again. This avoids redundant
     * work and prevents loops caused by implications such as P &rArr; Q and Q
     * &rArr; P.
     * 
     * @author Ciaran O'Reilly
     * @author Ravi Mohan
     * @author Mike Stampone
     */
    public class PLFCEntails
    {

        /**
         * PL-FC-ENTAILS?(KB, q) 
         * The forward-chaining algorithm for propositional logic.
         * 
         * @param kb
         *            the knowledge base, a set of propositional definite clauses.
         * @param q
         *            q, the query, a proposition symbol
         * @return true if KB |= q, false otherwise.
         * @throws IllegalArgumentException
         *             if KB contains any non-definite clauses.
         */
        public bool plfcEntails(KnowledgeBase kb, PropositionSymbol q)
        {
            // count <- a table, where count[c] is the number of symbols in c's
            // premise
            IDictionary<Clause, int> count = initializeCount(kb);
            // inferred <- a table, where inferred[s] is initially false for all
            // symbols
            IDictionary<PropositionSymbol, bool> inferred = initializeInferred(kb);
            // agenda <- a queue of symbols, initially symbols known to be true in
            // KB
            IQueue<PropositionSymbol> agenda = initializeAgenda(count);
            // Note: an index for p to the clauses where p appears in the premise
            IDictionary<PropositionSymbol, ISet<Clause>> pToClausesWithPInPremise = initializeIndex(count, inferred);

            // while agenda is not empty do
            while (!(agenda.Count == 0))
            {
                // p <- Pop(agenda)
                PropositionSymbol p = agenda.remove();
                // if p = q then return true
                if (p.Equals(q))
                {
                    return true;
                }
                // if inferred[p] = false then
                if (inferred[p].Equals(false))
                {
                    // inferred[p] <- true
                    inferred.Add(p, true);
                    // for each clause c in KB where p is in c.PREMISE do
                    foreach (Clause c in pToClausesWithPInPremise[p])
                    {
                        // decrement count[c]
                        decrement(count, c);
                        // if count[c] = 0 then add c.CONCLUSION to agenda
                        if (count[c] == 0)
                        {
                            agenda.add(conclusion(c));
                        }
                    }
                }
            }

            // return false
            return false;
        }

        //
        // SUPPORTING CODE
        //

        //
        // PROTECTED
        //
        protected IDictionary<Clause, int> initializeCount(KnowledgeBase kb)
        {
            // count <- a table, where count[c] is the number of symbols in c's
            // premise
            IDictionary<Clause, int> count = new Dictionary<Clause, int>();

            ISet<Clause> clauses = ConvertToConjunctionOfClauses.convert(kb.asSentence()).getClauses();
            foreach (Clause c in clauses)
            {
                if (!c.isDefiniteClause())
                {
                    throw new ArgumentException("Knowledge Base contains non-definite clauses:" + c);
                }
                // Note: # of negative literals is equivalent to the number of
                // symbols in c's premise
                count.Add(c, c.getNumberNegativeLiterals());
            }

            return count;
        }

        protected IDictionary<PropositionSymbol, bool> initializeInferred(KnowledgeBase kb)
        {
            // inferred <- a table, where inferred[s] is initially false for all
            // symbols
            IDictionary<PropositionSymbol, bool> inferred = new Dictionary<PropositionSymbol, bool>();
            foreach (PropositionSymbol p in SymbolCollector.getSymbolsFrom(kb.asSentence()))
            {
                inferred.Add(p, false);
            }
            return inferred;
        }

        // Note: at the point of calling this routine, count will contain all the
        // clauses in KB.
        protected IQueue<PropositionSymbol> initializeAgenda(IDictionary<Clause, int> count)
        {
            // agenda <- a queue of symbols, initially symbols known to be true in
            // KB
            IQueue<PropositionSymbol> agenda = new FifoQueue<PropositionSymbol>();
            foreach (Clause c in count.Keys)
            {
                // No premise just a conclusion, then we know its true
                if (c.getNumberNegativeLiterals() == 0)
                {
                    agenda.add(conclusion(c));
                }
            }
            return agenda;
        }

        // Note: at the point of calling this routine, count will contain all the
        // clauses in KB while inferred will contain all the proposition symbols.
        protected IDictionary<PropositionSymbol, ISet<Clause>> initializeIndex(
                IDictionary<Clause, int> count, IDictionary<PropositionSymbol, bool> inferred)
        {
            IDictionary<PropositionSymbol, ISet<Clause>> pToClausesWithPInPremise = new Dictionary<PropositionSymbol, ISet<Clause>>();
            foreach (PropositionSymbol p in inferred.Keys)
            {
                ISet<Clause> clausesWithPInPremise = new HashSet<Clause>();
                foreach (Clause c in count.Keys)
                {
                    // Note: The negative symbols comprise the premise
                    if (c.getNegativeSymbols().Contains(p))
                    {
                        clausesWithPInPremise.Add(c);
                    }
                }
                pToClausesWithPInPremise.Add(p, clausesWithPInPremise);
            }
            return pToClausesWithPInPremise;
        }

        protected void decrement(IDictionary<Clause, int> count, Clause c)
        {
            int currentCount = count[c];
            // Note: a definite clause can just be a fact (i.e. 1 positive literal)
            // However, we only decrement those where the symbol is in the premise
            // so we don't need to worry about going < 0.
            count.Add(c, currentCount - 1);
        }

        protected PropositionSymbol conclusion(Clause c)
        {
            // Note: the conclusion is from the single positive
            // literal in the definite clause (which we are
            // restricted to).
            return c.getPositiveSymbols().First();
        }
    }
}
