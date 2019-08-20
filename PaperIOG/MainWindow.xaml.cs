using System.IO;
using System.Windows;
using Newtonsoft.Json;
using PaperIOG.DataContracts;

namespace PaperIOG
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var visio = JsonConvert.DeserializeObject<JVisio>(File.ReadAllText("./visio"));

            ContentControl.Content = new PaperGame(visio);
        }
    }
}
