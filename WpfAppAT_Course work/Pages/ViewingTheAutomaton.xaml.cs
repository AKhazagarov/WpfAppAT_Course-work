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

namespace WpfAppAT_Course_work.Pages
{
    /// <summary>
    /// Логика взаимодействия для Transformation.xaml
    /// </summary>
    public partial class ViewingTheAutomaton : Page
    {

        Automaton automatonGraph;

        public ViewingTheAutomaton()
        {
            InitializeComponent();
            WhereToBegin();

        }

        private void SeeOnTab()
        {
            OutputToCanvas();
        }



        private void WhereToBegin()
        {

            automatonGraph = StaticAnyWhere.TeamTransfer.automaton;

            if (StaticAnyWhere.TeamTransfer.type == 0)
            {
                SeeOnTab();
            }
            else if (StaticAnyWhere.TeamTransfer.type == 1)
            {
                NFAtoDFA(StaticAnyWhere.TeamTransfer.automaton);
            }
            else if (StaticAnyWhere.TeamTransfer.type == 2)
            {
                ENFAtoDFA(StaticAnyWhere.TeamTransfer.automaton);
            }
        }


        #region Перевод с ЕНКА в ДКА

        private void ENFAtoDFA(Automaton automaton)
        {
            Automaton DFAdef = new Automaton(automaton.InputCharacters);
           // automatonGraph = automaton;
            Queue<NFAGraphNode<string>> queue = new Queue<NFAGraphNode<string>>();
            List<string> list = new List<string>();

            List<string> newInpetCh = new List<string>();



            NFAGraphNode<string> topnode = new NFAGraphNode<string>(true);

            komar.Clear();
            FindEclose(automaton.head, ref topnode);
            topnode.GetNewNode();

            queue.Enqueue(topnode);

            DFAdef.addState(topnode.GetDFANode());



            while (queue.Count > 0)
            {
                NFAGraphNode<string> nodeTemp = queue.Dequeue();

                if (!list.Contains(nodeTemp.Name))
                {
                    List<StateTransitionTable> _conversionTable = new List<StateTransitionTable>();

                    for (int i = 1; i < automaton.InputCharacters.Count; i++)
                    {
                        NFAGraphNode<string> node = new NFAGraphNode<string>();

                        Console.WriteLine(automaton.InputCharacters[i]);


                        for (int j = 0; j < nodeTemp.GetEntering().Count; j++)
                        {
                            FindESigma(nodeTemp.GetEntering()[j], automaton.InputCharacters[i], ref node);
                        }
                        if (node.GetEntering().Count > 0)
                        {
                            node.GetNewNode();
                            DFAGraphNode<string> dfaNode = node.GetDFANode();

                            if (DFAdef.findByName(dfaNode.Name) == null)
                            {
                                DFAdef.addState(dfaNode);
                                _conversionTable.Add(new StateTransitionTable(dfaNode, automaton.InputCharacters[i], true));
                            }
                            else
                            {
                                _conversionTable.Add(new StateTransitionTable(DFAdef.findByName(dfaNode.Name), automaton.InputCharacters[i], true));
                            }
                            queue.Enqueue(node);
                        }
                    }

                    list.Add(nodeTemp.Name);
                    DFAdef.findByName(nodeTemp.Name).ConversionTable = _conversionTable;
                }

                
                
            }

            int a = 6;

            automatonGraph = DFAdef;

            automatonGraph.findHead();
            OutputToCanvas();

        }

        /// <summary>
        /// Временный список
        /// </summary>
        private List<DFAGraphNode<string>> komar = new List<DFAGraphNode<string>>();

        private void FindEclose(DFAGraphNode<string> node, ref NFAGraphNode<string> rtrn)
        {

            Console.WriteLine(node.Name);
            

            if (!komar.Contains(node))
            {
                komar.Add(node);

                bool flagerror = false;

                for (int i = 0; i < rtrn.GetEntering().Count; i++)
                {
                    if (node.Name == rtrn.GetEntering()[i].Name)
                    {
                        flagerror = true;
                    }
                }

                if (!flagerror)
                {
                    rtrn.AddEntering(node);
                }


                for (int i = 0; i < node.ConversionTable.Count; i++)
                {
                    if (node.ConversionTable[i].InputCharacter == "ε")
                    {
                        FindEclose(node.ConversionTable[i].StateSet, ref rtrn);
                        
                    }
                }
            }

                
        }

        private void FindESigma(DFAGraphNode<string> node, string sigma, ref NFAGraphNode<string> rtrn)
        {
            komar.Clear();

            for (int i = 0; i < node.ConversionTable.Count; i++)
            {
                if (node.ConversionTable[i].InputCharacter == sigma)
                {
                    FindEclose(node.ConversionTable[i].StateSet, ref rtrn);
                }
            }

        }



        #endregion



        #region Перевод с НКА в ДКА

        private void NFAtoDFA(Automaton automaton)
        {
            Automaton DFAdef = new Automaton(automaton.InputCharacters);

            Queue<NFAGraphNode<string>> queue = new Queue<NFAGraphNode<string>>();
            List<string> list = new List<string>();

            NFAGraphNode<string> topnode = new NFAGraphNode<string>(automaton.head);

            queue.Enqueue(topnode);


            DFAdef.addState(topnode.GetDFANode());

            NFAGraphNode<string> node = new NFAGraphNode<string>();

            while (queue.Count > 0)
            {
                NFAGraphNode<string> nodeTemp = queue.Dequeue();

                if (!list.Contains(nodeTemp.Name))
                {
                    List<StateTransitionTable> _conversionTable = new List<StateTransitionTable>();

                    for (int i = 0; i < automaton.InputCharacters.Count; i++)
                    {
                        node = new NFAGraphNode<string>();
                        Console.WriteLine(automaton.InputCharacters[i]);

                        if (nodeTemp.GetEntering().Count == 0)
                        {
                            FindESigma(nodeTemp, automaton.InputCharacters[i], ref node);
                        }
                        else
                        {
                            for (int j = 0; j < nodeTemp.GetEntering().Count; j++)
                            {
                                FindESigma(nodeTemp.GetEntering()[j], automaton.InputCharacters[i], ref node);
                            }
                        }

                       
                        if (node.GetEntering().Count > 0)
                        {
                            node.GetNewNode();
                            DFAGraphNode<string> dfaNode = node.GetDFANode();

                            if (DFAdef.findByName(dfaNode.Name) == null)
                            {
                                DFAdef.addState(dfaNode);
                                _conversionTable.Add(new StateTransitionTable(dfaNode, automaton.InputCharacters[i], true));
                            }
                            else
                            {
                                _conversionTable.Add(new StateTransitionTable(DFAdef.findByName(dfaNode.Name), automaton.InputCharacters[i], true));
                            }
                            queue.Enqueue(node);
                        }
                    }
                    list.Add(nodeTemp.Name);
                    DFAdef.findByName(nodeTemp.Name).ConversionTable = _conversionTable;
                }
            }

            automatonGraph = DFAdef;
            automatonGraph.findHead();

            OutputToCanvas();
        }


        #region Старое определение автомата
        /*
        private void NFAtoDFA1(Automaton automatonGraph)
        {

            DefinitionAutomaton DFAdef = new DefinitionAutomaton();

            string[] newInputSymbols = NewStates(automatonGraph.definition);
            string[,] DFAInputSimbol = new string[newInputSymbols.Length, automatonGraph.InputCharacters.Count];


            for (int i = 1; i < newInputSymbols.Length; i++)
            {
                for (int ii = 0; ii < automatonGraph.InputCharacters.Count; ii++)
                {
                    string[] str;
                    str = newInputSymbols[i].Split(',');

                    for (int j = 0; j < str.Length; j++)
                    {
                        DFAInputSimbol[i, ii] += " " + Union(automatonGraph.findByName(str[j]).ConversionTable, automatonGraph.InputCharacters[ii]);
                        //automatonGraph.findByName(str[j]).ConversionTable
                    }
                    DFAInputSimbol[i, ii] = DFAInputSimbol[i, ii].Trim();
                    DFAInputSimbol[i, ii] = DFAInputSimbol[i, ii].Replace(" ", ",");
                    DFAInputSimbol[i, ii] = String.Join(",", RemoveDuplicates(DFAInputSimbol[i, ii].Split(',')));
                }
            }


            DFAdef.Q = newInputSymbols;
            DFAdef.Q0 = automatonGraph.definition.Q0;
           // DFAdef.Sigma = automatonGraph.InputCharacters;
            Automaton DFA = new Automaton(automatonGraph.InputCharacters);
            // DFAdef.F = ;


            for (int i = 1; i < newInputSymbols.Length; i++)
            {
                if (ItIsFinyly(DFAdef.Q[i].Split(','), automatonGraph.definition.F) && (DFAdef.Q[i] == DFAdef.Q0))
                {
                    DFA.addState(new DFAGraphNode<string>(DFAdef.Q[i], true, true));
                }
                else if (ItIsFinyly(DFAdef.Q[i].Split(','), automatonGraph.definition.F))
                {
                    DFA.addState(new DFAGraphNode<string>(DFAdef.Q[i], false, true));
                }
                else if (DFAdef.Q[i] == DFAdef.Q0)
                {
                    DFA.addState(new DFAGraphNode<string>(DFAdef.Q[i], true, false));
                }
                else
                {
                    DFA.addState(new DFAGraphNode<string>(DFAdef.Q[i], false, false));
                }
            }

            DFA.findHead();

            for (int i = 1; i < DFAdef.Q.Length; i++)
            {
                List<StateTransitionTable> _conversionTable = new List<StateTransitionTable>();


                for (int j = 0; j < DFAdef.Sigma.Length; j++)
                {

                    for (int ii = 0; ii < DFAdef.Q.Length; ii++)
                    {
                        if (DFAInputSimbol[i, j] == DFAdef.Q[ii])
                        {
                            _conversionTable.Add(new StateTransitionTable(DFA.findByName(DFAdef.Q[ii]), DFAdef.Sigma[j], true));
                        }
                    }

                }

                DFA.findByName(DFAdef.Q[i]).ConversionTable = _conversionTable;

            }



            for (int i = DFA.Size - 1; i > 0; i--)
            {
                if (DFA.Reachability(DFA.head, DFA.GetNodeByIndex(i)) == false)
                {
                    DFA.Remove(DFA.GetNodeByIndex(i));
                }
            }



            for (int i = DFA.Size - 1; i > 0; i--)
            {
                if (DFA.AttainabilityOfTheTop(DFA.GetNodeByIndex(i)) == false)
                {
                    DFA.Remove(DFA.GetNodeByIndex(i));
                }
            }

            automatonGraph = DFA;
            OutputToCanvas();
        }


        */



        private bool ItIsFinyly(string[] str1, string[] str2)
        {
            if (StaticAnyWhere.occurrence(str1, str2) == true)
                return true;
            else
                return false;

        }


        private string[] RemoveDuplicates(string[] s)
        {
            HashSet<string> set = new HashSet<string>(s);
            string[] result = new string[set.Count];
            set.CopyTo(result);
            return result;
        }


        private string Union(List<StateTransitionTable> table, string sigma)
        {
            string str = "";
            for (int i = 0; i < table.Count; i++)
            {
                if (table[i].InputCharacter == sigma)
                    str += table[i].StateSet.Name + " ";

            }

            str = str.Trim();
            str = str.Replace(" ", ",");


            return str;
        }

        private string[] NewStates(DefinitionAutomaton def)
        {
            string[] str = new string[(int)Math.Pow(2, def.Q.Length)];
            int[] a = new int[def.Q.Length + 1];
            int count = 1;

            for (int m = 1; m < def.Q.Length + 1; m++)
            {
                for (int i = 0; i < def.Q.Length + 1; i++)
                    a[i] = i + 1;
                int num = 1;
                for (int i = 0; i < m; i++)
                    str[count] += a[i] + " ";
                count++;
                if (def.Q.Length >= m)
                {
                    while (NextSet(ref a, def.Q.Length, m))
                    {
                        num = 1;
                        for (int i = 0; i < m; i++)
                            str[count] += a[i] + " ";
                        count++;
                    }
                }
            }


            for (int i = 1; i < str.Length; i++)
            {
                for (int j = 0; j < def.Q.Length; j++)
                {
                    str[i] = str[i].Replace(Convert.ToString(j + 1), def.Q[j]);
                }
            }

            for (int i = 1; i < str.Length; i++)
            {
                str[i] = str[i].Trim();
                str[i] = str[i].Replace(" ", ",");
            }

            return str;

        }


        private bool NextSet(ref int[] a, int n, int m)
        {
            int k = m;
            for (int i = k - 1; i >= 0; --i)
                if (a[i] < n - k + i + 1)
                {
                    ++a[i];
                    for (int j = i + 1; j < k; ++j)
                        a[j] = a[j - 1] + 1;
                    return true;
                }
            return false;
        }

        #endregion


#endregion

        private void SetPoint(Node objct, int x, int y)
        {
            objct.Margin = new Thickness(x , y, 0, 0);
        }

        private Vector GetCenterByNode(Node objct)
        {
            Vector vector = new Vector();
            vector.X = objct.Margin.Left + 68;
            vector.Y = objct.Margin.Top + 75;
            return vector;
        }


        Point oldPosition;
        List<Node> listNode = new List<Node>();

        private void OutputToCanvasG()
        {
            int count = 0, x = 0, y = 0, x1 = 520;

            if (automatonGraph.Size > 8)
            {
                CanvasGr.Height = ((automatonGraph.Size / 2) + 2) * 150;
            }
            

            while (count < automatonGraph.Size)
            {

                if (x > 391)
                {
                    x = 0;
                }
                if (x1 > 911)
                {
                    x1 = 520;
                }

                Console.WriteLine("Hello world");

                for (int i = 0; i < 2; i++, count++)
                {
                    if (count == automatonGraph.Size)
                    {
                        break;
                    }
                    if (i == 0)
                    {
                        Node node = new Node(automatonGraph.GetNodeByIndex(count));
                        SetPoint(node, x, y);
                        automatonGraph.GetNodeByIndex(count).NodeG = node;
                        node.MouseLeftButtonDown += new MouseButtonEventHandler((object sendere, MouseButtonEventArgs ee) =>
                        {
                            Node elem = sendere as Node;
                            oldPosition = ee.GetPosition(elem);
                            elem.CaptureMouse();
                        });

                        node.MouseLeftButtonUp += new MouseButtonEventHandler((object sendere, MouseButtonEventArgs ee) =>
                        {
                            (sendere as UIElement).ReleaseMouseCapture();
                            AddLine();
                        });

                        node.MouseMove += new MouseEventHandler((object sendere, MouseEventArgs ee) =>
                        {
                            Node elem = sendere as Node;
                            if (!elem.IsMouseCaptured) return;

                            if (elem.IsMouseCaptured)
                            {

                                elem.Margin = new Thickness(ee.GetPosition(this).X - oldPosition.X - 18, ee.GetPosition(this).Y - oldPosition.Y - 115, 0, 0);

                                AddLine();
                            }
                        });
                        listNode.Add(node);
                        //CanvasGr.Children.Add(node);
                    }
                    else
                    {
                        Node node = new Node(automatonGraph.GetNodeByIndex(count));
                        SetPoint(node, x1, y);
                        automatonGraph.GetNodeByIndex(count).NodeG = node;
                        node.MouseLeftButtonDown += new MouseButtonEventHandler((object sendere, MouseButtonEventArgs ee) =>
                        {
                            Node elem = sendere as Node;
                            oldPosition = ee.GetPosition(elem);
                            elem.CaptureMouse();
                        });

                        node.MouseLeftButtonUp += new MouseButtonEventHandler((object sendere, MouseButtonEventArgs ee) =>
                        {
                            (sendere as UIElement).ReleaseMouseCapture();
                            AddLine();
                        });

                        node.MouseMove += new MouseEventHandler((object sendere, MouseEventArgs ee) =>
                        {
                            Node elem = sendere as Node;
                            if (!elem.IsMouseCaptured) return;

                            if (elem.IsMouseCaptured)
                            {

                                elem.Margin = new Thickness(ee.GetPosition(this).X - oldPosition.X - 18, ee.GetPosition(this).Y - oldPosition.Y - 115, 0, 0);

                                AddLine();
                            }
                        });
                        listNode.Add(node);
                      //  CanvasGr.Children.Add(node);
                    }
                    
                }

                y += 150;
                x += 130;
                x1 += 130;
            }

            AddLine();
        }

        private void AddLine()
        {
            CanvasGr.Children.Clear();
            for (int i = 0; i < automatonGraph.Size; i++)
            {
                List<DFAGraphNode<string>> arrowLabel = new List<DFAGraphNode<string>>();
                for (int j = 0; j < automatonGraph.GetNodeByIndex(i).ConversionTable.Count; j++)
                {
                    string arrLabel = "";

                    if (!arrowLabel.Contains(automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet))
                    {
                        for (int e = 0; e < automatonGraph.GetNodeByIndex(i).ConversionTable.Count; e++)
                        {
                            if (automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.Name != automatonGraph.GetNodeByIndex(i).Name)
                            {
                                if (automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.Name == automatonGraph.GetNodeByIndex(i).ConversionTable[e].StateSet.Name)
                                {
                                    arrLabel += automatonGraph.GetNodeByIndex(i).ConversionTable[e].InputCharacter + " ";
                                }
                            }
                        }
                        arrLabel = arrLabel.Trim();
                        arrLabel = arrLabel.Replace(" ", ",");

                        if (arrLabel != "")
                        {
                            Arrow arrow = new Arrow(arrLabel,
                                GetCenterByNode(automatonGraph.GetNodeByIndex(i).NodeG).X,
                                GetCenterByNode(automatonGraph.GetNodeByIndex(i).NodeG).Y,
                                GetCenterByNode(automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG).X,
                                GetCenterByNode(automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG).Y);

                            Console.WriteLine(GetCenterByNode(automatonGraph.GetNodeByIndex(i).NodeG).X + " , " + GetCenterByNode(automatonGraph.GetNodeByIndex(i).NodeG).Y);
                            arrow.Margin = new Thickness(0, 0, 0, 0);

                            CanvasGr.Children.Add(arrow);
                        }



                        arrowLabel.Add(automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet);
                    }
                    

                    

                    /*
                        if (automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.Name != automatonGraph.GetNodeByIndex(i).Name)
                    {

                        Arrow arrow = new Arrow(automatonGraph.GetNodeByIndex(i).ConversionTable[j].InputCharacter,
                            GetCenterByNode(automatonGraph.GetNodeByIndex(i).NodeG).X,
                            GetCenterByNode(automatonGraph.GetNodeByIndex(i).NodeG).Y,
                            GetCenterByNode(automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG).X,
                            GetCenterByNode(automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG).Y);

                        Console.WriteLine(GetCenterByNode(automatonGraph.GetNodeByIndex(i).NodeG).X + " , " + GetCenterByNode(automatonGraph.GetNodeByIndex(i).NodeG).Y);
                        arrow.Margin = new Thickness(0, 0, 0, 0);
                        
                        CanvasGr.Children.Add(arrow);

                    }*/

                }
            }

            foreach (Node node in listNode)
            {
                CanvasGr.Children.Add(node);
            }
        }


        private void O1utputToCanvasG()
        {

            
            int size = 80;
            List<Node> listNode = new List<Node>();
        //    List<Node> listNode = new List<Node>();


            for (int i = 0, count = 0, step = 0; count < automatonGraph.Size; i++)
            {
                for (int j = 0; j < 2; j++, count++, step++)
                {
                    if (step == 6)
                    {
                        step = 0;
                    }

                    if (count == automatonGraph.Size)
                    {
                        break;
                    }
                    //  listNode.Add(new Node(automatonGraph.GetNodeByIndex(i), size));
                    //   listNode[i - 1].Margin = new Thickness((step * 100) + (j * 400), i * 50, 0, 0);



                    Node node = new Node(automatonGraph.GetNodeByIndex(count), size);
                    node.Margin = new Thickness(5 + (step * 50) + (j * 400), i * 100 + 5, 0, 0);
                    automatonGraph.GetNodeByIndex(count).NodeG = node;


                    CanvasGr.Children.Add(node);
                    listNode.Add(node);
                    

                }
                    
            }
            Random random = new Random();
            for (int i = 0; i < automatonGraph.Size; i++)
            {
                for (int j = 0; j < automatonGraph.GetNodeByIndex(i).ConversionTable.Count; j++)
                {
                    if (automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.Name != automatonGraph.GetNodeByIndex(i).Name)
                    {
                        //  automatonGraph.GetNodeByIndex(i).NodeG.Margin.Bottom

                     //   Console.WriteLine(automatonGraph.GetNodeByIndex(i).NodeG.Margin.Left);
                   //     Console.WriteLine(automatonGraph.GetNodeByIndex(i).NodeG.Margin.Top);
                   //     Console.WriteLine(automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG.Margin.Top);

                        Arrow arrow;
                       

                        if ((automatonGraph.GetNodeByIndex(i).NodeG.Margin.Left < automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG.Margin.Left)
                            && (automatonGraph.GetNodeByIndex(i).NodeG.Margin.Top > automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG.Margin.Top))
                        {
                            arrow = new Arrow(automatonGraph.GetNodeByIndex(i).ConversionTable[j].InputCharacter,
                           automatonGraph.GetNodeByIndex(i).NodeG.Margin.Left,
                           automatonGraph.GetNodeByIndex(i).NodeG.Margin.Top + (size / 2),
                           automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG.Margin.Left - (size ),
                           automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG.Margin.Top + (size * .5) + random.Next(-20, 20));

                            arrow.Margin = new Thickness(automatonGraph.GetNodeByIndex(i).NodeG.Margin.Left - (size * .3),
                               automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG.Margin.Top + (size / 2) + random.Next(-20, 20), 0, 0);

                        }
                        else if ((automatonGraph.GetNodeByIndex(i).NodeG.Margin.Left > automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG.Margin.Left)
                           && (automatonGraph.GetNodeByIndex(i).NodeG.Margin.Top < automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG.Margin.Top))
                        {
                            arrow = new Arrow(automatonGraph.GetNodeByIndex(i).ConversionTable[j].InputCharacter,
                           automatonGraph.GetNodeByIndex(i).NodeG.Margin.Left,
                           automatonGraph.GetNodeByIndex(i).NodeG.Margin.Top,
                           automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG.Margin.Left + (size * .5),
                           automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG.Margin.Top + (size * .5) + random.Next(-20, 20));

                            arrow.Margin = new Thickness(automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG.Margin.Left - size,
                            automatonGraph.GetNodeByIndex(i).NodeG.Margin.Top + (size * .5) + random.Next(-20, 20), 0, 0);
                        }
                        else if((automatonGraph.GetNodeByIndex(i).NodeG.Margin.Left > automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG.Margin.Left)
                            && (automatonGraph.GetNodeByIndex(i).NodeG.Margin.Top > automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG.Margin.Top))
                        {
                            arrow = new Arrow(automatonGraph.GetNodeByIndex(i).ConversionTable[j].InputCharacter,
                           automatonGraph.GetNodeByIndex(i).NodeG.Margin.Left,
                           automatonGraph.GetNodeByIndex(i).NodeG.Margin.Top,
                           automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG.Margin.Left,
                           automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG.Margin.Top);

                            arrow.Margin = new Thickness(automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG.Margin.Left + ((size * 1.5) / 2),
                                automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG.Margin.Top + (size / 2), 0, 0);
                        }
                        else if ((automatonGraph.GetNodeByIndex(i).NodeG.Margin.Left < automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG.Margin.Left)
                            && (automatonGraph.GetNodeByIndex(i).NodeG.Margin.Top < automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG.Margin.Top))
                        {
                            arrow = new Arrow(automatonGraph.GetNodeByIndex(i).ConversionTable[j].InputCharacter,
                           automatonGraph.GetNodeByIndex(i).NodeG.Margin.Left,
                           automatonGraph.GetNodeByIndex(i).NodeG.Margin.Top + (size * 1.5 / 2),
                           automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG.Margin.Left - (size / 2),
                           automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG.Margin.Top);

                            arrow.Margin = new Thickness(automatonGraph.GetNodeByIndex(i).NodeG.Margin.Left + ((size * 1.5) / 2),
                            automatonGraph.GetNodeByIndex(i).NodeG.Margin.Top + (size / 2), 0, 0);
                        }
                        else 
                        {
                            arrow = new Arrow(automatonGraph.GetNodeByIndex(i).ConversionTable[j].InputCharacter,
                           automatonGraph.GetNodeByIndex(i).NodeG.Margin.Left,
                           automatonGraph.GetNodeByIndex(i).NodeG.Margin.Top + (size * 1.5 / 2),
                           automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG.Margin.Left - (size / 2),
                           automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.NodeG.Margin.Top);

                            arrow.Margin = new Thickness(automatonGraph.GetNodeByIndex(i).NodeG.Margin.Left + ((size * 1.5) / 2),
                            automatonGraph.GetNodeByIndex(i).NodeG.Margin.Top + (size / 2), 0, 0);

                            
                        }


                        CanvasGr.Children.Add(arrow);

                        Console.WriteLine("Вершина создана из " + automatonGraph.GetNodeByIndex(i).ConversionTable[j].StateSet.Name + " в " + automatonGraph.GetNodeByIndex(i).Name + " по" + automatonGraph.GetNodeByIndex(i).ConversionTable[j].InputCharacter);

                    }
                }
            }

    


        }



        /// <summary>
        /// Вывод на экран
        /// </summary>
        /// <param name="automatonGraph"></param>
        private void OutputToCanvas()
        {
            defTB.Text = "Ваш автомат A = " + automatonGraph.Automatonlabel;
            if (defTB.Text.Length > 150)
            {
                defTB.Text = "Ваш автомат A = (Q, Σ, δ, q, F)";
            }

            OutputToCanvasG();

            CanvasTB.Width = 110 * (automatonGraph.InputCharacters.Count + 1);
            CanvasTB.Height = 40 * (automatonGraph.Size + 1);

            for (int i = 0; i < automatonGraph.Size + 1; i++)
            {
                for (int j = 0; j < automatonGraph.InputCharacters.Count + 1; j++)
                {
                    Label Tb = new Label();
                    Tb.Width = 100;
                    Tb.Height = 30;
                    Tb.FontSize = 14;
                    Tb.Margin = new Thickness(j * (Tb.Width + 10), i * (Tb.Height + 10), 0, 0);

                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            Tb.Content = "delta";
                        }
                        else
                        {
                            Tb.Content = automatonGraph.InputCharacters[j - 1];
                        }

                    }
                    else if ((i > 0) & (j == 0))
                    {
                        Tb.Content = "";

                        Tb.Content = automatonGraph.GetNodeByIndex(i - 1).Name;
                        if (automatonGraph.GetNodeByIndex(i - 1).ItIsInitialState == true)
                        {
                            Tb.Content = "->" + Tb.Content;
                        }

                        if (automatonGraph.GetNodeByIndex(i - 1).ItIsAdmissibleState == true)
                        {
                            Tb.Content = "*" + Tb.Content;
                        }

                    }
                    else
                    {
                        if (automatonGraph.FindTransition(automatonGraph.GetNodeByIndex(i - 1), automatonGraph.InputCharacters[j - 1]) != null)
                        {
                            Tb.Content = automatonGraph.FindTransition(automatonGraph.GetNodeByIndex(i - 1), automatonGraph.InputCharacters[j - 1]);
                        }
                        else
                        {
                            Tb.Content = "ø";
                        }
                    }
                    this.CanvasTB.Children.Add(Tb);
                }
            }

            
        }

        

        private void TestBtn_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;

            List<string> listtmm = new List<string>();

            for (int i = 0; i < inputCharactersTB.Text.Length; i++)
            {
                listtmm.Add(Convert.ToString(inputCharactersTB.Text[i]));
            }

            //  string[] inputCh = StaticAnyWhere.prepareStringArr(inputCharactersTB.Text);
            Queue<string> listCH = new Queue<string>();
            CanvasGrLine.Children.Clear();

            
            for (int i = 0; i < listtmm.Count; i++)
            {
                if ((automatonGraph.InputCharacters.Contains(listtmm[i]) || (inputCharactersTB.Text == "")))
                {
                    listCH.Enqueue(listtmm[i]);
                }
                else
                {
                    MessageBox.Show("Ошибка при вводу входных символов");
                    error = true;
                    break;
                }
            }
            List<DFAGraphNode<string>> view = new List<DFAGraphNode<string>>();
            if (!error)
            {
                bool flag = automatonGraph.CheckingTheValidityChain(listCH, ref view);

                if (flag)
                {
                    Node noded = sender as Node;

                    CanvasGrLine.Width = view.Count * 150;


                    for (int i = 0; i < view.Count; i++)
                    {
                        Node node = new Node(view[i]);
                        SetPoint(node, i * 150, 0);
                        //   automatonGraph.GetNodeByIndex(count).NodeG = node;
                        node.ItIsOnlyNode = true;
                        CanvasGrLine.Children.Add(node);
                    }
                }
                else
                {

                }


                MessageBox.Show(flag + "");

            }



        }

        private void addTransitionFunctionBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
