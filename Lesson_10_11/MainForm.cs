using Lesson_10_11.Models;
using System.Collections.ObjectModel;

namespace Lesson_10_11
{
    public partial class MainForm : Form
    {
        List<Category> category;
        List<Product> products;

        public MainForm()
        {
            InitializeComponent();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            listBoxProducts.SelectionMode = SelectionMode.MultiExtended;
            category = Seeder.InitCategory();
            products = Seeder.InitProduct(category);
            comboBoxCategory.DataSource = category;
            comboBoxCategory.DisplayMember = "Name";
            comboBoxCategory.ValueMember = "Id";
            FillData();

            ContextMenuStrip contextMenu = new ContextMenuStrip();

            ToolStripMenuItem menuItemDetail = new ToolStripMenuItem("Просмотр детальной информации о товаре");
            menuItemDetail.Click += MenuItemDetail_Click;

            contextMenu.Items.Add(menuItemDetail);

            listBoxProducts.ContextMenuStrip = contextMenu;
        }

        private void MenuItemDetail_Click(object sender, EventArgs e)
        {
            Product selectedProduct = listBoxProducts.SelectedItem as Product;

            if (selectedProduct != null)
            {
                List<Product> selectedProducts = new List<Product> { selectedProduct };

                using (DetailForm detailForm = new DetailForm(selectedProducts))
                {
                    detailForm.ShowDialog();
                }
            }
        }

        private void FillData()
        {
            listBoxProducts.DataSource = products
                .Where(e => e.Category.Id.Equals(comboBoxCategory.SelectedValue)).ToList();
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillData();
        }

        private void AddCartBtn_Click(object sender, EventArgs e)
        {
            var selectedProducts = listBoxProducts
                .SelectedItems
                .OfType<Product>().ToList();

            if (selectedProducts != null)
            {
                foreach (var el in selectedProducts)
                {
                    listBoxCart.Items.Add(el);

                }
                textBoxTotalPay.Text = listBoxCart.Items.OfType<Product>().Sum(e => e.Price).ToString();
                textBoxCount.Text = listBoxCart.Items.Count.ToString();
            }

        }

        private void Remove_Click(object sender, EventArgs e)
        {
            var selectedCartItems = listBoxCart.SelectedItems.OfType<Product>().ToList();

            foreach (var selectedItem in selectedCartItems)
            {
                listBoxCart.Items.Remove(selectedItem);
            }

            textBoxTotalPay.Text = listBoxCart.Items.OfType<Product>().Sum(item => item.Price).ToString();
            textBoxCount.Text = listBoxCart.Items.Count.ToString();
        }

        private void Detail_Click(object sender, EventArgs e)
        {
            using (var DetailForm = new DetailForm(listBoxCart.Items.OfType<Product>().ToList()))
            {
                DetailForm.ShowDialog();
            }
        }
    }
}