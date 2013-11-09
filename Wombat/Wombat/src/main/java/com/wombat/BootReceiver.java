package com.wombat;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.util.Log;

public class BootReceiver extends BroadcastReceiver
{
    @Override
    public void onReceive(Context context, Intent intent)
    {
        Log.i(BootReceiver.class.getName(), "onReceive");

        PageCheckerScheduler.schedulePageChecking(context);
    }
}
