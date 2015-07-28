using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Controls;
using HttpApp_Common;

namespace HttpApp.Toolkit.TileHelper
{
    /// <summary>
    /// 创建磁贴的通用类型
    /// 作者：梅须白
    /// 日期：2015/5/29
    /// </summary>
    public class TileCreator
    {
        private readonly string title;
        private Dictionary<KeyValuePair<string, TileTemplateType>, Grid> gridNameDict;

        /// <summary>
        /// 有参构造，用于SecondaryTile
        /// </summary>
        /// <param name="_title">磁贴实例ID</param>
        public TileCreator(string _title)
        {
            this.title = _title;
        }

        /// <summary>
        /// 无参构造
        /// </summary>
        public TileCreator() { }

        /// <summary>
        /// 利用XAML生成图片，先设置XAML生成图片的用户控件的信息
        /// </summary>
        /// <param name="tileTypeAndName">包含Grid的名称和磁贴类型的键值对</param>
        /// <param name="tileControl">用户控件</param>
        public void Configure(KeyValuePair<string, TileTemplateType> tileTypeAndName, UserControl tileControl)
        {
            if (gridNameDict == null)
            {
                gridNameDict = new Dictionary<KeyValuePair<string, TileTemplateType>, Grid>();
            }
            gridNameDict.Add(tileTypeAndName, tileControl.FindName(tileTypeAndName.Key) as Grid);
        }

        /// <summary>
        /// 固定或更新SecondaryTile
        /// </summary>
        public async void PinSecondaryTile()
        {
            Uri uri = new Uri("ms-appx:///Assets/Square71x71Logo.scale-240.png");
            Windows.UI.StartScreen.SecondaryTile tile = new Windows.UI.StartScreen.SecondaryTile(title, "口袋贵金属", "/MainPage.xaml", uri, Windows.UI.StartScreen.TileSize.Wide310x150);
            tile.VisualElements.Wide310x150Logo = uri;
            tile.DisplayName = " ";

            await RefreshImage();

            bool isPin = await tile.RequestCreateAsync();
            if (isPin)
            {
                var updator = Windows.UI.Notifications.TileUpdateManager.CreateTileUpdaterForSecondaryTile(title);
                this.UpdateTile(updator);
            }
        }

        /// <summary>
        /// 更新或固定应用磁贴
        /// </summary>
        public async void PinTile()
        {
            await RefreshImage();
            var updator = TileUpdateManager.CreateTileUpdaterForApplication();
            this.UpdateTile(updator);
        }

        /// <summary>
        /// 调用工具类生成图片
        /// </summary>
        /// <returns></returns>
        private async Task RefreshImage()
        {
            RenderImage randerImage = new RenderImage();
            foreach (var item in gridNameDict)
            {
                await randerImage.RenderImageToFileAsync(item.Value, item.Key.Key + this.title??"" + ".png");
            }
        }

        /// <summary>
        /// 刷新磁贴
        /// </summary>
        /// <param name="updator">传入更新类型参数：应用磁贴，二级磁贴</param>
        private void UpdateTile(TileUpdater updator)
        {
            updator.Clear();
            updator.EnableNotificationQueue(true);

            XmlDocument tile = new XmlDocument();
            foreach (var item in gridNameDict)
            {
                tile = Windows.UI.Notifications.TileUpdateManager.GetTemplateContent(item.Key.Value);
                var imageElement = tile.GetElementsByTagName("image")[0];
                imageElement.Attributes.GetNamedItem("src").NodeValue = "ms-appdata:///local/" + item.Key.Key + this.title??"" + ".png";
                var notification = new Windows.UI.Notifications.TileNotification(tile);
                updator.Update(notification);
            }
        }
    }
}
