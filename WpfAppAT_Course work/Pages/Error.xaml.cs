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
    /// Логика взаимодействия для Error.xaml
    /// </summary>
    public partial class Error : Page
    {
        public Error()
        {
            InitializeComponent();
            LabelAbout.Content = StaticAnyWhere.Error;
        }

        private void LabelOpen_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            if (openDialog.ShowDialog().Value)
            {

            }
        }

        private void LabelCreate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StaticAnyWhere.Rootclass.GoTo("Pages/BuildingAnAutomaton.xaml");
        }
    }
}
