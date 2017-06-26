 namespace aima.core.environment.wumpusworld.action;

import aima.core.agent.impl.DynamicAction;
import aima.core.environment.wumpusworld.AgentPosition;

/**
 * Artificial Intelligence A Modern Approach (3rd Edition): page 237.<br>
 * <br>
 * The agent can TurnLeft by 90 degrees.
 * 
 * @author Federico Baron
 * @author Alessandro Daniele
 * @author Ciaran O'Reilly
 */
public class TurnLeft : DynamicAction {
	public static readonly string TURN_LEFT_ACTION_NAME = "TurnLeft";
	public static readonly string ATTRIBUTE_TO_ORIENTATION = "toOrientation";
	//
	private AgentPosition.Orientation toOrientation;

	/**
	 * Constructor.
	 * 
	 * @param currentOrientation
	 */
	public TurnLeft(AgentPosition.Orientation currentOrientation) {
		base(TURN_LEFT_ACTION_NAME);

		switch (currentOrientation) {
		case FACING_NORTH:
			toOrientation = AgentPosition.Orientation.FACING_WEST;
			break;
		case FACING_SOUTH:
			toOrientation = AgentPosition.Orientation.FACING_EAST;
			break;
		case FACING_EAST:
			toOrientation = AgentPosition.Orientation.FACING_NORTH;
			break;
		case FACING_WEST:
			toOrientation = AgentPosition.Orientation.FACING_SOUTH;
			break;
		}
		setAttribute(ATTRIBUTE_TO_ORIENTATION, toOrientation);
	}

	/**
	 * 
	 * @return the orientation the agent should be after the action occurred.
	 *         <b>Note:<b> this may not be a legal orientation within the
	 *         environment in which the action was performed and this should be
	 *         checked for.
	 */
	public AgentPosition.Orientation getToOrientation() {
		return toOrientation;
	}
}