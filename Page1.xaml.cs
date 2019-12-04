using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
namespace Company
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        private const string ConnString = @"Data Source = (LocalDB)\MSSQLLocalDB; Initial Catalog=master; Integrated Security=True";

        public Page1()
        {
            InitializeComponent();
            ListBox_Filling();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Page2 employees = new Page2(this.peopleListBox.SelectedItem);
            this.NavigationService.Navigate(employees);
        }

        private void peopleListBox_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = (sender as ListBox).SelectedItem;
            if (item != null)
            {
                this.DataContext = this.peopleListBox.SelectedItem;
            }

        }

        public void ListBox_Filling()
        {
            SqlConnection con = new SqlConnection(ConnString);
            SqlCommand cmd = new SqlCommand("select FirstName, LastName, BirthDate, Department from Employees", con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            con.Open();
            DataSet ds = new DataSet();
            ad.Fill(ds,"Employees");
            peopleListBox.DataContext= ds;
            con.Close();
        }

        private void peopleListBox_GotFocus(object sender, RoutedEventArgs e)
        {

            this.DataContext = this.peopleListBox.SelectedItem;
            Variabile.indexListBox = peopleListBox.SelectedIndex;

        }

        public static class Variabile
        {
            public static int indexListBox;
        }
    }
}
