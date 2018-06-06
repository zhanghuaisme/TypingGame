/****************************
 * 项目名：指法练习游戏
 * 创建者：张华
 * 创建日：2010/03/28
 */

/*变更历史
 * 
 */
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace TypingGame
{
    /// <summary>
    /// 图像管理器
    /// </summary>
    public class ImageManager
    {
        /// <summary>
        /// 生成图像
        /// </summary>
        /// <param name="url">图片文件的相对路径</param>
        /// <returns>BitMap图像</returns>
        public static BitmapImage LoadBitMapImage(string url)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(Directory.GetCurrentDirectory() +
                                 System.IO.Path.DirectorySeparatorChar +
                                 url, UriKind.Absolute);
            bitmapImage.DecodePixelWidth = 50;
            bitmapImage.EndInit();
            return bitmapImage;
        }
    }
}
