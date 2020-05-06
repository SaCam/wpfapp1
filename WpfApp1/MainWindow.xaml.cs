using BeautySolutions.View.ViewModel;
using DropDownMenu;

using System;
using System.IO;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Instance variables
            string cwd = Directory.GetCurrentDirectory();

            //Set frame to welcome content
            Content.Children.Add(new Welcome());

            //create List of SubItem Objects
            var menuTeam = new List<SubItem>();
            //Loop through all teams <String> returned by Query
            foreach (string Team in data.DbHandler.QTable("name", "Team"))
            {
                menuTeam.Add(new SubItem(Team)); //Add to List
            }
            var item0 = new ItemMenu("Teams", menuTeam); //create variable of ItemMenu Object


            //Query Tournaments table and populate XAML element
            var menuTournaments = new List<SubItem>();
            foreach (string Team in data.DbHandler.QTable("name", "Tournament"))
            {
                menuTournaments.Add(new SubItem(Team)); //Add to List
            }
            var item1 = new ItemMenu("Tournaments", menuTournaments);

            //Query Highlight table and populate XAML element (Table not available so query team table for now)
            var menuHighlights = new List<SubItem>();
            foreach (string Tour in data.DbHandler.QTable("name", "Team"))
            {
                menuHighlights.Add(new SubItem(Tour));
            }
            var item2 = new ItemMenu("Highlights", menuHighlights);

            //Hardcoded Dashboard 
            var menuDashboard = new List<SubItem>();
            menuDashboard.Add(new SubItem("Add Team", new TeamDashboard()));
            menuDashboard.Add(new SubItem("Add Tournament", new TournamentDashboard()));
            menuDashboard.Add(new SubItem("Add Highlights", new HighlightDashboard()));
            var item3 = new ItemMenu("Dashboard", menuDashboard);

            //Add to UI at x:Name="Menu"
            Menu.Children.Add(new UserControlMenuItem(item3, this));
            Menu.Children.Add(new UserControlMenuItem(item0, this));
            Menu.Children.Add(new UserControlMenuItem(item1, this));
            Menu.Children.Add(new UserControlMenuItem(item2, this));
        }

        //function to change content in client is linked to dynamically created elements
        internal void SwitchScreen(object sender,string TitleText)
        {
            var screen = ((UserControl)sender);
            if (screen != null)
            {
                TextTitle.Text = TitleText.ToUpper();
                TextTitle.FontSize = 30;
                Content.Children.Clear();
                Content.Children.Add(screen);
            }
        }

        //Window control buttons title bar
        //Close button function
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //Minimize button
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        //Maximize button
        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
        }
        //draggable window
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
