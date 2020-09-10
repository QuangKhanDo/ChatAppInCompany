using ChatAppInCompany.ViewModels;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatAppInCompany.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        public ChatPage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<string>(this, "NewMessageComing", (message) =>
            {
                ((LinearLayout) chatListView.LayoutManager).ScrollToRowIndex(
                chatListView.DataSource.DisplayItems.Count - 1, Syncfusion.ListView.XForms.ScrollToPosition.End, true);
            });

            this.BindingContext = new ChatViewModel();
        }

        ~ChatPage()
        {
            MessagingCenter.Unsubscribe<string>(this, "NewMessageComing");
        }
    }
}