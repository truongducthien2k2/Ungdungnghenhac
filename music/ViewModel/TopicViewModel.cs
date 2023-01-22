using music.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace music.ViewModel
{
    internal class TopicViewModel
    {
        public int InsertNewTopic( string name, string img )
        {
            try
            {
                DataProvider.Ins.DB.Database.ExecuteSqlCommand($"INSERT INTO TOPIC (topicName, topicImage) VALUES (N'{name}', '{img}')");
            }
            catch
            {
                return 0;
            }
            return 1;
        }

        public int UpdateTopic( int id, string name, string img )
        {
            try
            {
                DataProvider.Ins.DB.Database.ExecuteSqlCommand($"UPDATE TOPIC SET topicName=N'{name}', topicImage='{img}' WHERE id={id}");
            }
            catch
            {
                return 0;
            }
            return 1;
        }

        public List<TOPIC> GetAllTopic()
        {
            return DataProvider.Ins.DB.TOPIC.ToList();
        }
    }
}
