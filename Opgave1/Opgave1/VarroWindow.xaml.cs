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
using System.Windows.Shapes;

namespace Opgave1
{
    /// <summary>
    /// Interaction logic for VarroWindow.xaml
    /// </summary>
    public partial class VarroWindow : Window
    {
        public VarroWindow()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (tbxBistade.Text == string.Empty)
                MessageBox.Show("Bistade skal have en værdi");
            else
            {
                DialogResult = true;
            }
        }
    }
}
