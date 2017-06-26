 namespace aima.core.logic.propositional.kb;

 
 
 
 
 

import aima.core.logic.propositional.inference.TTEntails;
import aima.core.logic.propositional.kb.data.Clause;
import aima.core.logic.propositional.kb.data.ConjunctionOfClauses;
import aima.core.logic.propositional.parsing.PLParser;
import aima.core.logic.propositional.parsing.ast.PropositionSymbol;
import aima.core.logic.propositional.parsing.ast.Sentence;
import aima.core.logic.propositional.visitors.ConvertToConjunctionOfClauses;
import aima.core.logic.propositional.visitors.SymbolCollector;

/**
 * @author Ravi Mohan
 * @author Mike Stampone
 */
public class KnowledgeBase {
	private List<Sentence>         sentences = new List<Sentence>();
	private ConjunctionOfClauses   asCNF     = new ConjunctionOfClauses(Collections.<Clause>emptySet());
	private ISet<PropositionSymbol> symbols   = new HashSet<PropositionSymbol>();
	private PLParser               parser    = new PLParser();

	public KnowledgeBase() {
	}

	/**
	 * Adds the specified sentence to the knowledge base.
	 * 
	 * @param aSentence
	 *            a fact to be added to the knowledge base.
	 */
	public void tell(string aSentence) {
		tell((Sentence) parser.parse(aSentence));
		
	}
	
	/**
	 * Adds the specified sentence to the knowledge base.
	 * 
	 * @param aSentence
	 *            a fact to be added to the knowledge base.
	 */
	public void tell(Sentence aSentence) {
		if (!(sentences.Contains(aSentence))) {
			sentences.Add(aSentence);
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
	public void tellAll(string[] percepts) {
		for (int i = 0; i < percepts.Length; ++i) {
			tell(percepts[i]);
		}

	}

	/**
	 * Returns the number of sentences in the knowledge base.
	 * 
	 * @return the number of sentences in the knowledge base.
	 */
	public int size() {
		return sentences.Count;
	}

	/**
	 * Returns the list of sentences in the knowledge base chained together as a
	 * single sentence.
	 * 
	 * @return the list of sentences in the knowledge base chained together as a
	 *         single sentence.
	 */
	public Sentence asSentence() {
		return Sentence.newConjunction(sentences);
	}
	
	/**
	 * 
	 * @return a Conjunctive Normal Form (CNF) representation of the Knowledge Base.
	 */
	public ISet<Clause> asCNF() {
		return asCNF.getClauses();
	}
	
	/**
	 * 
	 * @return a unique set of the symbols currently contained in the Knowledge Base.
	 */
	public ISet<PropositionSymbol> getSymbols() {
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
	public bool askWithTTEntails(string queryString) {
		PLParser parser = new PLParser();

		Sentence alpha = parser.parse(queryString);

		return new TTEntails().ttEntails(this, alpha);
	}

	 
	public override string ToString() {
		if (sentences.Count == 0) {
			return "";
		} else {
			return asSentence().ToString();
		}
	}

	/**
	 * Returns the list of sentences in the knowledge base.
	 * 
	 * @return the list of sentences in the knowledge base.
	 */
	public List<Sentence> getSentences() {
		return sentences;
	}
}