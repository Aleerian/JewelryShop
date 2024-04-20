using JewelryShop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryShop.ViewModel
{
    public class Authorization
    {
        public Model.User SignIn (string login, string password)
        {
            var user = DbCommandSender.SignIn(login, password);

            return user;
        }
    }
}
