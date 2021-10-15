using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Diagnostics;

namespace DataModifier.VocabularyModifier
{
    [Serializable]
    public class Word : IComparable<Word>, INotifyPropertyChanged
    {
        public Word()
        {
            phonic_changes = new ObservableCollection<string>();
            meanings = new ObservableCollection<Meaning>();

            PropertyChanged += (sender, arg) =>
            {
                Trace.WriteLine(string.Format("{0} changed notified", arg.PropertyName));
            };
        }

        [JsonIgnore]
        private ObservableCollection<string> m_Phonic_changes;

        public ObservableCollection<string> phonic_changes {
            get { return m_Phonic_changes; }
            set
            {
                m_Phonic_changes = value;
                m_Phonic_changes.CollectionChanged += OnPhonicsChanged;
            }
        }
        public ObservableCollection<Meaning> meanings;

        public event PropertyChangedEventHandler PropertyChanged;

        [JsonIgnore]
        private string m_Word;

        public string word
        {
            get => m_Word; set
            {
                m_Word = value;
                OnPropertyChanged(this, "word");
                OnPropertyChanged(this, "Summary");
            }
        }

        [JsonIgnore]
        private int m_Paging;
        public int paging
        {
            get => m_Paging;
            set
            {
                if (value >= 0) m_Paging = value;
                OnPropertyChanged(this, "paging");
                OnPropertyChanged(this, "Summary");
            }
        }

        [JsonIgnore]
        public ObservableCollection<string> PhonicChanges { get
            {
                if (phonic_changes == null) phonic_changes = new ObservableCollection<string>();
                return phonic_changes;
            } }

        [JsonIgnore]
        public ObservableCollection<Meaning> Meanings
        {
            get
            {
                if (meanings == null) meanings = new ObservableCollection<Meaning>();
                return meanings;
            }
        }

        public int CompareTo(object obj)
        {
            return word.CompareTo(obj);
        }

        public int CompareTo(Word other)
        {
            return string.Compare(this.word, other.word);
        }

        [JsonIgnore]
        public string Summary => GetSummary(this);

        public static string GetSummary(Word el)
        {
            StringBuilder res = new StringBuilder();

            res.Append(string.Format("[{0}] ", el.paging));

            if (el.PhonicChanges.Count > 0)
            {
                res.Append(el.word + "/" + el.PhonicChanges.Aggregate((sum, e) => sum += "/" + e));
            }
            else res.Append(el.word);
            
            return res.ToString();
        }

        public void OnPropertyChanged(object sender, string property)
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(sender, new PropertyChangedEventArgs(property));
        }

        public void OnPhonicsChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            OnPropertyChanged(sender, "PhonicChanges");
            OnPropertyChanged(this, "Summary");
        }
    }

    public class Meaning : INotifyPropertyChanged
    {
        public Meaning()
        {
            translations = new ObservableCollection<string>();
            verb_transformations = new ObservableCollection<VerbTransformation>();
            adjective_transformations = new ObservableCollection<AdjectiveTransformation>();
        }

        public ObservableCollection<string> translations;
        public string type;
        public ObservableCollection<VerbTransformation> verb_transformations;
        public ObservableCollection<AdjectiveTransformation> adjective_transformations;

        public event PropertyChangedEventHandler PropertyChanged;

        [JsonIgnore]
        public string Type
        {
            get => type;
            set
            {
                type = value;
                OnPropertyChanged(this, "Type");
            }
        }

        [JsonIgnore]
        public ObservableCollection<string> Translations
        {
            get
            {
                if (translations == null) translations = new ObservableCollection<string>();
                return translations;
            }
        }

        [JsonIgnore]
        public ObservableCollection<VerbTransformation> VerbTransformations
        {
            get
            {
                if (verb_transformations == null) verb_transformations = new ObservableCollection<VerbTransformation>();
                return verb_transformations;
            }
        }

        [JsonIgnore]
        public ObservableCollection<AdjectiveTransformation> AdjectiveTransformations
        {
            get
            {
                if (adjective_transformations == null) adjective_transformations = new ObservableCollection<AdjectiveTransformation>();
                return adjective_transformations;
            }
        }

        public void OnPropertyChanged(object sender, string property)
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(sender, new PropertyChangedEventArgs(property));
        }

        [JsonIgnore]
        public string Summary => GetSummary(this);

        public static string GetSummary(Meaning el)
        {
            if (el.translations.Count <= 0) return el.type + ".";
            return el.type + ". " + el.translations.Aggregate((sum, cur) => sum + "；" + cur);
        }
    }

    public class Transformation
    {

    }

    public class VerbTransformation : Transformation
    {
        public string present_participle, past_tense, past_participle;
    }

    public class AdjectiveTransformation : Transformation
    {
        public string comparative, superlative;
    }

    public class DefaultWordComparer : IComparer<Word>
    {
        public int Compare(Word x, Word y)
        {
            return x.word.CompareTo(y.word);
        }

        public static DefaultWordComparer instance;

        static DefaultWordComparer()
        {
            instance = new DefaultWordComparer();
        }
    }

    public class WordPageComparer : IComparer<Word>
    {
        public int Compare(Word x, Word y)
        {
            if (x.paging - y.paging != 0) return x.paging - y.paging;
            else return x.word.CompareTo(y.word);
        }

        public static WordPageComparer instance;

        static WordPageComparer()
        {
            instance = new WordPageComparer();
        }
    }
}
