using PersonsListPractice.ViewModel;
using System.Windows;

namespace PersonsListPractice.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // we connect the Viewmodel to the window throught
            this.DataContext = new MainWindowVM();
        }
    }
}