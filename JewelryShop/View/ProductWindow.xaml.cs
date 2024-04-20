using JewelryShop.Model;
using JewelryShop.Model.Struct;
using JewelryShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private const int ADMIN_ROLE = 4;
        private Model.User user;
        private ProductViewModel productViewModel;
        private readonly Model.Manufacture allManufacture;
        public Model.Manufacture SelectedManufacturer { get; set; }
        public SortItem SelectedSort { get; set; }
        public ObservableCollection<Model.Manufacture> Manufactures { get; set; }
        public ObservableCollection<SortItem> SortItems { get; set; }
        public int ItemCount
        {
            get { return lvProducts.Items.Count; }
        }

        public ProductWindow(Model.User user)
        {
            InitializeComponent();
            DataContext = this;
            this.user = user;
            productViewModel = new ProductViewModel();

            allManufacture = new Model.Manufacture() { Id = 0, Name = "Все производители" };
            var manufactures = productViewModel.GetAllManufacture();
            Manufactures = new ObservableCollection<Model.Manufacture>(manufactures);
            Manufactures.Insert(0, allManufacture);
            
            SortItems = new ObservableCollection<SortItem>
            {
                new SortItem() { 
                    Name = "Сортировать по возврастанию цены", 
                    Description = new SortDescription() {
                        PropertyName = "Cost",
                        Direction = ListSortDirection.Ascending } },
                new SortItem() { 
                    Name = "Сортировать по убыванию цены",
                    Description = new SortDescription() {
                        PropertyName = "Cost", 
                        Direction = ListSortDirection.Descending } },
                new SortItem() { 
                    Name = "Сортировать по количеству", 
                    Description = new SortDescription() { 
                        PropertyName = "QuantityInStock", 
                        Direction = ListSortDirection.Ascending } },
            };

            if (user.Role == ADMIN_ROLE) AddButton.Visibility = Visibility.Visible;

            UpdateProduct();
        }

        public void UpdateProduct()
        {
            var products = productViewModel.GetAllProducts();
            lvProducts.Items.Clear();
            foreach (var product in products)
            {
                ProductFrame productFrame = new ProductFrame(product, user, this);
                lvProducts.Items.Add(productFrame);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            var searchString = tbSearch.Text.Trim().ToLower();

            var view = CollectionViewSource.GetDefaultView(lvProducts.Items);
            if (view == null) return;

            view.Filter = (object o) =>
            {
                var productFrame = o as ProductFrame;
                if (productFrame == null) return false;
                

                var productName = productFrame.NameProduct.Trim().ToLower();
                var productManufacture = productFrame.Manufacture.Trim().ToLower();

                if (searchString.Length > 0)
                {
                    if (!(productName.Contains(searchString) || productManufacture.Contains(searchString)))
                    {
                        return false;
                    }

                }
                
                if (SelectedManufacturer != null && SelectedManufacturer != allManufacture) 
                {
                    var searchManufacturer = SelectedManufacturer.Name.Trim().ToLower();
                    if (productManufacture != searchManufacturer)
                    {
                        return false;
                    }
                }
                return true;
            };
        }

        private void ApplySort()
        {
            var view = CollectionViewSource.GetDefaultView(lvProducts.Items);
            if (view == null) return;

            view.SortDescriptions.Clear();

            view.SortDescriptions.Add(SelectedSort.Description);

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilter();
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ApplySort();
        }

        private void OnClickAddPost(object sender, RoutedEventArgs e)
        {
            AddProductWindow addProductWindow = new AddProductWindow(user);
            addProductWindow.Show();
            Close();
        }
    }
}
