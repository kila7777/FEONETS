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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FEONET
{
    public partial class Form5 : Form
    {
        Form2 f2 = new Form2 ();
        BD bd = new BD();
        public string logu = "";
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            bd.opcon();
            string p = $"SELECT * FROM USERR WHERE login = '{logu}'";
            SqlCommand command = new SqlCommand(p, bd.getcon());
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                login.Text = reader["name"].ToString();
                textBox1.Text = reader["familia"].ToString();
                textBox2.Text = reader["number"].ToString();
                textBox5.Text = reader["id_tarif"].ToString();
                textBox4.Text = reader["status"].ToString();
                textBox3.Text = reader["data_oplati"].ToString();
            }
            reader.Close();
            bd.clscon();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Для выбора тарифа или для ответов на ваши вопросы, обратитесь по номеру +7-***-***-**-**", "Помощь");
        }
    }
}
