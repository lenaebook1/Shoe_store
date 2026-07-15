using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class AddOrder : Form
    {
        public string DbConn = "Host = localhost; Port = 5432; Username = postgres; Password = ; Database = shoe_cool";
        public string nom_zak;

        // инициализация при редактировании
        public AddOrder(string id)
        {
            InitializeComponent();
            label1.Text = "Редактирование заказа";
            label2.Text = "Администратор";
            button1.Text = "Сохранить изменения";
            nom_zak = id;
            Edit();
        }

        // инициализация при добавлении 
        public AddOrder(string role, string text)
        {
            InitializeComponent();
            label1.Text = text;
            label2.Text = role;
        }

        // загружаем данные при редактировании
        public void Edit ()
        {
            using (var con = new NpgsqlConnection(DbConn))
            {
                con.Open();
                var sqlEdit = $@"SELECT data_zakaza, data_dostavki, adres.adress_punct, users.fio, stasus_zakaz.stasus
	FROM public.zakaz
	Join users on users.id = zakaz.fio_client_fk
	join public.stasus_zakaz ON stasus_zakaz.id = zakaz.status_zakaza_fk
	join public.adres ON adres.id = zakaz.adress_fk
    where nomer_zakaza_pk = '{nom_zak}'";
                var cmdEdit = new NpgsqlCommand(sqlEdit, con);
                var reader = cmdEdit.ExecuteReader();
                if (reader.Read())
                {
                    dateTimePicker1.Value = reader.GetDateTime(0);
                    dateTimePicker2.Value = reader.GetDateTime(1);
                    comboBox3.SelectedItem = reader.GetString(2);
                    comboBox2.SelectedItem = reader.GetString(3);
                    comboBox4.SelectedItem = reader.GetString(4);

                }

                con.Close();              
                    
            }

            using (var con = new NpgsqlConnection(DbConn))
            {
                con.Open();
                var sqlEd2 = $@"SELECT article_fk, quantity
	FROM public.zakaz_tovar
    where id_zakaz_fk = '{nom_zak}'";
                var cmdEd2 = new NpgsqlCommand(sqlEd2, con);
                var res = cmdEd2.ExecuteReader();
                while (res.Read())
                {
                    listBox1.Items.Add(res.GetString(0) + " " + res.GetInt32(1));
                }
                con.Close();
            }

        }

        // обновляем заказ
        private void EditZak()
        {
            using (var con = new NpgsqlConnection(DbConn))
            {
                con.Open();
                var sqlSaveEdit = $@"UPDATE public.zakaz
	SET 
	data_zakaza=@data_zakaza, 
	data_dostavki=@data_dostavki, 
	adress_fk=@adress_fk, 
	fio_client_fk=@fio_client_fk, 
	status_zakaza_fk=@status_zakaza_fk
	WHERE nomer_zakaza_pk = @nomer_zakaza_pk;";
                var cmdSaveEdit = new NpgsqlCommand(sqlSaveEdit, con);
                cmdSaveEdit.Parameters.AddWithValue("@data_zakaza", dateTimePicker1.Value);
                cmdSaveEdit.Parameters.AddWithValue("@data_dostavki", dateTimePicker2.Value);
                cmdSaveEdit.Parameters.AddWithValue("@adress_fk", comboBox3.SelectedIndex + 1);
                cmdSaveEdit.Parameters.AddWithValue("@fio_client_fk", comboBox2.SelectedIndex + 1);
                cmdSaveEdit.Parameters.AddWithValue("@status_zakaza_fk", comboBox4.SelectedIndex + 1);
                cmdSaveEdit.Parameters.AddWithValue("@nomer_zakaza_pk", Convert.ToInt32(nom_zak));
                cmdSaveEdit.ExecuteNonQuery();
                
            }
            MessageBox.Show("Заказ успешно обновлен", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Main main = (Main)Application.OpenForms["Main"];
            main.Order();
            this.Close();


        }

        // добавляем заказ
        public void AddZakaz ()
        {
            int orderId = 0;
            using (var con = new NpgsqlConnection(DbConn))
            {
                con.Open();
                var sqlZak = $@"INSERT INTO public.zakaz(
	data_zakaza, data_dostavki, adress_fk, fio_client_fk, status_zakaza_fk)
	VALUES (
    @data_zakaza, 
    @data_dostavki, 
    @adress_fk, 
    @fio_client_fk, 
    @status_zakaza_fk) returning nomer_zakaza_pk;";
                var cmdZak = new NpgsqlCommand(sqlZak, con);
                cmdZak.Parameters.AddWithValue("@data_zakaza", dateTimePicker1.Value);
                cmdZak.Parameters.AddWithValue("@data_dostavki", dateTimePicker2.Value);
                cmdZak.Parameters.AddWithValue("@adress_fk", comboBox3.SelectedIndex + 1);
                cmdZak.Parameters.AddWithValue("@fio_client_fk", comboBox2.SelectedIndex + 1);
                cmdZak.Parameters.AddWithValue("@status_zakaza_fk", comboBox4.SelectedIndex + 1);
                orderId = Convert.ToInt32(cmdZak.ExecuteScalar());

                var sql2 = $@"INSERT INTO public.zakaz_tovar(
	id_zakaz_fk, article_fk, quantity)
	VALUES (@id_zakaz_fk, @article_fk, @quantity);";
                var cmd2 = new NpgsqlCommand(sql2, con);
                foreach (var item in listBox1.Items)
                {
                    cmd2.Parameters.Clear();
                    cmd2.Parameters.AddWithValue("@article_fk", item.ToString().Split(' ')[0]);
                    cmd2.Parameters.AddWithValue("@quantity", Convert.ToInt32(item.ToString().Split(' ')[1]));
                    cmd2.Parameters.AddWithValue("@id_zakaz_fk", orderId);
                    cmd2.ExecuteNonQuery();
                }
            }
                
            MessageBox.Show("Заказ успешно добавлен","Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information );
            Main main = (Main)Application.OpenForms["Main"];
            main.Order();
            this.Close();

        }      
        
        // проверка ввода и выбор метода
        private void button1_Click(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value < dateTimePicker1.Value)
            {
                MessageBox.Show("Дата доставки не может быть меньше даты заказа", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (comboBox2.SelectedIndex == -1 ||
         comboBox3.SelectedIndex == -1 ||
         comboBox4.SelectedIndex == -1)
            {
                MessageBox.Show("Заполните все поля", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("Введите товары для заказа", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (button1.Text == "Добавить заказ")
                AddZakaz();
            else
                EditZak();
        }

        
        // добавить товар
        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 || numericUpDown1.Value <= 0 )
            {
                MessageBox.Show("Выберите товар и количество", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (var item in listBox1.Items)
            {
                if (item.ToString().Split(' ')[0].Equals(comboBox1.Text))
                {
                    MessageBox.Show("Этот товар уже есть в списке", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (label1.Text != "Редактирование заказа")
            {
                listBox1.Items.Add(comboBox1.Text + " " + numericUpDown1.Value);
            }
            else
            {
                var art = comboBox1.SelectedItem;
                var quan = numericUpDown1.Value;
                using (var con = new NpgsqlConnection(DbConn))
                {
                    con.Open();
                    var sqlDob = $@"INSERT INTO public.zakaz_tovar(
	id_zakaz_fk, article_fk, quantity)
	VALUES (@id_zakaz_fk, @article_fk, @quantity);";
                    var cmdDob = new NpgsqlCommand(sqlDob, con);
                    cmdDob.Parameters.AddWithValue("@article_fk", art);
                    cmdDob.Parameters.AddWithValue("@quantity", quan);
                    cmdDob.Parameters.AddWithValue("@id_zakaz_fk", Convert.ToInt32(nom_zak));
                    cmdDob.ExecuteNonQuery();
                    
                }
                MessageBox.Show("Товар успешно добавлен", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listBox1.Items.Add(comboBox1.Text + " " + numericUpDown1.Value);

            }




        }

        // удалить товар 
        private void button4_Click_1(object sender, EventArgs e)
        {
            if (label1.Text != "Редактирование заказа")
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
            else
            {
                var art = listBox1.SelectedItem.ToString().Split(' ')[0];
                using (var con = new NpgsqlConnection(DbConn))
                {
                    con.Open();
                    var sql = $@"DELETE FROM public.zakaz_tovar
	WHERE id_zakaz_fk = @id_zakaz_fk AND article_fk = @article_fk";
                    var cmd = new NpgsqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@id_zakaz_fk", Convert.ToInt32(nom_zak));
                    cmd.Parameters.AddWithValue("@article_fk", art);
                    cmd.ExecuteNonQuery();
                }
                    
                MessageBox.Show("Товар успешно удален", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }
                
        // назад
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        // очистить 
        private void button6_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}
