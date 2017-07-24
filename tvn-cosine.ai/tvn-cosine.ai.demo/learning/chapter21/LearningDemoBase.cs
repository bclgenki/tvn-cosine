﻿using System.Text;
using tvn.cosine.ai.common;
using tvn.cosine.ai.common.api;
using tvn.cosine.ai.common.collections;
using tvn.cosine.ai.common.collections.api;
using tvn.cosine.ai.common.exceptions;
using tvn.cosine.ai.environment.cellworld;
using tvn.cosine.ai.learning.reinforcement.agent;
using tvn.cosine.ai.learning.reinforcement.example;
using tvn.cosine.ai.probability.example;

namespace tvn_cosine.ai.demo.learning.chapter21
{
    public abstract class LearningDemoBase
    {
        protected static void output_utility_learning_rates(
              ReinforcementAgent<Cell<double>, CellWorldAction> reinforcementAgent,
              int numRuns, int numTrialsPerRun, int rmseTrialsToReport,
              int reportEveryN)
        {

            if (rmseTrialsToReport > (numTrialsPerRun / reportEveryN))
            {
                throw new IllegalArgumentException("Requesting to report too many RMSE trials, max allowed for args is "
                                + (numTrialsPerRun / reportEveryN));
            }

            CellWorld<double> cw = CellWorldFactory.createCellWorldForFig17_1();
            CellWorldEnvironment cwe = new CellWorldEnvironment(
                    cw.getCellAt(1, 1),
                    cw.getCells(),
                    MDPFactory.createTransitionProbabilityFunctionForFigure17_1(cw),
                    new DefaultRandom());

            cwe.AddAgent(reinforcementAgent);

            IMap<int, ICollection<IMap<Cell<double>, double>>> runs = CollectionFactory.CreateInsertionOrderedMap<int, ICollection<IMap<Cell<double>, double>>>();
            for (int r = 0; r < numRuns; r++)
            {
                reinforcementAgent.reset();
                ICollection<IMap<Cell<double>, double>> trials = CollectionFactory.CreateQueue<IMap<Cell<double>, double>>();
                for (int t = 0; t < numTrialsPerRun; t++)
                {
                    cwe.executeTrial();
                    if (0 == t % reportEveryN)
                    {
                        IMap<Cell<double>, double> u = reinforcementAgent
                                .getUtility();
                        //if (null == u.Get(cw.getCellAt(1, 1)))
                        //{
                        //    throw new IllegalStateException(
                        //            "Bad Utility State Encountered: r=" + r
                        //                    + ", t=" + t + ", u=" + u);
                        //}
                        trials.Add(u);
                    }
                }
                runs.Put(r, trials);
            }

            StringBuilder v4_3 = new StringBuilder();
            StringBuilder v3_3 = new StringBuilder();
            StringBuilder v1_3 = new StringBuilder();
            StringBuilder v1_1 = new StringBuilder();
            StringBuilder v3_2 = new StringBuilder();
            StringBuilder v2_1 = new StringBuilder();
            for (int t = 0; t < (numTrialsPerRun / reportEveryN); t++)
            {
                // Use the last run
                IMap<Cell<double>, double> u = runs.Get(numRuns - 1).Get(t);
                v4_3.Append((u.ContainsKey(cw.getCellAt(4, 3)) ? u.Get(cw
                        .getCellAt(4, 3)) : 0.0) + "\t");
                v3_3.Append((u.ContainsKey(cw.getCellAt(3, 3)) ? u.Get(cw
                        .getCellAt(3, 3)) : 0.0) + "\t");
                v1_3.Append((u.ContainsKey(cw.getCellAt(1, 3)) ? u.Get(cw
                        .getCellAt(1, 3)) : 0.0) + "\t");
                v1_1.Append((u.ContainsKey(cw.getCellAt(1, 1)) ? u.Get(cw
                        .getCellAt(1, 1)) : 0.0) + "\t");
                v3_2.Append((u.ContainsKey(cw.getCellAt(3, 2)) ? u.Get(cw
                        .getCellAt(3, 2)) : 0.0) + "\t");
                v2_1.Append((u.ContainsKey(cw.getCellAt(2, 1)) ? u.Get(cw
                        .getCellAt(2, 1)) : 0.0) + "\t");
            }

            StringBuilder rmseValues = new StringBuilder();
            for (int t = 0; t < rmseTrialsToReport; t++)
            {
                // Calculate the Root Mean Square Error for utility of 1,1
                // for this trial# across all runs
                double xSsquared = 0;
                for (int r = 0; r < numRuns; r++)
                {
                    IMap<Cell<double>, double> u = runs.Get(r).Get(t);
                    double val1_1 = u.Get(cw.getCellAt(1, 1));
                    //if (null == val1_1)
                    //{
                    //    throw new IllegalStateException(
                    //            "U(1,1,) is not present: r=" + r + ", t=" + t
                    //                    + ", runs.size=" + runs.Size()
                    //                    + ", runs(r).Size()=" + runs.Get(r).Size()
                    //                    + ", u=" + u);
                    //}
                    xSsquared += System.Math.Pow(0.705 - val1_1, 2);
                }
                double rmse = System.Math.Sqrt(xSsquared / runs.Size());
                rmseValues.Append(rmse);
                rmseValues.Append("\t");
            }

            System.Console
                .WriteLine("Note: You may copy and paste the following lines into a spreadsheet to generate graphs of learning rate and RMS error in utility:");
            System.Console.WriteLine("(4,3)" + "\t" + v4_3);
            System.Console.WriteLine("(3,3)" + "\t" + v3_3);
            System.Console.WriteLine("(1,3)" + "\t" + v1_3);
            System.Console.WriteLine("(1,1)" + "\t" + v1_1);
            System.Console.WriteLine("(3,2)" + "\t" + v3_2);
            System.Console.WriteLine("(2,1)" + "\t" + v2_1);
            System.Console.WriteLine("RMSeiu" + "\t" + rmseValues);
        }
    }
}
