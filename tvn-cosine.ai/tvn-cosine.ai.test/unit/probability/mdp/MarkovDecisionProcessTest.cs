﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using tvn.cosine.ai.environment.cellworld;
using tvn.cosine.ai.probability.example;
using tvn.cosine.ai.probability.mdp;

namespace tvn_cosine.ai.test.unit.probability.mdp
{
    [TestClass]
    public class MarkovDecisionProcessTest
    {
        public static readonly double DELTA_THRESHOLD = 1e-3;

        private CellWorld<double> cw = null;
        private MarkovDecisionProcess<Cell<double>, CellWorldAction> mdp = null;

        [TestInitialize]
        public void setUp()
        {
            cw = CellWorldFactory.createCellWorldForFig17_1();
            mdp = MDPFactory.createMDPForFigure17_3(cw);
        }

        [TestMethod]
        public void testActions()
        {
            // Ensure all actions can be performed in each cell
            // except for the terminal states.
            foreach (Cell<double> s in cw.getCells())
            {
                if (4 == s.getX() && (3 == s.getY() || 2 == s.getY()))
                {
                    Assert.AreEqual(0, mdp.actions(s).Size());
                }
                else
                {
                    Assert.AreEqual(5, mdp.actions(s).Size());
                }
            }
        }

        [TestMethod]
        public void testMDPTransitionModel()
        {
            Assert.AreEqual(0.8, mdp.transitionProbability(cw.getCellAt(1, 2),
                    cw.getCellAt(1, 1), CellWorldAction.Up));
            Assert.AreEqual(0.1, mdp.transitionProbability(cw.getCellAt(1, 1),
                    cw.getCellAt(1, 1), CellWorldAction.Up));
            Assert.AreEqual(0.1, mdp.transitionProbability(cw.getCellAt(2, 1),
                    cw.getCellAt(1, 1), CellWorldAction.Up));
            Assert.AreEqual(0.0, mdp.transitionProbability(cw.getCellAt(1, 3),
                    cw.getCellAt(1, 1), CellWorldAction.Up));

            Assert.AreEqual(0.9, mdp.transitionProbability(cw.getCellAt(1, 1),
                    cw.getCellAt(1, 1), CellWorldAction.Down));
            Assert.AreEqual(0.1, mdp.transitionProbability(cw.getCellAt(2, 1),
                    cw.getCellAt(1, 1), CellWorldAction.Down));
            Assert.AreEqual(0.0, mdp.transitionProbability(cw.getCellAt(3, 1),
                    cw.getCellAt(1, 1), CellWorldAction.Down));
            Assert.AreEqual(0.0, mdp.transitionProbability(cw.getCellAt(1, 2),
                    cw.getCellAt(1, 1), CellWorldAction.Down));

            Assert.AreEqual(0.9, mdp.transitionProbability(cw.getCellAt(1, 1),
                    cw.getCellAt(1, 1), CellWorldAction.Left));
            Assert.AreEqual(0.0, mdp.transitionProbability(cw.getCellAt(2, 1),
                    cw.getCellAt(1, 1), CellWorldAction.Left));
            Assert.AreEqual(0.0, mdp.transitionProbability(cw.getCellAt(3, 1),
                    cw.getCellAt(1, 1), CellWorldAction.Left));
            Assert.AreEqual(0.1, mdp.transitionProbability(cw.getCellAt(1, 2),
                    cw.getCellAt(1, 1), CellWorldAction.Left));

            Assert.AreEqual(0.8, mdp.transitionProbability(cw.getCellAt(2, 1),
                    cw.getCellAt(1, 1), CellWorldAction.Right));
            Assert.AreEqual(0.1, mdp.transitionProbability(cw.getCellAt(1, 1),
                    cw.getCellAt(1, 1), CellWorldAction.Right));
            Assert.AreEqual(0.1, mdp.transitionProbability(cw.getCellAt(1, 2),
                    cw.getCellAt(1, 1), CellWorldAction.Right));
            Assert.AreEqual(0.0, mdp.transitionProbability(cw.getCellAt(1, 3),
                    cw.getCellAt(1, 1), CellWorldAction.Right));
        }

        [TestMethod]
        public void testRewardFunction()
        {
            // Ensure all actions can be performed in each cell.
            foreach (Cell<double> s in cw.getCells())
            {
                if (4 == s.getX() && 3 == s.getY())
                {
                    Assert.AreEqual(1.0, mdp.reward(s));
                }
                else if (4 == s.getX() && 2 == s.getY())
                {
                    Assert.AreEqual(-1.0, mdp.reward(s));
                }
                else
                {
                    Assert.AreEqual(-0.04, mdp.reward(s));
                }
            }
        }
    }

}