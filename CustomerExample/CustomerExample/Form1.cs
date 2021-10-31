using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CustomerExample
{
    public partial class Form1 : Form
    {

        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter;

        public Form1()
        {
            InitializeComponent();
        }

        void GetCustomers()
        {
            connection = new SqlConnection("Data Source=DESKTOP-NOQBATS;Initial Catalog=DatabaseExample;Integrated Security=True");
            connection.Open();
            adapter = new SqlDataAdapter("Select * from customer", connection);

            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            connection.Close();

            // dataGridView'in prop. kısmında "AutoSizeColumnsMode" kısmını fill yparsak dolu olarak gelir.
            // dataGridView -> "selectionMode" > fullmodeselected dersek bir değer seçince tüm satır seçilip okunabilir.
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetCustomers();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Bir veriye tıklandığı zaman olacakları burada kodlayabiliyoruz. 
            TxtCustomerNo.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            TxtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            TxtSurname.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            TxtDate.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            TxtPhone.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string query = "insert into customer(name,surname,date,tel) values (@name,@surname,@date,@tel)";
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", TxtName.Text);
            command.Parameters.AddWithValue("@surname", TxtSurname.Text);
            command.Parameters.AddWithValue("@date", TxtDate.Value);
            command.Parameters.AddWithValue("@tel", TxtPhone.Text);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            GetCustomers();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            string query = "delete from customer where mno=@mno";
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@mno", Convert.ToInt32(TxtCustomerNo.Text));

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            GetCustomers();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            string query = "update customer set name=@name, surname=@surname, date=@date, tel=@tel where mno=@mno";
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@mno", Convert.ToInt32(TxtCustomerNo.Text));
            command.Parameters.AddWithValue("@name", (TxtName.Text));
            command.Parameters.AddWithValue("@surname", (TxtSurname.Text));
            command.Parameters.AddWithValue("@date", (TxtDate.Value));
            command.Parameters.AddWithValue("@tel", (TxtPhone.Text));

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            GetCustomers();
        }
    }
}
