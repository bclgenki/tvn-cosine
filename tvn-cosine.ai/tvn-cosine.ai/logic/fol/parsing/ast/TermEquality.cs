﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tvn.cosine.ai.logic.fol.parsing.ast
{
    /**
     * @author Ravi Mohan
     * @author Ciaran O'Reilly
     */
    public class TermEquality : AtomicSentence
    {

        private Term term1, term2;
        private List<Term> terms = new List<Term>();
        private string stringRep = null;
        private int hashCode = 0;

        public static string getEqualitySynbol()
        {
            return "=";
        }

        public TermEquality(Term term1, Term term2)
        {
            this.term1 = term1;
            this.term2 = term2;
            terms.Add(term1);
            terms.Add(term2);
        }

        public Term getTerm1()
        {
            return term1;
        }

        public Term getTerm2()
        {
            return term2;
        }

        //
        // START-AtomicSentence
        public string getSymbolicName()
        {
            return getEqualitySynbol();
        }

        public bool isCompound()
        {
            return true;
        }

        public IList<Term> getArgs()
        {
            return new ReadOnlyCollection<Term>(terms);
        }


        IList<FOLNode> FOLNode.getArgs()
        {
            return new ReadOnlyCollection<FOLNode>(terms.Select(x => x as FOLNode).ToList());
        }

        public object accept(FOLVisitor v, object arg)
        {
            return v.visitTermEquality(this, arg);
        }

        public Sentence copy()
        {
            return new TermEquality(term1.copy(), term2.copy());
        }

        FOLNode FOLNode.copy()
        {
            return copy() as FOLNode;
        }

        // END-AtomicSentence
        //

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
            TermEquality te = (TermEquality)o;

            return te.getTerm1().Equals(term1) && te.getTerm2().Equals(term2);
        }

        public override int GetHashCode()
        {
            if (0 == hashCode)
            {
                hashCode = 17;
                hashCode = 37 * hashCode + getTerm1().GetHashCode();
                hashCode = 37 * hashCode + getTerm2().GetHashCode();
            }
            return hashCode;
        }

        public override string ToString()
        {
            if (null == stringRep)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(term1.ToString());
                sb.Append(" = ");
                sb.Append(term2.ToString());
                stringRep = sb.ToString();
            }
            return stringRep;
        }
    }
}