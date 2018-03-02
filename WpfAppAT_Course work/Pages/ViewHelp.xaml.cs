using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Xps.Packaging;
using WpfAppAT_Course_work.Classes;

namespace WpfAppAT_Course_work.Pages
{
    /// <summary>
    /// Логика взаимодействия для ViewHelp.xaml
    /// </summary>
    public partial class ViewHelp : Page
    {
        public ViewHelp()
        {
            InitializeComponent();
            
            XpsDocument doc = new XpsDocument(StaticAnyWhere.HelpURL, FileAccess.Read);
            docViewer.Document = doc.GetFixedDocumentSequence();
            doc.Close();
        }
    }
}
