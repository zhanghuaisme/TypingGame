/****************************
 * 项目名：指法练习游戏
 * 创建者：张华
 * 创建日：2010/03/28
 */

/*变更历史
 * 
 */

using System.Windows;
using System.Windows.Controls;

namespace TypingGame
{
    /// <summary>
    /// 自定义控件，消息控件
    /// </summary>
    public partial class MyMessage : UserControl
    {
        #region 添加的属性
        private MyMsgEntity myMsgClass = null;

        /// <summary>
        /// 消息对象属性
        /// </summary>
        public MyMsgEntity MyMsgClass
        {
            get { return myMsgClass; }
            set { 
                myMsgClass = value;
                this.ImgMessage.Source = myMsgClass.Image;
                this.LblDispScore.Content = myMsgClass.ScoreMessage;
                this.LblDispMsg.Content = myMsgClass.DispMessage;
            }
        }
        #endregion 

        #region 构造方法
        public MyMessage()
        {
            InitializeComponent();
        }
        #endregion 

        #region 控件加载事件
        /// <summary>
        /// 控件加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (myMsgClass != null)
            {
                this.ImgMessage.Source = myMsgClass.Image;
                this.LblDispScore.Content = myMsgClass.ScoreMessage;
                this.LblDispMsg.Content = myMsgClass.DispMessage;
            }
        }
        #endregion 
    }
}
