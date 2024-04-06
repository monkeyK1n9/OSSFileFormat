using Microsoft.Win32;
using OSSFileLibrary;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static OSSFileLibrary.FileStructure;

namespace CVCreator
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

        // event handler to save the file in our folder (Debug folder)
        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            bool correctAge = UInt16.TryParse(age.Text, out var ageNumber);

            if(correctAge == true)
            {

                KingFile writeKingFile = FileHandler.CreateFile(name.Text, UInt16.Parse(age.Text), 0,
                    new Job[] {}
                   );

                byte[] writeBytes = FileHandler.FileToByte(writeKingFile);

                SaveFileDialog saveFileDialog = new SaveFileDialog();

                saveFileDialog.Filter = "king files (*.king)|*.king|All files (*.*)|*.*";

                bool? result = saveFileDialog.ShowDialog();

                if (result == true)
                {

                    File.WriteAllBytes(saveFileDialog.FileName, writeBytes);
                }
            }
            else
            {
                MessageBox.Show("Enter Correct Age");
            }
        }

        // event handler to import and read the file 
        private void ImportMenuItem_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog fileDialog = new OpenFileDialog();
            bool? result = fileDialog.ShowDialog();

            if (result == true)
            {
                byte[] readBytes = File.ReadAllBytes(fileDialog.FileName);
                KingFile readKingFile = FileHandler.ByteToFile(readBytes);

                name.Text = readKingFile.Name;
                age.Text = readKingFile.Age.ToString();
            }

        }

        private void ReadMoreMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}