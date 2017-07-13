﻿using System.Collections.Generic;
using tvn.cosine.ai.agent;
using tvn.cosine.ai.environment.cellworld;

namespace tvn.cosine.ai.learning.reinforcement.example
{
    /**
     * An implementation of the EnvironmentState interface for a Cell World.
     * 
     * @author Ciaran O'Reilly
     * 
     */
    public class CellWorldEnvironmentState : IEnvironmentState
    {
        private IDictionary<IAgent, CellWorldPercept> agentLocations = new Dictionary<IAgent, CellWorldPercept>();

        /**
         * Default Constructor.
         */
        public CellWorldEnvironmentState()
        {
        }

        /**
         * Reset the environment state to its default state.
         */
        public void reset()
        {
            agentLocations.Clear();
        }

        /**
         * Set an agent's location within the cell world environment.
         * 
         * @param anAgent
         *            the agents whose location is to be tracked.
         * @param location
         *            the location for the agent in the cell world environment.
         */
        public void setAgentLocation(IAgent anAgent, Cell<double> location)
        { 
            if (!agentLocations.ContainsKey(anAgent))
            { 
                agentLocations[anAgent] = new CellWorldPercept(location);
            }
            else
            {
                agentLocations[anAgent].setCell(location);
            } 
        }

        /**
         * Get the location of an agent within the cell world environment.
         * 
         * @param anAgent
         *            the agent whose location is being queried.
         * @return the location of the agent within the cell world environment.
         */
        public Cell<double> getAgentLocation(IAgent anAgent)
        {
            return agentLocations[anAgent].getCell();
        }

        /**
         * Get a percept for an agent, representing what it senses within the cell
         * world environment.
         * 
         * @param anAgent
         *            the agent a percept is being queried for.
         * @return a percept for the agent, representing what it senses within the
         *         cell world environment.
         */
        public CellWorldPercept getPerceptFor(IAgent anAgent)
        {
            return agentLocations[anAgent];
        }
    }
}
