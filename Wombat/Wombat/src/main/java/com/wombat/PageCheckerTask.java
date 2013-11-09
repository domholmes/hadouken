package com.wombat;

import android.content.Context;
import android.os.AsyncTask;
import android.util.Log;

import org.apache.http.HttpStatus;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.Date;

public class PageCheckerTask extends AsyncTask<Void, Void, Void>
{
    private static final String ITEM_NAME = "Nexus 5 cover";
    private final String URI = "https://play.google.com/store/devices/details/Nexus_5_Bumper_Case_Bright_Red?id=nexus_5_bumper_case_red&hl=en_GB";
    private final String SEARCH_TOKEN = "Coming soon";
    private final Context context;

    public PageCheckerTask(Context context)
    {
        this.context = context;
    }

    @Override
    protected Void doInBackground(Void... params)
    {
        Log.i(this.getClass().getName(), "Checking page");

        String okResponse = getServerOkResponse();

        if(okResponse != null)
        {
            boolean pageContainsToken = okResponse.contains(SEARCH_TOKEN);

            Log.i(this.getClass().getName(), "Page contains token:" + String.valueOf(pageContainsToken));

            if(!pageContainsToken)
            {
                sendAlert();
            }
        }

        return null;
    }

    private void sendAlert()
    {
        new Notifier().Notify(this.context, ITEM_NAME + " is here!");
    }

    private String getServerOkResponse()
    {
        StringBuffer response = null;

        try
        {
            URL obj = new URL(URI);
            HttpURLConnection con = (HttpURLConnection) obj.openConnection();

            int responseCode = con.getResponseCode();

            Log.i(this.getClass().getName(), responseCode+" code received");

            if(responseCode == HttpStatus.SC_OK)
            {
                BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(con.getInputStream()));
                String inputLine;
                response = new StringBuffer();

                while ((inputLine = bufferedReader.readLine()) != null)
                {
                    response.append(inputLine);
                }
                bufferedReader.close();
            }

        } catch (IOException e)
        {
            e.printStackTrace();
        }

        return response.toString();
    }
}