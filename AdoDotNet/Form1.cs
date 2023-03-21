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
using System.Data;
using System.Configuration;


namespace AdoDotNet
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form1()
        {
            con =new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString );

           
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                string query = "insert into Product values(@id,@name,@price,@company)";
                cmd = new SqlCommand(query,con);
                cmd.Parameters.AddWithValue("@id",Convert.ToInt32(txtProductId.Text));
                cmd.Parameters.AddWithValue("@name",txtProductName.Text);
                cmd.Parameters.AddWithValue("@price",Convert.ToInt32(txtPrice.Text));
                cmd.Parameters.AddWithValue("@company",txtCompany.Text);

                con.Open();
                int result=cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Record inserted...");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            {
                con.Close(); 
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string qur = "Update Product set p_name=@name,price=@price,company=@company where p_id=@id";
                cmd= new SqlCommand(qur,con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtProductId.Text));
                cmd.Parameters.AddWithValue("@name", txtProductName.Text);
                cmd.Parameters.AddWithValue("@price", Convert.ToInt32(txtPrice.Text));
                cmd.Parameters.AddWithValue("@company", txtCompany.Text);
                con.Open();
                int result=cmd.ExecuteNonQuery();
                if(result >= 1)
                {
                    MessageBox.Show("Recorde Updated...");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                con.Close();
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string qury = "Select * from Product where p_id=@id";
                cmd = new SqlCommand(qury, con);
                cmd.Parameters.AddWithValue("@id", txtProductId.Text);

                con.Open();
                dr=cmd.ExecuteReader();
                if (dr.HasRows)
                {
                   while(dr.Read())
                    {
                        txtProductName.Text = dr["p_name"].ToString();
                        txtPrice.Text = dr["price"].ToString();
                        txtCompany.Text = dr["company"].ToString();

                    }
                }
                else
                {
                    MessageBox.Show("Record not fount..");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { con.Close(); }


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string qury = "Delete from Product where p_id=@id";
                cmd = new SqlCommand(qury, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtProductId.Text));
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Record deleted..");
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            finally { con.Close(); }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                string qury = "select * from Product ";
                cmd = new SqlCommand(qury, con);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                      DataTable dataTable = new DataTable();
                        dataTable.Load(dr);
                        dataGridView1.DataSource = dataTable;

                    
                }
                else
                {
                    MessageBox.Show("Record not found..");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { con.Close(); }
        }
    }
}
