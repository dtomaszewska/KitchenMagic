using System;
using UIKit;

using KitchenMagic.Interfaces;
using static KitchenMagic.iOS.Helpers.Utils;

namespace KitchenMagic.iOS.Helpers
{
    public class MessageDialog : IMessageDialog
    {

        public void SendMessage(string message, string title = null)
        {
            EnsureInvokedOnMainThread(() =>
            {
                var alertView = new UIAlertView(title ?? string.Empty, message, null, "OK");
                alertView.Show();
            });
        }


        public void SendToast(string message)
        {
            SendMessage(message);
        }

        public void SendConfirmation(string message, string title, Action<bool> confirmationAction)
        {
            EnsureInvokedOnMainThread(() =>
            {
                var alertView = new UIAlertView(title ?? string.Empty, message, null, "OK", "Cancel");
                alertView.Clicked += (sender, e) =>
                {
                    confirmationAction(e.ButtonIndex == 0);
                };
                alertView.Show();
            });
        }
    }
}
