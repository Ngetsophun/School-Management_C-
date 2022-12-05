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
namespace School_Management
{
    public partial class Students : Form
    {
        public Students()
        {
            InitializeComponent();
            DisplayStudent();
        }
        private void DisplayStudent()
        {
            Con.Open();
            string Query = "Select * from StudentTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            StudentGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-66B1IDO\SQLEXPRESS;Initial Catalog=SchoolDb;Integrated Security=True");

        private void btnadd_Click(object sender, EventArgs e)
        {
            if(StName.Text =="" || FeesTb.Text=="" || AddressTb.Text==""|| StGencb.SelectedIndex == -1 || cbclass.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into StudentTb1(StName,StGen,StDOB,StClass,StFees,StAdd) values (@Sname,@SGen,@SDob,@SClass,@SFees,@SAdd)", Con);
                    cmd.Parameters.AddWithValue("@Sname", StName.Text);
                    cmd.Parameters.AddWithValue("@SGen", StGencb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SDob", DOBPicker.Value.Date);
                    cmd.Parameters.AddWithValue("@SClass", cbclass.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SFees", FeesTb.Text);
                    cmd.Parameters.AddWithValue("@SAdd", AddressTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Added");
                    Con.Close();
                    DisplayStudent();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
               

            }
        }
        int key = 0;
        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void StudentGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void StudentGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            StName.Text = StudentGV.SelectedRows[0].Cells[1].Value.ToString();
            StGencb.SelectedItem = StudentGV.SelectedRows[0].Cells[2].Value.ToString();
            DOBPicker.Text = StudentGV.SelectedRows[0].Cells[3].Value.ToString();
            cbclass.SelectedItem = StudentGV.SelectedRows[0].Cells[5].Value.ToString();
            FeesTb.Text = StudentGV.SelectedRows[0].Cells[5].Value.ToString();
            AddressTb.Text = StudentGV.SelectedRows[0].Cells[6].Value.ToString();
            if (StName.Text == "")
            {
                key = 0;

            }
            else
            {
                key = Convert.ToInt32(StudentGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
