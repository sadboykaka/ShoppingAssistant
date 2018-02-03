﻿using System;

using Android.Content;
using Android.OS;
using Android.Util;
using Xamarin.Forms;

namespace ShoppingAssistant.Droid.Services
{
    /// <summary>
    /// A Broadcast Receiver implementation that checks for shopping lists if running in the background
    /// </summary>
    [BroadcastReceiver]
    public class BackgroundReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            try
            {
                PowerManager pm = (PowerManager) context.GetSystemService(Context.PowerService);
                PowerManager.WakeLock wakeLock = pm.NewWakeLock(WakeLockFlags.Partial, "BackgroundReceiver");
                wakeLock.Acquire();
                
                Log.Debug("BackgroundReceiver", "Woken by alarm manager");
                
                App.MasterController.ShoppingListController.GetShoppingListModelsAsync();
                App.MasterController.LocationController.GetNearbyLocations();

                Log.Debug("BackgroundReceiver", "Registering with alarm manager");
                ((MainActivity)Forms.Context).RegisterAlarmManager();

                wakeLock.Release();
            }
            catch (Exception ex)
            {
                App.Log.Error("OnReceive", ex.StackTrace);
            }
        }
    }
}