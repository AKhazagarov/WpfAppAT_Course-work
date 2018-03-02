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
    /// Логика взаимодействия для BuildingAnAutomaton.xaml
    /// </summary>
    public partial class BuildingAnAutomaton : Page
    {

        private DefinitionAutomaton def = new DefinitionAutomaton();


        public BuildingAnAutomaton()
        {
            InitializeComponent();
        }

        private void buildingDeltaBtn_Click(object sender, RoutedEventArgs e)
        {
            bool state = true;

            
            if (state != false)
            {
                if (enterQTb.Text.Length >= 1)
                {
                    def.Q = StaticAnyWhere.prepareStringArr(enterQTb.Text);
                }
                else
                {
                    state = false;
                    ErrorWindow errorWindow = new ErrorWindow("Ошибка ввода!", "Должно быть хоть одно начальное состояние ");
                    errorWindow.ShowDialog();
                }
            }

            if (state != false)
            {
                if ((enterEpsTb.Text.Length >= 1) || (enterDeltaRadioBtn.IsChecked == true))
                {
                    def.Sigma = StaticAnyWhere.prepareStringArr(enterEpsTb.Text);
                }
                else
                {
                    state = false;
                    ErrorWindow errorWindow = new ErrorWindow("Ошибка ввода!", "Должно быть хоть одно множество входных символов ");
                    errorWindow.ShowDialog();
                }
            }

            if (state != false)
            {
                if (StaticAnyWhere.prepareStringArr(enterqTb.Text).Length == 1)
                {
                    if (StaticAnyWhere.occurrence(def.Q, StaticAnyWhere.prepareStringArr(enterqTb.Text)))
                    {
                        def.Q0 = StaticAnyWhere.prepareString(enterqTb.Text);
                    }
                    else
                    {
                        state = false;
                        ErrorWindow errorWindow = new ErrorWindow("Ошибка ввода!", "Начальное состояние (q0) должно быть одним из конечного множества состояний (Q)");
                        errorWindow.ShowDialog();
                    }
                }
                else
                {
                    state = false;
                    ErrorWindow errorWindow = new ErrorWindow("Ошибка ввода!", "Начальное состояние может быть только одно");
                    errorWindow.ShowDialog();
                }
            }

            if (state != false)
            {
                if (StaticAnyWhere.occurrence(def.Q, StaticAnyWhere.prepareStringArr(enterFTb.Text)))
                {
                    def.F = StaticAnyWhere.prepareStringArr(enterFTb.Text);
                }
                else
                {
                    state = false;
                    ErrorWindow errorWindow = new ErrorWindow("Ошибка ввода!", "Множество заключительных состояний (F) должно быть подмножеством конечного множества состояний (Q)");
                    errorWindow.ShowDialog();
                }
            }

            if (state != false)
            {
                if (enterDeltaRadioBtn.IsChecked == true)
                {
                    def.Type = 2;
                    def.Sigma = StaticAnyWhere.prepareStringArr("ε " + enterEpsTb.Text);
                }
                else
                {
                    def.Type = 3;
                }
            }
            
            

            if (state == true)
            {
                StaticAnyWhere.Def = def;
                this.NavigationService.Navigate(new Uri("Pages/SettingTheTransitionFunction.xaml", UriKind.Relative));
            }
        }

        private void randomAutomatoinBtn_Click(object sender, RoutedEventArgs e)
        {

            
            
            enterQTb.Text = "{q0, q1, q2, q3, q4, q5}";
            enterEpsTb.Text = "{+ - . 0 1 2 3 4 5 6 7 8 9}";
            enterqTb.Text = "q0";
            enterFTb.Text = "{q5}";
            /*

            enterQTb.Text = "{1 2 3 4 5 6 7 8}";
            enterEpsTb.Text = "{w e b a y q r t u i o p s d f g h j k l z x c v n m . , * ( ) + = ! @ # $ % ^ & : ;}";
            enterqTb.Text = "1";
            enterFTb.Text = "{4 8}";


            *

            enterQTb.Text = "{q0, q1, q2}";
            enterEpsTb.Text = "{0, 1}";
            enterqTb.Text = "q0";
            enterFTb.Text = "{q2}";
            */

        }

        private void HelpBtn_Click(object sender, RoutedEventArgs e)
        {
            StaticAnyWhere.HelpURL = "HelpA.xps";
            StaticAnyWhere.Rootclass.GoTo("Pages/ViewHelp.xaml");
        }

        private void CleanBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            CleanBtn.Foreground = new SolidColorBrush(Color.FromArgb(255, 29, 185, 84));
        }

        private void CleanBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            CleanBtn.Foreground = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
        }
    }
}
