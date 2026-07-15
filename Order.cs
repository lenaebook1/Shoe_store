using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace WindowsFormsApp1
{
    public partial class Order : UserControl
    {
        public string DbConn = "Host = localhost; Port = 5432; Username = postgres; Password = ; Database = shoe_cool";

        public string nomer_zak;
        public Order(string role)
        {
            InitializeComponent();

            if (role != "Администратор")
            {
                button1.Visible = false;
                button2.Visible = false;
            }
        }

        // удаление
        private void button2_Click(object sender, EventArgs e)
        {
            var con = new NpgsqlConnection(DbConn); 
            con.Open();
            var sql = $@"DELETE FROM public.zakaz
	WHERE nomer_zakaza_pk = '{nomer_zak}';";
            var cmd = new NpgsqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Заказ успешно удален","Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            var main = (Main)Application.OpenForms["main"];
            main.Order();
        }

        // редактирование
        private void button1_Click(object sender, EventArgs e)
        {
            var editZak = new AddOrder(nomer_zak);
            editZak.ShowDialog();
        }
    }
}
