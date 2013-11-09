package com.wombat;

import android.app.AlarmManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.util.Log;

public class PageCheckerScheduler
{
    public static void schedulePageChecking(Context context)
    {
        Intent checkPage = new Intent(context, PageCheckerReceiver.class);
        context.sendBroadcast(checkPage);

        PendingIntent recurringCheckPage = PendingIntent.getBroadcast(context, 0, checkPage, PendingIntent.FLAG_CANCEL_CURRENT);
        AlarmManager alarms = (AlarmManager)context.getSystemService(Context.ALARM_SERVICE);

        alarms.cancel(recurringCheckPage);
        alarms.setInexactRepeating(AlarmManager.RTC_WAKEUP, System.currentTimeMillis(), AlarmManager.INTERVAL_HALF_HOUR, recurringCheckPage);

        Log.i(PageCheckerScheduler.class.getName(), "schedulePageChecking()");
    }
}
