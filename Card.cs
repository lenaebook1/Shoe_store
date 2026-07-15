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
    public partial class Card : UserControl
    {
        public string DbConn = "Host = localhost; Port = 5432; Username = postgres; Password = Ltyecz2007; Database = shoe_cool";

        public string Art;
        public string Foto;

        public Card(string role)
        {
            InitializeComponent();

            if (role != "Администратор")
            {
                button1.Visible = false;
                button2.Visible = false;
            }
        }

        // редактирование
        private void button1_Click(object sender, EventArgs e)
        {
            AddChange addChange = new AddChange(Art, Foto);
            addChange.ShowDialog();
        }

        // удаление
        public void Delete()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(DbConn))
            {
                conn.Open();
                var sql = $@"SELECT id, id_zakaz_fk, article_fk, quantity
	FROM public.zakaz_tovar
    where article_fk = '{Art}'";
                using (NpgsqlCommand cmd =  new NpgsqlCommand(sql, conn))
                {
                    int order = Convert.ToInt32(cmd.ExecuteScalar());
                    if (order > 0)
                    {
                        MessageBox.Show("Товар есть в заказах!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (MessageBox.Show("Вы уверены, что хотите удалить товар?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var sql2 = $@"DELETE FROM public.tovar
	WHERE id_article = '{Art}'";
                        using (NpgsqlCommand cmd2 = new NpgsqlCommand(sql2, conn))
                        {
                            int res = cmd2.ExecuteNonQuery();
                            if (res > 0)
                            {
                                MessageBox.Show("Товар удален!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Parent.Controls.Remove(this);

                                return;
                            }
                        }
                    }
                }


                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Delete();
        }
    }
}
