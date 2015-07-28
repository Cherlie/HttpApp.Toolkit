using System;
using Windows.ApplicationModel.Background;

namespace HttpApp.Toolkit.AppHelpers
{
    public class BackgroundTaskHelper
    {
        /// <summary>
        /// 注册后台事件
        /// </summary>
        /// <param name="name">后台dll名称</param>
        /// <param name="point">dll入口</param>
        public static async void RegisterBackgroundTask(string name,string point)
        {
            BackgroundExecutionManager.RemoveAccess();
            var backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();
            if (backgroundAccessStatus == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity
                || backgroundAccessStatus == BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity)
            {
                foreach (var task in BackgroundTaskRegistration.AllTasks)
                {
                    task.Value.Unregister(true);
                }

                BackgroundTaskBuilder taskBuilder = new BackgroundTaskBuilder();
                taskBuilder.Name = name;
                taskBuilder.TaskEntryPoint = point;
                taskBuilder.SetTrigger(new TimeTrigger(15, false));
                var registration = taskBuilder.Register();
            }
        }
    }
}
