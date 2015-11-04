using Microsoft.Phone.Controls;
using Microsoft.Phone.Notification;
using Microsoft.WindowsAzure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;


namespace MyLocalPharmacy.Entities
{
    public class PushNotificationHandler
    {
        #region Private Variables

        private NotificationHub hub;
        string xmlPayLoadTemplate = string.Empty;
        bool _isInitialHandle = false;

        #endregion

        #region Constructors

        public PushNotificationHandler()
        {
            WindowsAzureConfig windowsAzureConfig = this.GetWindowsAzureConfigInfo();
            if (windowsAzureConfig != null)
            {
                    hub = new NotificationHub(windowsAzureConfig.DevHubName, windowsAzureConfig.DevListenAccessConnectionString);
            }
        }

        #endregion

        #region Public Methods
        public async Task SubscribeToPushes(IEnumerable<string> deviceIds) 
        {
            try
            {
                var channel = HttpNotificationChannel.Find("MlpPushChannel");
                if (channel == null)
                {
                    channel = new HttpNotificationChannel("MlpPushChannel");
                    channel.Open();
                    channel.BindToShellToast();
                }
                channel.ShellToastNotificationReceived += channel_ShellToastNotificationReceived;

                if (channel.ChannelUri != null)
                {
                    try
                    {
                        if (!(string.IsNullOrEmpty(App.LoginEmailId) || string.IsNullOrWhiteSpace(App.LoginEmailId)))
                        {
                            await this.Register(deviceIds, channel.ChannelUri.ToString());

                        }
                    }
                    catch (Exception)
                    {

                    }
                }
                channel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(async (o, args) =>
                {
                    try
                    {
                        await this.Register(deviceIds, args.ChannelUri.ToString());
                    }
                    catch (Exception)
                    {

                    }
                    
                });
            }            
            catch(Exception)
            {
               
            }           

           
        }

        private async Task Register(IEnumerable<string> deviceIds, string path)
        {
           
                if (deviceIds != null)
                {
                    try
                    {
                        await hub.RegisterNativeAsync(path,deviceIds);

                    }
                    catch (Microsoft.WindowsAzure.Messaging.RegistrationAuthorizationException)
                    {

                    }
                    catch (Exception)
                    {

                    }

                }
                else
                {
                    try
                    {
                        await hub.RegisterNativeAsync(path);

                    }
                    catch (Microsoft.WindowsAzure.Messaging.RegistrationAuthorizationException)
                    {

                    }
                    catch (Exception)
                    {

                    }

                
            }
           
        }

        void channel_ShellToastNotificationReceived(object sender, NotificationEventArgs e)
        {
         
            if (e.Collection != null && e.Collection.Count > 0)
            {
                var entry = e.Collection.FirstOrDefault().Value;
                var message = e.Collection.ElementAt(1).Value;
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    message = entry + "~~" + message;
                    Uri notificationUrl = new Uri("/View/Notification.xaml?contentVal=" + message, UriKind.RelativeOrAbsolute);
                    PhoneApplicationFrame fram = (Application.Current.RootVisual as PhoneApplicationFrame);
                    if (fram != null)
                        fram.Navigate(notificationUrl);
                });                
            }
        }

        public WindowsAzureConfig GetWindowsAzureConfigInfo()
        {
            try
            {
                WindowsAzureConfig windowsAzureConfig = null;
                windowsAzureConfig = new WindowsAzureConfig()
                {
                    DevHubName = "rx",
                    DevListenAccessConnectionString = "Endpoint=sb://rxsystems.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=cOE4KMCoMdIBVlWjR0eRcjlv6+FakY0OOb2XJIeDxbI=",
                    ProductionHubName = "ProductionHubName",
                    ProductionListenAccessConnectionString = "ProductionListenAccessConnectionString"
                };

                return windowsAzureConfig;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

    }
}
