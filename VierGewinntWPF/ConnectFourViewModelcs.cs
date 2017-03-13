﻿using System;
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

        string redPlayerTurn;
        string blackPlayerTurn;
        string redPlayerWins;
        string blackPlayerWins;
        string isBoardEnabled;

        ObservableCollection<string> boardLocationColors;
        public ObservableCollection<string> BoardLocationColors
        {
            get { return boardLocationColors; }
            private set { boardLocationColors = value; FirePropertyChanged("BoardLocationColors"); }
        }

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
                (Enumerable.Repeat("lightblue", GameBoard.MaxRow * GameBoard.MaxColumn));
            RedPlayerTurn = "Hidden";
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
                var index = gameBoard.LastLocationPlayedAsOrderIndex();
                boardLocationColors[index] = TokenToColor(CurrentPlayerToken);
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

        private string TokenToColor(Token token)
        {
            if (token == Token.Red)
                return "Red";
            if (token == Token.Black)
                return "Black";

            return "lightblue";
        }
    }
}
