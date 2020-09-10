using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using ChatAppInCompany.FirebaseHelper;
using ChatAppInCompany.Services;
using Xamarin.Forms;

namespace ChatAppInCompany.Android.Services
{
    [Service]
    public class GetUserService : Service
    {
        private bool ServiceRunning;
        MyFirebaseHelper helper = new MyFirebaseHelper();
        public override IBinder OnBind(Intent intent)
        {
            // Return null because this is a pure started service. A hybrid service would return a binder that would
            // allow access to the GetFormattedStamp() method.
            return null;
        }

        public override void OnDestroy()
        {
            ServiceRunning = false;
            StopSelf();
            base.OnDestroy();
        }

        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            ServiceRunning = true;
            GetAllRoomMember();
            return StartCommandResult.NotSticky;
        }

        public void GetAllRoomMember()
        {
            // Gửi vị trí mỗi 30s
            Task.Run(() => {
                Device.StartTimer(new TimeSpan(0, 0, 5), () =>
                {
                    // do something every 30 seconds
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        // interact with UI elements
                        await helper.GetAllUserInRoomChat();
                        MessagingCenter.Send(App.UserInRoom.Count.ToString(), "UpdateUser");
                        Log.Debug("UpdateRoom", "Cập nhật người dùng lúc " + DateTime.Now.ToString("hh:mm:ss") + " \nCó " + App.UserInRoom.Count + " người trong phòng");

                    });
                    return ServiceRunning; // runs again, or false to stop
                });
            });
        }
    }
}