using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace WindowsFormsApp1
{
    public partial class Main : Form
    {
        public string DbConn = "Host = localhost; Port = 5432; Username = postgres; Password = Ltyecz2007; Database = shoe_cool";

        public string Role;
        public string Sort;
        public string Filter;
        
        
        public Main(string role, string fio)
        {
            InitializeComponent();

            label1.Text = Role = role;
            label4.Text = fio;  

            if (role == "Гость" || role == "Авторизированный клиент")
            {
                Tovar();
                panel1.Visible = false;
            }
            if (role == "Менеджер")
            {
                button3.Visible = false;
                button4.Visible = false;
            }

            textBox2.TextChanged += (s, e) => Tovar();
        }

        // вывод товаров
        public void Tovar()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(DbConn))
            {
                conn.Open();
                var sql = $@"SELECT name_tovar.name_tovar, edin_izmer, sale, postavchik.postavchik_name, proizvoditel.name_proizv, 
category_tovar.category, dicsount, quantity, opisanie, picture, id_article, Round(sale * (100 - dicsount)/100,2) as totalsale
	FROM public.tovar
	join name_tovar on name_tovar.id = tovar.name_tovar_fk
	join public.postavchik ON postavchik.id = tovar.postavchik_fk
	join public.proizvoditel ON proizvoditel.id_pk_proiz = tovar.proizvoditel_fk
	join public.category_tovar ON category_tovar.id_pk_category_tovar = tovar.category_name_fk

    where (name_tovar.name_tovar ILIKE '%{textBox2.Text}%'
    or proizvoditel.name_proizv ILIKE '%{textBox2.Text}%'
    or category_tovar.category ILIKE '%{textBox2.Text}%'
    or opisanie ILIKE '%{textBox2.Text}%')
    AND ('%{Filter}%' IS NULL OR postavchik.postavchik_name ILIKE '%{Filter}%')
    ORDER BY quantity {Sort}"; 
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        flowLayoutPanel1.Controls.Clear();

                        while (reader.Read())
                        {
                            Card card = new Card(Role);
                            card.label2.Text = reader.GetString(0);
                            card.label7.Text += reader.GetInt32(2) + " руб.";
                            card.label5.Text += reader.GetString(3);
                            card.label4.Text += reader.GetString(4);
                            card.label1.Text = reader.GetString(5);
                            card.label10.Text = reader.GetInt32(6) + " %";
                            if (reader.GetInt32(6) > 15)
                            {
                                card.label10.BackColor = ColorTranslator.FromHtml("#2E8B57");
                            }
                            if (reader.GetInt32(6) == 0)
                            {
                                card.label10.Visible = false;
                                card.label6.Visible = false;
                            }
                            else
                            {
                                card.label7.Font = new Font(card.label6.Font, FontStyle.Strikeout);
                                card.label7.ForeColor = Color.Red;
                            }                            

                            card.label9.Text += reader.GetInt32(7).ToString();
                            if (reader.GetInt32(7) == 0)
                            {
                                card.label9.ForeColor = Color.Blue;
                            }

                            card.label3.Text = reader.GetString(8);

                            string pic = card.Foto = reader.IsDBNull(9) ? "picture.png" : reader.GetString(9);
                            card.pictureBox1.ImageLocation = Path.Combine("import", pic);

                            card.Art = reader.GetString(10);
                            card.label6.Text = reader.GetInt32(11) + " руб.";

                            flowLayoutPanel1.Controls.Add(card);


                        }
                    }


                }


                conn.Close();
            }
        }

        // назад
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
            this.Close();
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            Tovar();
        }

        // добавление заказа
        private void button4_Click(object sender, EventArgs e)
        {
            AddOrder addOrder = new AddOrder(Role, "Добавление заказа");
            addOrder.ShowDialog();
        }

        // вывод заказов
        public void Order ()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(DbConn))
            {
                conn.Open();
                var sql = $@"SELECT nomer_zakaza_pk, data_zakaza, data_dostavki, adres.adress_punct, fio_client_fk, cod_poluch, stasus_zakaz.stasus
	FROM public.zakaz
	join public.stasus_zakaz ON stasus_zakaz.id = zakaz.status_zakaza_fk
	join adres on adres.id = zakaz.adress_fk";
	
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        flowLayoutPanel1.Controls.Clear();

                        while (reader.Read())
                        {
                            Order order = new Order(Role);
                            order.nomer_zak = reader.GetInt32(0).ToString();
                            order.label1.Text += reader.GetInt32(0).ToString();
                            order.label4.Text += reader.GetDateTime(1).ToString("d");
                            order.label5.Text += reader.GetDateTime(2).ToString("d");
                            order.label3.Text = reader.GetString(3);
                            order.label2.Text += reader.GetString(6); 
                            if (reader.GetString(6) == "Завершен")
                            {
                                order.label2.ForeColor = Color.Red;
                            }
                            else
                            {
                                order.label2.ForeColor = Color.Blue;
                            }

                            flowLayoutPanel1.Controls.Add(order);


                        }
                    }


                }


                conn.Close();
            }
        }

        
        private void button5_Click(object sender, EventArgs e)
        {
            Order();
        }

        // добавить товар
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddChange addChange = new AddChange(Role);
            if (addChange.ShowDialog() == DialogResult.OK)
            {
                Tovar();
            }
            this.Show();


        }

        // по возрастанию сортировка
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                Sort = "ASC";
                Tovar();
            }
        }

        // по убыванию сортировка
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                Sort = "DESC";
                Tovar();
            }
        }

        // по поставщикам
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Все поставщики") 
            {
                Tovar();
            }
            else
            {
                Filter = comboBox1.Text;
                Tovar();
            }
        }

        
    }
}
