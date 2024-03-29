﻿using System;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for InputBox.xaml
    /// </summary>
    public partial class SelectOccupiedDateBox : Window
    {

        public SelectOccupiedDateBox()
        {
            InitializeComponent();
        }

        public DateTime SelectedOccupiedDate { get; set; }

        private void SelectOccupiedDate_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedOccupiedDate = sender;
            this.Close();
        }
    }
}
