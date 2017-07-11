﻿using System;
using System.Collections.Generic;
using tvn.cosine.ai.logic.propositional.kb.data;
using tvn.cosine.ai.logic.propositional.parsing;
using tvn.cosine.ai.logic.propositional.parsing.ast;

namespace tvn.cosine.ai.logic.propositional.kb
{
    /**
     * @author Ravi Mohan
     * @author Mike Stampone
     */
    public class KnowledgeBase
    {
        private IList<Sentence> sentences = new List<Sentence>();
        private ConjunctionOfClauses asCNF = new ConjunctionOfClauses(new HashSet<Clause>());
        private ISet<PropositionSymbol> symbols = new HashSet<PropositionSymbol>();
        private PLParser parser = new PLParser();

        public KnowledgeBase()
        {
        }

        /**
         * Adds the specified sentence to the knowledge base.
         * 
         * @param aSentence
         *            a fact to be added to the knowledge base.
         */
        public void tell(string aSentence)
        {
            tell((Sentence)parser.parse(aSentence));

        }

        /**
         * Adds the specified sentence to the knowledge base.
         * 
         * @param aSentence
         *            a fact to be added to the knowledge base.
         */
        public void tell(Sentence aSentence)
        {
            if (!(sentences.contains(aSentence)))
            {
                sentences.add(aSentence);
                asCNF = asCNF.extend(ConvertToConjunctionOfClauses.convert(aSentence).getClauses());
                symbols.addAll(SymbolCollector.getSymbolsFrom(aSentence));
            }
        }

        /**
         * Each time the agent program is called, it TELLS the knowledge base what
         * it perceives.
         * 
         * @param percepts
         *            what the agent perceives
         */
        public void tellAll(String[] percepts)
        {
            for (int i = 0; i < percepts.length; i++)
            {
                tell(percepts[i]);
            }

        }

        /**
         * Returns the number of sentences in the knowledge base.
         * 
         * @return the number of sentences in the knowledge base.
         */
        public int size()
        {
            return sentences.size();
        }

        /**
         * Returns the list of sentences in the knowledge base chained together as a
         * single sentence.
         * 
         * @return the list of sentences in the knowledge base chained together as a
         *         single sentence.
         */
        public Sentence asSentence()
        {
            return Sentence.newConjunction(sentences);
        }

        /**
         * 
         * @return a Conjunctive Normal Form (CNF) representation of the Knowledge Base.
         */
        public Set<Clause> asCNF()
        {
            return asCNF.getClauses();
        }

        /**
         * 
         * @return a unique set of the symbols currently contained in the Knowledge Base.
         */
        public Set<PropositionSymbol> getSymbols()
        {
            return symbols;
        }

        /**
         * Returns the answer to the specified question using the TT-Entails
         * algorithm.
         * 
         * @param queryString
         *            a question to ASK the knowledge base
         * 
         * @return the answer to the specified question using the TT-Entails
         *         algorithm.
         */
        public boolean askWithTTEntails(String queryString)
        {
            PLParser parser = new PLParser();

            Sentence alpha = parser.parse(queryString);

            return new TTEntails().ttEntails(this, alpha);
        }

        @Override
        public String toString()
        {
            if (sentences.size() == 0)
            {
                return "";
            }
            else
            {
                return asSentence().toString();
            }
        }

        /**
         * Returns the list of sentences in the knowledge base.
         * 
         * @return the list of sentences in the knowledge base.
         */
        public List<Sentence> getSentences()
        {
            return sentences;
        }
    }
}
