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
using WpfAppAT_Course_work.My_elements;
using WpfAppAT_Course_work.Pages;

namespace WpfAppAT_Course_work
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
            Frame FrameMain = new Frame();
            FrameMain.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            FrameMain.NavigationService.Navigate(new Uri("Pages/WelcomePage.xaml", UriKind.Relative));

            TabControlMain.Items.Add(new TabItem
            {
                Header = new TextBlock { Text = "Новая вкладка" },
                Content = FrameMain  
            });

            StaticAnyWhere.Rootclass = this;
        }


        public void GoTo(string url, string name = "Новая вкладка")
        {


            Frame FrameMain = new Frame();
            FrameMain.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            FrameMain.NavigationService.Navigate(new Uri(url, UriKind.Relative));

            TabControlMain.Items.Add(new TabItem
            {
                Header = new TextBlock { Text = name }, // установка заголовка вкладки
                Content = FrameMain  // notesList // установка содержимого вкладки
            });


        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void AboutBtn_Click(object sender, RoutedEventArgs e)
        {
            StaticAnyWhere.HelpURL = "About.xps";
            StaticAnyWhere.Rootclass.GoTo("Pages/ViewHelp.xaml");
        }

        private void AddIem_Click(object sender, RoutedEventArgs e)
        {
            Frame FrameMain = new Frame();
            FrameMain.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            FrameMain.NavigationService.Navigate(new Uri("Pages/BuildingAnAutomaton.xaml", UriKind.Relative));
            TabControlMain.Items.Add(new TabItem
            {
                Header = new TextBlock { Text = "Новая вкладка" }, 
                Content = FrameMain  
            });
            //StaticAnyWhere.Rootclass.GoTo("Pages/BuildingAnAutomaton.xaml");
        }

        private void AboutApp_Click(object sender, RoutedEventArgs e)
        {
            StaticAnyWhere.HelpURL = "About.xps";
            StaticAnyWhere.Rootclass.GoTo("Pages/ViewHelp.xaml");
        }

        private void ClodeItem_Click(object sender, RoutedEventArgs e)
        {
            TabItem current = (TabItem)TabControlMain.SelectedItem;
            current.Visibility = Visibility.Collapsed;
            TabControlMain.SelectedIndex = TabControlMain.AlternationCount;

        }

        private void TestWPF_Click(object sender, RoutedEventArgs e)
        {
            DefinitionAutomaton definitionAutomaton = new DefinitionAutomaton();

            definitionAutomaton.Type = 2;
            definitionAutomaton.Q = new string[] { "9", "0", "1", "2", "3", "4", "5", "6", "7", "8"};
            definitionAutomaton.Sigma = new string[] { "ε", "w", "e", "b", "a", "y", "q", "r", "t", "u", "i", "o", "p", "s", "d", "f", "g", "h", "j", "k", "l", "z", "x", "c", "v", "n", "m", ",", ".", "?", ":", ";", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            definitionAutomaton.Q0 = "9";
            definitionAutomaton.F = new string[] { "4", "8" };
            definitionAutomaton.ItIsTest = true;
            definitionAutomaton.Tabel = new string[] {
                "0 1 9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9", "9",
                "", "", "5", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "2", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "3", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "4", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "6", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "7", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "8", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
            };

            StaticAnyWhere.Def = definitionAutomaton;

            Frame FrameMain = new Frame();
            FrameMain.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            FrameMain.NavigationService.Navigate(new Uri("Pages/SettingTheTransitionFunction.xaml", UriKind.Relative));
            TabControlMain.Items.Add(new TabItem
            {
                Header = new TextBlock { Text = "Тест Ebay и web с e" },
                Content = FrameMain
            });
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            DefinitionAutomaton definitionAutomaton = new DefinitionAutomaton();

            definitionAutomaton.Type = 2;
            definitionAutomaton.Q = new string[] { "q0", "q1", "q2", "q3", "q4", "q5" };
            definitionAutomaton.Sigma = new string[] {"ε", "+", "-", ".", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            definitionAutomaton.Q0 = "q0";
            definitionAutomaton.F = new string[] { "q5"};
            definitionAutomaton.ItIsTest = true;
            definitionAutomaton.Tabel = new string[] {
                "q1", "q1", "q1", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "q2", "q1 q4", "q1 q4", "q1 q4", "q1 q4", "q1 q4", "q1 q4", "q1 q4", "q1 q4", "q1 q4", "q1 q4",
                "", "", "", "", "q3", "q3", "q3", "q3", "q3", "q3", "q3", "q3", "q3", "q3",
                "q5", "", "", "", "q3", "q3", "q3", "q3", "q3", "q3", "q3", "q3", "q3", "q3",
                "", "", "", "q3", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", ""
            };

            StaticAnyWhere.Def = definitionAutomaton;

            Frame FrameMain = new Frame();
            FrameMain.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            FrameMain.NavigationService.Navigate(new Uri("Pages/SettingTheTransitionFunction.xaml", UriKind.Relative));
            TabControlMain.Items.Add(new TabItem
            {
                Header = new TextBlock { Text = "Тест цифры" },
                Content = FrameMain
            });
        }

        private void WebEbayNFA_Click(object sender, RoutedEventArgs e)
        {
            DefinitionAutomaton definitionAutomaton = new DefinitionAutomaton();

            definitionAutomaton.Type = 1;
            definitionAutomaton.Q = new string[] {"1", "2", "3", "4", "5", "6", "7", "8" };
            definitionAutomaton.Sigma = new string[] { "w", "e", "b", "a", "y", "q", "r", "t", "u", "i", "o", "p", "s", "d", "f", "g", "h", "j", "k", "l", "z", "x", "c", "v", "n", "m", ",", ".", "?", ":", ";", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            definitionAutomaton.Q0 = "1";
            definitionAutomaton.F = new string[] { "4", "8" };
            definitionAutomaton.ItIsTest = true;
            definitionAutomaton.Tabel = new string[] {
                "1 2", "1 5", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1",
                "", "3", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
                "", "", "4", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
                "", "", "6", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
                "", "", "", "7", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
                "", "", "", "", "8", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 
            };

            StaticAnyWhere.Def = definitionAutomaton;

            Frame FrameMain = new Frame();
            FrameMain.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            FrameMain.NavigationService.Navigate(new Uri("Pages/SettingTheTransitionFunction.xaml", UriKind.Relative));
            TabControlMain.Items.Add(new TabItem
            {
                Header = new TextBlock { Text = "Тест Ebay и web без e" },
                Content = FrameMain
            });
        }
    }
}
