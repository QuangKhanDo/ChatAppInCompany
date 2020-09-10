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
using ChatAppInCompany.Services;

namespace ChatAppInCompany.Android.Services
{
    [Service]
    public class SendMessageService : Service
    {
        private string MessageContent;
        public override IBinder OnBind(Intent intent)
        {
            // Return null because this is a pure started service. A hybrid service would return a binder that would
            // allow access to the GetFormattedStamp() method.
            MessageContent = intent.GetStringExtra("Message");
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            MessageContent = intent.GetStringExtra("Message");
            OnesignalService onesignalService = new OnesignalService();
            onesignalService.SendMessageToRoomChat(MessageContent);

            return StartCommandResult.NotSticky;
        }
    }
}