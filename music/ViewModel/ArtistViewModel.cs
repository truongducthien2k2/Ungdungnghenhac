using music.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace music.ViewModel
{
    internal class ArtistViewModel
    {
        public int InsertNewArtist(string name, string intro, string avt)
        {
            try
            {
                DataProvider.Ins.DB.Database.ExecuteSqlCommand($"INSERT INTO SINGER (singerName, intro, singerImage) VALUES (N'{name}', N'{intro}', '{avt}')");
            }
            catch 
            {
                return 0;
            }
            return 1;
        }

        public int UpdateArtist( int id, string name, string intro, string avt )
        {
            try
            {
                DataProvider.Ins.DB.Database.ExecuteSqlCommand($"UPDATE SINGER SET singerName=N'{name}', intro=N'{intro}', singerImage='{avt}' WHERE id={id}");
            }
            catch
            {
                return 0;
            }
            return 1;
        }

        public List<SINGER> GetAllArtist()
        {
            return DataProvider.Ins.DB.SINGER.SqlQuery("SELECT * FROM SINGER").ToList<SINGER>();
        }
    }
}
