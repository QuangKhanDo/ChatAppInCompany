using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ChatAppInCompany.CustomControl
{
    public class BorderlessEditor : Editor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BorderlessEditor"/> class.
        /// </summary>
        public BorderlessEditor()
        {
            this.TextChanged += this.ExtendableEditor_TextChanged;
        }

        #region Methods

        /// <summary>
        /// Invoked when editor text is changed.
        /// </summary>
        /// <param name="sender">The editor</param>
        /// <param name="e">Text changed event args</param>
        private void ExtendableEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.InvalidateMeasure();
        }

        #endregion
    }
}
