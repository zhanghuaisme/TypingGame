/****************************
 * 项目名：指法练习游戏
 * 创建者：张华
 * 创建日：2010/03/28
 */

/*变更历史
 * 
 */

namespace TypingGame
{
    /// <summary>
    /// 常量类
    /// </summary>
    public class Constant
    {
        /// <summary>
        /// 枚举，消息类型
        /// </summary>
        public enum MessageType
        {
            MyGod,
            GodLike,
            VeryGood,
            Good,
            Shy
        }

        /// <summary>
        /// 枚举，声音类型
        /// </summary>
        public enum SoundType
        {
            Bom,
            Coin,
            ButtonEnter,
            ButtonClick,
            PressError,
            Open
        }

        public static string MsgScore = "您本次的得分是：";

        public static string MsgSaySorry = "继续努力，good good study, day day up！";
        public static string MsgCongratulation = "恭喜你！你已经很不错了！先休息一下吧！";
        public static string MsgPraise = "你太棒了！我崇拜死你了！";
        public static string MsgTalent = "你简直是天才！我对你的敬仰如滔滔江水！";
        public static string MsgMyGod = "你已经超越神了！";

        public static string RecordFileName = "record.xml";

        public static string DefaultTxtBoxName = "请输入练习者名字";
        public static string DefaultName = "匿名";
    }
}
