﻿using System;
using System.Collections.Generic;
using tvn.cosine.ai.search.adversarial;
using tvn.cosine.ai.util.datastructure;

namespace tvn.cosine.ai.environment.tictactoe
{
    /**
     * Provides an implementation of the Tic-tac-toe game which can be used for
     * experiments with the Minimax algorithm.
     * 
     * @author Ruediger Lunde
     * 
     */
    public class TicTacToeGame : Game<TicTacToeState, XYLocation, string>
    {

        TicTacToeState initialState = new TicTacToeState();

        public TicTacToeState getInitialState()
        {
            return initialState;
        }

        public string[] getPlayers()
        {
            return new string[] { TicTacToeState.X, TicTacToeState.O };
        }

        public string getPlayer(TicTacToeState state)
        {
            return state.getPlayerToMove();
        }

        public IList<XYLocation> getActions(TicTacToeState state)
        {
            return state.getUnMarkedPositions();
        }

        public TicTacToeState getResult(TicTacToeState state, XYLocation action)
        {
            TicTacToeState result = state.Clone();
            result.mark(action);
            return result;
        }

        public bool isTerminal(TicTacToeState state)
        {
            return state.getUtility() != -1;
        }

        public double getUtility(TicTacToeState state, string player)
        {
            double result = state.getUtility();
            if (result != -1)
            {
                if (player.Equals(TicTacToeState.O))
                    result = 1 - result;
            }
            else
            {
                throw new ArgumentException("State is not terminal.");
            }
            return result;
        }
    }
}