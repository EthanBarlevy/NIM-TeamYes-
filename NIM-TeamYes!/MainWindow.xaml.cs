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
			if ((sender as Button).Background == Brushes.Black) // !sender.background == black
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
						// call resetrow(selected row)
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
			RemoveMatches(SelectedRow, MatchesSelected);
			CheckGameOver();
			Turn = !Turn;

			if (Turn)
			{
				Background = Brushes.CornflowerBlue; 
			}
			if (!Turn)
			{
				Background = Brushes.MediumVioletRed;
			}

			// eef do this -> Change color of elements, based on turn, to indicate that it is the next players turn.
		}

		public void OnResetGame()
		{

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
						b.IsEnabled = false;
					}
				}
			}
		}

		public void ResetRow()
		{

		}

		public void CheckGameOver()
		{
			int totalMatchesLeft = 0;

			foreach (int matches in Matches)
			{
				totalMatchesLeft += matches;
			}

			if (totalMatchesLeft == 0) 
			{
				IsGameOver = true;

				
				// eef do this -> Display who won
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