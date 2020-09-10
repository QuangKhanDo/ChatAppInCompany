using Com.OneSignal;
using Com.OneSignal.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppInCompany.Services
{
    public class OnesignalService
    {
        #region Properties
        private static string oneSignalDebugMessage;
        #endregion


        public async Task<bool> SendMessageToRoomChat(string Message)
        {
            //// Just an example userId, use your own or get it the devices by calling OneSignal.GetIdsAvailable
            //string userId = "b2f7f966-d8cc-11e4-bed1-df8f05be55ba";

            //var notification = new Dictionary<string, object>();
            //notification["contents"] = new Dictionary<string, string>() { { "en", Message } };

            //notification["included_segments"] = new List<string>() { "All" };
            //// Example of scheduling a notification in the future.
            ////notification["send_after"] = System.DateTime.Now.ToUniversalTime().AddSeconds(30).ToString("U");

            //OneSignal.Current.PostNotification(notification, (responseSuccess) =>
            //{
            //    oneSignalDebugMessage = "Notification posted successful! Delayed by about 30 secounds to give you time to press the home button to see a notification vs an in-app alert.\n" + Json.Serialize(responseSuccess);
            //}, (responseFailure) =>
            //{
            //    oneSignalDebugMessage = "Notification failed to post:\n" + Json.Serialize(responseFailure);
            //});

            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", "Basic OTU3ZGE4MjQtOTUwYy00Mjg1LWEzNjUtODc4NDMxYmRlMjFh");
            //var contentMessage = string.Format("\"contents\": {\"en\": \"{0}\"},", Message);


            byte[] byteArray = Encoding.UTF8.GetBytes("{"
                                                    + "\"app_id\": \"44b0e932-0b17-4b6b-b1d6-5d61f919b8f9\","
                                                    + "\"contents\": {\"en\": \"" + Message + "\"},"
                                                    + "\"filters\": [{\"field\": \"tag\", \"key\": \"Room\", \"relation\": \"=\", \"value\": \"9221\"}]}");
            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }

            System.Diagnostics.Debug.WriteLine(responseContent);

            return true;
        }
    }
}
