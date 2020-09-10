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
using ChatAppInCompany.Android.Renderers;
using ChatAppInCompany.CustomControl;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Application = Android.App.Application;

[assembly: ExportRenderer(typeof(BorderlessEditor), typeof(BorderlessEditorRenderer))]

namespace ChatAppInCompany.Android.Renderers
{
    public class BorderlessEditorRenderer : EditorRenderer
    {
        #region Constructor

        public BorderlessEditorRenderer() : base(Application.Context)
        {
        }

        #endregion

        #region Methods
        /// <summary>
        /// Used to set the transparent color for editor control background property.
        /// </summary>
        /// <param name="e">The editor</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null)
            {
                //Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
                this.Control.SetBackground(null);
            }
        }

        #endregion
    }
}