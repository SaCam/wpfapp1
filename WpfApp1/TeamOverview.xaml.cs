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

using WpfApp1.data;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for TeamOverview.xaml
    /// </summary>
    public partial class TeamOverview : UserControl
    {
        public string _teamName {get; set;}
        public TeamOverview(string teamName)
        {
            InitializeComponent();

            _teamName = teamName;
            int team_id = DbHandler.Qid(_teamName);
            List<string> teamData = DbHandler.QPlayers(team_id);
            foreach (string str in teamData)
            {
                Console.WriteLine(str);
            }
        }
    }
}
