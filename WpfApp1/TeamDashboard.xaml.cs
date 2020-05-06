using Microsoft.Win32;
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
        MainWindow _context;

        public TeamDashboard(MainWindow context = null)
        {
            InitializeComponent();
            SetPlaceHolders();

            _context = context;
        }

        private void SetPlaceHolders()
        {
            // Player Button Place holders --------------------------------------
            List<Button> PlayerButtons = new List<Button>();
            PlayerButtons.Add(Button1);
            PlayerButtons.Add(Button2);
            PlayerButtons.Add(Button4);
            PlayerButtons.Add(Button5);

            //Loop through buttons in list
            foreach (Button Btn in PlayerButtons)
            {
                //Create placeholders
                StackPanel StackPanelPlaceHolder = new StackPanel();
                Image ImagePlaceHolder = new Image();
                TextBlock TextBlockPlaceHolder = new TextBlock();

                //Configure placeholders
                ImagePlaceHolder.Source = new BitmapImage(new Uri("/Assets/add_player.png", UriKind.RelativeOrAbsolute));
                ImagePlaceHolder.Height = 40;
                TextBlockPlaceHolder.Text = "Add Player";

                StackPanelPlaceHolder.Children.Add(ImagePlaceHolder);
                StackPanelPlaceHolder.Children.Add(TextBlockPlaceHolder);

            
                Btn.Content = "";
                Btn.Background = Brushes.LightGray;
                Btn.Content = StackPanelPlaceHolder;
            }

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

            Button3.Content = "";
            Button3.Background = Brushes.LightGray;
            Button3.Content = grid;
            Button3.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            Button3.VerticalContentAlignment = VerticalAlignment.Stretch;

            //Team Button Place Holder -----------------------------------------
            TeamButton.Content = "";

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

            TeamButton.Content = "";
            TeamButton.Background = Brushes.LightGray;
            TeamButton.Content = StackPanelPlaceHolderTeam;

            //Form Place Holder -------------------------------------

            List<StackPanel> PlayerForms = new List<StackPanel>();
            PlayerForms.Add(InputForm0);
            PlayerForms.Add(InputForm1);
            PlayerForms.Add(InputForm2);
            PlayerForms.Add(InputForm3);
            PlayerForms.Add(InputForm4);

            foreach (StackPanel stack in PlayerForms)
            {
                stack.Children.Clear();

                TextBlock TextBlockName = new TextBlock();
                TextBlockName.Text = "Name:";
                TextBox TextBoxName = new TextBox();

                TextBlock TextBlockAge = new TextBlock();
                TextBlockAge.Text = "Age:";
                TextBox TextBoxAge = new TextBox();

                TextBlock TextBlockNationality = new TextBlock();
                TextBlockNationality.Text = "Nationality:";
                TextBox TextBoxNationality = new TextBox();

                stack.Children.Add(TextBlockName);
                stack.Children.Add(TextBoxName);
                stack.Children.Add(TextBlockAge);
                stack.Children.Add(TextBoxAge);
                stack.Children.Add(TextBlockNationality);
                stack.Children.Add(TextBoxNationality);

                stack.Visibility = Visibility.Hidden;
            }

        }

        public void ResetPlaceHolders(object sender, RoutedEventArgs e)
        {
            SetPlaceHolders();
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
    }
}
