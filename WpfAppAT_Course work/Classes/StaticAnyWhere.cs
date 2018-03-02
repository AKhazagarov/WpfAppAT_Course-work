using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppAT_Course_work.Classes
{
    class StaticAnyWhere
    {
        private static MainWindow rootclass;
        private static DefinitionAutomaton def;



        public static MainWindow Rootclass { get => rootclass; set => rootclass = value; }
        public static DefinitionAutomaton Def { get => def; set => def = value; }


        public Automaton Automaton
        {
            get;
            set;
        }

        public struct TeamTransferStruct
        {
            public Automaton automaton;
            public int type;  //1 - в ДКА, 2 - НКА
        }


        public static string HelpURL { get; set; }


        public static TeamTransferStruct TeamTransfer
        {
            get;
            set;
        }

        public static string Error { get; set; }



        public static string[] prepareStringArr(string str)
        {
            str = str.Trim();

            str = str.Replace("{", "");

            str = str.Replace("}", "");

            str = str.Replace(" ", "$");

            str = str.Replace(",", "$");

            str = str.Replace("$$", "$");

            str = str.Replace("$", ",");

            return str.Split(','); ;
        }


        public static string prepareString(string str)
        {
            str = str.Trim();

            str = str.Replace("{", "");

            str = str.Replace("}", "");

            str = str.Replace(" ", "$");

            str = str.Replace(",", "$");

            str = str.Replace("$$", "$");

            str = str.Replace("$", ",");

            return str;
        }

        public static bool occurrence(string[] original, string[] str)
        {
            bool state = true, localState = false;

            for (int i = 0; i < str.Length; i++)
            {
                localState = false;
                for (int j = 0; j < original.Length; j++)
                {
                    if (str[i] == original[j])
                    {
                        localState = true;
                        break;
                    }
                    
                }

                if (localState == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
