using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;


namespace Locadora_Kramos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=local;Integrated Security=True");

        public MainWindow()
        {
            InitializeComponent();
            LoadGrid();
        }

        public void clearData()
        {
            name_txt.Clear();
            phone_txt.Clear();
            address_txt.Clear();
            email_txt.Clear();
            search_txt.Clear();

        }
        private void ClearDataBtn_Click(object sender, RoutedEventArgs e)
        {
            clearData();
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("select * from local", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            datagrid.ItemsSource = dt.DefaultView;
        }


        public bool isValid()
        {
            if (name_txt.Text == string.Empty)
            {
                MessageBox.Show("Name is require");
                return false;
            }
            if (phone_txt.Text == string.Empty)
            {
                MessageBox.Show("Phone is require");
                return false;
            }
            if (address_txt.Text == string.Empty)
            {
                MessageBox.Show("Address is require");
                return false;
            }
            if (email_txt.Text == string.Empty)
            {
                MessageBox.Show("Email is require");
                return false;
            }

            return true;
        }

        private SqlConnection GetConnection()
        {
            return con;
        }

        private void InsertBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO LOCAL VALUES (@Name, @Phone, @Address, @Email)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Name", name_txt.Text);
                    cmd.Parameters.AddWithValue("@Phone", phone_txt.Text);
                    cmd.Parameters.AddWithValue("@Address", address_txt.Text);
                    cmd.Parameters.AddWithValue("@Email", email_txt.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadGrid();
                    MessageBox.Show("Succefully register");
                    clearData();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand delete = new SqlCommand("delete FROM local WHERE ID = " + search_txt.Text + " ", con);
            try
            {
                delete.ExecuteNonQuery();
                MessageBox.Show("Record is been deleted");
                con.Close();
                clearData();
                LoadGrid();
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Not deleted " + ex.Message);
                clearData();

            }
            finally
            {
                con.Close();
            }


        }

    }
}
