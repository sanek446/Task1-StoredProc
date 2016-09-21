using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace Task1WithUC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            textBox.Text = "Server = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=|DataDirectory|DB.mdf;Integrated Security = true";
        }

        public string str;
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MyUC.getData(textBox.Text, textBox1.Text);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            MyUC.blockColumn(Convert.ToInt32(textBox2.Text));
        }

    }
}
