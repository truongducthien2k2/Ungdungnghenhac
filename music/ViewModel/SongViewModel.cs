using music.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace music.ViewModel
{
    internal class SongViewModel
    {
        public List<TOPIC> GetAllTopic()
        {
            return DataProvider.Ins.DB.TOPIC.ToList();
        }

        public List<ALBUM> GetAllAlbum()
        {
            return DataProvider.Ins.DB.ALBUM.ToList();
        }

        public List<SINGER> GetAllSinger() 
        { 
            return DataProvider.Ins.DB.SINGER.ToList();
        }

        public List<AREA> GetAllArea()
        {
            return DataProvider.Ins.DB.AREA.ToList();
        }

        public List<SONG> GetAllSong()
        {
            return DataProvider.Ins.DB.SONG.ToList();
        }

        public int InsertSong(string songName, string topicName, string albumName, string singerName, string areaName, string lyrics, string songPath, string imagePath)
        {
            int topicId = GetAllTopic().Where(topic => topic.topicName == topicName).Select(topic => topic.id).First();
            int albumId = albumName == "" ? -1 : GetAllAlbum().Where(album => album.albumName == albumName).Select(album => album.id).First();
            int singerId = GetAllSinger().Where(singer => singer.singerName == singerName).Select(singer => singer.id).First();
            string areaId = GetAllArea().Where(area => area.areaName == areaName).Select(area => area.id).First();
            try
            {
                if (albumId == -1)
                {
                    DataProvider.Ins.DB.Database.ExecuteSqlCommand($"INSERT INTO SONG (songName, topicId, singerId, lyrics, songImage, songCode, areaId) VALUES (N'{songName}', {topicId}, {singerId}, N'{lyrics}', N'{imagePath}', N'{songPath}', '{areaId}')");
                }
                else
                {
                    DataProvider.Ins.DB.Database.ExecuteSqlCommand($"INSERT INTO SONG (songName, topicId, albumId, singerId, lyrics, songImage, songCode, areaId) VALUES (N'{songName}', {topicId}, {albumId}, {singerId}, N'{lyrics}', N'{imagePath}', N'{songPath}', '{areaId}')");
                } 
            }
            catch
            {
                return 0;
            }
            return 1;
        }

        public int UpdateSong(int id, string songName, string topicName, string albumName, string singerName, string areaName, string lyrics, string songPath, string imagePath )
        {
            int topicId = GetAllTopic().Where(topic => topic.topicName == topicName).Select(topic => topic.id).First();
            int albumId = albumName == "" ? -1 : GetAllAlbum().Where(album => album.albumName == albumName).Select(album => album.id).First();
            int singerId = GetAllSinger().Where(singer => singer.singerName == singerName).Select(singer => singer.id).First();
            string areaId = GetAllArea().Where(area => area.areaName == areaName).Select(area => area.id).First();
            try
            {
                if ( albumId == -1 )
                {
                    DataProvider.Ins.DB.Database.ExecuteSqlCommand($"UPDATE SONG SET songName=N'{songName}', topicId={topicId}, albumId=null, singerId={singerId}, lyrics=N'{lyrics}', songImage=N'{imagePath}', songCode=N'{songPath}', areaId='{areaId}' WHERE id={id}");
                }
                else
                {
                    DataProvider.Ins.DB.Database.ExecuteSqlCommand($"UPDATE SONG SET songName=N'{songName}', topicId={topicId}, albumId={albumId}, singerId={singerId}, lyrics=N'{lyrics}', songImage=N'{imagePath}', songCode=N'{songPath}', areaId='{areaId}' WHERE id={id}");
                }
            }
            catch
            {
                return 0;
            }
            return 1;
        }

        public int RemoveSong(int id)
        {
            try
            {
                DataProvider.Ins.DB.Database.ExecuteSqlCommand($"DELETE FROM SONG WHERE id={id}");
            }
            catch
            {
                return 0;
            }
            return 1;
        }

        public int InsertComment(string content, string username, int songId) 
        {
            int clientId = DataProvider.Ins.DB.CLIENT.Where(user => user.userName == username).First().id;
            try
            {
                DataProvider.Ins.DB.Database.ExecuteSqlCommand($"INSERT INTO COMMENT (content, commentDate, songId, clientId) VALUES (N'{content}', '{DateTime.Now.ToString("yyyy/MM/dd")}', {songId}, {clientId})");
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
                return 0;
            }
            return 1;
        }

        public List<COMMENT> GetAllCommentOfSong(int songId)
        {
            return DataProvider.Ins.DB.COMMENT.Where(comment => comment.songId == songId).ToList();
        }
    }
}
