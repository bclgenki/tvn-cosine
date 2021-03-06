﻿using tvn.cosine.ai.agent.api;
using tvn.cosine.ai.agent;
using tvn.cosine.ai.environment.vacuum;
 
namespace tvn_cosine.ai.demo.agent.trivial
{
    public class TableDrivenVacuumAgentDemo
    {
        static void Main(params string[] args)
        {
            // create environment with random state of cleaning.
            IEnvironment env = new VacuumEnvironment();
            IEnvironmentView view = new SimpleEnvironmentView();
            env.AddEnvironmentView(view);

            IAgent a = new TableDrivenVacuumAgent();

            env.AddAgent(a);
            env.Step(16);
            env.NotifyViews("Performance=" + env.GetPerformanceMeasure(a));
        }
    }
}
