using Plugin.LocalNotification;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace FinanceApp.Services
{
    
    public class NotificationService
    {
        static public bool IsNotificationAllowed = true;
 
        static public async Task ShowNotification(string title, string message)
        {
            if (!IsNotificationAllowed)
                return;

            if (await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
            {
                await LocalNotificationCenter.Current.RequestNotificationPermission();
            }
        
        var notification = new NotificationRequest
            {
                Title = title,
                Description = message,
                NotificationId = new Random().Next(1, 1000),
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(10)
                }
            };
            try
            {
                await LocalNotificationCenter.Current.Show(notification);
            }
            catch(Exception ex)
            {
                return;   
            }
        }

    } 
}

