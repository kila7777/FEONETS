using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FEONET
{
    public partial class Form3 : Form
    {
        BD bd = new BD();
        public Form3()
        {
            InitializeComponent();
        }

      

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (name.Text == "" || famil.Text == "" || num.Text == "" || login.Text== "" || pass.Text=="")
            {
                MessageBox.Show("Некоторые из полей пустые, необходимо чтобы она были все заполнены", "Ошибка");
                return;
            }
            bd.opcon();
            Int64 lol;
            var nam = name.Text;
            var fam = famil.Text;
            var proverka = num.Text;
            var log = login.Text;
            var passw = pass.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            SqlDataAdapter adapter1 = new SqlDataAdapter();
            DataTable table1 = new DataTable();
            string n = $"SELECT * FROM USERR WHERE number = '{proverka}'";
            string l = $"SELECT * FROM USERR WHERE login='{log}'";
            SqlCommand command1 = new SqlCommand(n, bd.getcon());
            SqlCommand command2 = new SqlCommand(l, bd.getcon());
            adapter.SelectCommand = command1;
            adapter.Fill(table);
            adapter1.SelectCommand = command2;
            adapter1.Fill(table1);
            if (table.Rows.Count == 0)
            {
                if (table1.Rows.Count == 0)
                {

                    if ((Int64.TryParse(num.Text, out lol)) && (proverka.Length == 12))
                    {
                        var add = $"insert into USERR (login, password, name, familia, number) values ('{log}', '{passw}', '{nam}', '{fam}', '{proverka}')";
                        var command = new SqlCommand(add, bd.getcon());
                        command.ExecuteNonQuery();
                        MessageBox.Show("Аккаунт успешно создан", "Задача выполнена");
                        this.Close();
                    }
                    else
                    {

                        MessageBox.Show("Телефоный номер должен начинатся с +7 и не иметь латинский букв, также номер состоит из 11 цифр", "Ошибка");
                    }
                }
                else
                {
                    MessageBox.Show("Такой логин уже существует", "Ошибка");
                    login.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Такой номер уже существует", "Ошибка");
                num.Text = "";
            }
        }
    }
}
