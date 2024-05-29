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

namespace FEONET
{
    public partial class Form2 : Form
    {
        public string a = "admin";
        public string d = "admin";
        public string log = "0";
        public string pas = "0";

        BD bd = new BD();
        public Form2()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            if (login.Text == a && password.Text == d)
            {
                MessageBox.Show("Вы успешно зашли под аккаунтом администратора", "Задача выполнена");
                Form4 f4 = new Form4();   
                f4.ShowDialog();
                this.Close();

            }
            log = login.Text;
            pas = password.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string com = $"select id, login, password from USERR where login= '{log}' and password='{pas}'";
            SqlCommand comm = new SqlCommand(com, bd.getcon());
            adapter.SelectCommand = comm;
            adapter.Fill(table);
            if(table.Rows.Count == 1)
            {
                MessageBox.Show("Вы успешно вошли!", "Задача выполнена");
                Form5 f5 = new Form5();
                f5.logu = login.Text;
                f5.ShowDialog();

            }
            else
            {
                MessageBox.Show("Не правильно введён логин или пароль!", "Ошибка");
            }
        }
    }
}
