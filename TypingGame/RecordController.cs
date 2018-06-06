/****************************
 * 项目名：指法练习游戏
 * 创建者：张华
 * 创建日：2010/04/08
 */

/*变更历史
 * 
 */

using System.Data;
using System.IO;

namespace TypingGame
{
    public class RecordController
    {
        string recordFileName = string.Empty;

        public RecordController()
        {
            recordFileName = Directory.GetCurrentDirectory() +
                                 System.IO.Path.DirectorySeparatorChar +
                                 Constant.RecordFileName;
        }
        public void SaveScore(int score, int level, string name)
        {
            if (score < 80)
            {
                return;
            }
            if (Constant.DefaultTxtBoxName.Equals(name))
            {
                name = Constant.DefaultName;
            }

            RecordDS recordDS = (RecordDS)ReadRecord();
            recordDS.RecordDT.AddRecordDTRow(
                score, level, name,
                System.DateTime.Now, level * score, 0);

            if (recordDS.RecordDT.Rows.Count > 10)
            {
                DataRow[] drs = Sort(recordDS);
                recordDS.RecordDT.Rows.Remove(drs[0]);
            }
            
            recordDS.WriteXml(recordFileName);
        }

        private static DataRow[] Sort(RecordDS recordDS)
        {
            DataRow[] drs = recordDS.RecordDT.Select("", "TatolScore DESC");
            return drs;
        }

        private DataSet ReadRecord()
        {
            return ReadXml();
        }

        private RecordDS ReadXml()
        {
            RecordDS recordDS = new RecordDS();
            if (File.Exists(recordFileName))
            {
                recordDS.ReadXml(recordFileName);
            }
            return recordDS;
        }

        public DataRow[] GetRecord()
        {
            RecordDS recordDS = ReadXml();

            DataRow[] drs = Sort(recordDS);
            for (int i = 0; i < drs.Length; i++)
            {
                drs[i]["No"] = i + 1;
            }
            return drs;
        }
    }
}
