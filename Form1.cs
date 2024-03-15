using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
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

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            SqlTransaction transaction;
            con.ConnectionString = "Data Source=.\\SQLExpress;Initial Catalog=DB_130324_ADO;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
            con.Open();
            transaction = con.BeginTransaction();
            try
            {          
           
            String myQuery = "insert into tblStudent values("+textBox1.Text+",'"+textBox2.Text+"',"+textBox3.Text+")";
            SqlCommand cmd1 = new SqlCommand(myQuery, con,transaction);
                SqlCommand cmd2 = new SqlCommand("insert into temp values("+textBox1.Text+",'"+textBox2.Text+"')", con,transaction);
                int m = cmd2.ExecuteNonQuery();
                int n=cmd1.ExecuteNonQuery();
               
                if (n>0)
            {
                MessageBox.Show("Record inserted successfully");
            }
            else
            {
                MessageBox.Show("Record not inserted!!!!");
            }

              transaction.Commit();
                MessageBox.Show("commit done..");
            }
            catch (SqlException se)
            {
               
                transaction.Rollback();
                MessageBox.Show("Roll back done..");

            }


            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //step1 
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=.\\sqlexpress;Initial Catalog=DB_130324_ADO;Integrated Security=True;TrustServerCertificate=True";
            con.Open();

            //step2
            int roll = int.Parse(textBox1.Text);
            string name = textBox2.Text;
            int contact = int.Parse(textBox3.Text);

             string qry= "insert into tblStudent values("+roll+",'"+name+"',"+contact+")";

            // SqlCommand cmd = new SqlCommand(qry, con);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = qry;
            cmd.CommandType = CommandType.Text;

            //step3
            int n=cmd.ExecuteNonQuery();
            if (n<=0)
            {
                MessageBox.Show("Record not inserted !!!");
            }
            else
            {
                MessageBox.Show("Record inserted Successfully.....");

            }

            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //step1 
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=.\\sqlexpress;Initial Catalog=DB_130324_ADO;Integrated Security=True;TrustServerCertificate=True";
            con.Open();

            //step2
            int roll = int.Parse(textBox1.Text);
            string name = textBox2.Text;
            int contact = int.Parse(textBox3.Text);

            string qry = "sp_insetStud";

            // SqlCommand cmd = new SqlCommand(qry, con);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = qry;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@roll", roll);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@contact", contact);

            //step3
            int n = cmd.ExecuteNonQuery();
            if (n <= 0)
            {
                MessageBox.Show("Record not inserted !!!");
            }
            else
            {
                MessageBox.Show("Record inserted Successfully.....");

            }

            con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=DB_130324_ADO;Integrated Security=True;TrustServerCertificate=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("update tblStudent set name='" + textBox2.Text + "',contact=" + textBox3.Text + " where roll=" + textBox1.Text,con);

            int n=cmd.ExecuteNonQuery();
            if (n>0)
            {
                MessageBox.Show("Record updated successfully..");
            }
            else

            {
                MessageBox.Show("Record not updated!!!!");
            }
            con.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=DB_130324_ADO;Integrated Security=True;TrustServerCertificate=True");
            con.Open();

            SqlCommand cmd = new SqlCommand("delete from tblStudent where roll=" + textBox1.Text, con);
              int n=cmd.ExecuteNonQuery();
            if (n>0)
            {
                MessageBox.Show("Record deleted successfully....");
            }
            else
            {
                MessageBox.Show("Record NOT deleted");
            }
            con.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=DB_130324_ADO;Integrated Security=True;TrustServerCertificate=True");
            con.Open();

            SqlCommand cmd = new SqlCommand("select * from tblStudent where roll=" + textBox1.Text, con);
            

            SqlDataReader rdr= cmd.ExecuteReader();
            if (rdr.Read())
            {
                textBox2.Text = rdr[1].ToString();//   0-> roll  1-> name   2-> contact
                textBox3.Text = rdr["contact"].ToString();
            }
            else
            {
                MessageBox.Show("Record not found!!!");
            }
            con.Close();
        }
    }
}
