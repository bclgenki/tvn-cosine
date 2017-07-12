﻿using System.Collections.Generic;
using tvn.cosine.ai.logic.fol.parsing.ast;

namespace tvn.cosine.ai.logic.fol.parsing
{
    /**
     * @author Ravi Mohan
     * 
     */
    public class AbstractFOLVisitor : FOLVisitor
    {


        public AbstractFOLVisitor()
        {
        }

        protected virtual Sentence recreate(object ast)
        {
            return ((Sentence)ast).copy();
        }

        public virtual object visitVariable(Variable variable, object arg)
        {
            return variable.copy();
        }

        public virtual object visitQuantifiedSentence(QuantifiedSentence sentence, object arg)
        {
            List<Variable> variables = new List<Variable>();
            foreach (Variable var in sentence.getVariables())
            {
                variables.Add((Variable)var.accept(this, arg));
            }

            return new QuantifiedSentence(sentence.getQuantifier(), variables,
                    (Sentence)sentence.getQuantified().accept(this, arg));
        }

        public virtual object visitPredicate(Predicate predicate, object arg)
        {
            IList<Term> terms = predicate.getTerms();
            IList<Term> newTerms = new List<Term>();
            for (int i = 0; i < terms.Count; i++)
            {
                Term t = terms[i];
                Term subsTerm = (Term)t.accept(this, arg);
                newTerms.Add(subsTerm);
            }
            return new Predicate(predicate.getPredicateName(), newTerms);

        }

        public virtual object visitTermEquality(TermEquality equality, object arg)
        {
            Term newTerm1 = (Term)equality.getTerm1().accept(this, arg);
            Term newTerm2 = (Term)equality.getTerm2().accept(this, arg);
            return new TermEquality(newTerm1, newTerm2);
        }

        public virtual object visitConstant(Constant constant, object arg)
        {
            return constant;
        }

        public virtual object visitFunction(Function function, object arg)
        {
            IList<Term> terms = function.getTerms();
            IList<Term> newTerms = new List<Term>();
            for (int i = 0; i < terms.Count; i++)
            {
                Term t = terms[i];
                Term subsTerm = (Term)t.accept(this, arg);
                newTerms.Add(subsTerm);
            }
            return new Function(function.getFunctionName(), newTerms);
        }

        public virtual object visitNotSentence(NotSentence sentence, object arg)
        {
            return new NotSentence((Sentence)sentence.getNegated().accept(this, arg));
        }

        public virtual object visitConnectedSentence(ConnectedSentence sentence, object arg)
        {
            Sentence substFirst = (Sentence)sentence.getFirst().accept(this, arg);
            Sentence substSecond = (Sentence)sentence.getSecond()
                    .accept(this, arg);
            return new ConnectedSentence(sentence.getConnector(), substFirst,
                    substSecond);
        }
    }
}