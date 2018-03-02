using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppAT_Course_work.My_elements;

namespace WpfAppAT_Course_work.Classes
{

    #region Структура> Таблица переходов для узла автомата

    /// <summary>
    /// Таблица переходов
    /// </summary>
    public class StateTransitionTable
    {
        public StateTransitionTable(DFAGraphNode<string> stateSet, string inputCharacter, bool availability)
        {
            StateSet = stateSet;
            InputCharacter = inputCharacter;
        }

        public StateTransitionTable() { }

        public void SateStateTransitionTable(DFAGraphNode<string> stateSet, string inputCharacter)
        {
            StateSet = stateSet;
            InputCharacter = inputCharacter;
        }

        public DFAGraphNode<string> StateSet
        {
            get;
            set;
        }

        public string InputCharacter
        {
            get;
            private set;
        }
        
    }

    #endregion



    #region Структура> Определение узла автомата

    /// <summary>
    /// Определение узла автомата
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DFAGraphNode <T> : IComparable<T> where T : IComparable<T>
    {
        /// <summary>
        /// имя узла
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// это начальное состояние?
        /// </summary>
        public bool ItIsInitialState { get; protected set; }

        /// <summary>
        /// это заключительное состояние?
        /// </summary>
        public bool ItIsAdmissibleState { get; protected set; }

        /// <summary>
        /// таблица переходов
        /// </summary>
        public List<StateTransitionTable> ConversionTable { get; set; }

        public Node NodeG { get; set; }


        public DFAGraphNode(DFAGraphNode<string> node)
        {
            Name = node.Name;                                        
            ItIsInitialState = node.ItIsInitialState;                
            ItIsAdmissibleState = node.ItIsAdmissibleState;          
            ConversionTable = new List<StateTransitionTable>(node.ConversionTable);                  
        }

        public DFAGraphNode(string name, bool itIsInitialState, bool itIsAdmissibleState)
        {
            Name = name;
            ItIsInitialState = itIsInitialState;
            ItIsAdmissibleState = itIsAdmissibleState;
        }


        protected DFAGraphNode() { }


        int IComparable<T>.CompareTo(T other)
        {
            throw new NotImplementedException();
        }
    }

    public class NFAGraphNode<T>  : DFAGraphNode<T> , IComparable<T> where T : IComparable<T>
    {
        private List<DFAGraphNode<T>> Entering = new List<DFAGraphNode<T>>();

        public NFAGraphNode() { }
        public NFAGraphNode(bool intt) { if (intt) ItIsInitialState = true; }

        public NFAGraphNode(string name, bool itIsInitialState, bool itIsAdmissibleState, List<StateTransitionTable> conversionTable)
        {
            Name = name;
            ItIsInitialState = itIsInitialState;
            ItIsAdmissibleState = itIsAdmissibleState;
            ConversionTable = conversionTable;
        }


        public NFAGraphNode(DFAGraphNode <T> node)
        {
            Name = node.Name;
            ItIsInitialState = node.ItIsInitialState;
            ItIsAdmissibleState = node.ItIsAdmissibleState;
            ConversionTable = node.ConversionTable;
        }


        public void AddEntering(DFAGraphNode<T> node)
        {
            Entering.Add(node);
        }

        public List<DFAGraphNode<T>> GetEntering() { return Entering; }

        public void GetNewNode()
        {
            for (int i = 0; i < Entering.Count; i++)
            {
                Name += Entering[i].Name + " ";

                if (Entering[i].ItIsAdmissibleState)
                {
                    ItIsAdmissibleState = true;
                }
            }

            Name = Name.Trim();
            Name = Name.Replace(" ", ",");
        }

        public DFAGraphNode<T> GetDFANode()
        {
            DFAGraphNode<T> node = new DFAGraphNode<T>(Name, ItIsInitialState, ItIsAdmissibleState);
            return node;
        }


        public int CompareTo(T other)
        {
            throw new NotImplementedException();
        }
    }



    #endregion



    #region Структура> Определение автомата

    /// <summary>
    /// Автомат
    /// </summary>
    public class Automaton
    {
        /// <summary>
        /// Множество состояний Q
        /// </summary>
        private List<DFAGraphNode<string>> Q = new List<DFAGraphNode<string>>();

        /// <summary>
        /// Временный список
        /// </summary>
        private List<DFAGraphNode<string>> komar = new List<DFAGraphNode<string>>();

        /// <summary>
        /// Множество входных символов
        /// </summary>
        public List<string> InputCharacters { get; private set; }

        

        

        public Automaton(List<string> inputCharacters)
        {
            InputCharacters = inputCharacters;
        }

        public Automaton(Automaton automaton)
        {
            Q = new List<DFAGraphNode<string>>(automaton.Q);
            InputCharacters = automaton.InputCharacters;
            this.head = automaton.head;
        }


        /// <summary>
        /// Начальное состояние
        /// </summary>
        public DFAGraphNode<string> head { get; private set; }

        /// <summary>
        /// Старое определение автомата
        /// </summary>
       // public DefinitionAutomaton definition { get; private set; }

        /// <summary>
        /// Количество состояний
        /// </summary>
        /// <returns></returns>
        public int Size  {get { return Q.Count;}}







        public string FindTransition(DFAGraphNode<string> q, string delta)
        {
            string rtrn = "";
            for (int i = 0; i < q.ConversionTable.Count; i++)
            {
                if (q.ConversionTable[i].InputCharacter == delta)
                {
                    rtrn += q.ConversionTable[i].StateSet.Name + " ";
                }
            }

            rtrn = rtrn.Trim();
            rtrn = rtrn.Replace(" ", ",");
            if (rtrn == "")
            {
                return null;
            }
            else
            {
                return rtrn;
            }
        }



        /// <summary>
        /// Добавить состояние
        /// </summary>
        /// <param name="state"></param>
        public void addState(DFAGraphNode<string> state)
        {
            Q.Add(state);
        }

        /// <summary>
        /// Найти узел по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DFAGraphNode<string> findByName(string name)
        {
            for (int i = 0; i < Q.Count; i++)
            {
                if (Q[i].Name == name)
                {
                    return Q[i];
                }
            }

            return null;
        }


        /// <summary>
        /// Найти начальное состояние
        /// </summary>
        public void findHead()
        {
            for (int i = 0; i < Q.Count; i++)
            {
                if (Q[i].ItIsInitialState == true)
                {
                    head =  Q[i];
                }
            }
        }   


        /// <summary>
        /// Удалить узел
        /// </summary>
        /// <param name="name"></param>
        public void Remove(DFAGraphNode<string> name)
        {
            Q.Remove(name);
        }


        /// <summary>
        /// Найти узел по индексу
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DFAGraphNode<string> GetNodeByIndex(int index)
        {
            return Q[index];
        }

        public string Automatonlabel
        {
            get
            {
                string str = "(";
                string tempstr = "";

                for (int i = 0; i < Q.Count; i++)
                {
                    tempstr += Q[i].Name + " ";
                }

                tempstr = tempstr.Trim();
                tempstr = tempstr.Replace(" ", ",");

                str += "{" + tempstr + "}, ";

                tempstr = "";

                for (int i = 0; i < InputCharacters.Count; i++)
                {
                    tempstr += InputCharacters[i] + " ";
                }

                tempstr = tempstr.Trim();
                tempstr = tempstr.Replace(" ", ",");

                str += "{" + tempstr + "}, ";

                tempstr = "";

                for (int i = 0; i < Q.Count; i++)
                {
                    if (Q[i].ItIsInitialState == true)
                    {
                        tempstr = Q[i].Name;
                        break;
                    }
                }

                str += tempstr + ", ";

                tempstr = "";

                for (int i = 0; i < Q.Count; i++)
                {
                    if (Q[i].ItIsAdmissibleState == true)
                    {
                        tempstr += Q[i].Name + " ";
                    }
                }

                tempstr = tempstr.Trim();
                tempstr = tempstr.Replace(" ", ",");

                str += "{" + tempstr + "} )";


                return str;


            }
        }



        #region Достижимость автомата до заключительного состояния

        /// <summary>
        /// Достижимо ли заключительное состояние 
        /// </summary>
        /// <returns></returns>
        public bool AttainabilityOfTheTop()
        {
            komar.Clear();
            return StartAttainabilityOfTheTop(head);
        }

        /// <summary>
        /// Достижимо ли заключительное состояние от текущего узла
        /// </summary>
        /// <param name="curent"></param>
        /// <returns></returns>
        public bool AttainabilityOfTheTop(DFAGraphNode<string> curent)
        {
            komar.Clear();
            return StartAttainabilityOfTheTop(curent);
        }


        public bool StartAttainabilityOfTheTop(DFAGraphNode<string> curent)
        {
            bool flagM = false;

            if (curent.ItIsAdmissibleState == true)
                return true;

            for (int i = 0; i < curent.ConversionTable.Count; i++)
            {

                if (curent.ConversionTable[i] != null)
                {
                    if (curent.ConversionTable[i].StateSet.ItIsAdmissibleState == true)
                    {
                        Console.WriteLine(curent.ConversionTable[i].StateSet.Name);
                        return true;
                    }


                    if (!komar.Contains(curent.ConversionTable[i].StateSet))
                    {
                        komar.Add(curent.ConversionTable[i].StateSet);
                        Console.WriteLine(curent.ConversionTable[i].StateSet.Name);
                        flagM = StartAttainabilityOfTheTop(curent.ConversionTable[i].StateSet);

                        if (flagM == true)
                        {
                            return true;
                        }

                    }

                }
            }

            return false;
        }


        #endregion

        #region Достижимость вершины

        /// <summary>
        /// Проверка на достижимость
        /// </summary>
        /// <param name="Начальное состояния"></param>
        /// <param name="Конечное состояние"></param>
        /// <returns></returns>
        public bool Reachability(DFAGraphNode<string> enter, DFAGraphNode<string> finish)
        {
            komar.Clear();
            return StartReachability(enter, finish);
        }

        private bool StartReachability(DFAGraphNode<string> curent, DFAGraphNode<string> finish)
        {
            bool flagM = false;

            if (curent == finish)
                return true;

            for (int i = 0; i < curent.ConversionTable.Count; i++)
            {

                if (curent.ConversionTable[i] != null)
                {
                    if (curent.ConversionTable[i].StateSet == finish)
                    {
                        Console.WriteLine(curent.ConversionTable[i].StateSet.Name);
                        return true;
                    }


                    if (!komar.Contains(curent.ConversionTable[i].StateSet))
                    {
                        komar.Add(curent.ConversionTable[i].StateSet);
                        Console.WriteLine(curent.ConversionTable[i].StateSet.Name);
                        flagM = StartReachability(curent.ConversionTable[i].StateSet, finish);

                        if (flagM == true)
                        {
                            return true;
                        }

                    }

                }
            }

            return false;
        }

        #endregion
        

        #region проверка прохождение цепочек


        /// <summary>
        /// Проверка прохождение цепочки
        /// </summary>
        /// <param name="chain"></param>
        /// <returns></returns>
        public bool CheckingTheValidityChain(Queue<string> chain, ref List<DFAGraphNode<string>> nodeList)
        {
            komar.Clear();
            return Cheeck(head, chain, ref nodeList);
        }

        private bool Cheeck(DFAGraphNode<string> node, Queue<string> chain, ref List<DFAGraphNode<string>> nodeList)
        {

            bool error = true, result = false;

            
            if (!komar.Contains(node))
            {
                
                for (int i = 0; i < node.ConversionTable.Count; i++)
                {
                    if (node.ConversionTable[i].InputCharacter == "ε")
                    {
                        Console.WriteLine(node.ConversionTable[i].InputCharacter);
                        error = false;
                        List<DFAGraphNode<string>> tempL = new List<DFAGraphNode<string>>(nodeList);
                        List<DFAGraphNode<string>> tempKomar = new List<DFAGraphNode<string>>(komar);
             //           komar.Add(node);
                        nodeList.Add(node);
                        
                        result = Cheeck(node.ConversionTable[i].StateSet, new Queue<string>(chain), ref nodeList);                        
                        if (result == true)
                        {
                            return true;
                        }
                        else
                        {
                            komar = new List<DFAGraphNode<string>>(tempKomar);
                            nodeList = new List<DFAGraphNode<string>>(tempL);
                        }
                    }
                }
            }
           // komar.Clear();




            if ((chain.Count == 0) && (node.ItIsAdmissibleState == true))
            {
                nodeList.Add(node);
                return true;
            }
            else if ((chain.Count == 0) && (node.ItIsAdmissibleState == false))
            {
                return false;
            }

            

            if (chain.Count != 0)
            {

                
                for (int i = 0; i < node.ConversionTable.Count; i++)
                {
                    if ((node.ConversionTable[i].InputCharacter == chain.Peek()))
                    {
                        Queue<string> tempQ = new Queue<string>(chain);
                        List<DFAGraphNode<string>> tempL = new List<DFAGraphNode<string>>(nodeList);
                        string ch = chain.Dequeue();
                        nodeList.Add(node);
                        Console.WriteLine(node.ConversionTable[i].InputCharacter);

                        error = false;
                        result = Cheeck(node.ConversionTable[i].StateSet, new Queue<string>(chain), ref nodeList);
                        if (result == true)
                        {
                            return true;
                        }
                        else
                        {
                            chain = new Queue<string>(tempQ);
                            nodeList = new List<DFAGraphNode<string>>(tempL);
                        }
                    }

                    
                }
            }

            

            return false;
        }
        

    }

    #endregion




    #endregion
}
