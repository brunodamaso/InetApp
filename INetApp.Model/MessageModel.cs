using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;
using Xamarin.CommunityToolkit;
using Xamarin.CommunityToolkit.ObjectModel;

namespace INetApp.Models
{    
    /**
     * Class that represents a user in the presentation layer.
     */
    public class MessageModel : ObservableObject
    {        
        public static readonly string URL_LABEL = "URL";

        [PrimaryKey]
        public int messageId { get; set; }
        [Indexed]
        public int categoryId { get; set; }
        public string name { get; set; }
        public DateTime date { get; set; }
        public bool favorite { get; set; }
        [Ignore]
        public MessageDetails fields { get; set; }
        public bool checkeado { get; set; }
    }
}
