namespace aima.core.logic.basic.firstorder.parsing.ast;

using java.util.ArrayList;
using java.util.Collections;
using java.util.List;
using java.util.StringJoiner;

using aima.core.logic.basic.firstorder.parsing.FOLVisitor;

/**
 * @author Ravi Mohan
 * @author Ciaran O'Reilly
 */
public class Function implements Term {
	private String functionName;
	private List<Term> terms = new ArrayList<Term>();
	private String stringRep = null;
	private int hashCode = 0;

	public Function(String functionName, List<Term> terms) {
		this.functionName = functionName;
		this.terms.addAll(terms);
	}

	public String getFunctionName() {
		return functionName;
	}

	public List<Term> getTerms() {
		return Collections.unmodifiableList(terms);
	}

	//
	// START-Term
	public String getSymbolicName() {
		return getFunctionName();
	}

	public bool isCompound() {
		return true;
	}

	public List<Term> getArgs() {
		return getTerms();
	}

	public Object accept(FOLVisitor v, Object arg) {
		return v.visitFunction(this, arg);
	}

	public Function copy() {
		List<Term> copyTerms = new ArrayList<Term>();
		for (Term t : terms) {
			copyTerms.add(t.copy());
		}
		return new Function(functionName, copyTerms);
	}

	// END-Term
	//

	 
	public override bool Equals(object o) {

		if (this == o) {
			return true;
		}
		if (!(o is Function)) {
			return false;
		}

		Function f = (Function) o;

		return f.getFunctionName().Equals(getFunctionName())
				&& f.getTerms().Equals(getTerms());
	}

	 
	public override int GetHashCode() {
		if (0 == hashCode) {
			hashCode = 17;
			hashCode = 37 * hashCode + functionName.GetHashCode();
			for (Term t : terms) {
				hashCode = 37 * hashCode + t.GetHashCode();
			}
		}
		return hashCode;
	}

	 
	public override string ToString() {
		if (null == stringRep) {
			StringBuilder sb = new StringBuilder();
			sb.append(functionName);
			StringJoiner sj = new StringJoiner(",", "(", ")");
			for (Term t : terms) {
				sj.add(t.ToString());
			}
			stringRep = sb.ToString() + sj.ToString();
			return stringRep;
		}
		return stringRep;
	}
}