﻿namespace tvn.cosine.ai.environment.map
{
    /**
     * The AIMA framework uses dynamic attributes to make implementations of agents
     * and environments completely independent of each other. The disadvantage of
     * this concept is, that it's error-prone. This set of constants is designed to
     * make information exchange more reliable for map agents. Two kinds of
     * attributes are distinguished. Percept attributes are attached to percepts.
     * They are generated by the environment and read by by the agent.
     * EnvironmentState attributes are attached to the EnvironmentState of the
     * Environment.
     * 
     * @author Ruediger Lunde
     */
    public class DynAttributeNames
    {
        /**
         * Name of a dynamic attribute, which contains the current location of the
         * agent. Expected value type: String.
         */
        public const string AGENT_LOCATION = "location";
        /**
         * Name of a dynamic attribute, which tells the agent where it is. Expected
         * value type: String.
         */
        public const string PERCEPT_IN = "in";
    }
}
