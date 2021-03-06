﻿using tvn.cosine;
using tvn.cosine.api;
using tvn.cosine.collections;
using tvn.cosine.collections.api;
using tvn.cosine.text;
using tvn.cosine.text.api;

namespace tvn.cosine.ai.logic.fol.parsing.ast
{
    public class Function : Term
    { 
        private string functionName;
        private ICollection<Term> terms = CollectionFactory.CreateQueue<Term>();
        private string stringRep = null;
        private int hashCode = 0;

        public Function(string functionName, ICollection<Term> terms)
        {
            this.functionName = functionName;
            this.terms.AddAll(terms);
        }

        public string getFunctionName()
        {
            return functionName;
        }

        public ICollection<Term> getTerms()
        {
            return CollectionFactory.CreateReadOnlyQueue<Term>(terms);
        }
         
        public string getSymbolicName()
        {
            return getFunctionName();
        }

        public bool isCompound()
        {
            return true;
        }

        ICollection<FOLNode> FOLNode.getArgs()
        {
            ICollection<FOLNode> obj = CollectionFactory.CreateQueue<FOLNode>();
            foreach (Term term in getTerms())
            {
                obj.Add(term);
            }

            return CollectionFactory.CreateReadOnlyQueue<FOLNode>(obj);
        }

        public ICollection<Term> getArgs()
        {
            return getTerms();
        }

        public object accept(FOLVisitor v, object arg)
        {
            return v.visitFunction(this, arg);
        }

        FOLNode FOLNode.copy()
        {
            return copy();
        }

        Term Term.copy()
        {
            return copy();
        }

        public Function copy()
        {
            ICollection<Term> copyTerms = CollectionFactory.CreateQueue<Term>();
            foreach (Term t in terms)
            {
                copyTerms.Add(t.copy());
            }
            return new Function(functionName, copyTerms);
        }
         
        public override bool Equals(object o)
        {

            if (this == o)
            {
                return true;
            }
            if (!(o is Function))
            {
                return false;
            }

            Function f = (Function)o;

            return f.getFunctionName().Equals(getFunctionName())
                    && f.getTerms().SequenceEqual(getTerms());
        }
         
        public override int GetHashCode()
        {
            if (0 == hashCode)
            {
                hashCode = 17;
                hashCode = 37 * hashCode + functionName.GetHashCode();
                foreach (Term t in terms)
                {
                    hashCode = 37 * hashCode + t.GetHashCode();
                }
            }
            return hashCode;
        }
         
        public override string ToString()
        {
            if (null == stringRep)
            {
                IStringBuilder sb = TextFactory.CreateStringBuilder();
                sb.Append(functionName);
                sb.Append("(");

                bool first = true;
                foreach (Term t in terms)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        sb.Append(",");
                    }
                    sb.Append(t.ToString());
                }

                sb.Append(")");

                stringRep = sb.ToString();
            }
            return stringRep;
        }
    }
}
