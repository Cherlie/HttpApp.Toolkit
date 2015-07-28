using System.Threading;

namespace HttpApp.Toolkit.Timers
{
    /// <summary>
    /// 计时器通用类
    /// 作者：梅须白
    /// 时间：2015/5/29
    /// </summary>
    public class DefaultTimer
    {
        private readonly int _period;
        public DefaultTimer(int period)
        {
            _period = period;
        }

        public delegate void TimerEventHandler();

        public event TimerEventHandler UpdateUI;

        private void RaiseTimeCome()
        {
            if (UpdateUI != null)
            {
                UpdateUI();
            }
        }

        private Timer PeriodicTimer = null;

        public void Run()
        {
            if (PeriodicTimer == null)
            {
                PeriodicTimer = new Timer((s) =>
                    {
                        RaiseTimeCome();
                    }, null, 0,_period);
            }
        }

        public void Stop()
        {
            if (PeriodicTimer != null)
            {
                PeriodicTimer.Dispose();
            }
        }
    }
}
