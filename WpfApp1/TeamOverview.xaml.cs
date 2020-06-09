using System;
using System.Collections.Generic;
using System.Drawing;
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

using WpfApp1.data;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for TeamOverview.xaml
    /// </summary>
    public partial class TeamOverview : UserControl
    {
        public List<System.Windows.Controls.Image> PlayerPlaceHolders { get; set; } = new List<System.Windows.Controls.Image>();
        public List<StackPanel> playerInfo { get; set; } = new List<StackPanel>();
        public string _teamName {get; set;}
        public Dictionary<string, object> _teamData { get; set; }
        public List<Dictionary<string, object>> playerData { get; set; }
        public MainWindow _context { get; set; }
        public UserControl screen { get; set; }

        public TeamOverview(string teamName, MainWindow context = null)
        {
            InitializeComponent();

            _context = context;
            screen = new TeamDashboard(_context);

            PopulatePlaceHolders();
            populateStackPanels();

            _teamName = teamName;

            _teamData = DbHandler.QTeam(_teamName);
            playerData = DbHandler.QPlayers((int)_teamData["teamId"]);

            SetPlayerData();
            SetBackground();
        }

        private void SetBackground()
        {
            System.Drawing.Image _img = DbHandler.ByteArrayToImage((byte[])_teamData["image"]);
            teamImg.Source = ConvertImgControl(_img);
        }

        private void PopulatePlaceHolders()
        {
            PlayerPlaceHolders.Add(player0);
            PlayerPlaceHolders.Add(player1);
            PlayerPlaceHolders.Add(player2);
            PlayerPlaceHolders.Add(player3);
            PlayerPlaceHolders.Add(player4);
        }

        private void populateStackPanels()
        {
            playerInfo.Add(info0);
            playerInfo.Add(info1);
            playerInfo.Add(info2);
            playerInfo.Add(info3);
            playerInfo.Add(info4);
        }

        private void SetPlayerData()
            //sets player images from data base to Imageplace holders
        {
            var i = 0;
            foreach (Dictionary<string, object> dict in playerData)
            {
                //creat UIelements
                TextBlock name = new TextBlock();
                name.Text = $"Name:      {dict["name"]}";
                TextBlock age = new TextBlock();
                age.Text = $"Age:         {dict["age"]}";
                TextBlock country = new TextBlock();
                country.Text = $"Country:   {dict["country"]}";
                //playerPlaceHolders[i].Source = dict["image"];
                playerInfo[i].Children.Clear();

                playerInfo[i].Children.Add(name);
                playerInfo[i].Children.Add(age);
                playerInfo[i].Children.Add(country);

                System.Drawing.Image _img = DbHandler.ByteArrayToImage((byte[])dict["image"]);
                PlayerPlaceHolders[i].Source = ConvertImgControl(_img);
                i++;
            }
        }

        private System.Windows.Media.ImageSource ConvertImgControl(System.Drawing.Image _img)
            //method to convert Image type from Drawing.Image to Windows.Controls.Image
        {
            System.Windows.Controls.Image img = new System.Windows.Controls.Image();

            //convert System.Drawing.Image to WPF image
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(_img);
            IntPtr hBitmap = bmp.GetHbitmap();
            System.Windows.Media.ImageSource WpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, 
                IntPtr.Zero, 
                Int32Rect.Empty, 
                BitmapSizeOptions.FromEmptyOptions());

            return WpfBitmap;
        }

        private void DeleteTeam(object sender, RoutedEventArgs e)
        {
            DbHandler.DeleteTeam((int)_teamData["teamId"]);
            _context.Content.Children.Clear();
            _context.Content.Children.Add(screen);
            _context.ReinitMenu();
        }
    }
}
