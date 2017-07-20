﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using tvn.cosine.ai.common.collections;
using tvn.cosine.ai.environment.cellworld;
using tvn.cosine.ai.probability.example;
using tvn.cosine.ai.probability.mdp;
using tvn.cosine.ai.probability.mdp.search;

namespace tvn_cosine.ai.test.unit.probability.mdp
{
    [TestClass] public class ValueIterationTest
    {
        public static readonly  double DELTA_THRESHOLD = 1e-3;

        private CellWorld<double> cw = null;
        private MarkovDecisionProcess<Cell<double>, CellWorldAction> mdp = null;
        private ValueIteration<Cell<double>, CellWorldAction> vi = null;

        [TestInitialize]
        public void setUp()
        {
            cw = CellWorldFactory.createCellWorldForFig17_1();
            mdp = MDPFactory.createMDPForFigure17_3(cw);
            vi = new ValueIteration<Cell<double>, CellWorldAction>(1.0);
        }

        [TestMethod]
        public void testValueIterationForFig17_3()
        {
            IMap<Cell<double>, double> U = vi.valueIteration(mdp, 0.0001);

            Assert.AreEqual(0.705, U.Get(cw.getCellAt(1, 1)) );
            Assert.AreEqual(0.762, U.Get(cw.getCellAt(1, 2)) );
            Assert.AreEqual(0.812, U.Get(cw.getCellAt(1, 3)) );

            Assert.AreEqual(0.655, U.Get(cw.getCellAt(2, 1)) );
            Assert.AreEqual(0.868, U.Get(cw.getCellAt(2, 3)) );

            Assert.AreEqual(0.611, U.Get(cw.getCellAt(3, 1)) );
            Assert.AreEqual(0.660, U.Get(cw.getCellAt(3, 2)) );
            Assert.AreEqual(0.918, U.Get(cw.getCellAt(3, 3)) );

            Assert.AreEqual(0.388, U.Get(cw.getCellAt(4, 1)) );
            Assert.AreEqual(-1.0, U.Get(cw.getCellAt(4, 2)) );
            Assert.AreEqual(1.0, U.Get(cw.getCellAt(4, 3)) );
        }
    }

}