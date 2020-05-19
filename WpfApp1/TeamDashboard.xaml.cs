using Microsoft.Win32;
using WpfApp1.data;
using WpfApp1.layout;
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
using System.Drawing.Imaging;

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
        private static Dictionary<string, Player> players { get; set; } = new Dictionary<string, Player>(); //Dictionary with players
        private static int teamId { get; set; }

        //Class delegates
        public delegate void Layout(Button UI); //returns nothing, but takes a button as parameter

        public TeamDashboard(MainWindow context = null)
        {
            _context = context;

            InitializeComponent();

            CreatePlayerBtnList();

            SetBtnLayout(DefaultLayout.DefaultPlayerBtnContent, playerButtons); //Set player btn layout
            SetBtnLayout(DefaultLayout.DefaultIglBtnContent, new List<Button>{ Button2 }); //Igl Button
            SetBtnLayout(DefaultLayout.DefaultTeamBtnContent, new List<Button> { TeamButton }); // Set TeamButton content

            PopulatePlayerDict();

            ClearForm();
        }

        public void CreatePlayerBtnList()
            //Adds buttons from WPF to list
        {
            playerButtons.Add(Button0); //Player button 1
            playerButtons.Add(Button1);
            playerButtons.Add(Button3);
            playerButtons.Add(Button4);
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
            SetBtnLayout(DefaultLayout.DefaultPlayerBtnContent, playerButtons); //Reset player btns to default
            SetBtnLayout(DefaultLayout.DefaultIglBtnContent, new List<Button> { Button2 }); //Reset Igl button to default
            SetBtnLayout(DefaultLayout.DefaultTeamBtnContent, new List<Button> { TeamButton }); // Set TeamButton content

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
            string btnName = ((Button)sender).Name;
            char lastChar = btnName[btnName.Length - 1];
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
                //add byte[] to player instance
                players[$"player{lastChar}"].Img = DbHandler.ImageToByteArray(System.Drawing.Image.FromFile(op.FileName));
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

        private void LostFocusText(object sender, RoutedEventArgs e)
            //When Focus is lost on a textbox, the text will be set to a player instance.
        {
            UIElementCollection stackpanel = ((StackPanel)sender).Children;
            TextBox name = (TextBox)stackpanel[1];
            TextBox age = (TextBox)stackpanel[3];
            TextBox country = (TextBox)stackpanel[5];

            string stackPanelName = ((StackPanel)sender).Name;
            char lastChar = stackPanelName[stackPanelName.Length - 1];

            players[$"player{lastChar}"].Name = name.Text;
            players[$"player{lastChar}"].Age = age.Text;
            players[$"player{lastChar}"].Country = country.Text;
        }

        private void PopulatePlayerDict()
            //Populate the players Dict with Player instances.
        {
            players.Clear();
            for (var i = 0; i < 5; i++)
                //Loop a total of 5 times to create 5 instances.
            {
                players.Add($"player{i}", new Player());
            }
        }

        private void SaveData()
            //Save team and players to database
        {
            //add team
            DbHandler.StoreTable("Team", "name,rank,country", String.Format("'{0}',{1},'{2}'", TeamName.Text, TeamRank.Text, TeamCountry.Text));

            //get Team id 
            teamId = DbHandler.Qid(TeamName.Text);

            //Add players
            foreach (KeyValuePair<string, Player> player in players)
                //Saves all player instances in database. An empty instance will result in an error
            {
                DbHandler.StorePlayer(player.Value.Name, Convert.ToInt32(player.Value.Age), teamId, player.Value.Country, (byte[])player.Value.Img);
            }
        }

        private void Submit(object sender, RoutedEventArgs e)
            //When submit is pressed perform these actions
        {
            SaveData();

            ResetPlaceHolders(sender, e);

            _context.ReinitMenu();
        }
    }
}
