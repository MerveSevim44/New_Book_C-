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

namespace ANewBook_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Data Source=EVREM_ANKA;Initial Catalog=Books;Integrated Security=True");   


        void List()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Newbook",conn); 
            da.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
             List();
       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List();
        }
        string occasion= "";
        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into Newbook (BookName,Author,Type,Page,Occation) values (@b1,@b2,@b3,@b4,@b5)",conn);
            cmd.Parameters.AddWithValue("@b1",textBox2.Text);
            cmd.Parameters.AddWithValue("@b2",textBox3.Text);
            cmd.Parameters.AddWithValue("@b3",comboBox1.Text);
            cmd.Parameters.AddWithValue("@b4",textBox4.Text);
            cmd.Parameters.AddWithValue("@b5",occasion);
            cmd.ExecuteNonQuery();  
            conn.Close();
            MessageBox.Show("the book saved in the system!");
            List();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            occasion = "0";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            occasion="1";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int choose = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView1.Rows[choose].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[choose].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[choose].Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[choose].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[choose].Cells[4].Value.ToString();

            if (dataGridView1.Rows[choose].Cells[5].Value.ToString() == "True")
            {
                radioButton1.Checked = true;

            }
            else
            {
                radioButton2.Checked = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("delete from Newbook where BookId = @b1",conn);
            cmd.Parameters.AddWithValue("@b1", textBox1.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("the book deleted from list!");
            List();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("update NewBook set BookName=@b1,Author=@b2,Type=@b3,Page=@b4,Occation=@b5 where BookId=@b6",conn);
            cmd.Parameters.AddWithValue("@b1",textBox2.Text);
            cmd.Parameters.AddWithValue("@b2",textBox3.Text);
            cmd.Parameters.AddWithValue("@b3",comboBox1.Text);
            cmd.Parameters.AddWithValue("@b4",textBox4.Text);
            
            if(radioButton1.Checked == true)
            {
                cmd.Parameters.AddWithValue("@b5",occasion);
            }
            if(radioButton2.Checked == true)
            {
                cmd.Parameters.AddWithValue("@b5", occasion);
            }

            cmd.Parameters.AddWithValue("@b6",textBox1.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Book updated!");
            List();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Newbook where BookName= @b1",conn);
            cmd.Parameters.AddWithValue("@b1",textBox5.Text);
            DataTable dt = new DataTable(); 
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }
    }
}
