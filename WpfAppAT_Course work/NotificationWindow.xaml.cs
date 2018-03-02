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
    /// Логика взаимодействия для NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow : Window
    {
        public bool NewTab
        {
            get; set;
        }
        public NotificationWindow()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            CurrentTB.Foreground = new SolidColorBrush(Color.FromArgb(255, 29, 185, 84));

        }

        private void NewTabTB_MouseEnter(object sender, MouseEventArgs e)
        {
            NewTabTB.Foreground = new SolidColorBrush(Color.FromArgb(255, 29, 185, 84));
        }

        private void CurrentTB_MouseLeave(object sender, MouseEventArgs e)
        {
            CurrentTB.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }

        private void NewTabTB_MouseLeave(object sender, MouseEventArgs e)
        {
            NewTabTB.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }

        private void CurrentTB_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NewTab = false;
            this.DialogResult = true;
        }

        private void NewTabTB_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NewTab = true;
            this.DialogResult = true;
        }
    }
}
