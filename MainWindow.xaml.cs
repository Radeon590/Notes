using Microsoft.Win32;
using System;
using System.IO;
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
        private string _openedFile = "";

        public MainWindow()
        {
            InitializeComponent();
            //
            LoadTextStyleSettings();
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
            if(_openedFile != "")
            {
                File.WriteAllText(_openedFile, NotingField.Text);
            }
            else
            {
                SaveAs(sender, e);
            }
        }

        private void SaveAs(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.FileName = "Document";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                SetOpenedFile(dialog);
                File.WriteAllText(dialog.FileName, NotingField.Text);
            }
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.FileName = "Document";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt";
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                NotingField.Text = File.ReadAllText(dialog.FileName);
                SetOpenedFile(dialog);
            }
        }

        private void SetOpenedFile(FileDialog dialog)
        {
            _openedFile = dialog.FileName;
            var filePathSplitted = dialog.FileName.Split('\\');
            Title = filePathSplitted[filePathSplitted.Length - 1].Split('.')[0];
        }

        private void LoadTextStyleSettings()
        {
            for (int i = 1; i < 101; i++)
            {
                FontSizeBox.Items.Add(new TextBox() { Text = i.ToString() });
            }
            if (!File.Exists(@"FontSizeSettings"))
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
        }
    }
}