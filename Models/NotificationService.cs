using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    class NotificationService : INotificationService
    {
        public delegate void CreatedNotificationEventHandler(object o, EventArgs e);
        public event CreatedNotificationEventHandler CreatedNotification;
        public string Message { get; set; }

        public void PopulateTask()
        {
            OnCreatedNotification();
        }

        protected virtual void OnCreatedNotification()
        {
            if (CreatedNotification != null) CreatedNotification(this, new EventArgs());
        }

        public void ShowUserNotification()
        {
            throw new NotImplementedException();
        }
    }
}
