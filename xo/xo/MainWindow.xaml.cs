using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace xo
{
    public partial class MainWindow : Window
    {
        private TicTacToe game;
        private Button[,] buttons;
        private int boardSize;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeGameBoard(int size)
        {
            gameBoard.Children.Clear();
            gameBoard.Rows = size;
            gameBoard.Columns = size;

            buttons = new Button[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Button button = new Button();
                    button.FontSize = 32;
                    button.Click += Button_Click;
                    buttons[i, j] = button;
                    gameBoard.Children.Add(button);
                }
            }
        }

        private void btnPlayerVsBot_Click(object sender, RoutedEventArgs e)
        {
            StartGame(true);
        }

        private void btnPlayerVsPlayer_Click(object sender, RoutedEventArgs e)
        {
            StartGame(false);
        }

        private void StartGame(bool isBotGame)
        {
            if (int.TryParse(txtBoardSize.Text, out boardSize) && boardSize >= 3)
            {
                game = new TicTacToe('X', 'O', isBotGame, boardSize);
                InitializeGameBoard(boardSize);
                ClearBoard();
            }
            else
            {
                MessageBox.Show("Please enter a valid board size (minimum 3).");
            }
        }

        private void ClearBoard()
        {
            foreach (Button button in buttons)
            {
                button.Content = null;
                button.IsEnabled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Find the row and column of the clicked button
            int row = -1, col = -1;
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (buttons[i, j] == clickedButton)
                    {
                        row = i;
                        col = j;
                        break;
                    }
                }
                if (row != -1) break;
            }

            if (game.MakeMove(row, col))
            {
                clickedButton.Content = game.CurrentPlayerSymbol;
                clickedButton.IsEnabled = false;

                if (game.CheckWinner(game.CurrentPlayerSymbol))
                {
                    MessageBox.Show($"Player {game.CurrentPlayerSymbol} wins!");
                    ClearBoard();
                    return;
                }

                game.SwitchPlayer();

                if (game.IsBotGame && game.CurrentPlayerSymbol == game.BotSymbol)
                {
                    (int botRow, int botCol) = game.MakeBotMove();
                    buttons[botRow, botCol].Content = game.BotSymbol;
                    buttons[botRow, botCol].IsEnabled = false;

                    if (game.CheckWinner(game.BotSymbol))
                    {
                        MessageBox.Show("Bot wins!");
                        ClearBoard();
                        return;
                    }

                    game.SwitchPlayer();
                }
            }
        }
    }
}