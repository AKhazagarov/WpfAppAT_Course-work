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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppAT_Course_work.Classes;
using static WpfAppAT_Course_work.Classes.StaticAnyWhere;

namespace WpfAppAT_Course_work.Pages
{
    /// <summary>
    /// Логика взаимодействия для SettingTheTransitionFunction.xaml
    /// </summary>
    public partial class SettingTheTransitionFunction : Page
    {
        Automaton automatonGraph;                   // автомат
        private DefinitionAutomaton def;            // первое определение автомата
        List<TextBox> C = new List<TextBox>();      // список textBox


        public SettingTheTransitionFunction()
        {
            InitializeComponent();
       //     AutTB.Text = "Ваш автомат A = (" + def.Q;
            prepar();
        }

        




        #region Обработчие нажатия кнопок


        private void ClearTransitionFunctionBtn_Click(object sender, RoutedEventArgs e)         // Очистить таблицу переходов
        {
            CleanTB();
        }           

        private void RandomTransitionFunctionBtn_Click(object sender, RoutedEventArgs e)        // Задать случайную таблицу переходов
        {
            Random random = new Random();
            CleanTB();
            int z = 0;
            for (int i = 0, counter = 0; i < def.Q.Length; i++)
                for (int j = 0; j < def.Sigma.Length; j++, counter++)
                {
                    int zd = random.Next(def.Q.Length - 1);
                    for (z = 0; z < zd; z++)
                    {
                        if (z == zd - 1)
                        {
                            C[counter].Text = C[counter].Text + def.Q[z];
                        }
                        else
                        {
                            C[counter].Text = C[counter].Text + def.Q[z] + ",";
                        }

                    }

                    if (z == 0)
                    {
                        C[counter].Text = def.Q[random.Next(def.Q.Length)];                      

                    }
                }
        }

        private void AddTransFuncBtn_Click(object sender, RoutedEventArgs e)                    // Добавить таблицу переходов
        {
            string[,][] arrayTransition = new string[def.Q.Length, def.Sigma.Length][];

            if (AddTransitionFunction(ref arrayTransition))
            {
                CreateAutomatonGraph(arrayTransition);
            }
            else
            {
                ErrorWindow errorWindow = new ErrorWindow("Ошибка ввода!", "Проверьте правильность введённых вами данных");
                errorWindow.ShowDialog();
            }


            
        }
        
        private void TransToDFABtn_Click(object sender, RoutedEventArgs e)                      // Перевод в ДКА
        {
            if (def.Type == 1)
            {
                // StaticAnyWhere.Rootclass.GoTo("Pages/ViewingTheAutomaton.xaml", "Перевод");
                SelectTytpe("Pages/ViewingTheAutomaton.xaml", "to DFA");
                //this.NavigationService.Navigate(new Uri("Pages/ViewingTheAutomaton.xaml", UriKind.Relative));

                StaticAnyWhere.TeamTransfer = new TeamTransferStruct
                {
                    automaton = automatonGraph,
                    type = 1
                };
            }
            else if (def.Type == 2)
            {
                SelectTytpe("Pages/ViewingTheAutomaton.xaml", "to DFA");
             //   this.NavigationService.Navigate(new Uri("Pages/ViewingTheAutomaton.xaml", UriKind.Relative));

                StaticAnyWhere.TeamTransfer = new TeamTransferStruct
                {
                    automaton = automatonGraph,
                    type = 2
                };
                
            }
            
        }
        
        private void viewToDFABtn_Click(object sender, RoutedEventArgs e)                       // Просмотр автомата
        {
            SelectTytpe("Pages/ViewingTheAutomaton.xaml", "Просмотр автомата");

            StaticAnyWhere.TeamTransfer = new TeamTransferStruct
            {
                automaton = automatonGraph,
                type = 0
            };
        }

        private void SelectTytpe(string url, string name = null)
        {
            NotificationWindow window = new NotificationWindow();
            if (window.ShowDialog() == true)
            {
                if (window.NewTab == true)
                    StaticAnyWhere.Rootclass.GoTo(url, name);
                else
                    this.NavigationService.Navigate(new Uri(url, UriKind.Relative));
            }
        }


        #endregion


        #region Логика


        /// <summary>
        /// Очистить TextBlock
        /// </summary>
        private void CleanTB()
        {
            for (int i = 0, counter = 0; i < def.Q.Length; i++)
                for (int j = 0; j < def.Sigma.Length; j++, counter++)
                {
                    C[counter].Text = "";
                    C[counter].Background = Brushes.LightYellow;
                }
        }


        /// <summary>
        /// Создание автомата
        /// </summary>
        /// <param name="arrayTransition"></param>
        private void CreateAutomatonGraph(string[,][] arrayTransition)
        {
            List<DFAGraphNode<string>> Q = new List<DFAGraphNode<string>>();
            bool flagAttainability;
            automatonGraph = new Automaton(new List<string>(def.Sigma));
            Automaton automatonGraphTemp = new Automaton(automatonGraph);

        //    automatonGraph.
        //  = new Automaton(def);

            for (int i = 0; i < def.Q.Length; i++)
            {
                if ((StaticAnyWhere.occurrence(def.F, new string[] { def.Q[i] })) && (def.Q[i] == def.Q0))
                {
                    automatonGraphTemp.addState(new DFAGraphNode<string>(def.Q[i], true, true));
                }
                else if (StaticAnyWhere.occurrence(def.F, new string[] { def.Q[i] }))
                {
                    automatonGraphTemp.addState(new DFAGraphNode<string>(def.Q[i], false, true));
                }
                else if (def.Q[i] == def.Q0)
                {
                    automatonGraphTemp.addState(new DFAGraphNode<string>(def.Q[i], true, false));
                }
                else
                {
                    automatonGraphTemp.addState(new DFAGraphNode<string>(def.Q[i], false, false));
                }
                                
            }

            automatonGraphTemp.findHead();                      


            for (int i = 0; i < def.Q.Length; i++)
            {
                List<StateTransitionTable> _conversionTable = new List<StateTransitionTable>();
                       

                for (int j = 0; j < def.Sigma.Length; j++)
                {
                    
                    for (int k = 0; k < arrayTransition[i, j].Length; k++)
                    {


                        for (int ii = 0; ii < def.Q.Length; ii++)
                        {
                            if (arrayTransition[i, j][k] == def.Q[ii])
                            {
                                _conversionTable.Add(new StateTransitionTable(automatonGraphTemp.findByName(def.Q[ii]), def.Sigma[j], true));
                            }
                        }
                        
                    }

                }

                automatonGraphTemp.findByName(def.Q[i]).ConversionTable = _conversionTable;

            }

            flagAttainability = automatonGraphTemp.AttainabilityOfTheTop();
            
            if (flagAttainability)
            {
                automatonGraph = automatonGraphTemp;
                if (def.Type == 0)
                {
                    viewToDFABtn.Visibility = Visibility.Visible;
                }
                else if (def.Type == 1)
                {
                    viewToDFABtn.Visibility = Visibility.Visible;
                    transToDFABtn.Visibility = Visibility.Visible;
                }
                else if (def.Type == 2)
                {
                    viewToDFABtn.Visibility = Visibility.Visible;
                    transToDFABtn.Visibility = Visibility.Visible;

                }
            }
            else
            {
                ErrorWindow errorWindow = new ErrorWindow("Ошибка ввода", "Заключительное состояние не достижимо Возможно вы ошиблись при заполнении таблицы перехода, попробуйте изменить значения. Убедитесь, что конечное состояние достижимо.");
                errorWindow.ShowDialog();
               // StaticAnyWhere.Error = "Заключительное состояние автомата не достижимо";
              //  StaticAnyWhere.Rootclass.GoTo("Pages/Error.xaml");
            }
        }


        /// <summary>
        /// Создания матрицы для ввода таблицы переходов
        /// </summary>
        private void prepar()
        {


            transToDFABtn.Visibility = Visibility.Collapsed;
            viewToDFABtn.Visibility = Visibility.Collapsed;
            def = StaticAnyWhere.Def;
            


            CanvasTB.Width = 60 * (def.Sigma.Length + 1);
            CanvasTB.Height = 35 * (def.Q.Length + 1);

            for (int i = 0, countTestQ = 0; i < def.Q.Length + 1; i++)
                for (int j = 0; j < def.Sigma.Length + 1; j++)
                {
                    TextBox Tb = new TextBox();
                    Tb.Width = 50;
                    Tb.Height = 25;
                    Tb.FontSize = 16;
                    // Tb.Name = Tb + Convert.ToString(i) + Convert.ToString(j);
                    Tb.Margin = new Thickness(j * (Tb.Width + 10), i * (Tb.Height + 20), 0, 0);

                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            Tb.Text = "delta";
                        }
                        else
                        {
                            Tb.Text = def.Sigma[j - 1];
                        }

                        Tb.IsEnabled = false;
                    }
                    else if ((i > 0) & (j == 0))
                    {
                        Tb.Text = "";

                        Tb.Text = def.Q[i - 1];
                        Tb.IsEnabled = false;

                        if (def.Q[i - 1] == def.Q0)
                        {
                            Tb.Text = "->" + Tb.Text;
                        }

                        if (StaticAnyWhere.occurrence(def.F, new string[] { def.Q[i - 1] }))
                        {
                            Tb.Text = "*" + Tb.Text;
                        }


                    }
                    else
                    {
                        if (def.ItIsTest == true)
                        {
                            Tb.Text = def.Tabel[countTestQ];
                            countTestQ++;
                        }
                        else
                        {
                            Tb.Text = "";
                        }

                        Tb.Background = Brushes.LightYellow;
                        C.Add(Tb);
                    }

                    this.CanvasTB.Children.Add(Tb);
                }
        }


        /// <summary>
        /// Добавление таблицы переходов
        /// </summary>
        /// <param name="arrayTransition"></param>
        /// <returns></returns>
        private bool AddTransitionFunction(ref string[,][] arrayTransition)
        {

            bool flagType2 = false, erorFlag = false;

            if (def.Type == 2)
            {
                for (int i = 0, counter = 0; i < def.Q.Length; i++)
                    for (int j = 0; j < def.Sigma.Length; j++, counter++)
                    {
                        if ((StaticAnyWhere.occurrence(def.Q, StaticAnyWhere.prepareStringArr(C[counter].Text))) || (C[counter].Text == "Null" || (C[counter].Text == "null") || (C[counter].Text == " ") || (C[counter].Text == "")))
                        {
                            arrayTransition[i, j] = StaticAnyWhere.prepareStringArr(C[counter].Text);
                            C[counter].Background = Brushes.LightGreen;
                        }
                        else
                        {
                            erorFlag = true;
                            C[counter].Background = Brushes.LightPink;
                        }

                    }
            }
            else
            {

                for (int i = 0, counter = 0; i < def.Q.Length; i++)
                    for (int j = 0; j < def.Sigma.Length; j++, counter++)
                    {
                        if ((StaticAnyWhere.occurrence(def.Q, StaticAnyWhere.prepareStringArr(C[counter].Text))) || (C[counter].Text == "Null" || (C[counter].Text == "null" || (C[counter].Text == ""))))
                        {
                            arrayTransition[i, j] = StaticAnyWhere.prepareStringArr(C[counter].Text);
                            C[counter].Background = Brushes.LightGreen;

                            if (arrayTransition[i, j].Length > 1)
                                flagType2 = true;
                        }
                        else
                        {
                            erorFlag = true;
                            C[counter].Background = Brushes.LightPink;
                        }

                    }

                if (flagType2 == true && def.Type != 2)
                {
                    def.Type = 1;
                }
                else
                {
                    def.Type = 0;
                }

            }



            

            if (erorFlag)
            {
                return false;
            }
            else
            {
                return true;
            }


        }




        #endregion

        
    }
}
