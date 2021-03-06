﻿using tvn.cosine; 
using tvn.cosine.ai.environment.cellworld;
using tvn.cosine.ai.learning.reinforcement.agent;
using tvn.cosine.ai.learning.reinforcement.example;
using tvn.cosine.ai.probability.example;

namespace tvn_cosine.ai.demo.learning.chapter21
{
    class QLearningAgentDemo : LearningDemoBase
    {
        static void Main(params string[] args)
        {
            System.Console.WriteLine("======================");
            System.Console.WriteLine("DEMO: Q-Learning-Agent");
            System.Console.WriteLine("======================");
            qLearningAgentDemo();
            System.Console.WriteLine("=========================");
        }

        static void qLearningAgentDemo()
        {
            CellWorld<double> cw = CellWorldFactory.CreateCellWorldForFig17_1();
            CellWorldEnvironment cwe = new CellWorldEnvironment(
                    cw.GetCellAt(1, 1),
                    cw.GetCells(),
                    MDPFactory.createTransitionProbabilityFunctionForFigure17_1(cw),
                    CommonFactory.CreateRandom());

            QLearningAgent<Cell<double>, CellWorldAction> qla = new QLearningAgent<Cell<double>, CellWorldAction>(
                    MDPFactory.createActionsFunctionForFigure17_1(cw),
                    CellWorldAction.None, 0.2, 1.0, 5,
                    2.0);

            cwe.AddAgent(qla);

            output_utility_learning_rates(qla, 20, 10000, 500, 20); 
        }
    }
}
