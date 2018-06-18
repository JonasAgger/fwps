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

namespace Opgave1
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

        private void dgridAgents_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
                varrocounts.EditCommand.Execute(new Object());
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            string search = tbxSearchBox.Text;

            if (search == string.Empty)
            {
                dgridVarros.Items.Filter = o => true;
            }
            else
            {
                dgridVarros.Items.Filter = o =>
                {
                    var obj = o as VarroCount;

                    if (obj == null) return false;

                    bool shouldBeIncluded = String.Equals(obj.Bistade, search,
                        StringComparison.CurrentCultureIgnoreCase);

                    DateTime searchDateTime;

                    if (DateTime.TryParseExact(search, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None,
                        out searchDateTime))
                        shouldBeIncluded |= obj.Time.Date.Equals(searchDateTime.Date);

                    return shouldBeIncluded;
                };
            }

        }
    }
}
