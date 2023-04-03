using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Notes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //
            for(int i = 1; i < 101; i++)
            {
                FontSizeBox.Items.Add(new TextBox() { Text = i.ToString() });
            }
            if(!File.Exists(@"FontSizeSettings"))
                FontSizeBox.SelectedItem = FontSizeBox.Items[20];
            else
                NotingField.FontSize = Convert.ToDouble(File.ReadAllText(@"FontSizeSettings"));
            if (!File.Exists(@"FontFamilySettings"))
                FontFamilyBox.SelectedItem = FontFamilyBox.Items[0];
            else
                NotingField.FontFamily = new FontFamily(File.ReadAllText(@"FontFamilySettings"));
            if (!File.Exists(@"FontColorSettings"))
                FontColorBox.SelectedItem = FontColorBox.Items[0];
            else
                NotingField.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(File.ReadAllText(@"FontColorSettings")));
            if (File.Exists(@"Source")) 
                NotingField.Text = File.ReadAllText(@"Source");
        }

        private void FontFamilySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextBox selected = ((sender as ComboBox).SelectedItem as TextBox);
            NotingField.FontFamily = new FontFamily(selected.Text);
            File.WriteAllText(@"FontFamilySettings", selected.Text);
        }

        private void FontSizeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextBox selected = ((sender as ComboBox).SelectedItem as TextBox);
            NotingField.FontSize = Convert.ToDouble(selected.Text);
            File.WriteAllText(@"FontSizeSettings", selected.Text);
        }

        private void FontColorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextBox selected = ((sender as ComboBox).SelectedItem as TextBox);
            NotingField.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(selected.Text));
            File.WriteAllText(@"FontColorSettings", selected.Text);
        }

        private void SaveNotes(object sender, RoutedEventArgs e)
        {
            var alphabit = new string[]{ "a", "b", "c", "d"};
            string notes = Regex.Replace(NotingField.Text, @"^\s*$\n|\r", string.Empty, RegexOptions.Multiline).TrimEnd();
            NotingField.Text = notes;
            File.WriteAllText(@"Source", NotingField.Text);
        }
    }
}