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

namespace traviBot.Content
{
    /// <summary>
    /// Interaction logic for MapWIndow.xaml
    /// </summary>
    public partial class MapWIndow : UserControl
    {
        public MapWIndow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Map elephants= new Map();
            richTextBox1.AppendText(elephants.animalFinder(int.Parse(comboBoxDistance.Text), new Village("","","-51","33"), comboBoxAnimals.Text));
        }
    }
}
