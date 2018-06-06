/****************************
 * 项目名：指法练习游戏
 * 创建者：张华
 * 创建日：2010/04/16
 */

/*变更历史
 * 
 */

using System;
using System.IO;
using System.Windows.Media.Imaging;
using NUnit.Framework;
using TypingGame;

namespace TypingGameTest
{
    [TestFixture]
    public class ImageManagerTest
    {
        #region 变量
        private BitmapImage bitmapImage = new BitmapImage();
        string url = "Image\\bom.ico";
        #endregion 

        #region SetUp
        /// <summary>
        /// 初始化
        /// </summary>
        [SetUp]
        public void Init()
        {
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(Directory.GetCurrentDirectory() +
                                 System.IO.Path.DirectorySeparatorChar +
                                 url, UriKind.Absolute);
            bitmapImage.DecodePixelWidth = 50;
            bitmapImage.EndInit();
        }
        #endregion 

        #region 加载图像测试
        /// <summary>
        /// 加载图像测试
        /// </summary>
        [Test]
        public void LoadBitMapImageTest()
        {
            BitmapImage bitmapImageTest = ImageManager.LoadBitMapImage(url);
            Assert.AreEqual(bitmapImage.UriSource, bitmapImageTest.UriSource);
        }
        #endregion 
    }
}
