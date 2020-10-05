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

namespace DataModifier
{
    /// <summary>
    /// AddDeleteButton.xaml 的交互逻辑
    /// </summary>
    public partial class AddDeleteButton : UserControl
    {
        public event RoutedEventHandler AddButtonClick, DeleteButtonClick;
        public Orientation Orientation
        {
            get; set;
        }

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(AddDeleteButton));

        public AddDeleteButton()
        {
            InitializeComponent();
        }

        public void Add_Click(object sender, RoutedEventArgs args)
        {
            AddButtonClick?.Invoke(sender, args);
        }

        public void Delete_Click(object sender, RoutedEventArgs args)
        {
            DeleteButtonClick?.Invoke(sender, args);
        }
    }
}
