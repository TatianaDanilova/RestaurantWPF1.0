using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWPF
{
    public static class AppSettings
    {
        private static int userId;
        private static string userFirstName;
        private static string userLastName;


        public static int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        public static string UserFirstName
        {
            get { return userFirstName; }
            set { userFirstName = value; }
        }
        public static string UserLastName
        {
            get { return userLastName; }
            set { userLastName = value; }
        }
    }

}

