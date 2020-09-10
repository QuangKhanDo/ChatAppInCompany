using ChatAppInCompany.Models;
using ChatAppInCompany.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChatAppInCompany.ViewModels
{
    public class ChatViewModel : INotifyPropertyChanged
    {
        //Constructor
        public ChatViewModel()
        {
            //Set Property
            ChatMessages = new ObservableCollection<ChatModel>();


            //Method
            Generate();



            //Command
            BackCommand = new Command(GetBack);
            SendCommand = new Command(SendMessage);


            //Messaging Center
            MessagingCenter.Subscribe<ChatModel>(this, "ReceiveMessage", (Message) =>
            {
                var myMessage = ChatMessages.FirstOrDefault(x => x.Oid == Message.Oid);
                if (myMessage != null) return;
                ChatMessages.Add(Message);
                MessagingCenter.Send(string.Empty, "NewMessageComing");
            });
        }

        #region Properties
        private OnesignalService service = new OnesignalService();
        //private readonly string[] descriptions = { "Hi, can you tell me the specifications of the Dell Inspiron 5370 laptop?",
        //    " * Processor: Intel Core i5-8250U processor " +
        //    "\n" + " * OS: Pre-loaded Windows 10 with lifetime validity" +
        //    "\n" + " * Display: 13.3-inch FHD (1920 x 1080) LED display" +
        //    "\n" + " * Memory: 8GB DDR RAM with Intel UHD 620 Graphics" +
        //    "\n" + " * Battery: Lithium battery",
        //    "How much battery life does it have with wifi and without?",
        //    "Approximately 5 hours with wifi. About 7 hours without.",
        //};

        private ObservableCollection<ChatModel> _chatMessages;
        public ObservableCollection<ChatModel> ChatMessages
        {
            get { return _chatMessages; }
            set
            {
                _chatMessages = value;
                OnPropertyChanged();
            }
        }

        private string _newMessage;
        public string NewMessage
        {
            get { return _newMessage; }
            set
            {
                _newMessage = value;
                OnPropertyChanged();
            }
        }

        private bool _isExpired;
        public bool IsExpired
        {
            get { return _isExpired; }
            set
            {
                _isExpired = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Methods
        private void Generate()
        {
            var currentTime = DateTime.Now;
            //ChatMessages = new ObservableCollection<ChatModel>
            //{
            //    new ChatModel
            //    {
            //        ChatMessage = this.descriptions[0],
            //        Time = currentTime.AddMinutes(-2517),
            //        IsReceived = true,
            //    },
            //    new ChatModel
            //    {
            //        ChatMessage = this.descriptions[1],
            //        Time = currentTime.AddMinutes(-2408),
            //    },
            //    new ChatModel
            //    {
            //        ChatMessage = this.descriptions[2],
            //        Time = currentTime.AddMinutes(-1103),
            //        IsReceived = true,
            //    },
            //    new ChatModel
            //    {
            //        ChatMessage = this.descriptions[3],
            //        Time = currentTime.AddMinutes(-1006),
            //    },
            //};
        }

        private void SendMessage()
        {
            if (!string.IsNullOrWhiteSpace(NewMessage))
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ChatModel chatModel = new ChatModel
                    {
                        Oid = Guid.NewGuid(),
                        ChatMessage = NewMessage,
                        Time = DateTime.Now,
                        IsReceived = false
                    };
                    ChatMessages.Add(chatModel);
                    
                    DependencyService.Get<IMessageService>().SendMessage(chatModel.Oid.ToString() + " " + NewMessage);
                    NewMessage = string.Empty;
                });

            }
        }

        private async void GetBack()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        #endregion

        #region Commands
        public Command BackCommand { get; }
        public Command SendCommand { get; }
        #endregion

        #region Property Interface
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        ~ChatViewModel()
        {
            MessagingCenter.Unsubscribe<ChatModel>(this, "ReceiveMessage");
        }
    }
}
