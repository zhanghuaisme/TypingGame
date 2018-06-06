/****************************
 * 项目名：指法练习游戏
 * 创建者：张华
 * 创建日：2010/03/28
 */

/*变更历史
 * 
 */

using System.Windows.Media.Imaging;

namespace TypingGame
{
    /// <summary>
    /// 消息类
    /// </summary>
    public class MyMsgEntity
    {
        #region 属性
        private BitmapImage image = null;
        private string scoreMessage = string.Empty;
        private string dispMessage = string.Empty;

        /// <summary>
        /// 得分说明
        /// </summary>
        public string ScoreMessage
        {
            get { return scoreMessage; }
            set { scoreMessage = value; }
        }

        /// <summary>
        /// 显示图像
        /// </summary>
        public BitmapImage Image
        {
            get { return image; }
            set { image = value; }
        }

        /// <summary>
        /// 评论
        /// </summary>
        public string DispMessage
        {
            get { return dispMessage; }
            set { dispMessage = value; }
        }

        #endregion
        
        #region 构造方法
        public MyMsgEntity()
        {

        }

        public MyMsgEntity(BitmapImage image, string dispMessage, string scoreMessage)
        {
            this.image = image;
            this.dispMessage = dispMessage;
            this.scoreMessage = scoreMessage;
        }
        #endregion
    }
}
