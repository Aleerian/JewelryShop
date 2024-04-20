using JewelryShop.Model;
using JewelryShop.ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JewelryShop.View
{
    /// <summary>
    /// Логика взаимодействия для ProductFrame.xaml
    /// </summary>
    public partial class ProductFrame : UserControl
    {
        public string NameProduct {  get; set; }
        public string Description { get; set; }
        public string Manufacture { get; set; }
        public double Cost { get; set; }
        public int QuantityInStock { get; set; }

        private ProductViewModel productViewModel;
        private Model.ProductOutput product;
        private const int ADMIN_ROLE = 4;
        private Model.User user;
        private ProductWindow productWindow;
        public ProductFrame(Model.ProductOutput product,  Model.User user, ProductWindow productWindow)
        {
            InitializeComponent();
            this.productWindow = productWindow;
            DataContext = this;
            NameProduct = product.Name;
            Description = product.Description;
            Manufacture = product.Manufacture.Name;
            Cost = product.Cost;
            QuantityInStock = product.QuantityInStock;
            productViewModel = new ProductViewModel();
            this.product = product;
            this.user = user;

            if (ADMIN_ROLE == this.user.Role)
            {
                DeleteButton.Visibility = Visibility.Visible;
                EditButton.Visibility = Visibility.Visible;
            }

        }

        private void OnClickDeletePost(object sender, RoutedEventArgs e)
        {
            var isProductDeleted = productViewModel.DeleteProduct(product.ArticleNumber);
            if (!isProductDeleted) 
            {
                MessageBox.Show("Не удалось удалить");
            }
            productWindow.UpdateProduct();
        }

        private void OnClickEditPost(object sender, RoutedEventArgs e)
        {
            EditProductWindow editProductWindow = new EditProductWindow(product, user);
            editProductWindow.Show();
            productWindow.Close();
        }
    }
}
