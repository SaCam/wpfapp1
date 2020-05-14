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
        //Class properties
        public static List<ItemMenu> itemMenuList { get; set; } = new List<ItemMenu>();

        public MainWindow()
        {
            InitializeComponent();

            //Set frame to welcome content
            Content.Children.Add(new Welcome());

            //initialize Menu
            CreateItems();
            InitMenu();
        }

        public void ReinitMenu()
            //Clears Items from Menu
        {
            itemMenuList.Clear();

            CreateItems();

            Menu.Children.Clear();

            InitMenu();
        }

        public void CreateItems()
            //Creates a list of ItemMenu's
        {
            var menuTeam = new List<SubItem>();
            var menuTournaments = new List<SubItem>();
            var menuHighlights = new List<SubItem>();

            //Hardcoded Dashboard 
            var menuDashboard = new List<SubItem>();
            menuDashboard.Add(new SubItem("Add Team", new TeamDashboard(this)));
            menuDashboard.Add(new SubItem("Add Tournament", new TournamentDashboard()));
            menuDashboard.Add(new SubItem("Add Highlights", new HighlightDashboard()));
            ItemMenu item0 = new ItemMenu("Dashboard", menuDashboard);

            //Loop through all teams <String> returned by Query
            foreach (string Team in data.DbHandler.QTable("name", "Team"))
            {
                menuTeam.Add(new SubItem(Team)); //Add to List
            }
            ItemMenu item1 = new ItemMenu("Teams", menuTeam); //create variable of ItemMenu Object

            //Query Tournaments table and populate XAML element
            foreach (string Team in data.DbHandler.QTable("name", "Tournament"))
            {
                menuTournaments.Add(new SubItem(Team)); //Add to List
            }
            ItemMenu item2 = new ItemMenu("Tournaments", menuTournaments);

            //Query Highlight table and populate XAML element (Table not available so query team table for now)
            foreach (string Tour in data.DbHandler.QTable("name", "Team"))
            {
                menuHighlights.Add(new SubItem(Tour));
            }
            ItemMenu item3 = new ItemMenu("Highlights", menuHighlights);

            itemMenuList.Add(item0);
            itemMenuList.Add(item1);
            itemMenuList.Add(item2);
            itemMenuList.Add(item3);
        }

        public void InitMenu()
            //Adds items from itemMenuList to UI Menu
        {
            foreach (var item in itemMenuList)
                //Add to UI at x:Name="Menu"
            {
                Menu.Children.Add(new UserControlMenuItem(item, this));
            }
        }

        internal void SwitchScreen(object sender,string TitleText)
            //function to change content in client is linked to dynamically created elements
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
        private void CloseButton_Click(object sender, RoutedEventArgs e)
            //Close button function
        {
            Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
            //Minimize button
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
            //Maximize button
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

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
            //draggable window
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
