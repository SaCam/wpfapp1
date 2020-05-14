using Microsoft.Win32;
using WpfApp1.data;
using WpfApp1.classBin;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for TeamDashboard.xaml
    /// </summary>
    public partial class TeamDashboard : UserControl
    {
        //Class properties
        private static MainWindow _context { get; set; } //Mainwindow instance
        private static List<Button> playerButtons { get; set; } = new List<Button>(); //List with button place holders
        private static List<Player> Players { get; set; } = new List<Player>();
        private static int teamId { get; set; }

        //Class delegates
        public delegate void Layout(Button UI); //returns nothing, but takes a button as parameter

        public TeamDashboard(MainWindow context = null)
        {
            _context = context;

            InitializeComponent();

            CreatePlayerBtnList();

            SetBtnLayout(DefaultPlayerBtnContent, playerButtons); //Set player btn layout
            SetBtnLayout(DefaultIglBtnContent, new List<Button>{ Button3 }); //Igl Button
            SetBtnLayout(DefaultTeamBtnContent, new List<Button> { TeamButton }); // Set TeamButton content

            ClearForm();
        }

        public void CreatePlayerBtnList()
            //Adds buttons from WPF to list
        {
            playerButtons.Add(Button1); //Player button 1
            playerButtons.Add(Button2);
            playerButtons.Add(Button4);
            playerButtons.Add(Button5);
        }

        public void DefaultPlayerBtnContent(Button btn)
            //Create a layout to be applied to a button
        {
            //Create placeholders
            StackPanel playerLayout = new StackPanel();
            Image ImagePlaceHolder = new Image();
            TextBlock TextBlockPlaceHolder = new TextBlock();

            //Configure placeholders
            ImagePlaceHolder.Source = new BitmapImage(new Uri("/Assets/add_player.png", UriKind.RelativeOrAbsolute));
            ImagePlaceHolder.Height = 40;
            TextBlockPlaceHolder.Text = "Add Player";

            playerLayout.Children.Add(ImagePlaceHolder);
            playerLayout.Children.Add(TextBlockPlaceHolder);

            //Button layout
            btn.Content = "";
            btn.Background = Brushes.LightGray;
            btn.Content = playerLayout;
        }

        public void SetBtnLayout(Layout layout, List<Button> buttons)
            //Apply layout to buttons in playerButton
            //use Layout delegate, so layout function has to returen an object
        {
            foreach (Button Btn in buttons)
                //Loop through buttons in list and set passed layout to it
            {
                layout(Btn);
            }
        }

        private void DefaultIglBtnContent(Button btn)
            //Igl button content
        {
            //IGL place Holder ---------------------------------------
            StackPanel IglStackPanelPlaceHolder = new StackPanel();
            Image IglImagePlaceHolder = new Image();
            Image IglImagePlaceHolderLogo = new Image();
            TextBlock IglTextBlockPlaceHolder = new TextBlock();
            Grid grid = new Grid();

            //Configure placeholders
            IglImagePlaceHolder.Source = new BitmapImage(new Uri("/Assets/add_player.png", UriKind.RelativeOrAbsolute));
            IglImagePlaceHolder.Height = 40;

            IglImagePlaceHolderLogo.Source = new BitmapImage(new Uri("/Assets/igl.png", UriKind.RelativeOrAbsolute));
            IglImagePlaceHolderLogo.Height = 20;
            IglImagePlaceHolderLogo.VerticalAlignment = VerticalAlignment.Bottom;
            IglImagePlaceHolderLogo.HorizontalAlignment = HorizontalAlignment.Right;


            IglTextBlockPlaceHolder.Text = "Add Player";
            IglTextBlockPlaceHolder.HorizontalAlignment = HorizontalAlignment.Center;

            IglStackPanelPlaceHolder.Children.Add(IglImagePlaceHolder);
            IglStackPanelPlaceHolder.Children.Add(IglTextBlockPlaceHolder);
            IglStackPanelPlaceHolder.VerticalAlignment = VerticalAlignment.Center;

            grid.Children.Add(IglStackPanelPlaceHolder);
            grid.Children.Add(IglImagePlaceHolderLogo);

            btn.Content = "";
            btn.Background = Brushes.LightGray;
            btn.Content = grid;
            btn.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            btn.VerticalContentAlignment = VerticalAlignment.Stretch;
        }

        private void DefaultTeamBtnContent(Button btn)
        {
            //Create placeholders
            StackPanel StackPanelPlaceHolderTeam = new StackPanel();
            Image ImagePlaceHolderTeam = new Image();
            TextBlock TextBlockPlaceHolderTeam = new TextBlock();

            //Configure placeholders
            ImagePlaceHolderTeam.Source = new BitmapImage(new Uri("/Assets/logo.png", UriKind.RelativeOrAbsolute));
            ImagePlaceHolderTeam.Height = 40;
            TextBlockPlaceHolderTeam.Text = "Team Logo";

            StackPanelPlaceHolderTeam.Children.Add(ImagePlaceHolderTeam);
            StackPanelPlaceHolderTeam.Children.Add(TextBlockPlaceHolderTeam);

            btn.Content = "";
            btn.Background = Brushes.LightGray;
            btn.Content = StackPanelPlaceHolderTeam;
        }

        public void ClearForm()
        {
            //Form Place Holder -------------------------------------
            Player0.Text = "";
            Player1.Text = "";
            Player2.Text = "";
            Player3.Text = "";
            Player4.Text = "";

            Age0.Text = "";
            Age1.Text = "";
            Age2.Text = "";
            Age3.Text = "";
            Age4.Text = "";

            Country0.Text = "";
            Country1.Text = "";
            Country2.Text = "";
            Country3.Text = "";
            Country4.Text = "";

            InputForm0.Visibility = Visibility.Hidden;
            InputForm1.Visibility = Visibility.Hidden;
            InputForm2.Visibility = Visibility.Hidden;
            InputForm3.Visibility = Visibility.Hidden;
            InputForm4.Visibility = Visibility.Hidden;

            //reset TeamForm
            TeamName.Text = "";
            TeamRank.Text = "";
            TeamCountry.Text = "";
            TeamForm.Visibility = Visibility.Hidden;
        }

        public void ResetPlaceHolders(object sender, RoutedEventArgs e)
            //Reset all buttons
        {
            SetBtnLayout(DefaultPlayerBtnContent, playerButtons); //Reset player btns to default
            SetBtnLayout(DefaultIglBtnContent, new List<Button> { Button3 }); //Reset Igl button to default
            SetBtnLayout(DefaultTeamBtnContent, new List<Button> { TeamButton }); // Set TeamButton content

            ClearForm(); //Clears the forms
        }

        private void CreateTeamBackground(object sender, RoutedEventArgs e)
        {
            //Open File Dialog to select picture
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                Button BtnClicked = e.Source as Button;
                BtnClicked.Content = "";

                //create new image object
                Image NewImage = new Image();
                //Set image object attributes
                NewImage.Source = new BitmapImage(new Uri(op.FileName, UriKind.RelativeOrAbsolute));
                NewImage.Stretch = Stretch.Fill;
                //add to button
                BtnClicked.Content = NewImage;
                BtnClicked.Background = Brushes.Transparent;

            }

            //Show TeamInputForm
            TeamForm.Visibility = Visibility.Visible;


        }

        private void ShowInputForm(object sender, RoutedEventArgs e)
        {
            //to get name of element
            //var mouseWasDownOn = e.Source as FrameworkElement;
            //if (mouseWasDownOn != null)
            //{
            //    string elementName = mouseWasDownOn.Name;
            //    Console.WriteLine(elementName);
            //}

            //Open File Dialog to select picture
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                //Set new content on button
                // define button as button
                Button BtnClicked = e.Source as Button;
                //clear content
                BtnClicked.Content = "";
                //Create new stackpanel
                //var NewStackPanel = new StackPanel();
                //create new image object
                Image NewImage = new Image();
                //Set image object attributes
                NewImage.Source = new BitmapImage(new Uri(op.FileName, UriKind.RelativeOrAbsolute));
                NewImage.Stretch = Stretch.Fill;
                //add to stackpanel object
                //NewStackPanel.Children.Add(NewImage);
                //add to button
                BtnClicked.Content = NewImage;
                BtnClicked.Background = Brushes.Transparent;
            }

            //Show correct input form
            if (sender != null)
            {
                var element = (UIElement)e.Source;
                int c = Grid.GetColumn(element);
                switch (c)
                {
                    case 0:
                        InputForm0.Visibility = Visibility.Visible;
                        break;
                    case 1:
                        InputForm1.Visibility = Visibility.Visible;
                        break;
                    case 2:
                        InputForm2.Visibility = Visibility.Visible;
                        break;
                    case 3:
                        InputForm3.Visibility = Visibility.Visible;
                        break;
                    case 4:
                        InputForm4.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

        private void SetPlayerData()
            //asign data from form to player object
        {
            Players.Clear();

            Players.Add(new Player(Player0.Text, Age0.Text, Country0.Text, teamId));
            Players.Add(new Player(Player1.Text, Age1.Text, Country1.Text, teamId));
            Players.Add(new Player(Player2.Text, Age2.Text, Country2.Text, teamId));
            Players.Add(new Player(Player3.Text, Age3.Text, Country3.Text, teamId));
            Players.Add(new Player(Player4.Text, Age4.Text, Country4.Text, teamId));
        }

        private void SaveForm()
            //Save team and players to database
        {
            //add team
            data.DbHandler.StoreTable("Team", "name,rank,country", String.Format("'{0}',{1},'{2}'", TeamName.Text, TeamRank.Text, TeamCountry.Text));

            //get Team id 
            teamId = data.DbHandler.Qid(TeamName.Text);

            //Add players
            foreach (Player player in Players)
            {
                data.DbHandler.StoreTable("Player", "name,age,country,team_id", $"'{player.Name}',{player.Age},'{player.Country}',{teamId}");
            }
        }

        private void Submit(object sender, RoutedEventArgs e)
            //When submit is pressed perform these actions
        {
            SetPlayerData();

            SaveForm();

            ResetPlaceHolders(sender, e);

            _context.ReinitMenu();
        }
    }
}
