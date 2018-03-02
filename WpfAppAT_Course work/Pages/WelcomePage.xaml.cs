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
using WpfAppAT_Course_work.Classes;

namespace WpfAppAT_Course_work.Pages
{
    /// <summary>
    /// Логика взаимодействия для WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : Page
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        private void createAutomatonBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/BuildingAnAutomaton.xaml", UriKind.Relative));
        }

        private void openAutomatonBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            if (openDialog.ShowDialog().Value)
            {

            }
        }

        private void helpAutomatonBtn_Click(object sender, RoutedEventArgs e)
        {
            StaticAnyWhere.HelpURL = "HelpA.xps";
            StaticAnyWhere.Rootclass.GoTo("Pages/ViewHelp.xaml", "Справка");
            // DFAGraphNode<string> a = new DFAGraphNode<string>("A", true, false);

        }
    }
}
