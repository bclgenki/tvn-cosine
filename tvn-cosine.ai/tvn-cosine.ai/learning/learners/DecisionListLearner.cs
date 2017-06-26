 namespace aima.core.learning.learners;

 

import aima.core.learning.framework.DataSet;
import aima.core.learning.framework.Example;
import aima.core.learning.framework.Learner;
import aima.core.learning.inductive.DLTest;
import aima.core.learning.inductive.DLTestFactory;
import aima.core.learning.inductive.DecisionList;

/**
 * @author Ravi Mohan
 * @author Mike Stampone
 */
public class DecisionListLearner : Learner {
	public static readonly string FAILURE = "Failure";

	private DecisionList decisionList;

	private string positive, negative;

	private DLTestFactory testFactory;

	public DecisionListLearner(string positive, string negative,
			DLTestFactory testFactory) {
		this.positive = positive;
		this.negative = negative;
		this.testFactory = testFactory;
	}

	//
	// START-Learner

	/**
	 * Induces the decision list from the specified set of examples
	 * 
	 * @param ds
	 *            a set of examples for constructing the decision list
	 */
	 
	public void train(DataSet ds) {
		this.decisionList = decisionListLearning(ds);
	}

	 
	public string predict(Example e) {
		if (decisionList == null) {
			throw new  Exception(
					"learner has not been trained with dataset yet!");
		}
		return decisionList.predict(e);
	}

	 
	public int[] test(DataSet ds) {
		int[] results = new int[] { 0, 0 };

		for (Example e : ds.examples) {
			if (e.targetValue() .Equals(decisionList.predict(e))) {
				results[0] = results[0] + 1;
			} else {
				results[1] = results[1] + 1;
			}
		}
		return results;
	}

	// END-Learner
	//

	/**
	 * Returns the decision list of this decision list learner
	 * 
	 * @return the decision list of this decision list learner
	 */
	public DecisionList getDecisionList() {
		return decisionList;
	}

	//
	// PRIVATE METHODS
	//
	private DecisionList decisionListLearning(DataSet ds) {
		if (ds.Count == 0) {
			return new DecisionList(positive, negative);
		}
		List<DLTest> possibleTests = testFactory
				.createDLTestsWithAttributeCount(ds, 1);
		DLTest test = getValidTest(possibleTests, ds);
		if (test == null) {
			return new DecisionList(null, FAILURE);
		}
		// at this point there is a test that classifies some subset of examples
		// with the same target value
		DataSet matched = test.matchedExamples(ds);
		DecisionList list = new DecisionList(positive, negative);
		list.Add(test, matched.getExample(0).targetValue());
		return list.mergeWith(decisionListLearning(test.unmatchedExamples(ds)));
	}

	private DLTest getValidTest(List<DLTest> possibleTests, DataSet ds) {
		for (DLTest test : possibleTests) {
			DataSet matched = test.matchedExamples(ds);
			if (!(matched.Count == 0)) {
				if (allExamplesHaveSameTargetValue(matched)) {
					return test;
				}
			}

		}
		return null;
	}

	private bool allExamplesHaveSameTargetValue(DataSet matched) {
		// assumes at least i example in dataset
		String targetValue = matched.getExample(0).targetValue();
		for (Example e : matched.examples) {
			if (!(e.targetValue() .Equals(targetValue))) {
				return false;
			}
		}
		return true;
	}
}
