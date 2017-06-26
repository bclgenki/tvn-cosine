 namespace aima.core.environment.eightpuzzle;

 
import aima.core.search.framework.Node;
import aima.core.util.datastructure.XYLocation;

 
 
import java.util.function.ToDoubleFunction;

/**
 * @author Ruediger Lunde
 * @author Ravi Mohan
 * @author Ciaran O'Reilly
 */
public class EightPuzzleFunctions {

	public static readonly EightPuzzleBoard GOAL_STATE = new EightPuzzleBoard(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 });

	public static List<Action> getActions(EightPuzzleBoard state) {
		List<Action> actions = new List<>(4);

		if (state.canMoveGap(EightPuzzleBoard.UP))
			actions.Add(EightPuzzleBoard.UP);
		if (state.canMoveGap(EightPuzzleBoard.DOWN))
			actions.Add(EightPuzzleBoard.DOWN);
		if (state.canMoveGap(EightPuzzleBoard.LEFT))
			actions.Add(EightPuzzleBoard.LEFT);
		if (state.canMoveGap(EightPuzzleBoard.RIGHT))
			actions.Add(EightPuzzleBoard.RIGHT);

		return actions;
	}

	public static EightPuzzleBoard getResult(EightPuzzleBoard state, Action action) {
		EightPuzzleBoard result = new EightPuzzleBoard(state);

		if (EightPuzzleBoard.UP .Equals(action) && state.canMoveGap(EightPuzzleBoard.UP))
			result.moveGapUp();
		else if (EightPuzzleBoard.DOWN .Equals(action) && state.canMoveGap(EightPuzzleBoard.DOWN))
			result.moveGapDown();
		else if (EightPuzzleBoard.LEFT .Equals(action) && state.canMoveGap(EightPuzzleBoard.LEFT))
			result.moveGapLeft();
		else if (EightPuzzleBoard.RIGHT .Equals(action) && state.canMoveGap(EightPuzzleBoard.RIGHT))
			result.moveGapRight();
		return result;
	}

	public static ToDoubleFunction<Node<EightPuzzleBoard, Action>> createManhattanHeuristicFunction() {
		return new ManhattanHeuristicFunction();
	}

	public static ToDoubleFunction<Node<EightPuzzleBoard, Action>> createMisplacedTileHeuristicFunction() {
		return new MisplacedTileHeuristicFunction();
	}

	/**
     * @author Ravi Mohan
     * @author Ruediger Lunde
     *
     */
    private static class ManhattanHeuristicFunction : ToDoubleFunction<Node<EightPuzzleBoard, Action>> {

         
        public double applyAsDouble(Node<EightPuzzleBoard, Action> node) {
            EightPuzzleBoard board = node.getState();
            int retVal = 0;
            for (int i = 1; i < 9; ++i) {
                XYLocation loc = board.getLocationOf(i);
                retVal += evaluateManhattanDistanceOf(i, loc);
            }
            return retVal;

        }

        private int evaluateManhattanDistanceOf(int i, XYLocation loc) {
            int retVal = -1;
            int xpos = loc.getXCoOrdinate();
            int ypos = loc.getYCoOrdinate();
            switch (i) {

            case 1:
                retVal = Math.Abs(xpos - 0) + Math.Abs(ypos - 1);
                break;
            case 2:
                retVal = Math.Abs(xpos - 0) + Math.Abs(ypos - 2);
                break;
            case 3:
                retVal = Math.Abs(xpos - 1) + Math.Abs(ypos - 0);
                break;
            case 4:
                retVal = Math.Abs(xpos - 1) + Math.Abs(ypos - 1);
                break;
            case 5:
                retVal = Math.Abs(xpos - 1) + Math.Abs(ypos - 2);
                break;
            case 6:
                retVal = Math.Abs(xpos - 2) + Math.Abs(ypos - 0);
                break;
            case 7:
                retVal = Math.Abs(xpos - 2) + Math.Abs(ypos - 1);
                break;
            case 8:
                retVal = Math.Abs(xpos - 2) + Math.Abs(ypos - 2);
                break;

            }
            return retVal;
        }
    }

	/**
     * @author Ravi Mohan
     * @author Ruediger Lunde
     */
    private  static class MisplacedTileHeuristicFunction : ToDoubleFunction<Node<EightPuzzleBoard, Action>> {

        public double applyAsDouble(Node<EightPuzzleBoard, Action> node) {
            EightPuzzleBoard board = (EightPuzzleBoard) node.getState();
            return getNumberOfMisplacedTiles(board);
        }

        private int getNumberOfMisplacedTiles(EightPuzzleBoard board) {
            int numberOfMisplacedTiles = 0;
            if (!(board.getLocationOf(0) .Equals(new XYLocation(0, 0)))) {
                numberOfMisplacedTiles++;
            }
            if (!(board.getLocationOf(1) .Equals(new XYLocation(0, 1)))) {
                numberOfMisplacedTiles++;
            }
            if (!(board.getLocationOf(2) .Equals(new XYLocation(0, 2)))) {
                numberOfMisplacedTiles++;
            }
            if (!(board.getLocationOf(3) .Equals(new XYLocation(1, 0)))) {
                numberOfMisplacedTiles++;
            }
            if (!(board.getLocationOf(4) .Equals(new XYLocation(1, 1)))) {
                numberOfMisplacedTiles++;
            }
            if (!(board.getLocationOf(5) .Equals(new XYLocation(1, 2)))) {
                numberOfMisplacedTiles++;
            }
            if (!(board.getLocationOf(6) .Equals(new XYLocation(2, 0)))) {
                numberOfMisplacedTiles++;
            }
            if (!(board.getLocationOf(7) .Equals(new XYLocation(2, 1)))) {
                numberOfMisplacedTiles++;
            }
            if (!(board.getLocationOf(8) .Equals(new XYLocation(2, 2)))) {
                numberOfMisplacedTiles++;
            }
            // Subtract the gap position from the # of misplaced tiles
            // as its not actually a tile (see issue 73).
            if (numberOfMisplacedTiles > 0) {
                numberOfMisplacedTiles--;
            }
            return numberOfMisplacedTiles;
        }
    }
}