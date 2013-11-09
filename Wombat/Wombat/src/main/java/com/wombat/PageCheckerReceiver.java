package com.wombat;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.util.Log;

public class PageCheckerReceiver extends BroadcastReceiver
{
    @Override
    public void onReceive(Context context, Intent intent)
    {
        Log.i(PageCheckerReceiver.class.getName(), "onReceive()");

        new PageCheckerTask(context).execute();
    }
}
