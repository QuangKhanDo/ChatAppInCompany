﻿using ChatAppInCompany.Models;
using ChatAppInCompany.Views;
using Com.OneSignal;
using Com.OneSignal.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ChatAppInCompany
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjkwMzQzQDMxMzgyZTMyMmUzMFJhZTBwTStibVhxOXVvUlkwQmVXOWdWZFB1US9QS2dlaitmT2xCSkEwbjA9");
            InitializeComponent();

            OneSignal.Current.SetLogLevel(LOG_LEVEL.VERBOSE, LOG_LEVEL.NONE);

            OneSignal.Current.StartInit("44b0e932-0b17-4b6b-b1d6-5d61f919b8f9")
            .InFocusDisplaying(OSInFocusDisplayOption.None)
            .HandleNotificationReceived(HandleNotificationReceived)
            .EndInit();

            // The promptForPushNotificationsWithUserResponse function will show the iOS push notification prompt. We recommend removing the following code and instead using an In-App Message to prompt for notification permission (See step 7)
            OneSignal.Current.RegisterForPushNotifications();

            OneSignal.Current.SendTag("Room", "9221");


            MainPage = new NavigationPage(new ChatPage());
        }

        private static void HandleNotificationReceived(OSNotification notification)
        {
            OSNotificationPayload payload = notification.payload;
            string message = payload.body;

            string guid = message.Substring(0, 37);

            ChatModel chatModel = new ChatModel()
            {
                Oid = Guid.Parse(guid.Trim()),
                ChatMessage = message.Substring(37),
                Time = DateTime.Now,
                IsReceived = true
            };

            MessagingCenter.Send(chatModel, "ReceiveMessage");
            //Dictionary<string, object> additionalData = payload.additionalData;

            //if (additionalData != null)
            //{
            //    if (additionalData.ContainsKey("screenName"))
            //    {
            //        //pushScreenName = Convert.ToString(additionalData["screenName"]);
            //    }
            //    else
            //    {
            //       // pushScreenName = "";
            //    }
            //}
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
