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

        //TranslateTransform left = new TranslateTransform(-80, 0);
        //TranslateTransform right = new TranslateTransform(80, 0);
        //TranslateTransform up = new TranslateTransform(0, 80);
        //TranslateTransform down = new TranslateTransform(0, 80);
        public void test(object sender, MouseEventArgs e)
        {
            var pawn = e.OriginalSource as FrameworkElement;
            var element = pawn.Tag;
            var name = pawn.Name;

            var brush = new SolidColorBrush(Colors.Green);
            brush.Opacity = 0.5;

            //name.Fill = brush;
            MessageBox.Show($"{element}");

            
        }
    }
}
