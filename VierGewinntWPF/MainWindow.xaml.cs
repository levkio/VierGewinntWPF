using System.Windows;
using System.Windows.Controls;

namespace VierGewinntWPF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ConnectFourViewModelcs();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if(button != null)
            {
                int col = (int)button.GetValue(Grid.ColumnProperty);
                (DataContext as ConnectFourViewModelcs).PlayTokenCommand.Execute(col);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ConnectFourViewModelcs).StartGameCommand.Execute(null);
        }
    }
}
