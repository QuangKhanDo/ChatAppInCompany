using ChatAppInCompany.FirebaseHelper;
using ChatAppInCompany.Services;
using ChatAppInCompany.Views;
using ChatAppInCompany.Views.Popup;
using Com.OneSignal;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace ChatAppInCompany.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        //Constructor
        public MainPageViewModel()
        {
            //Set Property





            //Method





            //Command
            BackCommand = new Command(GetBack);
            RoomChatCommand = new Command(OpenRoomChat);
        }

        #region Properties
        private MyFirebaseHelper helper = new MyFirebaseHelper();

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set 
            { 
                _userName = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Method
        private async void OpenRoomChat()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                await Application.Current.MainPage.DisplayAlert("Cảnh báo", "Bạn phải nhập tên của minh!", "OK");
                return;
            }
            await PopupNavigation.Instance.PushAsync(new LoadingPopup());
            await helper.AddUserToRoomChat(new Models.User() { UserName = this.UserName , Oid = App.MYUID.ToString() });
            App.MYNAME = UserName;
            await helper.GetAllUserInRoomChat();
            DependencyService.Get<IMessageService>().GetAllUser();
            Application.Current.MainPage.Navigation.PushAsync(new ChatPage());
        }

        private void GetBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
        #endregion

        #region Command
        public Command BackCommand { get; }
        public Command RoomChatCommand { get; }
        #endregion

        #region Property Interface
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
