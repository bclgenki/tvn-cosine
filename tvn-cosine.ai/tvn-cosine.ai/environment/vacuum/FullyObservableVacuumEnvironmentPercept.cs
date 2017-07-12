﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tvn.cosine.ai.agent;

namespace tvn.cosine.ai.environment.vacuum
{
    /**
     * Implements a fully observable environment percept, in accordance with page
     * 134, AIMAv3.
     *
     * @author Andrew Brown
     */
    public interface FullyObservableVacuumEnvironmentPercept : Percept
    {
        /**
         * Returns the agent location
         *
         * @param a
         * @return the agents location
         */
        string getAgentLocation(Agent a);

        /**
         * Returns the location state
         *
         * @param location
         * @return the location state
         */
        VacuumEnvironment.LocationState getLocationState(string location);
    }
}