/****************************
 * 项目名：指法练习游戏
 * 创建者：张华
 * 创建日：2010/04/16
 */

/*变更历史
 * 
 */

using System;
using NUnit.Framework;
using TypingGame;

namespace TypingGameTest
{
    [TestFixture]
    public class MessageControllerTest
    {
        #region 变量

        private MessageController msgCtl = new MessageController();

        #endregion

        #region SetUp
        /// <summary>
        /// 初始化
        /// </summary>
        [SetUp]
        public void Init()
        {

        }
        #endregion

        #region 消息测试
        /// <summary>
        /// 设置消息内容和大小测试
        /// </summary>
        [Test]
        public void SetMessageTest()
        {
            //msgCtl.SetMessage();
        }

        /// <summary>
        /// 设显示消息测试
        /// </summary>
        [Test]
        public void ShowMessageTest()
        {
            msgCtl.ShowMessage();
        }

        /// <summary>
        /// 隐藏消息测试
        /// </summary>
        [Test]
        public void HideMessageTest()
        {
            msgCtl.HideMessage();
        }
        #endregion 
    }
}
