using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace INetApp.Models
{
    //using com.ineco.inetapp.presentation.R;
    /**
     * Class that represents a user in the presentation layer.
     */
    public class CategoryModel : BindableObject
    {

        //private readonly string LABEL_TRAVEL = "VIAJES";
        //private readonly string LABEL_INFO = "VENTANILLA";
        //private readonly string LABEL_PROFILE = "PERFIL";
        //private readonly string LABEL_EPI = "EPIs";
        //private readonly string LABEL_EMPLOYEE = "PortalEmpleado";

     
        public CategoryModel() { }
        public CategoryModel(int categoryId)
        {
            this.categoryId = categoryId;
        }
        public int categoryId { get; set; }
        public string name { get; set; }
        public int pendingMessages { get; set; }
        public string urIcon { get; set; }


        public int getCategoryId()
        {
            return categoryId;
        }


        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public int getPendingMessages()
        {
            return pendingMessages;
        }

        public void setPendingMessages(int pendingMessages)
        {
            this.pendingMessages = pendingMessages;
        }

        //public int getDrawableFromName() {
        //    int drawable = R.drawable.ic_editor_insert_comment;
        //    if (name.equals(LABEL_TRAVEL)) {
        //        drawable = R.drawable.ic_plane;
        //    } else if (name.equals(LABEL_INFO)) {
        //        drawable = R.drawable.ic_windows;
        //    } else if (name.equals( LABEL_PROFILE)) {
        //        drawable = R.drawable.ic_profile;
        //    } else if (name.equals(LABEL_EPI)) {
        //        drawable = R.drawable.ic_epi;
        //    } else if (name.equals(LABEL_EMPLOYEE)) {
        //        drawable = R.drawable.ic_employee;
        //    }
        //    return drawable;
        //}

        public string getUrIcon()
        {
            return urIcon;
        }

        public void setUrIcon(string urIcon)
        {
            this.urIcon = urIcon;
        }
        //override
        //public string ToString()
        //{
        //    StringBuilder stringBuilder = new StringBuilder();

        //    stringBuilder.Append("***** Message Model Category *****\n");
        //    stringBuilder.Append("id=" + this.getCategoryId() + "\n");
        //    stringBuilder.Append("name=" + this.getName() + "\n");
        //    stringBuilder.Append("pending messages=" + this.getPendingMessages() + "\n");
        //    stringBuilder.Append("*******************************");

        //    return stringBuilder.ToString();
        //}
    }
}
