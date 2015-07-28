using System;
using Windows.UI.ViewManagement;

namespace HttpApp.Toolkit.AppHelpers
{
    public class UIHelper
    {
        /// <summary>
        /// 隐藏WP8.1系统状态栏
        /// </summary>
        public async static void HideSystemTray()
        {
            StatusBar statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            // Hide the status bar
            await statusBar.HideAsync();
        }
    }
}
