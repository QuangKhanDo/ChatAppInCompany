using ChatAppInCompany.FirebaseHelper;
using ChatAppInCompany.Services;
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

        public static Action EmulateBackPressed;

        private bool AcceptBack;

        protected override bool OnBackButtonPressed()
        {
            if (AcceptBack)
                return false;

            PromptForExit();
            return true;
        }

        private async void PromptForExit()
        {
            if (await Application.Current.MainPage.DisplayAlert("Thông báo", "Bạn chắc chắn muốn rời phòng?", "Rời phòng", "Ở lại"))
            {
                AcceptBack = true;
                OneSignal.Current.DeleteTag("Room");
                DependencyService.Get<IMessageService>().LogoutRoom();
                MyFirebaseHelper helper = new MyFirebaseHelper();
                helper.RemoveUserFromRoomChat();
                App.UserInRoom.Clear();
                EmulateBackPressed();
            }
        }

        ~ChatPage()
        {
            MessagingCenter.Unsubscribe<string>(this, "NewMessageComing");
        }
    }
}