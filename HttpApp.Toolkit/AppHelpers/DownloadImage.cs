using System;
using System.Threading.Tasks;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;

namespace HttpApp.Toolkit.AppHelpers
{
    /// <summary>
    /// 简单从后台下载图片，不能显示进度
    /// </summary>
    public sealed class DownloadImage
    {
        private async static Task<StorageFile> SaveAsync(
              Uri fileUri,
              StorageFolder folder,
              string fileName)
        {
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            var downloader = new BackgroundDownloader();
            var download = downloader.CreateDownload(
                fileUri,
                file);

            var res = await download.StartAsync();

            return file;
        }

        /// <summary>
        /// 保存图片为jpeg格式，保存路径是本地存储
        /// </summary>
        /// <param name="url">图片地址</param>
        /// <param name="imageName">图片名称</param>
        public static async void SaveImage(string url, string imageName)
        {
            try
            {
                StorageFolder applicationFolder = ApplicationData.Current.LocalFolder;
                await SaveAsync(new Uri(url), applicationFolder, imageName + ".jpg");
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
