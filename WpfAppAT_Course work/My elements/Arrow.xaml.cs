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

namespace WpfAppAT_Course_work.My_elements
{
    /// <summary>
    /// Логика взаимодействия для Arrow.xaml
    /// </summary>
    public partial class Arrow : UserControl
    {
  //      

        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }
        public string Text { get; set; }



        public Arrow(string text, double X1, double Y1, double X2, double Y2)
        {
            InitializeComponent();

            this.X1 = X1;
            this.X2 = X2;
            this.Y1 = Y1;
            this.Y2 = Y2;
            this.Text = text;

            Random random = new Random();

            // координаты центра отрезка
        //    double X3 = (this.X1 + this.X2) / 2;
        //    double Y3 = (this.Y1 + this.Y2) / 2



            double X3 = this.X1 + (this.X2 - this.X1) / 3;
            double Y3 = this.Y1 + (this.Y2 - this.Y1) / 3;

            double XL = this.X1 + (this.X2 - this.X1) / 3;
            double YL = this.Y1 + (this.Y2 - this.Y1) / 3;

            // длина отрезка
            double d = Math.Sqrt(Math.Pow(this.X2 - this.X1, 2) + Math.Pow(this.Y2 - this.Y1, 2));

            // координаты вектора
            double X = this.X2 - this.X1;
            double Y = this.Y2 - this.Y1;

            // координаты точки, удалённой от центра к началу отрезка на 10px
            double X4 = X3 - (X / d) * 10;
            double Y4 = Y3 - (Y / d) * 10;

            // полученные множители x и y => координаты вектора перпендикуляра
            double Xp = this.Y2 - this.Y1;
            double Yp = this.X1 - this.X2;

            // координаты перпендикуляров, удалённой от точки X4;Y4 на 5px в разные стороны
            double X5 = X4 + (Xp / d) * 5;
            double Y5 = Y4 + (Yp / d) * 5;
            double X6 = X4 - (Xp / d) * 5;
            double Y6 = Y4 - (Yp / d) * 5;

            SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));

            Line line = new Line
            {
                Stroke = brush,
                X1 = this.X1,
                Y1 = this.Y1,
                X2 = this.X2,
                Y2 = this.Y2
            };
            line.StrokeThickness = 3;
            mCanvas.Children.Add(line);


            line = new Line
            {
                Stroke = brush,
                X1 = X3,
                Y1 = Y3,
                X2 = X5,
                Y2 = Y5
            };
            line.StrokeThickness = 3;
            mCanvas.Children.Add(line);

            line = new Line
            {
                Stroke = brush,
                X1 = X3,
                Y1 = Y3,
                X2 = X6,
                Y2 = Y6
            };
            line.StrokeThickness = 3;
            mCanvas.Children.Add(line);



            Label label = new Label();
            label.Margin = new Thickness(XL, YL - 20, 0, 0);
            label.Content = text;
            label.FontSize = 20;
            label.Foreground = brush = new SolidColorBrush(Color.FromArgb(255, 255, 105, 0));
            label.Background = brush = new SolidColorBrush(Color.FromArgb(100, 255, 255, 255));
            mCanvas.Children.Add(label);


        }


    }
}
