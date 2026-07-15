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

namespace WindowsFormsApp1
{
    public partial class AddChange : Form
    {
        public string DbConn = "Host = localhost; Port = 5432; Username = postgres; Password = Ltyecz2007; Database = shoe_cool";
        public string Foto;
        public string Art;


        List<Control> controls = new List<Control>();
        // инициализация для добавление товара
        public AddChange(string role)
        {
            InitializeComponent();
            label1.Text = "Добавление товара";
            label2.Text = role;
            button1.Text = "Добавить товар";
            button3.Text = "Добавить фото";

            controls.Add(maskedTextBox1);
            controls.Add(maskedTextBox2);
            controls.Add(comboBox1);
            controls.Add(comboBox2);
            controls.Add(comboBox3);
            controls.Add(comboBox4);
            controls.Add(textBox1);

            controls.ForEach(c => c.Click += (sen, ev) =>
            {
                c.BackColor = Color.White;
                if (c is MaskedTextBox)
                {
                    ((MaskedTextBox)c).Clear();
                }
            });


        }

        // инициализация для редактирования товара
        public AddChange(string art, string foto)
        {
            InitializeComponent();
            label1.Text = "Редактирование товара";
            label2.Text = "Администратор";
            button1.Text = "Сохранить изменения";
            button3.Text = "Новое фото";

            maskedTextBox1.Enabled = false;

            Art = art;
            Foto = foto;
            Load();
        }

        // добавление товара
        public void AddTov()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(DbConn))
            {
                conn.Open();
                var sqlIns = $@"INSERT INTO public.tovar(
	name_tovar_fk, edin_izmer, sale, postavchik_fk, proizvoditel_fk, category_name_fk, dicsount, quantity, opisanie, picture, id_article)
	VALUES (
	@name_tovar_fk, 
	@edin_izmer, 
	@sale, 
	@postavchik_fk, 
	@proizvoditel_fk, 
	@category_name_fk, 
	@dicsount, 
	@quantity, 
	@opisanie, 
	@picture, 
	@id_article);";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlIns, conn))
                {
                    cmd.Parameters.AddWithValue("@name_tovar_fk", comboBox1.SelectedIndex + 1);
                    cmd.Parameters.AddWithValue("@edin_izmer", label9.Text);
                    cmd.Parameters.AddWithValue("@sale", Convert.ToInt32(maskedTextBox2.Text));
                    cmd.Parameters.AddWithValue("@postavchik_fk", comboBox3.SelectedIndex + 1);
                    cmd.Parameters.AddWithValue("@proizvoditel_fk", comboBox2.SelectedIndex + 1);
                    cmd.Parameters.AddWithValue("@category_name_fk", comboBox4.SelectedIndex + 1);
                    cmd.Parameters.AddWithValue("@dicsount", numericUpDown2.Value);
                    cmd.Parameters.AddWithValue("@quantity", numericUpDown1.Value);
                    cmd.Parameters.AddWithValue("@opisanie", textBox1.Text);
                    if (button3.Text == "Добавить фото")
                    {
                        cmd.Parameters.AddWithValue("@picture", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@picture", button3.Text);
                    }
                    
                    cmd.Parameters.AddWithValue("@id_article", maskedTextBox1.Text);

                    int res = cmd.ExecuteNonQuery();
                    if (res > 0)
                    {
                        MessageBox.Show("Товар успешно добавлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        Main main = (Main)Application.OpenForms["Main"];
                        main.Tovar();
                        this.Close();
                    }
                    
                }

                conn.Close();
            }
        }

        // загрузка данных при редактировании
        public void Load()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(DbConn))
            {
                conn.Open();
                var sqlLoad = $@"SELECT name_tovar.name_tovar, sale, postavchik.postavchik_name, proizvoditel.name_proizv, 
category_tovar.category, dicsount, quantity, opisanie, picture, id_article
	FROM public.tovar
	join name_tovar on name_tovar.id = tovar.name_tovar_fk
	join public.postavchik ON postavchik.id = tovar.postavchik_fk
	join public.proizvoditel ON proizvoditel.id_pk_proiz = tovar.proizvoditel_fk
	join public.category_tovar ON category_tovar.id_pk_category_tovar = tovar.category_name_fk
    where id_article = '{Art}'";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlLoad, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader() )
                    {
                        if (reader.Read())
                        {
                            comboBox1.SelectedItem = reader.GetString(0);
                            maskedTextBox2.Text = reader.GetInt32(1).ToString();
                            comboBox3.SelectedItem = reader.GetString(2);
                            comboBox2.SelectedItem = reader.GetString(3);
                            comboBox4.SelectedItem = reader.GetString(4);
                            numericUpDown2.Value = reader.GetInt32(5);
                            numericUpDown1.Value = reader.GetInt32(6);
                            textBox1.Text = reader.GetString(7);
                            
                            pictureBox2.ImageLocation = Path.Combine("import", Foto);
                            maskedTextBox1.Text = reader.GetString(9);

                        }
                    }

                    

                }

                conn.Close();
            }
        }

        // обновление товара
        public void EditTov()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(DbConn))
            {
                conn.Open();
                var sqlUpd = $@"UPDATE public.tovar
SET 
name_tovar_fk = @name_tovar_fk, 
	edin_izmer = @edin_izmer, 
	sale = @sale, 
	postavchik_fk = @postavchik_fk, 
	proizvoditel_fk = @proizvoditel_fk, 
	category_name_fk = @category_name_fk, 
	dicsount = @dicsount, 
	quantity = @quantity, 
	opisanie = @opisanie, 
	picture = @picture
	where id_article = @id_article;";
                using (NpgsqlCommand cmdUpd = new NpgsqlCommand(sqlUpd, conn))
                {
                    cmdUpd.Parameters.AddWithValue("@name_tovar_fk", comboBox1.SelectedIndex + 1);
                    cmdUpd.Parameters.AddWithValue("@edin_izmer", label9.Text);
                    cmdUpd.Parameters.AddWithValue("@sale", Convert.ToInt32(maskedTextBox2.Text));
                    cmdUpd.Parameters.AddWithValue("@postavchik_fk", comboBox4.SelectedIndex + 1);
                    cmdUpd.Parameters.AddWithValue("@proizvoditel_fk", comboBox2.SelectedIndex + 1);
                    cmdUpd.Parameters.AddWithValue("@category_name_fk", comboBox3.SelectedIndex + 1);
                    cmdUpd.Parameters.AddWithValue("@dicsount", numericUpDown2.Value);
                    cmdUpd.Parameters.AddWithValue("@quantity", numericUpDown1.Value);
                    cmdUpd.Parameters.AddWithValue("@opisanie", textBox1.Text);
                    if (string.IsNullOrEmpty(Foto))
                    {
                        cmdUpd.Parameters.AddWithValue("@picture", DBNull.Value);
                    }
                    else
                    {
                        cmdUpd.Parameters.AddWithValue("@picture", Foto);
                    }
                    cmdUpd.Parameters.AddWithValue("@id_article", maskedTextBox1.Text);

                    var res = cmdUpd.ExecuteNonQuery();
                    if (res > 0)
                    {
                        MessageBox.Show("Товар успешно обновлен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        Main main = (Main)Application.OpenForms["main"];
                        main.Tovar();
                        this.Close();
                    }



                }


                conn.Close();
            }
        }

        // назад
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main main = (Main)Application.OpenForms["main"];
            this.Close();
        }

        // проверка заполнения всех полей при редактировании и вызов нужного метода
        private void button1_Click(object sender, EventArgs e)
        {
            if (label1.Text == "Добавление товара")
            {
                bool hasEr = false;
                controls.ForEach(c =>
                {
                    if (string.IsNullOrEmpty(c.Text))
                    {
                        hasEr = true;
                        c.BackColor = Color.Red;
                    }
                });
                if (hasEr)
                {
                    MessageBox.Show("Не все поля заполнены","Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    AddTov();
                }
            }
            else
            {
                EditTov();
            }

            
        }

        // работа с фото
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "Picture Foles = | *.jpeg; *.png"};
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = Path.Combine(Application.StartupPath, "import", openFileDialog.SafeFileName);
                File.Copy(openFileDialog.FileName, path, true);
                pictureBox2.Image = Image.FromFile(path);
                MessageBox.Show("Фото успешно загружено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Foto = button3.Text = openFileDialog.FileName;

            }

        }
    }
}
