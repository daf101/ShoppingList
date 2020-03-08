using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingList.Helpers
{
    public class Constants
    {
        // REST API Specific:
        public const string SERVER_HOST = "http://shopping-list-server.prod-apps.notsoslow.tk";
        public const string GET_ALL_ITEMS = SERVER_HOST + "/items";
        public const string ITEM = SERVER_HOST + "/item/";
        public const string AUTH = SERVER_HOST + "/auth";
        public const string REGISTER = SERVER_HOST + "/register";

        // Authentication Required (Used for earlier dev while API security is being built):
        public const bool LOGON_REQUIRED = false;
    }
}
