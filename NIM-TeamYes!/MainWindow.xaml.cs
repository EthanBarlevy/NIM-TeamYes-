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
	}
}