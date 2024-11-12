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

namespace Chess2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        string x = "up";
        public void Click(object sender, MouseEventArgs e)
        {
            var border = sender as Border;

            var brush = new SolidColorBrush(Colors.Green);
            brush.Opacity = 0.5;

            border.Background=brush;
            switch(x)
            {
                case "up":
                    {
                        MoveUp(border);
                        break;
                    }
                case "down":
                    {
                        MoveDown(border);
                        break;
                    }
                case "left":
                    {
                        MoveLeft(border);
                        break;
                    }
                case "right":
                    {
                        MoveRight(border);
                        break;
                    }
            }

            //border.Background = null;
        }

        public void MoveUp(Border border)
        {

            var margin=border.Margin;

            border.Margin = new Thickness(margin.Left, margin.Top - 160, margin.Right, Margin.Bottom);
        }

        public void MoveDown(Border border)
        {

            var margin = border.Margin;

            border.Margin = new Thickness(margin.Left, margin.Top + 160, margin.Right, Margin.Bottom);
        }
        public void MoveLeft(Border border)
        {

            var margin = border.Margin;

            border.Margin = new Thickness(margin.Left - 160, margin.Top, margin.Right, Margin.Bottom);
        }
        public void MoveRight(Border border)
        {

            var margin = border.Margin;

            border.Margin = new Thickness(margin.Left + 160, margin.Top, margin.Right, Margin.Bottom);
        }

        private void movemode(object sender, RoutedEventArgs e)
        {
            x = movement.Text;
        }
    }
}