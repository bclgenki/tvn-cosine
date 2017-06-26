 namespace aima.core.logic.fol;

 
 
 

import aima.core.logic.fol.kb.data.Literal;
import aima.core.logic.fol.parsing.AbstractFOLVisitor;
import aima.core.logic.fol.parsing.ast.AtomicSentence;
import aima.core.logic.fol.parsing.ast.Function;
import aima.core.logic.fol.parsing.ast.QuantifiedSentence;
import aima.core.logic.fol.parsing.ast.Sentence;
import aima.core.logic.fol.parsing.ast.Term;
import aima.core.logic.fol.parsing.ast.Variable;

/**
 * @author Ravi Mohan
 * @author Ciaran O'Reilly
 */
public class SubstVisitor : AbstractFOLVisitor {

	public SubstVisitor() {
	}

	/**
	 * Note: Refer to Artificial Intelligence A Modern Approach (3rd Edition):
	 * page 323.
	 * 
	 * @param theta
	 *            a substitution.
	 * @param sentence
	 *            the substitution has been applied to.
	 * @return a new Sentence representing the result of applying the
	 *         substitution theta to aSentence.
	 * 
	 */
	public Sentence subst(IDictionary<Variable, Term> theta, Sentence sentence) {
		return (Sentence) sentence.accept(this, theta);
	}

	public Term subst(IDictionary<Variable, Term> theta, Term aTerm) {
		return (Term) aTerm.accept(this, theta);
	}

	public Function subst(IDictionary<Variable, Term> theta, Function function) {
		return (Function) function.accept(this, theta);
	}

	public Literal subst(IDictionary<Variable, Term> theta, Literal literal) {
		return literal.newInstance((AtomicSentence) literal
				.getAtomicSentence().accept(this, theta));
	}

	@SuppressWarnings("unchecked")
	 
	public object visitVariable(Variable variable, object arg) {
		IDictionary<Variable, Term> substitution = (IDictionary<Variable, Term>) arg;
		if (substitution.ContainsKey(variable)) {
			return substitution.get(variable).copy();
		}
		return variable.copy();
	}

	@SuppressWarnings("unchecked")
	 
	public object visitQuantifiedSentence(QuantifiedSentence sentence,
			Object arg) {

		IDictionary<Variable, Term> substitution = (IDictionary<Variable, Term>) arg;

		Sentence quantified = sentence.getQuantified();
		Sentence quantifiedAfterSubs = (Sentence) quantified.accept(this, arg);

		List<Variable> variables = new List<Variable>();
		for (Variable v : sentence.getVariables()) {
			Term st = substitution.get(v);
			if (null != st) {
				if (st is Variable) {
					// Only if it is a variable to I replace it, otherwise
					// I drop it.
					variables.Add((Variable) st.copy());
				}
			} else {
				// No substitution for the quantified variable, so
				// keep it.
				variables.Add(v.copy());
			}
		}

		// If not variables remaining on the quantifier, then drop it
		if (variables.Count == 0) {
			return quantifiedAfterSubs;
		}

		return new QuantifiedSentence(sentence.getQuantifier(), variables,
				quantifiedAfterSubs);
	}
}