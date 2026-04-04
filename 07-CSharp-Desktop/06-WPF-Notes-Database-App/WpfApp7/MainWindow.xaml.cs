using System.Windows;

namespace WpfApp7
{
    public partial class MainWindow : Window
    {
        ViewModel vm = new ViewModel();

        public MainWindow()
        {
            InitializeComponent();
            NotesListView.ItemsSource = vm.Notes;
            vm.LoadNotes();
        }

        private void AddNote_Click(object sender, RoutedEventArgs e)
        {
            vm.Title = TitleBox.Text;
            vm.Text = TextBoxNote.Text;
            vm.Deadline = DeadlinePicker.SelectedDate;
            vm.AddNote();
            NotesListView.Items.Refresh();
        }

        private void DeleteNote_Click(object sender, RoutedEventArgs e)
        {
            vm.SelectedNote = (Note)NotesListView.SelectedItem;
            vm.DeleteNote();
            NotesListView.Items.Refresh();
        }
    }
}
