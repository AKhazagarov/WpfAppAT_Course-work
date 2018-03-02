using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppAT_Course_work.Classes
{
    public class DefinitionAutomaton
    {
        private string[] q;
        private string[] sigma;
        private string[][] delta;
        private string q0;
        private string[] f;
        private string a;
        private short type;     //0 - ДКА
                                //1 - НКА
                                //2 - еНКА
                                //3- неизвестно
                         

        
       

        public DefinitionAutomaton()
        {

        }

        public string[] Q { get => q; set => q = value; }
        public string[] Sigma { get => sigma; set => sigma = value; }
        public string[][] Delta { get => delta; set => delta = value; }
        public string Q0 { get => q0; set => q0 = value; }
        public string[] F { get => f; set => f = value; }
        public string A { get => a; set => a = value; }
        public short Type { get => type; set => type = value; }

        public bool ItIsTest { get; set; }
        public string[] Tabel { get; set; }
    }
}
