using JewelryShop.Model;
using JewelryShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JewelryShop.View
{
    /// <summary>
    /// Логика взаимодействия для EditProductWindow.xaml
    /// </summary>
    public partial class EditProductWindow : Window
    {
        private Model.ProductOutput product;
        private Model.User user;
        public ObservableCollection<Model.ProductCategory> Category { get; set; }
        public ObservableCollection<Model.Provider> Provider { get; set; }
        private EditProductViewModel viewModel;
        public EditProductWindow(Model.ProductOutput product, Model.User user)
        {
            InitializeComponent();
            this.product = product;
            DataContext = product;
            this.user = user;
            viewModel = new EditProductViewModel();
            var manufactures = viewModel.GetAllManufacture();
            var categories = viewModel.GetAllProductCategory();
            var providers = viewModel.GetAllProvider();
            DataContext = product;

            foreach (var manufacture in manufactures)
            {
                cbManufacturer.Items.Add(manufacture);
            }
            foreach (var categorie in categories)
            {
                cbCategory.Items.Add(categorie);
            }
            foreach (var provider in providers)
            {
                cbProvider.Items.Add(provider);
            }
        }

        private void OnClickEdit(object sender, RoutedEventArgs e)
        {
            var productName = tbName.Text.Trim();
            var description = tbDescription.Text.Trim();
            var category = cbCategory.SelectedItem as Model.ProductCategory;
            var manufactures = cbManufacturer.SelectedItem as Model.Manufacture;
            var provider = cbProvider.SelectedItem as Model.Provider;

            int cost, discountAmount, maxDiscountAmount, quantityInStock;
            try
            {
                cost = int.Parse(tbCost.Text.Trim());
                discountAmount = int.Parse(tbDiscountAmount.Text.Trim());
                maxDiscountAmount = int.Parse(tbMaxDiscountAmount.Text.Trim());
                quantityInStock = int.Parse(tbQuantityInStock.Text.Trim());

            }
            catch (Exception ex)
            {
                MessageBox.Show("В каком-то из полей ошибка");
                return;
            }

            if (productName == "" || description == "" || category == null || manufactures == null || provider == null ||
                cost == 0 || quantityInStock == 0)
            {
                MessageBox.Show("Все поля должны быть заполнены");
                return;
            }

            Product product = new Product
            {
                ArticleNumber = this.product.ArticleNumber,
                Name = productName,
                Description = description,
                Category = category.Id,
                Manufacture = manufactures.Id,
                Provider = provider.Id,
                Cost = cost,
                DiscountAmount = discountAmount,
                MaxDiscountAmount = maxDiscountAmount,
                QuantityInStock = quantityInStock,
                Unit = Unit.Шт
            };
            bool isProductEdit = viewModel.EditProduct(product);

            if (!isProductEdit)
            {
                MessageBox.Show("Что то не так");
                return;
            }
            MessageBox.Show("Товар обновлен");
        }

        private void OnClickBack(object sender, RoutedEventArgs e)
        {
            ProductWindow productWindow = new ProductWindow(user);
            productWindow.Show();
            Close();
        }
    }
}
