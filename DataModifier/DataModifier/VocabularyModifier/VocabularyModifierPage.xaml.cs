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
using System.IO;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace DataModifier.VocabularyModifier
{
    /// <summary>
    /// VocabularyModifierPage.xaml 的交互逻辑
    /// </summary>
    public partial class VocabularyModifierPage : Page
    {
        public JObject vocabulary;

        public ObservableCollection<Word> words;

        public Word SelectedWord { get; set; }

        public VocabularyModifierPage()
        {
            InitializeComponent();
            this.DataContext = this;

            //Window.GetWindow(this).Closing += (sender, args) =>
            //{
            //    if(MessageBox.Show(string.Format("退出前是否要保存？", words[overallList.SelectedIndex].Summary), "确认", MessageBoxButton.YesNo)
            //    == MessageBoxResult.Yes)
            //    {
            //        SaveFile();
            //    }
            //};
        }

        private void openFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog { Filter = "JSON 文件(*.json)|*.json", Multiselect = false };
            var result = openFileDialog.ShowDialog();
            if (result != true) {
                return;
            }

            var path = openFileDialog.FileName;

            var data = File.ReadAllText(path);

            vocabulary = JObject.Parse(data);

            InitializeVocabulary();
        }

        private void saveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFile();
        }

        private void SaveFile()
        {
            if (vocabulary == null) return;
            SortWords();
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog() { AddExtension = true, DefaultExt = "*.json", Filter = "JSON 文件(*.json)|*.json" };
            var result = saveFileDialog.ShowDialog();
            if (result != true) return;
            vocabulary["vocabulary"] = JArray.FromObject(words);
            File.WriteAllText(saveFileDialog.FileName, vocabulary.ToString());
        }

        private void createFile_Click(object sender, RoutedEventArgs e)
        {
            vocabulary = JObject.Parse(@"{" +
                "\"name\":\"\"," +
                "\"vocabulary\":[]" +
                "}");
            InitializeVocabulary();
        }

        private void InitializeVocabulary()
        {
            vocabularyName.Text = vocabulary.Value<string>("name");
            words = vocabulary["vocabulary"].ToObject<ObservableCollection<Word>>();
            overallList.ItemsSource = words;

            overallList.SelectionChanged += (sender, e) => {
                wordInspector.DataContext = SelectedWord;
            };
        }

        private void WordsAddDelete_AddButtonClick(object sender, RoutedEventArgs e)
        {
            if (words != null)
            {
                words.Add(new Word());
                overallList.SelectedIndex = words.Count - 1;
            }
        }

        private void WordsAddDelete_DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            if (words != null && overallList.SelectedIndex >= 0 &&
                MessageBox.Show(string.Format("确认要删除{0}？", words[overallList.SelectedIndex].Summary), "确认", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                words.RemoveAt(overallList.SelectedIndex);
            }
        }

        private void AddDeleteButton_AddButtonClick(object sender, RoutedEventArgs e)
        {
            if (SelectedWord != null)
            {
                SelectedWord.meanings.Add(new Meaning());
            }

        }

        private void AddDeleteButton_DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            if(MeaningList.SelectedValue != null && 
                MessageBox.Show(string.Format("确认要删除{0}？",((Meaning)MeaningList.SelectedValue).Summary), "确认", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                SelectedWord.meanings.Remove(MeaningList.SelectedValue as Meaning);
            }
        }

        public List<string> Types => types;

        public readonly List<string> types = new List<string>
        {
            "n","pron","adj","adv","v","prep","conj","art","num","int","aux.v"
        };

        private void sortWords_Click(object sender, RoutedEventArgs e)
        {
            SortWords();
        }

        private void SortWords()
        {
            if (words == null) return;
            words = new ObservableCollection<Word>(words.OrderBy((e) => e));
            overallList.ItemsSource = words;
        }
    }
}
