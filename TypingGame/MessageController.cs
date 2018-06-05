/****************************
 * 项目名：指法练习游戏
 * 创建者：张华
 * 创建日：2010/03/28
 */

/*变更历史
 * 
 */

using System.Windows;
using System.Windows.Media.Imaging;

namespace TypingGame
{
    /// <summary>
    /// 消息控制器
    /// </summary>
    public class MessageController
    {
        #region 变量
        private MyMessage myMessage = null;
        private BitmapImage bitImgShy;
        private BitmapImage bitImgGood;
        private BitmapImage bitImgVeryGood;
        private BitmapImage bitImgGodLike;
        #endregion 

        #region 构造方法
        /// <summary>
        /// 构造消息控制器
        /// </summary>
        public MessageController()
        {
            LoadImage();
        }

        /// <summary>
        /// 根据消息控件生成控制器
        /// </summary>
        /// <param name="myMessage"></param>
        public MessageController(MyMessage myMessage)
        {
            this.myMessage = myMessage;
            LoadImage();
        }

        /// <summary>
        /// 加载消息图像
        /// </summary>
        private void LoadImage()
        {
            bitImgShy = ImageManager.LoadBitMapImage("Image\\shy.gif");
            bitImgGood = ImageManager.LoadBitMapImage("Image\\good.gif");
            bitImgVeryGood = ImageManager.LoadBitMapImage("Image\\veryGood.gif");
            bitImgGodLike = ImageManager.LoadBitMapImage("Image\\godLike.gif");
        }
        #endregion 

        #region 设置消息内容和大小
        /// <summary>
        /// 获取消息对象
        /// </summary>
        /// <param name="type">消息类别</param>
        /// <param name="score">成绩</param>
        /// <returns>消息对象</returns>
        private MyMsgEntity GetMessage(Constant.MessageType type, int score)
        {
            BitmapImage imgMsg = null;
            string dispMsg = string.Empty;
            string scoreMsg = Constant.MsgScore;
            switch (type)
            {
                case Constant.MessageType.Shy:
                    {
                        imgMsg = bitImgShy;
                        dispMsg = Constant.MsgSaySorry;
                        break;
                    };
                case Constant.MessageType.Good:
                    {
                        imgMsg = bitImgGood;
                        dispMsg = Constant.MsgCongratulation;
                        break;
                    }
                case Constant.MessageType.VeryGood:
                    {
                        imgMsg = bitImgVeryGood;
                        dispMsg =  Constant.MsgPraise;
                        break;
                    }
                case Constant.MessageType.GodLike:
                    {
                        imgMsg = bitImgGodLike;
                        dispMsg =  Constant.MsgTalent;
                        break;
                    }
                case Constant.MessageType.MyGod:
                    {
                        imgMsg = bitImgGodLike;
                        dispMsg = Constant.MsgMyGod;
                        break;
                    }
                default:
                    {
                        //imgMsg = bitImgShy;
                        //dispMsg = (string)this.FindResource("");
                        //scoreMsg = (string)this.FindResource("");
                        break;
                    }
            }
            return new MyMsgEntity(imgMsg, scoreMsg + score, dispMsg);
        }

        /// <summary>
        /// 设置消息内容和大小
        /// </summary>
        /// <param name="myMessage">消息对象</param>
        /// <param name="level">游戏级别</param>
        /// <param name="score">游戏得分</param>
        public void SetMessage(MyMessage myMessage, int level, int score)
        {
            this.myMessage = myMessage;
            Constant.MessageType type = Constant.MessageType.Shy;
            if (score > 80)
            {
                switch (level)
                {
                    case 3:
                    case 4:
                        {
                            type = Constant.MessageType.Good;
                            break;
                        }
                    case 5:
                        {
                            type = Constant.MessageType.VeryGood;
                            break;
                        }
                    case 6:
                        {
                            type = Constant.MessageType.GodLike;
                            break;
                        }
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        {
                            type = Constant.MessageType.MyGod;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            myMessage.MyMsgClass = GetMessage(type, score * level);//设置消息内容
            myMessage.Height = 84;//消息高度
            myMessage.Width = 450;//消息宽度
        }
        #endregion 

        #region 设置是否显示消息
        /// <summary>
        /// 设置是否显示消息
        /// </summary>
        /// <param name="visibility">显示或者隐藏</param>
        private void SetMsgVisibility(Visibility visibility)
        {
            if (myMessage != null)
            {
                myMessage.Visibility = visibility;
            }
        }

        /// <summary>
        /// 显示消息
        /// </summary>
        public void ShowMessage()
        {
            if (myMessage != null)
            {
                SetMsgVisibility(Visibility.Visible);
            }
        }

        /// <summary>
        /// 隐藏消息
        /// </summary>
        public void HideMessage()
        {
            if (myMessage != null)
            {
                SetMsgVisibility(Visibility.Collapsed);
            }
        }
        #endregion 
    }
}
