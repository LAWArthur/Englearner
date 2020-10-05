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
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DataModifier
{
    /// <summary>
    /// EditableList.xaml 的交互逻辑
    /// </summary>
    public partial class EditableList : UserControl
    {
        private ObservableCollection<ListItem> items = new ObservableCollection<ListItem>();
        public ObservableCollection<ListItem> Items { get {
                return items;
        } }

        public string Title
        {
            get {
                return (string)GetValue(TitleProperty);
            }
            set {
                SetValue(TitleProperty, value);
            }
        }

        public class ListItem {
            public ListItem(int index, EditableList @this)
            {
                this.index = index;
                editableList = @this;
                Trace.WriteLine("Create");
            }

            public int index;

            public EditableList editableList;

            public string Value
            {
                get {
                    if ((editableList.DataContext as ObservableCollection<string>) != null)
                    {
                        return ((ObservableCollection<string>)editableList.DataContext)[index];
                    }
                    return null;
                }
                set
                {
                    if ((editableList.DataContext as ObservableCollection<string>) != null)
                    {
                        ((ObservableCollection<string>)editableList.DataContext)[index] = value;
                    }
                }
            }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(EditableList));
        public string Subtitle
        {
            get
            {
                return (string)GetValue(SubtitleProperty);
            }
            set
            {
                SetValue(SubtitleProperty, value);
            }
        }

        public static readonly DependencyProperty SubtitleProperty = DependencyProperty.Register("Subtitle", typeof(string), typeof(EditableList));

        public EditableList()
        {
            
            InitializeComponent();
            DataContextChanged += OnDataContextChange;
            phonicChangesList.DataContext = this;

        }

        private void PhonicsAdd_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as ObservableCollection<string>) != null)
            {
                ((ObservableCollection<string>)DataContext).Add("");
                phonicChangesList.SelectedIndex = ((ObservableCollection<string>)DataContext).Count - 1;
                ModifyData();
            }
        }

        private void PhonicsDelete_Click(object sender, RoutedEventArgs e)
        {
            if (phonicChangesList.SelectedIndex >= 0 && (DataContext as ObservableCollection<string>) != null)
            {
                ((ObservableCollection<string>)DataContext).RemoveAt(phonicChangesList.SelectedIndex);
                ModifyData();
            }
        }

        private void OnDataContextChange(object sender, DependencyPropertyChangedEventArgs args)
        {
            Trace.WriteLine("Data Context Changed");

            ModifyData();
        }

        private void ModifyData()
        {
            if ((DataContext as ObservableCollection<string>) != null)
            {
                int len = (DataContext as ObservableCollection<string>).Count;
                for (; Items.Count > len; Items.RemoveAt(Items.Count - 1)) ;
                for (; Items.Count < len; Items.Add(new ListItem(Items.Count, this))) ;
            }
        }
    }
}
