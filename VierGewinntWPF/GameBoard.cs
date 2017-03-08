using System.Collections.Generic;
using System.Linq;

namespace VierGewinntWPF
{
    class GameBoard
    {
        private readonly Dictionary<Location, Token> board;
        Location lastLocationPlayed;

        public static int MaxRow { get { return 6; } }
        public static int MaxColumn { get { return 7; } }

        public static bool Exists(Location loc)
        {
            return loc.Row > 0 && loc.Row <= MaxRow &&
                loc.Column > 0 && loc.Column <= MaxColumn;
        }

        public Location LastLocationPlayed
        {
            get { return lastLocationPlayed; }
        }
        public bool IsEmpty
        {
            get { return board.Values.All(d => d == Token.Empty); }
        }
        public int LastLocationPlayedAsOrderIndex()
        {
            return board.Keys.OrderBy(k => k.Row).ThenBy(k => k.Column).ToList().IndexOf(LastLocationPlayed);
        }


        public GameBoard()
        {
            board = new Dictionary<Location, Token>(MaxRow * MaxColumn);
        }

        public void Initialize()
        {
            int x, y;
            x = y = 1;
            for (int i = 1; i <= MaxRow * MaxColumn; i++)
            {
                board.Add(new Location(x, y), Token.Empty);
                x++;
                if(x > MaxColumn)
                {
                    y++;
                    x = 1;
                }
            }
        }

        public bool PlayToken(Token token, int column)
        {
            int row = MaxRow;
            while(row > 0 && board[new Location(row,column)] != Token.Empty)
            {
                row--;
            }

            if(row > 0)
            {
                board[new Location(row, column)] = token;
                return true;
            }
            return false;
        }
        public bool Winner()
        {
            if (HorizontalWins(lastLocationPlayed, board[lastLocationPlayed])) return true;
            if (NWSE_DiagonalWins(lastLocationPlayed, board[lastLocationPlayed])) return true;
            if (VerticalWins(lastLocationPlayed, board[lastLocationPlayed])) return true;
            if (NESW_DiagonalWins(lastLocationPlayed, board[lastLocationPlayed])) return true;
            return false;
        }

        private bool NESW_DiagonalWins(Location lastLocationPlayed, Token token)
        {
            var countToken = 0;
            var row = lastLocationPlayed.Row - 1;
            var column = lastLocationPlayed.Column + 1;

            // check NE
            while(row >= 1 && column <= MaxColumn)
            {
                var loc = new Location(row, column);
                if (board[loc] != token) break;
                if (board[loc] == token)
                {
                    countToken++;
                    column++;
                    row--;
                }
                else
                {
                    break;
                }
            }

            row = lastLocationPlayed.Row + 1;
            column = lastLocationPlayed.Column - 1;

            while(row <= MaxRow && column >= 1)
            {
                var loc = new Location(row, column);
                if(board[loc] == token)
                {
                    countToken++;
                    column--;
                    row++;
                }
                else
                {
                    break;
                }
            }
            return countToken >= 3;
        }
        private bool NWSE_DiagonalWins(Location lastLocationPlayed, Token token)
        {
            var countToken = 0;
            var row = lastLocationPlayed.Row-1;
            var column = lastLocationPlayed.Column-1;

            // check NW
            while(row >= 1 && column >= 1)
            {
                var loc = new Location(row, column);
                if(board[loc] == token)
                {
                    countToken++;
                    column--;
                    row--;
                }
                else
                {
                    break;
                }
            }
            row = lastLocationPlayed.Row + 1;
            column = lastLocationPlayed.Column + 1;

            // check SE
            while(row <= MaxRow && column <= MaxColumn)
            {
                var loc = new Location(row, column);
                if(board[loc] == token)
                {
                    countToken++;
                    column++;
                    row++;
                }
                else
                {
                    break;
                }
            }
            return countToken >= 3;
        }
        private bool HorizontalWins(Location location, Token token)
        {
            var countToken = 0;
            var column = location.Row + 1;

            while(column <= MaxColumn)
            {
                var loc = new Location(location.Row, column);
                if(board[loc] == token)
                {
                    countToken++;
                    column++;
                }
                else
                {
                    break;
                }
            }

            // check left 
            column = location.Column - 1;
            while(column >= 1){
                var loc = new Location(location.Row, column);
                if(board[loc] == token)
                {
                    countToken++;
                    column--;
                }
                else { break; }
            }
            return countToken >= 3;
        }
        private bool VerticalWins(Location location, Token token)
        {
            var countToken = 0;
            var row = location.Row + 1;

            while(row <= MaxRow)
            {
                var loc = new Location(row, location.Column);
                if(board[loc] == token)
                {
                    countToken++;
                    row++;
                }
                else { break; }
            }
            return countToken >= 3;
        }

    }
}
