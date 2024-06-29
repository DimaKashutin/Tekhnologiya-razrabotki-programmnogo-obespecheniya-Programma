using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xo
{
    public class TicTacToe
    {
        private char[,] board;
        public char PlayerSymbol { get; }
        public char BotSymbol { get; }
        public char CurrentPlayerSymbol { get; private set; }
        public bool IsBotGame { get; }
        private int boardSize;
        private int winCondition;

        public TicTacToe(char playerSymbol, char botSymbol, bool isBotGame, int size)
        {
            board = new char[size, size];
            PlayerSymbol = playerSymbol;
            BotSymbol = botSymbol;
            IsBotGame = isBotGame;
            CurrentPlayerSymbol = PlayerSymbol;
            boardSize = size;
            winCondition = boardSize >= 4 ? 4 : 3;
        }

        public bool MakeMove(int row, int col)
        {
            if (board[row, col] == '\0')
            {
                board[row, col] = CurrentPlayerSymbol;
                return true;
            }
            return false;
        }

        public void SwitchPlayer()
        {
            CurrentPlayerSymbol = CurrentPlayerSymbol == PlayerSymbol ? BotSymbol : PlayerSymbol;
        }

        public bool CheckWinner(char symbol)
        {
            for (int i = 0; i < boardSize; i++)
            {
                if (CheckRow(i, symbol) || CheckColumn(i, symbol))
                    return true;
            }
            return CheckDiagonals(symbol);
        }

        private bool CheckRow(int row, char symbol)
        {
            int count = 0;
            for (int i = 0; i < boardSize; i++)
            {
                if (board[row, i] == symbol)
                {
                    count++;
                    if (count == winCondition)
                        return true;
                }
                else
                {
                    count = 0;
                }
            }
            return false;
        }

        private bool CheckColumn(int col, char symbol)
        {
            int count = 0;
            for (int i = 0; i < boardSize; i++)
            {
                if (board[i, col] == symbol)
                {
                    count++;
                    if (count == winCondition)
                        return true;
                }
                else
                {
                    count = 0;
                }
            }
            return false;
        }

        private bool CheckDiagonals(char symbol)
        {
            for (int i = 0; i <= boardSize - winCondition; i++)
            {
                for (int j = 0; j <= boardSize - winCondition; j++)
                {
                    if (CheckDiagonalFromPosition(i, j, symbol))
                        return true;
                }
            }
            return false;
        }

        private bool CheckDiagonalFromPosition(int row, int col, char symbol)
        {
            // Check descending diagonal
            int count = 0;
            for (int i = 0; i < winCondition; i++)
            {
                if (board[row + i, col + i] == symbol)
                {
                    count++;
                    if (count == winCondition)
                        return true;
                }
                else
                {
                    break;
                }
            }

            // Check ascending diagonal
            count = 0;
            for (int i = 0; i < winCondition; i++)
            {
                if (board[row + winCondition - 1 - i, col + i] == symbol)
                {
                    count++;
                    if (count == winCondition)
                        return true;
                }
                else
                {
                    break;
                }
            }

            return false;
        }

        public (int, int) MakeBotMove()
        {
            Random rnd = new Random();
            int row, col;
            do
            {
                row = rnd.Next(boardSize);
                col = rnd.Next(boardSize);
            }
            while (board[row, col] != '\0');

            board[row, col] = BotSymbol;
            return (row, col);
        }
    }
}