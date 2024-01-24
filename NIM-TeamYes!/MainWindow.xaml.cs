using System.Runtime;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NIM_TeamYes_
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private int[] Matches = { 1, 3, 5, 7 };
		private int SelectedRow = -1;
		private int MatchesSelected = 0;
		private bool Turn = true;
		private bool IsGameOver = false;

		public MainWindow()
		{
			InitializeComponent();
		}

		public void OnMatchClicked(object sender, RoutedEventArgs e)
		{
			if ((sender as Button).Background == Brushes.Black) // sender.background == black
			{
				if ((sender as Button).Tag.ToString() == SelectedRow.ToString()) // sender.row == selectedrow
				{
					MatchesSelected++;
					(sender as Button).Background = Brushes.Gray; // sender.background = gray
				}
				else
				{
					if (SelectedRow == -1) // selected row == -1
					{
						MatchesSelected = 1;
						SelectedRow = int.Parse((sender as Button).Tag.ToString());
						(sender as Button).Background = Brushes.Gray; // sender.background = gray
					}
					else
					{
						ResetRow();
						MatchesSelected = 1;
						SelectedRow = int.Parse((sender as Button).Tag.ToString()); // set selected row
						(sender as Button).Background = Brushes.Gray; // sender.background = gray
					}
				}
			}
			else
			{
				MatchesSelected--;
				(sender as Button).Background = Brushes.Black; // sender.background = black
				if (MatchesSelected == 0)
				{
					SelectedRow = -1;
				}
			}
        }

		public void OnEndTurnClick(object sender, RoutedEventArgs e)
		{
			if (IsGameOver)
			{ 
				OnResetGame();
			}
			if (MatchesSelected > 0)
			{ 
				RemoveMatches(SelectedRow, MatchesSelected);
				CheckGameOver();
				Turn = !Turn;
				MatchesSelected = 0;

				if (Turn)
				{
					Background = Brushes.CornflowerBlue; 
				}
				if (!Turn)
				{
					Background = Brushes.MediumVioletRed;
				}
			}
			// eef do this -> Change color of elements, based on turn, to indicate that it is the next players turn.
		}

		public void OnResetGame()
		{
			foreach (Button b in FindVisualChildren<Button>(this))
			{
				if (b.Visibility == Visibility.Hidden)
				{
					b.Background = Brushes.Black;
					b.Visibility = Visibility.Visible;
				}
				if ((string)b.Content == "Restart Game")
				{
					b.Content = "End Turn";
				}
			}
			int[] ma = { 1, 3, 5, 7 };
			Matches = ma;
			SelectedRow = -1;
			Turn = true;
			IsGameOver = false;
			MatchesSelected = 0;
			GameOver.Visibility = Visibility.Hidden;
			Player1Win.Visibility = Visibility.Hidden;
			Player2Win.Visibility = Visibility.Hidden;
		}

		public void RemoveMatches(int row, int matches)
		{
			if (Matches[SelectedRow - 1] >= matches)
			{
				Matches[SelectedRow - 1] = Matches[SelectedRow - 1] - matches;
				// eef do this -> Hide all match images that have been selected
				foreach (Button b in FindVisualChildren<Button>(this))
				{
					if (b.Background == Brushes.Gray)
					{ 
						b.Visibility = Visibility.Hidden;
					}
				}
			}
		}

		public void ResetRow()
		{
			foreach (Button b in FindVisualChildren<Button>(this))
			{
				if (b.Background == Brushes.Gray)
				{
					b.Background = Brushes.Black;
				}
			}
		}

		public void CheckGameOver()
		{
			int totalMatchesLeft = 0;

			foreach (int matches in Matches)
			{
				totalMatchesLeft += matches;
			}

			if (totalMatchesLeft == 1) 
			{
				IsGameOver = true;
				foreach (Button b in FindVisualChildren<Button>(this))
				{
					if ((string)b.Content == "End Turn")
					{
						b.Content = "Restart Game";
					}
				}
				// eef do this -> Display who won
				GameOver.Visibility = Visibility.Visible;
				switch (Turn)
				{
					case true:
						Player1Win.Visibility = Visibility.Visible;
						break;
					case false:
						Player2Win.Visibility = Visibility.Visible;
						break;
				}
			}
		}

		// stealing code
		// https://stackoverflow.com/questions/974598/find-all-controls-in-wpf-window-by-type
		public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
		{
			if (depObj == null) yield return (T)Enumerable.Empty<T>();
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
			{
				DependencyObject ithChild = VisualTreeHelper.GetChild(depObj, i);
				if (ithChild == null) continue;
				if (ithChild is T t) yield return t;
				foreach (T childOfChild in FindVisualChildren<T>(ithChild)) yield return childOfChild;
			}
		}
	}
}