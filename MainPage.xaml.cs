using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Notifications.Management;
using Windows.UI.Notifications;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Hao123
{
    public sealed partial class MainPage : Page
    {
        private UserNotificationListener _listener;
        public Client ws;
        public List<Msg> Msgs { get; set; } = new List<Msg>();
        public MainPage()
        {
            InitializeComponent();
            
            _listener = UserNotificationListener.Current;
            
            ws = new Client();
            ws.Start();
            
            try
            {
                _listener.NotificationChanged += Listener_NotificationChanged;
                System.Diagnostics.Trace.WriteLine("Event handler added successfully.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"Error adding event handler: {ex.Message}");
            }
            
            InitializeListener();
        }

        private async void InitializeListener()
        {
            UserNotificationListenerAccessStatus accessStatus = await _listener.RequestAccessAsync();

            switch (accessStatus)
            {
                case UserNotificationListenerAccessStatus.Allowed:
                    System.Diagnostics.Trace.WriteLine("Access granted. Listening for notifications...");
                    break;

                case UserNotificationListenerAccessStatus.Denied:
                    System.Diagnostics.Trace.WriteLine("Access denied. Please allow access in Windows settings.");
                    break;

                case UserNotificationListenerAccessStatus.Unspecified:
                    System.Diagnostics.Trace.WriteLine("Access status unspecified. Try again later.");
                    break;
            }
        }


        // 每单次消息更新，会根据未处理的消息队列做对应次数轮次的调用
        private async void Listener_NotificationChanged(UserNotificationListener sender, UserNotificationChangedEventArgs args)
        {
            System.Diagnostics.Trace.WriteLine("New notification received!");

            var notifications = await sender.GetNotificationsAsync(NotificationKinds.Toast);
            var count = notifications.Count;
            if (count <= 0) 
            {
                return;
            }

            while (count-- > 0)
            {
                var notification = notifications[count];

                NotificationBinding toastBinding = notification.Notification.Visual.GetBinding(KnownNotificationBindings.ToastGeneric);

                if (toastBinding != null)
                {
                    IReadOnlyList<AdaptiveNotificationText> textElements = toastBinding.GetTextElements();
                    Msgs.Add(new Msg()
                    {
                        Name = textElements.FirstOrDefault()?.Text,
                        Content = textElements[1].Text,
                        Time = notification.CreationTime.DateTime,
                        Id = notification.Id,
                    });
                    ws.Send(textElements.FirstOrDefault()?.Text + ":     " + textElements[1].Text);
                    System.Diagnostics.Trace.WriteLine(textElements.FirstOrDefault()?.Text + textElements[1].Text);
                }
            }
            _listener.ClearNotifications();
        }
    }
    public class Msg
    {
        public System.DateTime Time;

        public uint Id;

        public string Name;

        public string Content;
    }

    public class Client
    {
        public WebSocket ws;
        public void Start()
        {
            ws = new WebSocket("ws://localhost:4396");
            ws.Connect();
            if (ws.IsAlive)
            {
                System.Diagnostics.Trace.WriteLine("Hello!");
            }
        }

        public void Stop()
        {
            ws.Close();
        }

        public void Send (string Content)
        {
                ws.Send(Content);
        }

    }

}
