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
    public class PlayerControllerTest
    {
        #region 变量

        private PlayerController playerCtl = new PlayerController();

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

        #region 播放声音测试
        /// <summary>
        /// 播放声音测试
        /// </summary>
        [Test]
        public void PlayTest()
        {
            playerCtl.Play(Constant.SoundType.Bom);//播放声音
            playerCtl.Play(Constant.SoundType.ButtonClick);//播放声音
            playerCtl.Play(Constant.SoundType.ButtonEnter);//播放声音
            playerCtl.Play(Constant.SoundType.Coin);//播放声音
            playerCtl.Play(Constant.SoundType.Open);//播放声音
            playerCtl.Play(Constant.SoundType.PressError);//播放声音
        }
        #endregion 
    }
}
