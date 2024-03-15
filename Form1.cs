using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO_temp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = textBox3.Text = "";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=.\\SQLExpress;Initial Catalog=DB_130324_ADO;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
            con.Open();

            SqlCommand cmd = new SqlCommand("select * from tblStudent where roll="+textBox1.Text, con);
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                textBox2.Text = rdr[1].ToString();
                textBox3.Text = rdr[2].ToString();
            }
            else
            {
                MessageBox.Show("record not found");
                // this is new line added from github
            }
            con.Close();
        }
    }
}
