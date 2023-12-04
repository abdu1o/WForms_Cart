using Lesson_10_11.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lesson_10_11
{
    public partial class DetailForm : Form
    {
        private List<Product> cartItems;

        public DetailForm(List<Product> cartItems)
        {
            InitializeComponent();
            this.cartItems = cartItems;
            Console.WriteLine($"Number of items in cartItems: {cartItems.Count}");
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns.Add("Name", "Product Name");
            dataGridView1.Columns.Add("Price", "Price");
            dataGridView1.Columns.Add("Quantity", "Quantity");

            dataGridView1.Columns["Name"].DataPropertyName = "Name";
            dataGridView1.Columns["Price"].DataPropertyName = "Price";
            dataGridView1.Columns["Quantity"].DataPropertyName = "Quantity";

            dataGridView1.DataSource = GetDistinctCartItems();
        }

        private List<Product> GetDistinctCartItems()
        {
            var distinctCartItems = cartItems.GroupBy(item => item.Id)
            .Select(group => new Product
             {
                 Id = group.Key,
                 Name = group.First().Name,
                 Price = group.First().Price,
                 Quantity = group.Count()
             })
            .ToList();

            return distinctCartItems;
        }
    }
}
