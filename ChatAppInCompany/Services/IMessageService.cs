using System;
using System.Collections.Generic;
using System.Text;

namespace ChatAppInCompany.Services
{
    public interface IMessageService
    {
        void SendMessage(string Message);

        void GetAllUser();

        void LogoutRoom();
    }
}
