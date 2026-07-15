using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public string DbConn = "Host = localhost; Port = 5432; Username = postgres; Password = Ltyecz2007; Database = shoe_cool";
        public Form1()
        {
            InitializeComponent();
        }

        // авторизация
        public void Aut ()
        {
            using (NpgsqlConnection  conn = new NpgsqlConnection(DbConn) )
            {
                conn.Open ();
                var sql = $@"SELECT user_role.role, fio, login, password, users.id
	FROM public.users
	join user_role on user_role.id = users.role_fk
    where login = '{textBox1.Text}' and password = '{textBox2.Text}'";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    using (NpgsqlDataReader  reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string role = reader.GetString(0);
                            string fio = reader.GetString(1);

                            MessageBox.Show("Вы успешно авторизовались!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            Main main = new Main(role, fio);
                            main.ShowDialog();
                            this.Close();


                        }
                        else
                        {
                            MessageBox.Show("Не все поля заполнены или неверны данные", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                conn.Close ();
            }
        }

        // войти 
        private void button1_Click(object sender, EventArgs e)
        {
            Aut();
        }

        // войти как гость
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main main = new Main("Гость", "");
            main.ShowDialog();
            this.Close();
        }

        // показать пароль
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = !checkBox1.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Registration registration = new Registration();
            registration.ShowDialog();
            
        }
    }
}
