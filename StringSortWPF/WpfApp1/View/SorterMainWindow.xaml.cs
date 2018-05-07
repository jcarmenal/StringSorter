using StringSorter.Interface;
using StringSorter.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace StringSorter.View
{
    /// <summary>
    /// Interaction logic for SorterMainWindow.xaml
    /// </summary>
    public partial class SorterMainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _sortedString;

        public string SortedString
        {
            get { return _sortedString; }
            set
            {
                if (_sortedString != value)
                {
                    _sortedString = value;
                    NotifyPropertyChanged("SortedString");
                }
            }
        }

        private Sorter _sorter = new Sorter();

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public SorterMainWindow()
        {
            DataContext = this;
            InitializeComponent();
            InitAlgoCombo();
        }

        private void InitAlgoCombo()
        {
            IEnumerable<ISorter> sorters = _sorter.GetSorters();
            foreach(ISorter s in sorters)
            {
                cmbAlgo.Items.Add(s);
            }
            cmbAlgo.SelectedIndex = 0;
        }

        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SortedString = _sorter.Sort((ISorter)cmbAlgo.SelectedItem, txtInput.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
