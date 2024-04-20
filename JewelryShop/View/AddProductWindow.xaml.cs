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
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        public ObservableCollection<Model.Manufacture> Manufactures { get; set; }
        public ObservableCollection<Model.ProductCategory> Category { get; set; }
        public ObservableCollection<Model.Provider> Provider { get; set; }
        private AddProductViewModel addProductViewModel { get; set; }
        private Model.User user;
        public AddProductWindow(Model.User user)
        {
            this.user = user;
            InitializeComponent();
            DataContext = this;
            addProductViewModel = new AddProductViewModel();
            var manufactures = addProductViewModel.GetAllManufacture();
            var categories = addProductViewModel.GetAllProductCategory();
            var providers = addProductViewModel.GetAllProvider();

            Manufactures = new ObservableCollection<Model.Manufacture>(manufactures);
            Category = new ObservableCollection<Model.ProductCategory>(categories);
            Provider = new ObservableCollection<Model.Provider>(providers);
        }

        private void OnClickBack(object sender, RoutedEventArgs e)
        {
            ProductWindow productWindow = new ProductWindow(user);
            productWindow.Show();
            Close();
        }

        private void OnClickAdd(object sender, RoutedEventArgs e)
        {
            var articleNumber = tbArticleNumber.Text.Trim();
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

            if (articleNumber == "" || productName == "" || description == "" || category == null || manufactures == null || provider == null ||
                cost == 0 || quantityInStock == 0)
            {
                MessageBox.Show("Все поля должны быть заполнены");
                return;
            }

            Product product = new Product
            {
                ArticleNumber = articleNumber,
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

            addProductViewModel.AddProduct(product);

            MessageBox.Show("Продукт был добавлен");
        }
    }
}
