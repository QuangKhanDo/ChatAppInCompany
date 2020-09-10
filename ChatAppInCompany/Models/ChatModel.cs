using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ChatAppInCompany.Models
{
    public class ChatModel : INotifyPropertyChanged
    {
        private string _chatMessage;
        private DateTime _time;

        public Guid Oid { get; set; }
        
        /// <summary>
        ///  Nội dung tin nhắn
        /// </summary>
        public string ChatMessage
        {
            get { return _chatMessage; }
            set
            {
                _chatMessage = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Thời gian nhắn
        /// </summary>
        public DateTime Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Kiểm tra tin nhắn thuộc trạng thái "Nhận" hoặc "Gửi"
        /// </summary>
        public bool IsReceived { get; set; }
        #region Property Interface
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
