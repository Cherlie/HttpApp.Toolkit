using System;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI.Xaml;

namespace HttpApp.Toolkit.Utilitys
{
    public class CheckPixel
    {
        /// <summary>
        /// 检查是否是1080p设备
        /// </summary>
        /// <returns></returns>
        public static bool Is1080pScreen()
        {
            // Window Size
            Rect bounds = Window.Current.Bounds;
            // RawPixelsPerViewPixel
            double dpiRatio = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            // ScreenResolution
            double resolutionH = Math.Round(bounds.Height * dpiRatio);
            double resolutionW = Math.Round(bounds.Width * dpiRatio);
            if (resolutionW == 1080 && resolutionH == 1920)
                return true;
            else
                return false;
        }
    }
}
