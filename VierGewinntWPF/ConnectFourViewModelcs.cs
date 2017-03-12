using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace VierGewinntWPF
{
    class ConnectFourViewModelcs : BaseNotifyPropertyChanged
    {
        readonly ICommand starteGameCommand;
        readonly ICommand playTokenCommand;
        public ICommand StartGameCommand { get { return starteGameCommand; } }
        public ICommand PlayTokenCommand { get { return playTokenCommand; } }

        Token CurrentPlayerToken { get; set; }
        GameBoard gameBoard;

        ObservableCollection<string> boardLocationColors;
        public ObservableCollection<string> BoardLocationColors
        {
            get { return boardLocationColors; }
            private set { boardLocationColors = value; FirePropertyChanged("BoardLocationColors"); }
        }

        string redPlayerTurn;
        string blackPlayerTurn;
        string redPlayerWins;
        string blackPlayerWins;
        string isBoardEnabled;
        public string RedPlayerTurn
        {
            get { return redPlayerTurn; }
            private set { redPlayerTurn = value; FirePropertyChanged("RedPlayersTurn"); }
        }
        public string BlackPlayerTurn
        {
            get { return blackPlayerTurn; }
            private set { blackPlayerTurn = value; FirePropertyChanged("BlackPlayerTurn"); }
        }
        public string RedPlayerWins
        {
            get { return redPlayerWins; }
            private set { redPlayerWins = value; FirePropertyChanged("RedPlayerWins"); }
        }
        public string BlackPlayerWins
        {
            get { return blackPlayerWins; }
            private set { blackPlayerWins = value; FirePropertyChanged("BlackPlayerWins"); }
        }
        public string IsBoardEnabled
        {
            get { return isBoardEnabled; }
            private set { isBoardEnabled = value; FirePropertyChanged("IsBoardEnabled"); }
        }

        public ConnectFourViewModelcs()
        {
            starteGameCommand = new Command(InitializeGame);
            playTokenCommand = new Command(PlayToken);
            InitializeGame();
        }

        /// <summary>
        /// Initialize the Gameboard and set all default settings
        /// </summary>
        public void InitializeGame()
        {
            gameBoard = new GameBoard();
            gameBoard.Initialize();
            boardLocationColors = new ObservableCollection<string>
                (Enumerable.Repeat("AliceBlue", GameBoard.MaxRow * GameBoard.MaxColumn));
            RedPlayerTurn = "Visible";
            BlackPlayerTurn = "Hidden";
            RedPlayerWins = "Hidden";
            BlackPlayerWins = "Hidden";
            CurrentPlayerToken = Token.Red;
            IsBoardEnabled = "True";
        }

        void DeclareWinner(Token token)
        {
            RedPlayerWins = token == Token.Red ? "Visible" : "Hidden";
            BlackPlayerWins = token == Token.Black ? "Visible" : "Hidden";
            RedPlayerTurn = "Hidden";
            BlackPlayerTurn = "Hidden";
            IsBoardEnabled = "False";
        }

        void SwitchTurn(Token token)
        {
            BlackPlayerTurn = token == Token.Red ? "Visible" : "Hidden";
            RedPlayerTurn = token == Token.Black ? "Visible" : "Hidden";
            CurrentPlayerToken = token == Token.Red ? Token.Black : Token.Red;
        }

        void PlayToken(object column)
        {
            // col + 1, columns are not 0-based
            var tokenPlaced = gameBoard.PlayToken(CurrentPlayerToken, (int)column + 1);
            if (tokenPlaced)
            {
                string color = "";
                if(CurrentPlayerToken == Token.Empty)
                {
                    color = "Blue";
                }
                else
                {
                    color = CurrentPlayerToken == Token.Black ? "Black" : "Red";
                }

                var index = gameBoard.LastLocationPlayedAsOrderIndex();
                BoardLocationColors[index] = color;
                if (!gameBoard.Winner())
                {
                    SwitchTurn(CurrentPlayerToken);
                }
                else
                {
                    DeclareWinner(CurrentPlayerToken);
                }
            }
        }
    }
}
