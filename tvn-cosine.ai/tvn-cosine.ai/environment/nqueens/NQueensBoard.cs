﻿using System.Text;
using tvn.cosine.ai.common;
using tvn.cosine.ai.common.collections;
using tvn.cosine.ai.common.datastructures;

namespace tvn.cosine.ai.environment.nqueens
{
    /**
     * Represents a quadratic board with a matrix of squares on which queens can be
     * placed (only one per square) and moved.
     * 
     * @author Ravi Mohan
     * @author Ruediger Lunde
     */
    public class NQueensBoard : IEquatable, IHashable, IStringable
    {
        /** Parameters for initialization. */
        public enum Config
        {
            EMPTY, QUEENS_IN_FIRST_ROW, QUEEN_IN_EVERY_COL
        }

        /**
         * X---> increases left to right with zero based index Y increases top to
         * bottom with zero based index | | V
         */
        private int[,] squares;

        /**
         * Creates a board with <code>size</code> rows and size columns. Column and
         * row indices start with 0.
         */
        public NQueensBoard(int size)
        {
            squares = new int[size, size];
            for (int i = 0; i < size;++i)
            {
                for (int j = 0; j < size; j++)
                {
                    squares[i, j] = 0;
                }
            }
        }

        /**
         * Creates a board with <code>size</code> rows and size columns. Column and
         * row indices start with 0.
         * 
         * @param config
         *            Controls whether the board is initially empty or contains some
         *            queens.
         */
        public NQueensBoard(int size, Config config)
            : this(size)
        {

            if (config == Config.QUEENS_IN_FIRST_ROW)
            {
                for (int i = 0; i < size;++i)
                    addQueenAt(new XYLocation(i, 0));
            }
            else if (config == Config.QUEEN_IN_EVERY_COL)
            {
                IRandom r = new DefaultRandom();
                for (int i = 0; i < size;++i)
                    addQueenAt(new XYLocation(i, r.Next(size)));
            }
        }

        public int getSize()
        {
            return squares.GetLength(0);
        }

        public void clear()
        {
            for (int i = 0; i < getSize();++i)
            {
                for (int j = 0; j < getSize(); j++)
                {
                    squares[i, j] = 0;
                }
            }
        }

        public void setQueensAt(IQueue<XYLocation> locations)
        {
            clear();
            foreach (XYLocation location in locations)
                addQueenAt(location);
        }

        /** Column and row indices start with 0! */
        public void addQueenAt(XYLocation l)
        {
            if (!(queenExistsAt(l)))
                squares[l.getXCoOrdinate(), l.getYCoOrdinate()] = 1;
        }

        public void removeQueenFrom(XYLocation l)
        {
            if (squares[l.getXCoOrdinate(), l.getYCoOrdinate()] == 1)
            {
                squares[l.getXCoOrdinate(), l.getYCoOrdinate()] = 0;
            }
        }

        /**
         * Moves the queen in the specified column (x-value of <code>l</code>) to
         * the specified row (y-value of <code>l</code>). The action assumes a
         * complete-state formulation of the n-queens problem.
         */
        public void moveQueenTo(XYLocation l)
        {
            for (int i = 0; i < getSize();++i)
                squares[l.getXCoOrdinate(), i] = 0;
            squares[l.getXCoOrdinate(), l.getYCoOrdinate()] = 1;
        }

        public void moveQueen(XYLocation from, XYLocation to)
        {
            if ((queenExistsAt(from)) && (!(queenExistsAt(to))))
            {
                removeQueenFrom(from);
                addQueenAt(to);
            }
        }

        public bool queenExistsAt(XYLocation l)
        {
            return (queenExistsAt(l.getXCoOrdinate(), l.getYCoOrdinate()));
        }

        private bool queenExistsAt(int x, int y)
        {
            return (squares[x, y] == 1);
        }

        public int getNumberOfQueensOnBoard()
        {
            int count = 0;
            for (int i = 0; i < getSize();++i)
            {
                for (int j = 0; j < getSize(); j++)
                {
                    if (squares[i, j] == 1)
                        count++;
                }
            }
            return count;
        }

        public IQueue<XYLocation> getQueenPositions()
        {
            IQueue<XYLocation> result = Factory.CreateQueue<XYLocation>();
            for (int i = 0; i < getSize();++i)
            {
                for (int j = 0; j < getSize(); j++)
                {
                    if (queenExistsAt(i, j))
                        result.Add(new XYLocation(i, j));
                }
            }
            return result;

        }

        public int getNumberOfAttackingPairs()
        {
            int result = 0;
            foreach (XYLocation location in getQueenPositions())
            {
                result += getNumberOfAttacksOn(location);
            }
            return result / 2;
        }

        public int getNumberOfAttacksOn(XYLocation l)
        {
            int x = l.getXCoOrdinate();
            int y = l.getYCoOrdinate();
            return numberOfHorizontalAttacksOn(x, y) + numberOfVerticalAttacksOn(x, y) + numberOfDiagonalAttacksOn(x, y);
        }

        public bool isSquareUnderAttack(XYLocation l)
        {
            int x = l.getXCoOrdinate();
            int y = l.getYCoOrdinate();
            return (isSquareHorizontallyAttacked(x, y) || isSquareVerticallyAttacked(x, y)
                    || isSquareDiagonallyAttacked(x, y));
        }

        private bool isSquareHorizontallyAttacked(int x, int y)
        {
            return numberOfHorizontalAttacksOn(x, y) > 0;
        }

        private bool isSquareVerticallyAttacked(int x, int y)
        {
            return numberOfVerticalAttacksOn(x, y) > 0;
        }

        private bool isSquareDiagonallyAttacked(int x, int y)
        {
            return numberOfDiagonalAttacksOn(x, y) > 0;
        }

        private int numberOfHorizontalAttacksOn(int x, int y)
        {
            int retVal = 0;
            for (int i = 0; i < getSize();++i)
            {
                if ((queenExistsAt(i, y)))
                    if (i != x)
                        retVal++;
            }
            return retVal;
        }

        private int numberOfVerticalAttacksOn(int x, int y)
        {
            int retVal = 0;
            for (int j = 0; j < getSize(); j++)
            {
                if ((queenExistsAt(x, j)))
                    if (j != y)
                        retVal++;
            }
            return retVal;
        }

        private int numberOfDiagonalAttacksOn(int x, int y)
        {
            int retVal = 0;
            int i;
            int j;
            // forward up diagonal
            for (i = (x + 1), j = (y - 1); (i < getSize() && (j > -1));++i, j--)
            {
                if (queenExistsAt(i, j))
                    retVal++;
            }
            // forward down diagonal
            for (i = (x + 1), j = (y + 1); ((i < getSize()) && (j < getSize()));++i, j++)
            {
                if (queenExistsAt(i, j))
                    retVal++;
            }
            // backward up diagonal
            for (i = (x - 1), j = (y - 1); ((i > -1) && (j > -1)); i--, j--)
            {
                if (queenExistsAt(i, j))
                    retVal++;
            }

            // backward down diagonal
            for (i = (x - 1), j = (y + 1); ((i > -1) && (j < getSize())); i--, j++)
            {
                if (queenExistsAt(i, j))
                    retVal++;
            }

            return retVal;
        }

        public override int GetHashCode()
        {
            IQueue<XYLocation> locs = getQueenPositions();
            int result = 17;
            foreach (XYLocation loc in locs)
            {
                result = 37 * loc.GetHashCode();
            }
            return result;
        }

        public override bool Equals(object o)
        {
            if (this == o)
                return true;
            if (o != null && GetType() == o.GetType())
            {
                NQueensBoard aBoard = (NQueensBoard)o;
                if (aBoard.getQueenPositions().Size() != getQueenPositions().Size())
                    return false;
                for (int i = 0; i < getSize();++i)
                {
                    for (int j = 0; j < getSize(); j++)
                    {
                        if (queenExistsAt(i, j) != aBoard.queenExistsAt(i, j))
                            return false;
                    }
                }
                return true;
            }
            return false;
        }

        public void print()
        {
            System.Console.WriteLine(getBoardPic());
        }

        public string getBoardPic()
        {
            StringBuilder builder = new StringBuilder();
            for (int row = 0; (row < getSize()); row++)
            { // row
                for (int col = 0; (col < getSize()); col++)
                { // col
                    if (queenExistsAt(col, row))
                        builder.Append(" Q ");
                    else
                        builder.Append(" - ");
                }
                builder.Append("\n");
            }
            return builder.ToString();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int row = 0; row < getSize(); row++)
            { // rows
                for (int col = 0; col < getSize(); col++)
                { // columns
                    if (queenExistsAt(col, row))
                        builder.Append('Q');
                    else
                        builder.Append('-');
                }
                builder.Append("\n");
            }
            return builder.ToString();
        }
    }
}
