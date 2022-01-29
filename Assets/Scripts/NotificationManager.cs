using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AndroidNotificationCenter.CancelAllNotifications();
        var c01 = new AndroidNotificationChannel()
        {
            Id = "kevin_channel_01",
            Name = "Kevin 01 Channel",
            Importance = Importance.High,
            Description = "Generic notifications01",
        };
        var c03 = new AndroidNotificationChannel()
        {
            Id = "kevin_channel_dailygift",
            Name = "Kevin 02 Channel",
            Importance = Importance.High,
            Description = "Generic notifications02",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(c01);
        AndroidNotificationCenter.RegisterNotificationChannel(c03);
      
        if (LevelManager.lang == LevelManager.Language.pl)
        {
            MakeNotification(c01.Id, "Kevin potrzebuje twojej pomocy!", "Minęło już trochę czasu od naszej ostatniej przygody!", 1);
        }
        else if (LevelManager.lang == LevelManager.Language.de)
        {
            MakeNotification(c01.Id, "Kevin braucht deine Hilfe!", "Es ist eine Weile her seit unserem letzten Abenteuer!", 1);
        }
        else
        {
            MakeNotification(c01.Id, "Kevin needs your help!", "It's been a while since our last adventure!", 1);
        }
        if (DailyReward.GiftCollected < 5 && DailyReward.GiftCollected > 0)
        {
            MakeNotification(c03.Id, "Darmowy prezent!", "Twój codzienny prezent jest już do odbioru!", 3);
        }
        else if (DailyReward.GiftCollected >= 5)
        {
            AndroidNotificationCenter.DeleteNotificationChannel("kevin_channel_dailygift");
        }
    }

    void MakeNotification(String channel, String title, String text, int type)
    {
        
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        if (type < 3)
        {
            notification.FireTime = System.DateTime.Now.AddHours(type*24);
        }
        else
        {
            DateTime dtime = (DateTime) DailyReward.timeOfLastCollect;
            notification.FireTime = dtime.AddHours(24);
        }

        var identifier = AndroidNotificationCenter.SendNotification(notification, channel);
        
        if ( AndroidNotificationCenter.CheckScheduledNotificationStatus(identifier) == NotificationStatus.Scheduled)
        {
           AndroidNotificationCenter.UpdateScheduledNotification(identifier, notification, channel);
        }
        else if (AndroidNotificationCenter.CheckScheduledNotificationStatus(identifier)  == NotificationStatus.Delivered)
        {
            AndroidNotificationCenter.CancelNotification(identifier);
        }
        else if ( AndroidNotificationCenter.CheckScheduledNotificationStatus(identifier) == NotificationStatus.Unknown)
        {
            AndroidNotificationCenter.SendNotification(notification, channel);
        }
    }

}
