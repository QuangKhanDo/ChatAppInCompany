﻿using ChatAppInCompany.Models;
using Firebase.Database;
using Firebase.Database.Query;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppInCompany.FirebaseHelper
{
    public class MyFirebaseHelper
    {
        #region Firebase Client
        readonly FirebaseClient client = new FirebaseClient("https://naanchatroom.firebaseio.com/");
        //readonly FirebaseStorage
        #endregion

        #region Function
        /// <summary>
        /// Thêm người dùng vào phòng chat
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task AddUserToRoomChat(User user)
        {
            await CrossCloudFirestore.Current.Instance
                         .GetCollection("9221")
                         .GetDocument(App.MYUID.ToString())
                         .SetDataAsync(user);
        }

        /// <summary>
        /// Lấy tất cả người dùng trong phòng chat
        /// </summary>
        /// <returns></returns>
        public async Task GetAllUserInRoomChat()
        {
            var allUser = await CrossCloudFirestore.Current.Instance
                                     .GetCollection("9221")
                                     .GetDocumentsAsync();
            var convertAllUser = allUser.ToObjects<User>();
            App.UserInRoom = convertAllUser.ToList();
            //foreach (var user in convertAllUser)
            //{
            //    var oldUser = App.UserInRoom.FirstOrDefault(x => x.Oid == user.Oid);
                
            //    if (oldUser == null) App.UserInRoom.Add(user);
            //}
        }
        
        /// <summary>
        /// Xóa người dùng khỏi phòng chat
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task RemoveUserFromRoomChat()
        {
            await CrossCloudFirestore.Current.Instance
                                     .GetCollection("9221")
                                     .GetDocument(App.MYUID.ToString())
                                     .DeleteDocumentAsync();
        }
        #endregion
    }
}
