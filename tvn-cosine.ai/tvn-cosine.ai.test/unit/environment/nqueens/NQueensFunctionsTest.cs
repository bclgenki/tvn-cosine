﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using tvn.cosine.ai.common.collections;
using tvn.cosine.ai.common.datastructures;
using tvn.cosine.ai.environment.nqueens;
using tvn.cosine.ai.search.framework.problem;

namespace tvn_cosine.ai.test.unit.environment.nqueens
{
    [TestClass]
    public class NQueensFunctionsTest
    {
        private ActionsFunction<NQueensBoard, QueenAction> actionsFn;
        private ResultFunction<NQueensBoard, QueenAction> resultFn;
        private GoalTest<NQueensBoard> goalTest;

        private NQueensBoard oneBoard;
        private NQueensBoard eightBoard;
        private NQueensBoard board;


        [TestInitialize]
        public void setUp()
        {
            oneBoard = new NQueensBoard(1);
            eightBoard = new NQueensBoard(8);
            board = new NQueensBoard(8);

            actionsFn = NQueensFunctions.getIFActions;
            resultFn = NQueensFunctions.getResult;
            goalTest = NQueensFunctions.testGoal;
        }

        [TestMethod]
        public void testSimpleBoardSuccessorGenerator()
        {
            IQueue<QueenAction> actions = Factory.CreateQueue<QueenAction>(actionsFn(oneBoard));
            Assert.AreEqual(1, actions.Size());
            NQueensBoard next = resultFn(oneBoard, actions.Get(0));
            Assert.AreEqual(1, next.getNumberOfQueensOnBoard());
        }

        [TestMethod]
        public void testComplexBoardSuccessorGenerator()
        {
            IQueue<QueenAction> actions = Factory.CreateQueue<QueenAction>(actionsFn(eightBoard));
            Assert.AreEqual(8, actions.Size());
            NQueensBoard next = resultFn(eightBoard, actions.Get(0));
            Assert.AreEqual(1, next.getNumberOfQueensOnBoard());

            actions = Factory.CreateQueue<QueenAction>(actionsFn(next));
            Assert.AreEqual(6, actions.Size());
        }


        [TestMethod]
        public void testEmptyBoard()
        {
            Assert.IsFalse(goalTest(board));
        }

        [TestMethod]
        public void testSingleSquareBoard()
        {
            board = new NQueensBoard(1);
            Assert.IsFalse(goalTest(board));
            board.addQueenAt(new XYLocation(0, 0));
            Assert.IsTrue(goalTest(board));
        }

        [TestMethod]
        public void testInCorrectPlacement()
        {
            Assert.IsFalse(goalTest(board));
            // This is the configuration of Figure 3.5 (b) in AIMA 2nd Edition
            board.addQueenAt(new XYLocation(0, 0));
            board.addQueenAt(new XYLocation(1, 2));
            board.addQueenAt(new XYLocation(2, 4));
            board.addQueenAt(new XYLocation(3, 6));
            board.addQueenAt(new XYLocation(4, 1));
            board.addQueenAt(new XYLocation(5, 3));
            board.addQueenAt(new XYLocation(6, 5));
            board.addQueenAt(new XYLocation(7, 7));
            Assert.IsFalse(goalTest(board));
        }

        [TestMethod]
        public void testCorrectPlacement()
        {
            Assert.IsFalse(goalTest(board));
            // This is the configuration of Figure 5.9 (c) in AIMA 2nd Edition
            board.addQueenAt(new XYLocation(0, 1));
            board.addQueenAt(new XYLocation(1, 4));
            board.addQueenAt(new XYLocation(2, 6));
            board.addQueenAt(new XYLocation(3, 3));
            board.addQueenAt(new XYLocation(4, 0));
            board.addQueenAt(new XYLocation(5, 7));
            board.addQueenAt(new XYLocation(6, 5));
            board.addQueenAt(new XYLocation(7, 2));

            Assert.IsTrue(goalTest(board));
        }
    }
}