﻿using System.Collections.Generic;
using tvn.cosine.ai.agent;
using tvn.cosine.ai.agent.impl;
using tvn.cosine.ai.agent.impl.aprog;

namespace tvn.cosine.ai.environment.vacuum
{
    /// <summary>
    /// Artificial Intelligence A Modern Approach (3rd Edition): Figure 2.3, page 36. <para />
    /// Figure 2.3 Partial tabulation of a simple agent function for the
    /// vacuum-cleaner world shown in Figure 2.2.
    /// </summary>
    public class TableDrivenVacuumAgent : AbstractAgent
    {
        public TableDrivenVacuumAgent()
                : base(new TableDrivenAgentProgram(getPerceptSequenceActions()))
        { }

        private static IDictionary<IList<IPercept>, IAction> getPerceptSequenceActions()
        {
            IDictionary<IList<IPercept>, IAction> perceptSequenceActions = new Dictionary<IList<IPercept>, IAction>();

            // NOTE: While this particular table could be setup simply
            // using a few loops, the intent is to show how quickly a table
            // based approach grows and becomes unusable.
            IList<IPercept> ps;
            //
            // Level 1: 4 states
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                VacuumEnvironment.LOCATION_A,
                VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_RIGHT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                VacuumEnvironment.LOCATION_A,
                VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                VacuumEnvironment.LOCATION_B,
                VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_LEFT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                VacuumEnvironment.LOCATION_B,
                VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            //
            // Level 2: 4x4 states
            // 1
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                VacuumEnvironment.LOCATION_A, VacuumEnvironment.LocationState.Clean),
                new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_RIGHT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_LEFT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            // 2
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_RIGHT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_LEFT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            // 3
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_RIGHT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_LEFT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            // 4
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_RIGHT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_LEFT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;


            //
            // Level 3: 4x4x4 states
            // 1-1
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_RIGHT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_LEFT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;
            // 1-2
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_RIGHT;
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_LEFT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            // 1-3
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_RIGHT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_LEFT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            // 1-4
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_RIGHT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_LEFT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            // 2-1
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_RIGHT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_LEFT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            // 2-2
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_RIGHT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_LEFT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            // 2-3
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_RIGHT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_LEFT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            // 2-4
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_RIGHT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_LEFT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            // 3-1
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_RIGHT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_LEFT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            // 3-2
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_RIGHT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_LEFT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            // 3-3
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_RIGHT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_LEFT;

            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            // 3-4
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_RIGHT;
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_LEFT;
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;
            // 4-1
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_RIGHT;
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_LEFT;
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;
            // 4-2
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_RIGHT;
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_LEFT;
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;
            // 4-3
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_RIGHT;
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_LEFT;
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;
            // 4-4
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_RIGHT;
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_A,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Clean));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_MOVE_LEFT;
            ps = createPerceptSequence(new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty), new LocalVacuumEnvironmentPercept(
                    VacuumEnvironment.LOCATION_B,
                    VacuumEnvironment.LocationState.Dirty));
            perceptSequenceActions[ps] = VacuumEnvironment.ACTION_SUCK;

            //
            // Level 4: 4x4x4x4 states
            // ...

            return perceptSequenceActions;
        }

        private static IList<IPercept> createPerceptSequence(params IPercept[] percepts)
        {
            IList<IPercept> perceptSequence = new List<IPercept>();

            foreach (IPercept p in percepts)
            {
                perceptSequence.Add(p);
            }

            return perceptSequence;
        }
    } 
}
