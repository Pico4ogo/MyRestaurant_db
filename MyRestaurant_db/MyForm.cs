using MyRestaurant_db.Properties;
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

namespace MyRestaurant_db
{
    public partial class MyForm : Form
    {
        public MyForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var customers = GetDishesEf();
            int i = 0;
            dataGridView1.RowCount = 10;
            dataGridView1.ColumnCount = 3;
            foreach (var customer in customers)
            {
                dataGridView1.Rows[i].Cells[0].Value = customer.id_dish;
                dataGridView1.Rows[i].Cells[1].Value = customer.dish_name;
                dataGridView1.Rows[i].Cells[2].Value = customer.dish_price;
                i += 1;
            }
        }

        private static List<dish> GetDishesEf()
        {
            var context = new restaurant_dbContext();
            IQueryable<dish> query = context.dish;
            List<dish> customers = query.ToList();
            return customers;
        }
        private static List<dish> GetDishesEf(int n)
        {
            var context = new restaurant_dbContext();
            IQueryable<dish> query = context.dish.Where(c => c.id_dish == n);
            List<dish> customers = query.ToList();
            return customers;
        }
        private static List<recipes> GetRecipesEf(int n)
        {
            var context = new restaurant_dbContext();
            IQueryable<recipes> query = context.recipes.Where(c => c.dish_id == n);
            List<recipes> customers = query.ToList();
            return customers;
        }
        private static List<ingredients> GetIngredientsEf(int n)
        {
            var context = new restaurant_dbContext();
            IQueryable<ingredients> query = context.ingredients.Where(c => c.id_ingredient == n);
            List<ingredients> customers = query.ToList();
            return customers;
        }

        private static List<MyProxy> GetCustomers()
        {
            using (IDbConnection connection = new SqlConnection(Settings.Default.dbConnect))
            {
                IDbCommand command = new SqlCommand("SELECT * FROM dish");
                command.Connection = connection;

                connection.Open();

                IDataReader reader = command.ExecuteReader();

                List<MyProxy> customers = new List<MyProxy>();

                while (reader.Read())
                {
                    MyProxy customer = new MyProxy();
                    customer.Id = reader.GetInt32(0);
                    customer.Name = reader.GetString(1);
                    customers.Add(customer);
                }
                return customers;
            }
        }
        private void textBox_TextChanged(object sender, EventArgs e)
        {
        }
        private void button_Click(object sender, EventArgs e)
        {
            int numRec = 1;
            try
            {
                numRec = Convert.ToInt32(textBox.Text);
            } catch (Exception)
            {
                MessageBox.Show("Введите номер блюда.");
            }
            req(numRec);
        }
        private void req(int numRec)
        {
            dataGridView2.RowCount = 1;
            dataGridView2.ColumnCount = 3;
            var dishes = GetDishesEf(numRec);
            var recipes = GetRecipesEf(numRec);
            var ingredients = GetIngredientsEf(numRec);
            foreach (var dish_tmp in dishes)
            {
                dataGridView2.Rows[0].Cells[0].Value = dish_tmp.dish_name;
            }
            foreach (var recipe_tmp in recipes)
            {
                dataGridView2.Rows[0].Cells[1].Value = recipe_tmp.recipe;
            }
            foreach (var ingredient_tmp in ingredients)
            {
                dataGridView2.Rows[0].Cells[2].Value = ingredient_tmp.ingredients1;
            }
        }
    }
}
