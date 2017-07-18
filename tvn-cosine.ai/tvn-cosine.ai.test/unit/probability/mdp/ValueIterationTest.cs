﻿namespace tvn_cosine.ai.test.unit.probability.mdp
{
    public class ValueIterationTest
    {
        public static final double DELTA_THRESHOLD = 1e-3;

        private CellWorld<Double> cw = null;
        private MarkovDecisionProcess<Cell<Double>, CellWorldAction> mdp = null;
        private ValueIteration<Cell<Double>, CellWorldAction> vi = null;

        @Before
        public void setUp()
        {
            cw = CellWorldFactory.createCellWorldForFig17_1();
            mdp = MDPFactory.createMDPForFigure17_3(cw);
            vi = new ValueIteration<Cell<Double>, CellWorldAction>(1.0);
        }

        @Test
        public void testValueIterationForFig17_3()
        {
            Map<Cell<Double>, Double> U = vi.valueIteration(mdp, 0.0001);

            Assert.assertEquals(0.705, U.get(cw.getCellAt(1, 1)), DELTA_THRESHOLD);
            Assert.assertEquals(0.762, U.get(cw.getCellAt(1, 2)), DELTA_THRESHOLD);
            Assert.assertEquals(0.812, U.get(cw.getCellAt(1, 3)), DELTA_THRESHOLD);

            Assert.assertEquals(0.655, U.get(cw.getCellAt(2, 1)), DELTA_THRESHOLD);
            Assert.assertEquals(0.868, U.get(cw.getCellAt(2, 3)), DELTA_THRESHOLD);

            Assert.assertEquals(0.611, U.get(cw.getCellAt(3, 1)), DELTA_THRESHOLD);
            Assert.assertEquals(0.660, U.get(cw.getCellAt(3, 2)), DELTA_THRESHOLD);
            Assert.assertEquals(0.918, U.get(cw.getCellAt(3, 3)), DELTA_THRESHOLD);

            Assert.assertEquals(0.388, U.get(cw.getCellAt(4, 1)), DELTA_THRESHOLD);
            Assert.assertEquals(-1.0, U.get(cw.getCellAt(4, 2)), DELTA_THRESHOLD);
            Assert.assertEquals(1.0, U.get(cw.getCellAt(4, 3)), DELTA_THRESHOLD);
        }
    }

}
