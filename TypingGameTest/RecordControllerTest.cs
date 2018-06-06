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
    public class RecordControllerTest
    {
        #region 变量

        private RecordController recordCtl = new RecordController();
        private int score = 90;
        private int level = 9;
        private string name = "zhanghua";

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

        #region 保存记录测试(涉及文件操作，要考虑很多Case)
        /// <summary>
        /// 保存记录测试
        /// </summary>
        [Test]
        public void PlayTest()
        {
            recordCtl.SaveScore(score, level, name);
        }
        #endregion 
    }
}
