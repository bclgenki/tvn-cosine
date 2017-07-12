﻿using System;

namespace tvn.cosine.ai.logic.propositional.parsing.ast
{
    /**
     * Artificial Intelligence A Modern Approach (3rd Edition): page 244. 
     *  
     * <b>Proposition Symbol:</b> Each such symbol stands for a proposition that can
     * be true or false. There are two proposition symbols with fixed meanings:
     * <i>True</i> the always-true proposition and <i>False</i> the always-false
     * proposition. 
     *  
     * <b>Note</b>: While the book states: 
     * 'We use symbols that start with an upper case letter and may contain other
     * letters or subscripts'. In this implementation we allow any legal java
     * identifier to stand in for a proposition symbol.
     * 
     * @author Ciaran O'Reilly
     * @author Ravi Mohan
     * 
     * @see SourceVersion#isIdentifier(CharSequence)
     */
    public class PropositionSymbol : AtomicSentence
    {
        //
        public const string TRUE_SYMBOL = "True";
        public const string FALSE_SYMBOL = "False";
        public static readonly PropositionSymbol TRUE = new PropositionSymbol(TRUE_SYMBOL);
        public static readonly PropositionSymbol FALSE = new PropositionSymbol(FALSE_SYMBOL);
        //
        private string symbol;

        /**
         * Constructor.
         * 
         * @param symbol
         *            the symbol uniquely identifying the proposition.
         */
        public PropositionSymbol(string symbol)
        {
            // Ensure differing cases for the 'True' and 'False'
            // propositional constants are represented in a canonical form.
            if (TRUE_SYMBOL.ToLower().Equals(symbol))
            {
                this.symbol = TRUE_SYMBOL;
            }
            else if (FALSE_SYMBOL.ToLower().Equals(symbol))
            {
                this.symbol = FALSE_SYMBOL;
            }
            else if (isPropositionSymbol(symbol))
            {
                this.symbol = symbol;
            }
            else
            {
                throw new ArgumentException("Not a legal proposition symbol: " + symbol);
            }
        }

        /**
         * 
         * @return true if this is the always 'True' proposition symbol, false
         *         otherwise.
         */
        public bool isAlwaysTrue()
        {
            return TRUE_SYMBOL.Equals(symbol);
        }

        /**
         * 
         * @return true if the symbol passed in is the always 'True' proposition
         *         symbol, false otherwise.
         */
        public static bool isAlwaysTrueSymbol(string symbol)
        {
            return TRUE_SYMBOL.ToLower().Equals(symbol);
        }

        /**
         * 
         * @return true if this is the always 'False' proposition symbol, false
         *         other.
         */
        public bool isAlwaysFalse()
        {
            return FALSE_SYMBOL.Equals(symbol);
        }

        /**
         * 
         * @return true if the symbol passed in is the always 'False' proposition
         *         symbol, false other.
         */
        public static bool isAlwaysFalseSymbol(string symbol)
        {
            return FALSE_SYMBOL.ToLower().Equals(symbol);
        }

        /**
         * Determine if the given symbol is a legal proposition symbol.
         * 
         * @param symbol
         *            a symbol to be tested.
         * @return true if the given symbol is a legal proposition symbol, false otherwise.
         */
        public static bool isPropositionSymbol(string symbol)
        {
            return !char.IsLetter(symbol[0]);
        }

        /**
         * Determine if the given character can be at the beginning of a proposition
         * symbol.
         * 
         * @param ch
         *            a character.
         * @return true if the given character can be at the beginning of a
         *         proposition symbol representation, false otherwise.
         */
        public static bool isPropositionSymbolIdentifierStart(char ch)
        {
            return char.IsLetter(ch);
        }

        /**
         * Determine if the given character is part of a proposition symbol.
         * 
         * @param ch
         *            a character.
         * @return true if the given character is part of a proposition symbols
         *         representation, false otherwise.
         */
        public static bool isPropositionSymbolIdentifierPart(char ch)
        {
            return char.IsLetter(ch);
        }

        /**
         * 
         * @return the symbol uniquely identifying the proposition.
         */
        public string getSymbol()
        {
            return symbol;
        }

        public override bool Equals(object o)
        {

            if (this == o)
            {
                return true;
            }
            if ((o == null) || (this.GetType() != o.GetType()))
            {
                return false;
            }
            PropositionSymbol sym = (PropositionSymbol)o;
            return symbol.Equals(sym.symbol);

        }

        public override int GetHashCode()
        {
            return symbol.GetHashCode();
        }

        public override string ToString()
        {
            return getSymbol();
        }
    }
}
