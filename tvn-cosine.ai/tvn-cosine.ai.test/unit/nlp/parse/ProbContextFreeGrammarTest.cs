﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using tvn.cosine.ai.common.collections;
using tvn.cosine.ai.nlp.data.grammars;
using tvn.cosine.ai.nlp.parsing.grammars;

namespace tvn_cosine.ai.test.unit.nlp.parse
{
    [TestClass]
    public class ProbContextFreeGrammarTest
    {
        ProbUnrestrictedGrammar g;
        ProbUnrestrictedGrammar cfG;

        [TestInitialize]
        public void setup()
        {
            g = new ProbUnrestrictedGrammar();
            cfG = ProbContextFreeExamples.buildWumpusGrammar();
        }

        [TestMethod]
        public void testValidRule()
        {
            // This rule is a valid Context-Free rule
            Rule validR = new Rule(Factory.CreateQueue<string>(new[] { "W" }),
                                   Factory.CreateQueue<string>(new[] { "a", "s" }), (float)0.5);
            // This rule is of correct form but not a context-free rule
            Rule invalidR = new Rule(Factory.CreateQueue<string>(new[] { "W", "A" }), null, (float)0.5);
            Assert.IsFalse(cfG.validRule(invalidR));
            Assert.IsTrue(cfG.validRule(validR));
        }

    }

}
