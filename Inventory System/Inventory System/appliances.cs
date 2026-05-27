using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Inventory_System
{
    public partial class appliances : Form
    {
        public appliances()
        {
            InitializeComponent();

            DisplayData();
        }
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db_users.mdb");
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataAdapter da = new OleDbDataAdapter();
        DataTable table;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (panel1.Height > 56)
            {
                panel1.Height -= 20;
            }
            else
            {
                timer1.Enabled = false;
            }   
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (panel1.Height < 175)
            {
                panel1.Height += 20;
            }
            else
            {
                timer2.Enabled = false;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (panel1.Height > 56)
            {
                timer1.Enabled = true;
            }
            else
            {
                timer2.Enabled = true;
            }
        }

        private void appliancesbtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Already in the Page!", "Stockwise Inventory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void kitchenwarebtn_Click(object sender, EventArgs e)
        {
            var newForm = new kitchenware();
            newForm.Show();
            this.Hide();
        }

        private void furniturebtn_Click(object sender, EventArgs e)
        {
            var newForm = new furniture();
            newForm.Show();
            this.Hide();
        }

        private void appliances_Load(object sender, EventArgs e)
        {

        }
        private void ClearData()
        {
            itemnametbox.Text = "";
            itemamountbtox.Text = "";
        }
        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            da = new OleDbDataAdapter("select * from db_users.Appliances", con);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                itemnametbox.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                itemamountbtox.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }
        private void insertbtn_Click(object sender, EventArgs e)
        {
            if (itemnametbox.Text != "" && itemamountbtox.Text != "")
            {

                con.Open();
                cmd = new OleDbCommand("INSERT INTO db_users.Appliances(ItemName, Amount, Record_Date) VALUES (@itemname, @amount, @date)", con);

                cmd.Parameters.AddWithValue("@itemname", itemnametbox.Text);
                cmd.Parameters.AddWithValue("@amount", itemamountbtox.Text);
                cmd.Parameters.AddWithValue("@date", dateTimePicker1.Value);

                cmd.ExecuteNonQuery();

                con.Close();
                MessageBox.Show("Record Successfully inserted", "INSERT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayData();
                ClearData();

            }
            else
            {
                MessageBox.Show("Fill out all the information needed", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            if (itemamountbtox.Text != "" && itemnametbox.Text != "" && txtID.Text != "")
            {
                try
                {
                    con.Open();
                    cmd = new OleDbCommand("UPDATE Appliances SET ItemName=?, Amount=? WHERE ID=?", con);
                    cmd.Parameters.AddWithValue("@name", itemnametbox.Text);
                    cmd.Parameters.AddWithValue("@amount", itemamountbtox.Text);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record Successfully Updated", "UPDATE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DisplayData();
                        ClearData();
                    }
                    else
                    {
                        MessageBox.Show("No record updated", "UPDATE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Select the record you want to Update", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (itemamountbtox.Text != "" && itemnametbox.Text != "" && txtID.Text != "") 
            {
                cmd = new OleDbCommand("DELETE FROM Appliances WHERE ID=?", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", txtID.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Successfully Deleted", "DELETE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Select the record you want to Delete", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void searchData(string valueToSearch)
        {
            string query = "SELECT * FROM Appliances WHERE ID LIKE @searchValue OR ItemName LIKE @searchValue OR Amount LIKE @searchValue OR Record_Date LIKE @searchValue";
            cmd = new OleDbCommand(query, con);
            cmd.Parameters.AddWithValue("@searchValue", "%" + valueToSearch + "%");
            da = new OleDbDataAdapter(cmd);
            table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            string valueTosearch = txtsrc.Text.ToString();
            searchData(valueTosearch);
        }

        private void cleartxt_Click(object sender, EventArgs e)
        {
            txtsrc.Text = "";
            string valueTosearch = txtsrc.Text.ToString();
            searchData(valueTosearch);
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
