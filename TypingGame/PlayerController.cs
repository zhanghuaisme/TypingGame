/****************************
 * 项目名：指法练习游戏
 * 创建者：张华
 * 创建日：2010/04/14
 */

/* 变更历史
 */

using System;
using System.IO;
using System.Media;

namespace TypingGame
{
    public class PlayerController
    {
        #region 变量 声音播放器
        private SoundPlayer playerBom = null;//声音播放器,地面爆炸
        private SoundPlayer playerCoin = null;//声音播放器，空中得分
        private SoundPlayer playerButtonEnter = null;//声音播放器，鼠标进入
        private SoundPlayer playerButtonClick = null;//声音播放器，鼠标点击
        private SoundPlayer playerPressError = null;//声音播放器，按键错误
        private SoundPlayer playerOpen = null;//声音播放器，按键错误
        #endregion 

        #region 构造函数 初始化声音播放器
        /// <summary>
        /// 构造并初始化声音播放器
        /// </summary>
        public PlayerController()
        {
            InitializePlayer();
        }
        #endregion 

        #region 开放对外接口 播放声音
        /// <summary>
        /// 播放声音
        /// </summary>
        /// <param name="soundType">声音类型</param>
        public void Play(Constant.SoundType soundType)
        {
            switch (soundType)
            {
                case Constant.SoundType.Bom:
                    PlaySound(playerBom);
                    break;
                case Constant.SoundType.Coin:
                    PlaySound(playerCoin);
                    break;
                case Constant.SoundType.ButtonEnter:
                    PlaySound(playerButtonEnter);
                    break;
                case Constant.SoundType.ButtonClick:
                    PlaySound(playerButtonClick);
                    break;
                case Constant.SoundType.PressError:
                    PlaySound(playerPressError);
                    break;
                case Constant.SoundType.Open:
                    PlaySound(playerOpen);
                    break;
                default:
                    throw new Exception();
            }
        }

        /// <summary>
        /// 音效播放
        /// </summary>
        /// <param name="player">播放器</param>
        private void PlaySound(SoundPlayer player)
        {
            if (player != null)
            {
                player.Play();
            }
        }
        #endregion 

        #region 初始化声音播放器
        /// <summary>
        /// 初始化声音播放器
        /// </summary>
        private void InitializePlayer()
        {
            playerCoin = GetPlayer(Directory.GetCurrentDirectory() +
                                 System.IO.Path.DirectorySeparatorChar + "Sound\\coin.WAV");
            playerBom = GetPlayer(Directory.GetCurrentDirectory() +
                                 System.IO.Path.DirectorySeparatorChar + "Sound\\Bomb.WAV");

            playerButtonEnter = GetPlayer(Directory.GetCurrentDirectory() +
                                 System.IO.Path.DirectorySeparatorChar + "Sound\\ButtonEnter.WAV");
            playerButtonClick = GetPlayer(Directory.GetCurrentDirectory() +
                                 System.IO.Path.DirectorySeparatorChar + "Sound\\ButtonClick.WAV");
            playerPressError = GetPlayer(Directory.GetCurrentDirectory() +
                                 System.IO.Path.DirectorySeparatorChar + "Sound\\PressError.WAV");
            playerOpen = GetPlayer(Directory.GetCurrentDirectory() +
                                 System.IO.Path.DirectorySeparatorChar + "Sound\\play.WAV");
        }

        /// <summary>
        /// 生成播放器
        /// </summary>
        /// <param name="uri">声音文件路径</param>
        /// <returns>播放器</returns>
        private SoundPlayer GetPlayer(string uri)
        {
            SoundPlayer player = new SoundPlayer(uri);
            player.LoadAsync();//提前加载声音
            return player;
        }
        #endregion 
    }
}
