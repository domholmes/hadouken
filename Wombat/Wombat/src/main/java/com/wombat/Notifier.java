package com.wombat;

import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.media.RingtoneManager;
import android.net.Uri;
import android.support.v4.app.NotificationCompat;

public class Notifier
{
    public void Notify(Context context, String message)
    {
        Notification notification = createNotification(context, message);

        NotificationManager notificationManager = (NotificationManager) context.getSystemService(Context.NOTIFICATION_SERVICE);

        notificationManager.notify(0, notification);
    }

    private Notification createNotification(Context context, String message)
    {
        Uri sound = RingtoneManager.getDefaultUri(RingtoneManager.TYPE_NOTIFICATION);

        NotificationCompat.Builder builder =
                new NotificationCompat.Builder(context)
                        .setAutoCancel(true)
                        .setOnlyAlertOnce(true)
                        .setSmallIcon(R.drawable.ic_launcher)
                        .setContentTitle("Wombat")
                        .setContentText(message)
                        .setDefaults(Notification.DEFAULT_ALL);// requires VIBRATE permission

        return builder.build();
    }
}

