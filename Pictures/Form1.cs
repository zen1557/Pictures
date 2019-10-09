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
using System.IO;
namespace Pictures
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        public Form1()
        {
            con = new SqlConnection(@"Data Source=LAB7PC1\SQL2016ENT;Initial Catalog=Pic;Integrated Security=True");
            InitializeComponent();
        }
        string file;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png files(*.png|*.png| jpg files(*.jpg|*.jpg| All files(*.*)|(*.*)";
            if (dialog.ShowDialog()==DialogResult.OK)
            {
                file = dialog.FileName.ToString();
                pictureBox1.ImageLocation = file;
                Name.Text = file;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        { 
            byte[] images = null;
            FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(stream);
            images = br.ReadBytes((int)stream.Length);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into Pictures values('" + Name.Text + "',@image) ", con);
            cmd.Parameters.Add("@image", images);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Inserted", "Insert");
          
            con.Close();
      
            }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select Pic from Pictures where id=" + id.Text + " ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count>0)
            {
                MemoryStream ms = new MemoryStream((byte[])ds.Tables[0].Rows[0]["Pic"]);
                pictureBox2.Image = new Bitmap(ms);
            }
            con.Close();



        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
