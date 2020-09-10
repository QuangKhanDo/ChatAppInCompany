using ChatAppInCompany.FirebaseHelper;
using ChatAppInCompany.ViewModels;
using Com.OneSignal;
using Rg.Plugins.Popup.Services;
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

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (PopupNavigation.Instance.PopupStack.Count != 0)
                await PopupNavigation.Instance.PopAllAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            OneSignal.Current.DeleteTag("Room");
            MyFirebaseHelper helper = new MyFirebaseHelper();
            helper.RemoveUserFromRoomChat();
            return base.OnBackButtonPressed();
        }

        ~ChatPage()
        {
            MessagingCenter.Unsubscribe<string>(this, "NewMessageComing");
        }
    }
}