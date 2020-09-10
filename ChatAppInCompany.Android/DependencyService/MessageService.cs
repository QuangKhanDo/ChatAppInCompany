using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ChatAppInCompany.Android.DependencyService;
using ChatAppInCompany.Android.Services;
using ChatAppInCompany.Services;

[assembly: Xamarin.Forms.Dependency(typeof(MessageService))]
namespace ChatAppInCompany.Android.DependencyService
{
    public class MessageService : IMessageService
    {
        public void GetAllUser()
        {
            Intent intent = new Intent(Application.Context, typeof(GetUserService));
            Application.Context.StartService(intent);
        }

        public void LogoutRoom()
        {
            Intent intent = new Intent(Application.Context, typeof(GetUserService));
            Application.Context.StopService(intent);
        }

        public void SendMessage(string Message)
        {
            Intent intent = new Intent(Application.Context, typeof(SendMessageService));
            intent.PutExtra("Message", Message);
            Application.Context.StartService(intent);
        }
    }
}