﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using tvn.cosine.ai.logic.fol.kb.data;
using tvn.cosine.ai.logic.fol.parsing.ast;

namespace tvn_cosine.ai.test.logic.fol.kb.data
{
    [TestClass]
    public class ChainTest
    { 
        [TestMethod]
        public void testIsEmpty()
        {
            Chain c = new Chain();

            Assert.IsTrue(c.isEmpty());

            c.addLiteral(new Literal(new Predicate("P", new List<Term>())));

            Assert.IsFalse(c.isEmpty());

            List<Literal> lits = new List<Literal>();

            lits.Add(new Literal(new Predicate("P", new List<Term>())));

            c = new Chain(lits);

            Assert.IsFalse(c.isEmpty());
        }

        [TestMethod]
        public void testContrapositives()
        {
            IList<Chain> conts;
            Literal p = new Literal(new Predicate("P", new List<Term>()));
            Literal notq = new Literal(new Predicate("Q", new List<Term>()),
                    true);
            Literal notr = new Literal(new Predicate("R", new List<Term>()),
                    true);

            Chain c = new Chain();

            conts = c.getContrapositives();
            Assert.AreEqual(0, conts.Count);

            c.addLiteral(p);
            conts = c.getContrapositives();
            Assert.AreEqual(0, conts.Count);

            c.addLiteral(notq);
            conts = c.getContrapositives();
            Assert.AreEqual(1, conts.Count);
            Assert.AreEqual("<~Q(),P()>", conts[0].ToString());

            c.addLiteral(notr);
            conts = c.getContrapositives();
            Assert.AreEqual(2, conts.Count);
            Assert.AreEqual("<~Q(),P(),~R()>", conts[0].ToString());
            Assert.AreEqual("<~R(),P(),~Q()>", conts[1].ToString());
        }
    }
}
