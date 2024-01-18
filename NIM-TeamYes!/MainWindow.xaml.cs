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
			if (true) // !sender.selected
			{
				if ((sender as Button).Tag.ToString() == SelectedRow.ToString()) // sender.row == selectedrow
				{
					MatchesSelected++;
					// sender.selected = true
				}
				else
				{
					if (true) // selected row == -1
					{
						MatchesSelected = 1;
						// set selected row
						// sender.selected = true
					}
					else
					{
						// call resetrow(selected row)
						MatchesSelected = 1;
						// set selected row
						// sender.selected = true
					}
				}
			}
			else
			{
				MatchesSelected--;
				// sender.selected = false
				if (MatchesSelected == 0)
				{
					SelectedRow = -1;
				}
			}
        }
	}
}