using ChatAppInCompany.Models;
using ChatAppInCompany.Views.Template;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatAppInCompany.Views.Chat.Selector
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class MessageDataTemplateSelector : DataTemplateSelector
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageDataTemplateSelector" /> class.
        /// </summary>
        public MessageDataTemplateSelector()
        {
            this.IncomingTextTemplate = new DataTemplate(typeof(IncomingTextTemplate));
            this.OutgoingTextTemplate = new DataTemplate(typeof(OutgoingTextTemplate));
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the incoming text template.
        /// </summary>
        public DataTemplate IncomingTextTemplate { get; set; }

        /// <summary>
        /// Gets or sets the outgoing text template.
        /// </summary>
        public DataTemplate OutgoingTextTemplate { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the incoming or outgoing text template.
        /// </summary>
        /// <param name="item">The item</param>
        /// <param name="container">The bindable object</param>
        /// <returns>Returns the data template</returns>
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (((ChatModel)item).IsReceived)
            {
                return this.IncomingTextTemplate;
                
            }
            else
            {
                return this.OutgoingTextTemplate;
            }
        }

        #endregion
    }
}
