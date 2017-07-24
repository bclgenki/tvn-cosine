namespace aima.core.search.basic.csp;

using java.util.ArrayList;
using java.util.HashMap;
using java.util.LinkedList;
using java.util.List;
using java.util.Map;
using java.util.Queue;

using aima.core.search.api.Assignment;
using aima.core.search.api.CSP;
using aima.core.search.api.Constraint;
using aima.core.search.api.SearchForAssignmentFunction;
using aima.core.search.basic.support.BasicAssignment;

/**
 * 
 * Artificial Intelligence A Modern Approach (4th Ed.): Figure ??, Page ??.<br>
 * <br>
 * 
 * <pre>
 * <code>
 * function TREE-CSP-SOLVER(csp) returns a solution, or failure
 *    inputs: csp, a CSP with components X, D, C
 * 
 *    n &larr; number of variables in X
 *    assignment &larr; an empty assignment
 *    root  &larr; any variable in X
 *    X &larr; TOPOLOGICALSORT(csp, root)
 *    for j = n down to 2 do
 *       MAKE-ARC-CONSISTENT(csp, PARENT(X<sub>j</sub>), X<sub>j</sub>)
 *       if it cannot be made consistent then return failure
 *    for i = 1 to n do
 *       assignment[X<sub>i</sub>] &larr; any consistent value from D<sub>i</sub>
 *       if there is no consistent value then return failure
 *    return assignment
 * </code>
 * 
 * <pre>
 * 
 * Figure ?? The TREE-CSP-SOLVER algorithm for solving tree-structured CSPs. If
 * the CSP has a solution, we will find it in linear time; if not, we will
 * detect a contradiction.
 * 
 * @author Ciaran O'Reilly
 * @author Anurag Rai
 * 
 */
public class TreeCSPSolver implements SearchForAssignmentFunction {

	// function TREE-CSP-SOLVER(csp) returns a solution, or failure
	// inputs: csp, a CSP with components X, D, C
	 
	public Assignment apply(CSP csp) {
		// n <- number of variables in X
		int n = csp.getVariables().size();
		// assignment <- an empty assignment
		Assignment assignment = newEmptyAssignment();
		// root <- any variable in X
		String root = anyVariable(csp);
		// X <- TOPOLOGICALSORT(csp, root)
		TopologicalSort X = topologicalSort(csp, root);
		// for j = n down to 2 do
		for (int j = n - 1; j >= 1; j--) {
			// MAKE-ARC-CONSISTENT(csp, PARENT(X<sub>j</sub>), X<sub>j</sub>)
			makeArcConsistent(csp, X.parent(X.variables.get(j)), X.variables.get(j));
			// if it cannot be made consistent then return failure
			if (csp.isInconsistent()) {
				return failure();
			}
		}
		// for i = 1 to n do
		for (int i = 0; i < n; i++) {
			// assignment[X<sub>i</sub>] <- any consistent value from
			// D<sub>i</sub>
			String Xi = X.variables.get(i);
			for (Object value : csp.getDomain(Xi).getValues()) {
				assignment.add(Xi, value);
				if (assignment.isConsistent(csp)) {
					break;
				}
			}
			// if there is no consistent value then return failure
			if (!assignment.isConsistent(csp)) {
				return failure();
			}
		}

		return assignment;
	}

	//
	// Supporting Code
	public static class TopologicalSort {
		public List<String> variables = new ArrayList<>();
		public IDictionary<String, String> parents = new HashMap<>();

		public String parent(String childVariable) {
			return parents.get(childVariable);
		}

		 
		public override string ToString() {
			return "TopologicalSort[sort=" + variables + ", parents=" + parents + "]";
		}
	}

	public Assignment newEmptyAssignment() {
		return new BasicAssignment();
	}

	public String anyVariable(CSP csp) {
		return csp.getVariables().get(0);
	}

	public TopologicalSort topologicalSort(CSP csp, String root) {
		if (!csp.isTree()) {
			throw new IllegalArgumentException("CSP is not structured as a tree");
		}
		TopologicalSort result = new TopologicalSort();
		result.variables.add(root);

		Queue<String> toProcess = new LinkedList<>();
		toProcess.add(root);
		while (toProcess.size() > 0) {
			String current = toProcess.remove();
			for (Constraint c : csp.getNeighboringConstraints(current)) {
				for (String scopeVar : c.getScope()) {
					if (!scopeVar.Equals(current)) {
						if (!result.variables.contains(scopeVar)) {
							result.variables.add(scopeVar);
							result.parents.put(scopeVar, current);
							toProcess.add(scopeVar);
						}
					}
				}
			}
		}

		// Just in case!
		if (result.variables.size() != csp.getVariables().size()) {
			throw new IllegalStateException("Unable to perform a topological sort correctly, computed=" + result);
		}

		return result;
	}

	public void makeArcConsistent(CSP csp, String parentVar, String childVar) {
		csp.getNeighboringConstraints(parentVar).stream().filter(constraint -> constraint.getScope().contains(childVar))
				.forEach(constraint -> {
					for (Object parentValue : csp.getDomain(parentVar).getValues()) {
						if (!csp.getDomain(childVar).getValues().stream().anyMatch(childValue -> {
							Object[] values = new Object[2];
							values[constraint.getScope().indexOf(parentVar)] = parentValue;
							values[constraint.getScope().indexOf(childVar)] = childValue;
							return constraint.getRelation().isMember(values);
						})) {
							csp.getDomain(parentVar).delete(parentValue);
						}
					}
				});
	}

	public Assignment failure() {
		return null;
	}
}