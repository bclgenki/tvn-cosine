﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using tvn.cosine.ai.logic.fol;
using tvn.cosine.ai.logic.fol.domain;
using tvn.cosine.ai.logic.fol.kb.data;
using tvn.cosine.ai.logic.fol.parsing;
using tvn.cosine.ai.logic.fol.parsing.ast;

namespace tvn_cosine.ai.test.learning.fol
{
    [TestClass]
    public class SubsumptionEliminationTest
    {
        [TestMethod]
    public void testFindSubsumedClauses()
        {
            // Taken from AIMA2e pg 679
            FOLDomain domain = new FOLDomain();
            domain.addPredicate("patrons");
            domain.addPredicate("hungry");
            domain.addPredicate("type");
            domain.addPredicate("fri_sat");
            domain.addPredicate("will_wait");
            domain.addConstant("Some");
            domain.addConstant("Full");
            domain.addConstant("French");
            domain.addConstant("Thai");
            domain.addConstant("Burger");
            FOLParser parser = new FOLParser(domain);

            String c1 = "patrons(v,Some)";
            String c2 = "patrons(v,Full) AND (hungry(v) AND type(v,French))";
            String c3 = "patrons(v,Full) AND (hungry(v) AND (type(v,Thai) AND fri_sat(v)))";
            String c4 = "patrons(v,Full) AND (hungry(v) AND type(v,Burger))";
            String sh = "FORALL v (will_wait(v) <=> (" + c1 + " OR (" + c2
                    + " OR (" + c3 + " OR (" + c4 + ")))))";

            Sentence s = parser.parse(sh);

            CNFConverter cnfConv = new CNFConverter(parser);

            CNF cnf = cnfConv.convertToCNF(s);

            // Contains 9 duplicates
            Assert.AreEqual(40, cnf.getNumberOfClauses());

            ISet<Clause> clauses = new HashSet<Clause>(cnf.getConjunctionOfClauses());

            // duplicates removed
            Assert.AreEqual(31, clauses.Count);

            clauses.ExceptWith(SubsumptionElimination.findSubsumedClauses(clauses));

            // subsumed clauses removed
            Assert.AreEqual(8, clauses.Count);

            // Ensure only the 8 correct/expected clauses remain
            Clause cl1 = cnfConv
                    .convertToCNF(
                            parser.parse("(NOT(will_wait(v)) OR (patrons(v,Full) OR patrons(v,Some)))"))
                    .getConjunctionOfClauses()[0];
            Clause cl2 = cnfConv
                    .convertToCNF(
                            parser.parse("(NOT(will_wait(v)) OR (hungry(v) OR patrons(v,Some)))"))
                    .getConjunctionOfClauses()[0];
            Clause cl3 = cnfConv
                    .convertToCNF(
                            parser.parse("(NOT(will_wait(v)) OR (patrons(v,Some) OR (type(v,Burger) OR (type(v,French) OR type(v,Thai)))))"))
                    .getConjunctionOfClauses()[0];
            Clause cl4 = cnfConv
                    .convertToCNF(
                            parser.parse("(NOT(will_wait(v)) OR (fri_sat(v) OR (patrons(v,Some) OR (type(v,Burger) OR type(v,French)))))"))
                    .getConjunctionOfClauses()[0];
            Clause cl5 = cnfConv
                    .convertToCNF(
                            parser.parse("(NOT(patrons(v,Some)) OR will_wait(v))"))
                    .getConjunctionOfClauses()[0];
            Clause cl6 = cnfConv
                    .convertToCNF(
                            parser.parse("(NOT(hungry(v)) OR (NOT(patrons(v,Full)) OR (NOT(type(v,French)) OR will_wait(v))))"))
                    .getConjunctionOfClauses()[0];
            Clause cl7 = cnfConv
                    .convertToCNF(
                            parser.parse("(NOT(fri_sat(v)) OR (NOT(hungry(v)) OR (NOT(patrons(v,Full)) OR (NOT(type(v,Thai)) OR will_wait(v)))))"))
                    .getConjunctionOfClauses()[0];
            Clause cl8 = cnfConv
                    .convertToCNF(
                            parser.parse("(NOT(hungry(v)) OR (NOT(patrons(v,Full)) OR (NOT(type(v,Burger)) OR will_wait(v))))"))
                    .getConjunctionOfClauses()[0];

            Assert.IsTrue(clauses.Contains(cl1));
            Assert.IsTrue(clauses.Contains(cl2));
            Assert.IsTrue(clauses.Contains(cl3));
            Assert.IsTrue(clauses.Contains(cl4));
            Assert.IsTrue(clauses.Contains(cl5));
            Assert.IsTrue(clauses.Contains(cl6));
            Assert.IsTrue(clauses.Contains(cl7));
            Assert.IsTrue(clauses.Contains(cl8));
        }
    }
}
