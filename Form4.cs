using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace FEONET
{
    enum RowState
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }
    public partial class Form4 : Form
    {
        BD bd = new BD();
        int selectedRow;
        public Form4()
        {
            InitializeComponent();
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("id", "Номер пользователя");
            dataGridView1.Columns.Add("login", "LOG");
            dataGridView1.Columns.Add("password", "PASS");
            dataGridView1.Columns.Add("name", "Имя пользователя");
            dataGridView1.Columns.Add("familia", "Фамилия пользователя");
            dataGridView1.Columns.Add("number", "Номер пользователя");
            dataGridView1.Columns.Add("id_tarif", "Номер тарифа");
            dataGridView1.Columns.Add("status", "Статус тарифа");
            dataGridView1.Columns.Add("data_oplati", "Дата оплаты");
            dataGridView1.Columns.Add("IsNew", String.Empty);
            this.dataGridView1.Columns["login"].Visible = false;
            this.dataGridView1.Columns["password"].Visible = false;
            this.dataGridView1.Columns["IsNew"].Visible = false;
            dataGridView2.Columns.Add("id", "Номер тарифа");
            dataGridView2.Columns.Add("name", "Название тарифа");
            dataGridView2.Columns.Add("parametrs", "Параметры тарифа");
            dataGridView2.Columns.Add("ctoimost", "Стоимость тарифа");
            dataGridView2.Columns.Add("dlitelinost", "Длительность действия тарифа");
            dataGridView2.Columns.Add("IsNew", String.Empty);
            this.dataGridView2.Columns["IsNew"].Visible = false;
        }
            private void Form4_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1, dataGridView2);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }
        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            int id = record.IsDBNull(0) ? 0 : record.GetInt32(0);
            string status = record.IsDBNull(1) ? string.Empty : record.GetString(1);
            string data_oplati = record.IsDBNull(2) ? string.Empty : record.GetString(2);

            dgw.Rows.Add(id, status, data_oplati, record.GetString(3), record.GetString(4), record.GetString(5),
            record.IsDBNull(6) ? 0 : record.GetInt32(6), record.IsDBNull(7) ? string.Empty : record.GetString(7),
             record.IsDBNull(8) ? string.Empty : record.GetString(8), RowState.ModifiedNew);
        }
        private void ReadSingleRow2(DataGridView dgw2, IDataRecord record)
        {
            dgw2.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3),
                record.GetString(4), RowState.ModifiedNew);
        }
        private void search(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string searchStr = $"select * from USERR where concat (id, name, familia, number, id_tarif, status, data_oplati) like '%" + textBox6.Text + "%'";
            SqlCommand com = new SqlCommand(searchStr, bd.getcon());
            bd.opcon();
            SqlDataReader read = com.ExecuteReader();
            while (read.Read())
            {
                ReadSingleRow(dgw, read);
            }
            read.Close();
        }
        private void search2(DataGridView dgw2)
        {
            dgw2.Rows.Clear();
            string searchStr = $"select * from TARIF where concat (id, name, parametrs, ctoimost, dlitelinost) like '%" + textBox7.Text + "%'";
            SqlCommand com = new SqlCommand(searchStr, bd.getcon());
            bd.opcon();
            SqlDataReader read = com.ExecuteReader();
            while (read.Read())
            {
                ReadSingleRow(dgw2, read);
            }
            read.Close();
        }
        private void RefreshDataGrid(DataGridView dgw, DataGridView dgw2)
        {
            dgw.Rows.Clear();
            string queryString = $"select * from USERR";
            SqlCommand command = new SqlCommand(queryString, bd.getcon());
            bd.opcon();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();
            dgw2.Rows.Clear();
            string querystring = $"select * from TARIF";
            SqlCommand command1 = new SqlCommand(querystring, bd.getcon());
            bd.opcon();
            SqlDataReader read = command1.ExecuteReader();
            while (read.Read())
            {
                ReadSingleRow2(dgw2, read);
            }
            read.Close();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            search(dataGridView1);
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            search2(dataGridView2);
        }

        private void butrest_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1, dataGridView2);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox8.Text = row.Cells[1].Value.ToString();
                textBox9.Text = row.Cells[2].Value.ToString();
                name.Text = row.Cells[3].Value.ToString();
                famil.Text = row.Cells[4].Value.ToString();
                number.Text = row.Cells[5].Value.ToString();
                numtar.Text = row.Cells[6].Value.ToString();
                stat.Text = row.Cells[7].Value.ToString();
                dataoplat.Text = row.Cells[8].Value.ToString();
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[selectedRow];
                textBox10.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
                textBox5.Text = row.Cells[4].Value.ToString();
            }
        }

        private void delete()
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows[index].Visible = false;
            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells["IsNew"].Value = RowState.Deleted;
                return;
            }
            dataGridView1.Rows[index].Cells["IsNew"].Value = RowState.Deleted;
        }
        private void delete2()
        {
            int index = dataGridView2.CurrentCell.RowIndex;
            dataGridView2.Rows[index].Visible = false;
            if (dataGridView2.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView2.Rows[index].Cells[4].Value = RowState.Deleted;
                return;
            }
            dataGridView2.Rows[index].Cells[4].Value = RowState.Deleted;
        }
        private void Clear()
        {
            name.Text = "";
            famil.Text = "";
            number.Text = "";
            numtar.Text = "";
            stat.Text = "";
            dataoplat.Text = "";
        }

        private void Clear2()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }
        private void butdel_Click(object sender, EventArgs e)
        {
            delete();
            Clear();
            butsave.Visible = true;
        }

        private void butdel2_Click(object sender, EventArgs e)
        {
            delete2();
            Clear2();
            butsave2.Visible = false;
        }

        private void izmen()
        {

            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
            var id = textBox1.Text;
            var login = textBox8.Text;
            var pass = textBox9.Text;
            var nam = name.Text;
            var fam = famil.Text;
            var num = number.Text;
            var idtar = numtar.Text;
            var status = stat.Text;
            var datoplat="-";
            if (dataoplat.Text!="")
            datoplat = dataoplat.Text;
            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                if (name.Text == "" || famil.Text == "" || number.Text == "" )
                {
                    MessageBox.Show("Заполнены не все поля с личными данными", "Ошибка");
                    return;
                }
                dataGridView1.Rows[selectedRowIndex].SetValues(id, login, pass, nam, fam, num, idtar, status, datoplat);
                dataGridView1.Rows[selectedRowIndex].Cells["IsNew"].Value = RowState.Modified;
                Clear();

            }
        }

        private void izmen2()
        {

            var selectedRowIndex = dataGridView2.CurrentCell.RowIndex;
            var idt = textBox10.Text;
            var names = textBox2.Text;
            var par = textBox3.Text;
            var stoi = textBox4.Text;
            var dlit = textBox5.Text;
            if (dataGridView2.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
                {
                    MessageBox.Show("Заполнены не все поля", "Ошибка");
                    return;
                }
                dataGridView2.Rows[selectedRowIndex].SetValues(idt, names, par, stoi, dlit);
                dataGridView2.Rows[selectedRowIndex].Cells["IsNew"].Value = RowState.Modified;
                Clear();

            }
        }
        private void butiz_Click(object sender, EventArgs e)
        {
            izmen();
            butsave.Visible = true;
        }

        private void butsave_Click(object sender, EventArgs e)
        {
            update();
            butsave.Visible = false;
        }

        private void butiz2_Click(object sender, EventArgs e)
        {
            izmen2();
            butsave2.Visible =true;
        }
        private void update()
        {
            bd.opcon();
            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                if (dataGridView1.Rows[index].Cells["IsNew"].Value != null)
                {
                    
                        var rowState = (RowState)dataGridView1.Rows[index].Cells["IsNew"].Value;
                        if (rowState == RowState.Existed)
                        {
                            continue;
                        }
                        if (rowState == RowState.Deleted)
                        {
                            var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                            var deleteQuery = $"delete from USERR where id = {id}";
                            var command = new SqlCommand(deleteQuery, bd.getcon());
                            command.ExecuteNonQuery();
                        }
                        if (rowState == RowState.Modified)
                        {
                            var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                            var log = dataGridView1.Rows[index].Cells[1].Value.ToString();
                            var pas = dataGridView1.Rows[index].Cells[2].Value.ToString();
                            var nam = dataGridView1.Rows[index].Cells[3].Value.ToString();
                            var fam = dataGridView1.Rows[index].Cells[4].Value.ToString();
                            var num = dataGridView1.Rows[index].Cells[5].Value.ToString();
                            var numt = dataGridView1.Rows[index].Cells[6].Value.ToString();
                            var status = dataGridView1.Rows[index].Cells[7].Value.ToString();
                            var datop = dataGridView1.Rows[index].Cells[8].Value.ToString();
                            var change = $"update USERR set login = '{log}', password= '{pas}', name='{nam}', familia='{fam}', number='{num}', id_tarif='{numt}', status='{status}', data_oplati='{datop}' where id='{id}'";
                            var com = new SqlCommand(change, bd.getcon());
                            com.ExecuteNonQuery();
                        }
                    
                }
            }
            bd.clscon();
        }
        private void update2()
        {
            bd.opcon();
            for (int index = 0; index < dataGridView2.Rows.Count; index++)
            {
                
                    var rowState = (RowState)dataGridView2.Rows[index].Cells["IsNew"].Value;
                    if (rowState == RowState.Existed)
                    {
                        continue;
                    }
                    if (rowState == RowState.Deleted)
                    {
                        var id = Convert.ToInt32(dataGridView2.Rows[index].Cells[0].Value);
                        var deleteQuery = $"delete from TARIF where id = {id}";
                        var command = new SqlCommand(deleteQuery, bd.getcon());
                        command.ExecuteNonQuery();
                    }
                    if (rowState == RowState.Modified)
                    {
                        var id = dataGridView2.Rows[index].Cells[0].Value.ToString();
                        var nam = dataGridView2.Rows[index].Cells[1].Value.ToString();
                        var par = dataGridView2.Rows[index].Cells[2].Value.ToString();
                        var cto = dataGridView2.Rows[index].Cells[3].Value.ToString();
                        var dlit = dataGridView2.Rows[index].Cells[4].Value.ToString();
                        var change = $"update TARIF set name = '{nam}', parametrs= '{par}', ctoimost='{cto}', dlitelinost='{dlit}' where id='{id}'";
                        var com = new SqlCommand(change, bd.getcon());
                        com.ExecuteNonQuery();
                    }
                
            }
            bd.clscon();
        }
        private void butnew2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Некоторые из полей пустые, необходимо чтобы они были все заполнены", "Ошибка");
                return;
            }
            bd.opcon();
            Int64 lol;
            var nam = textBox2.Text;
            var param = textBox3.Text;
            var stoim = textBox4.Text;
            var dlit = textBox5.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string n = $"SELECT * FROM TARIF WHERE name = '{nam}'";
            SqlCommand command1 = new SqlCommand(n, bd.getcon());
            adapter.SelectCommand = command1;
            adapter.Fill(table);
            if (table.Rows.Count == 0)
            {
                    if (!(Int64.TryParse(textBox4.Text, out lol)))
                    {
                        MessageBox.Show("Неправильно введена стоимость тарифа", "Ошибка");
                        textBox4.Text = "";
                        return;
                    }
                   var add = $"insert into TARIF (name, parametrs, ctoimost, dlitelinost) values ('{nam}', '{param}', '{stoim + 'р'}', '{dlit}')";
                    var command = new SqlCommand(add, bd.getcon());
                    command.ExecuteNonQuery();
                    MessageBox.Show("Тариф успешно создан", "Задача выполнена");
 
            }
            else
            {
                MessageBox.Show("Тариф с таким названием уже существует.", "Ошибка");
                textBox2.Text = "";
            }
        }

        private void butsave2_Click(object sender, EventArgs e)
        {
            update2();
            butsave2.Visible = false;
        }
    }
}
