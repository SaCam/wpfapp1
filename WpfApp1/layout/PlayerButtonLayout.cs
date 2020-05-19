using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using System.Windows.Media;
using System.Windows;

namespace WpfApp1.layout
{
    class DefaultLayout
    {
        public static void DefaultPlayerBtnContent(Button btn)
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

        public static void DefaultIglBtnContent(Button btn)
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

        public static void DefaultTeamBtnContent(Button btn)
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
    }
}
