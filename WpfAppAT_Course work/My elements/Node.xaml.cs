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

namespace WpfAppAT_Course_work.My_elements
{
    /// <summary>
    /// Логика взаимодействия для Node.xaml
    /// </summary>
    public partial class Node : UserControl
    {
        public Node(string name, string loop = null, bool itIsAdmissibleState = false, bool itIsInitialState = false, int size = 100)
        {
            InitializeComponent();

            Size = size;

            if (loop != null)
            {
                mArrowArc.Visibility = Visibility.Visible;
                mLabelArc.Visibility = Visibility.Visible;
                mLabelArc.Content = loop;
            }
            else
            {
                mArrowArc.Visibility = Visibility.Hidden;
                mLabelArc.Visibility = Visibility.Hidden;
            }
            

            Name = name;

            if ((itIsAdmissibleState == false) && (itIsInitialState == false))
            {
                mOutEllipse.Visibility = Visibility.Collapsed;
            }
            else if ((itIsAdmissibleState == true) && (itIsInitialState == false))
            {
                mOutEllipse.Visibility = Visibility.Visible;
            }
            else if ((itIsAdmissibleState == false) && (itIsInitialState == true))
            {
                mOutEllipse.Visibility = Visibility.Visible;
                SolidColorBrush blueBrush = new SolidColorBrush();
                blueBrush.Color = Colors.LightGreen;
                mOutEllipse.Fill = blueBrush;

                SolidColorBrush strokeBrush = new SolidColorBrush();
                strokeBrush.Color = Colors.LightGreen;
                mOutEllipse.Stroke = strokeBrush;
            }
            else if ((itIsAdmissibleState == true) && (itIsInitialState == true))
            {
                mOutEllipse.Visibility = Visibility.Visible;
                SolidColorBrush blueBrush = new SolidColorBrush();
                blueBrush.Color = Colors.LightGreen;
                mOutEllipse.Fill = blueBrush;
            }


        }

        


        public bool ItIsOnlyNode
        {
            set
            {
                if (value == true)
                {
                    mOutEllipse.Visibility = Visibility.Collapsed;
                    mArrowArc.Visibility = Visibility.Hidden;
                    mLabelArc.Visibility = Visibility.Hidden;
                }
                else
                {
                    mOutEllipse.Visibility = Visibility.Collapsed;
                    mArrowArc.Visibility = Visibility.Hidden;
                    mLabelArc.Visibility = Visibility.Hidden;
                }
                
            }
        }

        public Node(DFAGraphNode<string> nodeA, int size = 100)
        {
            InitializeComponent();

            Size = size;

            string loop = "";

            for (int i = 0; i < nodeA.ConversionTable.Count; i++)
            {
                if (nodeA.ConversionTable[i].StateSet.Name == nodeA.Name)
                {
                    loop += nodeA.ConversionTable[i].InputCharacter + " ";
                }
            }

            if (loop != "")
            {
                mArrowArc.Visibility = Visibility.Visible;
                mLabelArc.Visibility = Visibility.Visible;
                mLabelArc.Content = loop;
            }
            else
            {
                mArrowArc.Visibility = Visibility.Hidden;
                mLabelArc.Visibility = Visibility.Hidden;
            }


            Name = nodeA.Name;

            if ((nodeA.ItIsAdmissibleState == false) && (nodeA.ItIsInitialState == false))
            {
                mOutEllipse.Visibility = Visibility.Collapsed;
            }
            else if ((nodeA.ItIsAdmissibleState == true) && (nodeA.ItIsInitialState == false))
            {
                mOutEllipse.Visibility = Visibility.Visible;
            }
            else if ((nodeA.ItIsAdmissibleState == false) && (nodeA.ItIsInitialState == true))
            {
                mOutEllipse.Visibility = Visibility.Visible;
                SolidColorBrush blueBrush = new SolidColorBrush();
                blueBrush.Color = Colors.LightGreen;
                mOutEllipse.Fill = blueBrush;

                SolidColorBrush strokeBrush = new SolidColorBrush();
                strokeBrush.Color = Colors.LightGreen;
                mOutEllipse.Stroke = strokeBrush;
            }
            else if ((nodeA.ItIsAdmissibleState == true) && (nodeA.ItIsInitialState == true))
            {
                mOutEllipse.Visibility = Visibility.Visible;
                SolidColorBrush blueBrush = new SolidColorBrush();
                blueBrush.Color = Colors.LightGreen;
                mOutEllipse.Fill = blueBrush;
            }


        }

        /*
        Point oldPosition;
        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Node elem = sender as Node;
            oldPosition = e.GetPosition(elem);
            elem.CaptureMouse();
        }

        private void Ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            (sender as UIElement).ReleaseMouseCapture();
        }

        private void Ellipse_MouseMove(object sender, MouseEventArgs e)
        {
            Node elem = sender as Node;
            if (!elem.IsMouseCaptured) return;

            elem.Margin = new Thickness(e.GetPosition(this).X - oldPosition.X, e.GetPosition(this).Y - oldPosition.Y, 0, 0);

            Console.WriteLine((e.GetPosition(this).X - oldPosition.X) + " " + (e.GetPosition(this).Y - oldPosition.Y));
            //   Canvas.SetLeft(elem, e.GetPosition(this).X - oldPosition.X);
            // Canvas.SetTop(elem, e.GetPosition(this).Y - oldPosition.Y);
        }  



        private void lbl1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid lbl = (Grid)sender;
            DragDrop.DoDragDrop(lbl, mGrid, DragDropEffects.Copy);
        }
         */
        public new int Size
        {
            set
            {
                this.Width = value;
                this.Height = value * 1.5;

                mGridBack.Width = value;
                mGridBack.Height = value * 1.5;
                mArrowArc.Height = value * 1.2;

                mGrid.Width = value;
                mGrid.Height = value;
                mOutEllipse.Width = value;
                mOutEllipse.Height = value;
                mEllipse.Width = value * 0.8;
                mEllipse.Height = value * 0.8;
                mLabel.FontSize = 0.16 * value;

            }
        }
        

        public new string Name
        {
            get
            {
                return mLabel.Content + "";
            }
            set
            {
                mLabel.Content = value;
            }
        }

    }
}
