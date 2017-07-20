﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using tvn.cosine.ai.agent;
using tvn.cosine.ai.common.collections;
using tvn.cosine.ai.environment.wumpusworld;
using tvn.cosine.ai.probability.mdp;
using tvn.cosine.ai.search.framework.problem;

namespace tvn_cosine.ai.test.unit.environment.wumpusworld
{
    //[TestClass]
    //public class WumpusFunctionsTest
    //{

    //    private ActionsFunction<AgentPosition, Action> actionFn;
    //    private ResultFunction<AgentPosition, Action> resultFn;

    //    [TestInitialize]
    //    public void setUp()
    //    {
    //        WumpusCave completeCave = new WumpusCave(4, 4);

    //        actionFn = WumpusFunctionFunctions.createActionsFunction(completeCave);
    //        resultFn = WumpusFunctionFunctions.createResultFunction();
    //    }

    //    [TestMethod]
    //    public void testSuccessors()
    //    {
    //        IQueue<AgentPosition> succPositions = Factory.CreateQueue<AgentPosition>();
    //        IQueue<AgentPosition.Orientation> succOrientation = Factory.CreateQueue<AgentPosition>();

    //        // From every position the possible actions are:
    //        //    - Turn right (change orientation, not position)
    //        //    - Turn left (change orientation, not position)
    //        //    - Forward (change position, not orientation)
    //        AgentPosition P11U = new AgentPosition(1, 1, AgentPosition.Orientation.FACING_NORTH);
    //        succPositions.Add(new AgentPosition(1, 2, AgentPosition.Orientation.FACING_NORTH));
    //        succOrientation.Add(AgentPosition.Orientation.FACING_EAST);
    //        succOrientation.Add(AgentPosition.Orientation.FACING_WEST);
    //        for (Action a : actionFn.apply(P11U))
    //        {
    //            if (a is Forward)
    //            {
    //                Assert.IsTrue(succPositions.Contains(((Forward)a).getToPosition()));
    //                Assert.IsTrue(succPositions.Contains(resultFn.apply(P11U, a)));
    //            }
    //            else if (a is TurnLeft)
    //            {
    //                Assert.IsTrue(succOrientation.Contains(((TurnLeft)a).getToOrientation()));
    //                Assert.AreEqual("[1,1]->FacingWest", resultFn.apply(P11U, a).ToString());
    //            }
    //            else if (a is TurnRight)
    //            {
    //                Assert.IsTrue(succOrientation.Contains(((TurnRight)a).getToOrientation()));
    //                Assert.AreEqual("[1,1]->FacingEast", resultFn.apply(P11U, a).ToString());
    //            }
    //        }


    //        //If you are in front of a wall forward action is not possible
    //        AgentPosition P31D = new AgentPosition(3, 1, AgentPosition.Orientation.FACING_SOUTH);
    //        AgentPosition P41R = new AgentPosition(4, 1, AgentPosition.Orientation.FACING_EAST);
    //        for (Action a : actionFn.apply(P31D))
    //        {
    //            Assert.IsFalse(a is Forward);
    //        }

    //        for (Action a : actionFn.apply(P41R))
    //        {
    //            Assert.IsFalse(a is Forward);
    //        }
    //    }
    //}

}