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
using System.Windows.Shapes;

namespace WpfAppAT_Course_work
{
    /// <summary>
    /// Логика взаимодействия для ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        public ErrorWindow(string label, string info)
        {
            InitializeComponent();
            LabelTB.Text = label;
            AboutTB.Text = info;
        }

        private void CloseGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            this.DialogResult = true;
        }
    }
}
