﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tvn.cosine.ai.logic.propositional.kb;
using tvn.cosine.ai.logic.propositional.kb.data;
using tvn.cosine.ai.logic.propositional.parsing.ast;
using tvn.cosine.ai.logic.propositional.visitors;

namespace tvn.cosine.ai.logic.propositional.inference
{
    /**
     * Artificial Intelligence A Modern Approach (3rd Edition): Figure 7.10, page
     * 248.<br>
     * <br>
     * 
     * <pre>
     * function TT-ENTAILS?(KB, &alpha;) returns true or false
     *   inputs: KB, the knowledge base, a sentence in propositional logic
     *           &alpha;, the query, a sentence in propositional logic
     *           
     *   symbols <- a list of proposition symbols in KB and &alpha
     *   return TT-CHECK-ALL(KB, &alpha; symbols, {})
     *   
     * --------------------------------------------------------------------------------
     * 
     * function TT-CHECK-ALL(KB, &alpha; symbols, model) returns true or false
     *   if EMPTY?(symbols) then
     *     if PL-TRUE?(KB, model) then return PL-TRUE?(&alpha;, model)
     *     else return true // when KB is false, always return true
     *   else do
     *     P <- FIRST(symbols)
     *     rest <- REST(symbols)
     *     return (TT-CHECK-ALL(KB, &alpha;, rest, model &cup; { P = true })
     *            and
     *            TT-CHECK-ALL(KB, &alpha;, rest, model &cup; { P = false }))
     * </pre>
     * 
     * Figure 7.10 A truth-table enumeration algorithm for deciding propositional
     * entailment. (TT stands for truth table.) PL-TRUE? returns true if a sentence
     * holds within a model. The variable model represents a partional model - an
     * assignment to some of the symbols. The keyword <b>"and"</b> is used here as a
     * logical operation on its two arguments, returning true or false.
     *
     * @author Ciaran O'Reilly
     * @author Ravi Mohan
     * @author Mike Stampone
     */
    public class TTEntails
    {

        /**
         * function TT-ENTAILS?(KB, &alpha;) returns true or false.
         * 
         * @param kb
         *            KB, the knowledge base, a sentence in propositional logic
         * @param alpha
         *            &alpha;, the query, a sentence in propositional logic
         * 
         * @return true if KB entails &alpha;, false otherwise.
         */
        public bool ttEntails(KnowledgeBase kb, Sentence alpha)
        {
            // symbols <- a list of proposition symbols in KB and &alpha
            List<PropositionSymbol> symbols = new List<PropositionSymbol>(
                    SymbolCollector.getSymbolsFrom(kb.asSentence(), alpha));

            // return TT-CHECK-ALL(KB, &alpha; symbols, {})
            return ttCheckAll(kb, alpha, symbols, new Model());
        }

        //
        /**
         * function TT-CHECK-ALL(KB, &alpha; symbols, model) returns true or false
         * 
         * @param kb
         *            KB, the knowledge base, a sentence in propositional logic
         * @param alpha
         *            &alpha;, the query, a sentence in propositional logic
         * @param symbols
         *            a list of currently unassigned propositional symbols in the
         *            model.
         * @param model
         *            a partially or fully assigned model for the given KB and
         *            query.
         * @return true if KB entails &alpha;, false otherwise.
         */
        public bool ttCheckAll(KnowledgeBase kb, Sentence alpha,
                List<PropositionSymbol> symbols, Model model)
        {
            // if EMPTY?(symbols) then
            if (symbols.Count == 0)
            {
                // if PL-TRUE?(KB, model) then return PL-TRUE?(&alpha;, model)
                if (model.isTrue(kb.asSentence()))
                {
                    return model.isTrue(alpha);
                }
                else
                {
                    // else return true // when KB is false, always return true
                    return true;
                }
            }

            // else do
            // P <- FIRST(symbols)
            PropositionSymbol p = symbols.First();
            // rest <- REST(symbols)
            List<PropositionSymbol> rest = symbols.Skip(1).ToList();
            // return (TT-CHECK-ALL(KB, &alpha;, rest, model &cup; { P = true })
            // and
            // TT-CHECK-ALL(KB, &alpha;, rest, model U { P = false }))
            return ttCheckAll(kb, alpha, rest, model.union(p, true))
                    && ttCheckAll(kb, alpha, rest, model.union(p, false));
        }
    }
}