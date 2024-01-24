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
			if ((sender as Button).Background == Brushes.Black) // if match is not selected
			{
				if ((sender as Button).Tag.ToString() == SelectedRow.ToString()) // if the match is on the same row that we already selected
				{
					MatchesSelected++;
					(sender as Button).Background = Brushes.Gray; // select the match
				}
				else
				{
					if (SelectedRow == -1) // if we havent selected a row
					{
						MatchesSelected = 1;
						SelectedRow = int.Parse((sender as Button).Tag.ToString()); // set the row 
						(sender as Button).Background = Brushes.Gray; // select the match
					}
					else
					{
						ResetRow(); // clear all selected matches
						MatchesSelected = 1;
						SelectedRow = int.Parse((sender as Button).Tag.ToString()); // set selected row
						(sender as Button).Background = Brushes.Gray; // select the match
					}
				}
			}
			else // if we click a match that is already selected
			{
				MatchesSelected--;
				(sender as Button).Background = Brushes.Black; // deselect the match
				if (MatchesSelected == 0) // if it was the last match in the row
				{
					SelectedRow = -1; // set selected row to none
				}
			}
        }

		public void OnEndTurnClick(object sender, RoutedEventArgs e)
		{
			if (IsGameOver) // check if the game is over
			{ 
				OnResetGame(); // reset the game
			}
			if (MatchesSelected > 0) // make sure that we have selected any matches
			{ 
				RemoveMatches(SelectedRow, MatchesSelected); // remove all matches that have been selected
				CheckGameOver(); // check if the game should end
				Turn = !Turn; // swap to other player's turn
				MatchesSelected = 0; // reset how many matches we have selected

				if (Turn) // change color to indicate player turn change
				{
					Background = Brushes.CornflowerBlue; 
				}
				if (!Turn)
				{
					Background = Brushes.MediumVioletRed;
				}
			}
		}

		public void OnResetGame()
		{
			foreach (Button b in FindVisualChildren<Button>(this)) // make each match visible and unselected
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
			// reset all variables and elements to their original value
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
			if (Matches[SelectedRow - 1] >= matches) // make sure that we arent removing more matches than we have
			{
				Matches[SelectedRow - 1] = Matches[SelectedRow - 1] - matches; // remove the matches
				foreach (Button b in FindVisualChildren<Button>(this)) // hide all of the matches that we had selected
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
			foreach (Button b in FindVisualChildren<Button>(this)) // deselect all matches that are selected
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
				totalMatchesLeft += matches; // find how many matches are left in the game
			}

			if (totalMatchesLeft == 1) // if we are left with one match
			{
				IsGameOver = true;
				foreach (Button b in FindVisualChildren<Button>(this)) // change the text of the end turn button
				{
					if ((string)b.Content == "End Turn")
					{
						b.Content = "Restart Game";
					}
				}
				// display which player won based on whose turn it is
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
		// basically it just looks at all of the elements on the window and returns them based on what type I want
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