﻿using tvn.cosine.ai.agent.impl;

namespace tvn.cosine.ai.environment.wumpusworld.action
{
    /**
     * Artificial Intelligence A Modern Approach (3rd Edition): page 237.<br>
     * <br>
     * The agent can move Forward.
     * 
     * @author Federico Baron
     * @author Alessandro Daniele
     * @author Ciaran O'Reilly
     */
    public class Forward : DynamicAction
    {
        public const string FORWARD_ACTION_NAME = "Forward";
        public const string ATTRIBUTE_TO_POSITION = "toPosition";

        private AgentPosition toPosition = null;

        /**
         * Constructor.
         * 
         * @param currentPosition
         * 
         */
        public Forward(AgentPosition currentPosition)
            : base(FORWARD_ACTION_NAME)
        {
            int x = currentPosition.getX();
            int y = currentPosition.getY();

            AgentPosition.Orientation orientation = currentPosition.getOrientation();
            if (orientation.Equals(AgentPosition.Orientation.FACING_NORTH))
            {
                toPosition = new AgentPosition(x, y + 1, orientation);
            }
            else if (orientation.Equals(AgentPosition.Orientation.FACING_SOUTH))
            {
                toPosition = new AgentPosition(x, y - 1, orientation);
            }
            if (orientation.Equals(AgentPosition.Orientation.FACING_EAST))
            {
                toPosition = new AgentPosition(x + 1, y, orientation);
            }
            if (orientation.Equals(AgentPosition.Orientation.FACING_WEST))
            {
                toPosition = new AgentPosition(x - 1, y, orientation);
            }
            setAttribute(ATTRIBUTE_TO_POSITION, toPosition);
        }

        /**
         * 
         * @return the position after the agent move's forward. <b>Note:</b> this
         *         may not be a legal position within the environment in which the
         *         action was performed and this should be checked for. For example,
         *         if an agent tries to move forward and bumps into a wall, then the
         *         agent does not move.
         */
        public AgentPosition getToPosition()
        {
            return toPosition;
        }
    }

}