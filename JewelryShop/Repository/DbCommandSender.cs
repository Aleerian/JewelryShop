using JewelryShop.Model;
using JewelryShop.ViewModel;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JewelryShop.Repository
{
    public static class DbCommandSender
    {
        public static User SignIn (string login, string password)
        {
            NpgsqlCommand command = DbConnector.GetCommand("Select * FROM \"User\" WHERE login=@login AND password=@password");
            command.Parameters.AddWithValue("@login", NpgsqlDbType.Varchar, login);
            command.Parameters.AddWithValue("@password", NpgsqlDbType.Varchar, password);
            using (NpgsqlDataReader result = command.ExecuteReader())
            {
                try
                {
                    if (result.HasRows)
                    {
                        result.Read();

                        User user = new User
                        {
                            Id = result.GetInt32(result.GetOrdinal("Id")),
                            Surname = result.GetString(result.GetOrdinal("Surname")),
                            Name = result.GetString(result.GetOrdinal("Name")),
                            Patronymic = result.GetString(result.GetOrdinal("Patronymic")),
                            Login = result.GetString(result.GetOrdinal("Login")),
                            Password = result.GetString(result.GetOrdinal("Password")),
                            Role = result.GetInt32(result.GetOrdinal("Role"))
                        };

                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return null;
                }
            }
        }

        public static bool DeleteProduct(string id)
        {
            try
            {
                NpgsqlCommand command = DbConnector.GetCommand("DELETE FROM \"Product\" WHERE article_number = @id");
                command.Parameters.AddWithValue("@id", NpgsqlDbType.Varchar, id);

                int rowsAffected = command.ExecuteNonQuery();

                // Если удаление прошло успешно и была хотя бы одна запись удалена, возвращаем true
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public static List<Product> GetAllProduct()
        {
            NpgsqlCommand command = DbConnector.GetCommand("Select * FROM \"Product\"");
            List<Product> products = new List<Product>();
            using (NpgsqlDataReader result = command.ExecuteReader())
            {
                try
                {
                    while (result.Read())
                    {
                        Product product = new Product
                        {
                            ArticleNumber = result.GetString(result.GetOrdinal("article_number")),
                            Name = result.GetString(result.GetOrdinal("Name")),
                            Description = result.GetString(result.GetOrdinal("Description")),
                            Category = result.GetInt32(result.GetOrdinal("Category")),
                            Manufacture = result.GetInt32(result.GetOrdinal("manufacturer")), //кривое название в бд
                            Provider = result.GetInt32(result.GetOrdinal("Provider")),
                            Cost = result.GetDouble(result.GetOrdinal("Cost")),
                            DiscountAmount = result.GetInt32(result.GetOrdinal("discount_amount")),
                            MaxDiscountAmount = result.GetInt32(result.GetOrdinal("max_discount_amount")),
                            QuantityInStock = result.GetInt32(result.GetOrdinal("quantity_in_stock"))
                        };

                        products.Add(product);
                    }

                    return products;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return null;
                }
            }
        }

        public static Manufacture GetManufactureById(int id)
        {
            NpgsqlCommand command = DbConnector.GetCommand("Select * FROM \"Manufacture\" WHERE id=@id");
            command.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);
            using (NpgsqlDataReader result = command.ExecuteReader())
            {
                try
                {
                    if (result.HasRows)
                    {
                        result.Read();

                        Manufacture manufacture = new Manufacture
                        {
                            Id = result.GetInt32(result.GetOrdinal("Id")),
                            Name = result.GetString(result.GetOrdinal("Name")),
                        };

                        return manufacture;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return null;
                }
            }
        }

        public static List<Manufacture> GetAllManufacture()
        {
            NpgsqlCommand command = DbConnector.GetCommand("Select * FROM \"Manufacture\"");
            List<Manufacture> manufactures = new List<Manufacture>();
            using (NpgsqlDataReader result = command.ExecuteReader())
            {
                try
                {
                    while (result.Read())
                    {
                        Manufacture manufacture = new Manufacture
                        {
                            Id = result.GetInt32(result.GetOrdinal("Id")),
                            Name = result.GetString(result.GetOrdinal("Name")),
                        };
                        manufactures.Add(manufacture);
                    }
                    return manufactures;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return null;
                }
            }
        }

        public static List<Provider> GetAllProvider()
        {
            NpgsqlCommand command = DbConnector.GetCommand("Select * FROM \"Provider\"");
            List<Provider> providers = new List<Provider>();
            using (NpgsqlDataReader result = command.ExecuteReader())
            {
                try
                {
                    while (result.Read())
                    {
                        Provider provider = new Provider
                        {
                            Id = result.GetInt32(result.GetOrdinal("Id")),
                            Name = result.GetString(result.GetOrdinal("Name")),
                        };
                        providers.Add(provider);
                    }
                    return providers;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return null;
                }
            }
        }

        public static List<ProductCategory> GetAllProductCategory()
        {
            NpgsqlCommand command = DbConnector.GetCommand("Select * FROM \"ProductCategory\"");
            List<ProductCategory> productCategorys = new List<ProductCategory>();
            using (NpgsqlDataReader result = command.ExecuteReader())
            {
                try
                {
                    while (result.Read())
                    {
                        ProductCategory productCategory = new ProductCategory
                        {
                            Id = result.GetInt32(result.GetOrdinal("Id")),
                            Name = result.GetString(result.GetOrdinal("Name")),
                        };
                        productCategorys.Add(productCategory);
                    }
                    return productCategorys;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return null;
                }
            }
        }

        public static void AddProduct(Product product)
        {
            NpgsqlCommand command = DbConnector.GetCommand(@"INSERT INTO ""Product"" 
                    (""article_number"", ""name"", ""description"", ""category"", ""manufacturer"", ""provider"", ""cost"", ""discount_amount"", ""max_discount_amount"", ""quantity_in_stock"") 
                    VALUES 
                    (@ArticleNumber, @Name, @Description, @Category, @Manufacturer, @Provider, @Cost, @DiscountAmount, @MaxDiscountAmount, @QuantityInStock)");

            command.Parameters.AddWithValue("@ArticleNumber", product.ArticleNumber);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Description", product.Description);
            command.Parameters.AddWithValue("@Category", product.Category);
            command.Parameters.AddWithValue("@Manufacturer", product.Manufacture);
            command.Parameters.AddWithValue("@Provider", product.Provider);
            command.Parameters.AddWithValue("@Cost", product.Cost);
            command.Parameters.AddWithValue("@DiscountAmount", product.DiscountAmount);
            command.Parameters.AddWithValue("@MaxDiscountAmount", product.MaxDiscountAmount);
            command.Parameters.AddWithValue("@QuantityInStock", product.QuantityInStock);
            using (NpgsqlDataReader result = command.ExecuteReader())
            {
                command.ExecuteNonQuery();
            }
        }

        public static bool UpdateProduct(Product product)
        {

            NpgsqlCommand command = DbConnector.GetCommand(@"UPDATE ""Product"" SET 
                        ""name"" = @Name,
                        ""description"" = @Description,
                        ""category"" = @Category,
                        ""manufacturer"" = @Manufacturer,
                        ""provider"" = @Provider,
                        ""cost"" = @Cost,
                        ""discount_amount"" = @DiscountAmount,
                        ""max_discount_amount"" = @MaxDiscountAmount,
                        ""quantity_in_stock"" = @QuantityInStock
                    WHERE ""article_number"" = @ArticleNumber");

            command.Parameters.AddWithValue("@ArticleNumber", product.ArticleNumber);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Description", product.Description);
            command.Parameters.AddWithValue("@Category", product.Category);
            command.Parameters.AddWithValue("@Manufacturer", product.Manufacture);
            command.Parameters.AddWithValue("@Provider", product.Provider);
            command.Parameters.AddWithValue("@Cost", product.Cost);
            command.Parameters.AddWithValue("@DiscountAmount", product.DiscountAmount);
            command.Parameters.AddWithValue("@MaxDiscountAmount", product.MaxDiscountAmount);
            command.Parameters.AddWithValue("@QuantityInStock", product.QuantityInStock);

            using (NpgsqlDataReader result = command.ExecuteReader())
            {
                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }
    }
}
