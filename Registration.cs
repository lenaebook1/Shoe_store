using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Registration : Form
    {
        public string DbConn = "Host = localhost; Port = 5432; Username = postgres; Password = Ltyecz2007; Database = shoe_cool";

        public bool Confirm;
        public bool Captcha;

        List<Control> controls = new List<Control>();

        public Registration()
        {
            InitializeComponent();
            controls.Add(textBox1);
            controls.Add(textBox4);

            controls.ForEach(c =>
            {
                c.BackColor = Color.White;
            });
        }

        // метод для регистрации (регистрируются только гости)
        public void Reg()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(DbConn))
            {
                conn.Open();
                var sql = $@"INSERT INTO public.users(
	role_fk, fio, login, password)
	VALUES (@role_fk, @fio, @login, @password);";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@role_fk", 3);
                    cmd.Parameters.AddWithValue("@fio", textBox4.Text);
                    cmd.Parameters.AddWithValue("@login", textBox1.Text);
                    cmd.Parameters.AddWithValue("@password", textBox2.Text);

                    var res = cmd.ExecuteNonQuery();
                    if (res > 0)
                    {
                        MessageBox.Show("Вы успешно зарегистрировались", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        Form1 form1 = new Form1();
                        form1.ShowDialog();
                        this.Close();
                    }
                }
            }
        }

        // если пароль подтвержден и если поля заполненв
        private void button2_Click(object sender, EventArgs e)
        {
            if (Confirm == true)
            {
                bool hasEr = false;
                controls.ForEach(c =>
                {
                    if (string.IsNullOrEmpty(c.Text)) { hasEr = true; }
                    c.BackColor = Color.Red;
                });
                if (hasEr)
                {
                    MessageBox.Show("Поля не заполнены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    if (Captcha == true)
                    {
                        controls.ForEach(c =>
                        {
                            c.BackColor = Color.White;
                        });
                        Reg();
                    }
                    else
                    {
                        MessageBox.Show("Капча не пройдена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                }
                   
            }
            else
            {
                MessageBox.Show("Введите данные и подтвердите пароль","Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
        }

        // на форму войти
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
            this.Close();
        }

        // подтверждение пароля 
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == textBox3.Text)
            {
                Confirm = true;
                MessageBox.Show("Пароль подтвержден", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
               
            else
            {
                Confirm = false;
                MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CaptchaForm captchaForm = new CaptchaForm();
            DialogResult result = captchaForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                Captcha = true;
            }
            else
                Captcha = false;
            
            
            
                
             
        }
    }
}
