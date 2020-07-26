using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingList.Helpers
{
    public class Constants
    {
        // REST API Specific:
        public const string SERVER_HOST = "http://shopping-list-server.dev-apps.notsoslow.tk";
        public const string GET_ALL_ITEMS = SERVER_HOST + "/items";
        public const string ITEM = SERVER_HOST + "/item/";
        public const string AUTH = SERVER_HOST + "/auth";
        public const string REGISTER = SERVER_HOST + "/register";

        // Authentication Required (Used for earlier dev while API security is being built):
        public const bool LOGON_REQUIRED = false;

        // Preference Keys and values:
        public const string SORT_BY = "SortBy";
        public const string SORT_BY_DEFAULT = "Default";
        public const string SORT_BY_NAME = "Name";

        // Messaging Centre values:
        public const string SORT_BY_DEFAULT_SELECTED = "SortByDefaultSelected";
        public const string SORT_BY_NAME_SELECTED = "SortByNameSelected";

        public const string DISPLAY_EMPTY_VIEW = "DisplayEmptyView";
        public const string DISPLAY_NETWORK_ERROR = "DisplayNetworkError";
        public const string LISTVIEW_REFRESH_COMPLETE = "ListViewRefreshComplete";

        public const string CLOSE_NEW_ITEM_PAGE = "CloseNewItemPage";
        public const string CLOSE_ITEM_DETAIL_PAGE = "CloseItemDetailPage";
        public const string POPUP_PAGE_FINISHED = "PopUpPageFinished";

        public const string UNDO_POPUP_CLOSED = "UndoPopupClosed";

        public const string ITEM_DELETED = "ItemDeleted";

        public const string REQUEST_UNDO_POPUP_CLOSE = "RequestUndoPopupClose";

        public const string REFOCUS = "Refocus";

        public const string REFOCUS_KEYBOARD = "RefocusKeyboard";


    }
}
