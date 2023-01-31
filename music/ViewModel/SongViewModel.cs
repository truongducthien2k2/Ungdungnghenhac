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

        public int RemoveComment(int commentId)
        {
            try
            {
                DataProvider.Ins.DB.Database.ExecuteSqlCommand($"DELETE FROM COMMENT WHERE id={commentId}");
            }
            catch
            {
                return 0;
            }
            return 1;
        }

        public List<CLIENT_VIEW_SONG> GetViewHistoryOfUser(string username)
        {
            int id = DataProvider.Ins.DB.CLIENT.Where(client => client.userName == username).First().id;
            return DataProvider.Ins.DB.CLIENT_VIEW_SONG.Where(view => view.clientId == id).ToList();
        }

        public void InsertSongIntoHistory(string username, int songId) 
        {
            int clientId = DataProvider.Ins.DB.CLIENT.Where(client => client.userName == username).First().id;

            try
            {
                List<CLIENT_VIEW_SONG> viewHistoryOfUser = DataProvider.Ins.DB.CLIENT_VIEW_SONG.Where(view => view.clientId == clientId && view.songId == songId).ToList();
                if ( viewHistoryOfUser.Count() > 0)
                {
                    int viewId = viewHistoryOfUser.First().id;
                    DataProvider.Ins.DB.Database.ExecuteSqlCommand($"UPDATE CLIENT_VIEW_SONG SET currentViews += 1, viewDate={DateTime.Now.ToString("yyyy/MM/dd")} WHERE songId={songId} AND clientId={clientId}");
                }
                else
                {
                    DataProvider.Ins.DB.Database.ExecuteSqlCommand($"INSERT INTO CLIENT_VIEW_SONG (songId, clientId, currentViews, viewDate) VALUES ({songId}, {clientId}, 1, {DateTime.Now.ToString("yyyy/MM/dd")})");
                }
            }
            catch
            {
            }
        }

        public int LikeSong(int songId, string username)
        {
            if ( IsLikedSong(songId, username) )
            {
                if (RemoveLikeSong(songId, username) == 1)
                {
                    return 1;
                }
                    
            }
            else
            {
                if (InsertLikeSong(songId, username) == 1)
                {
                    return 1;
                }
            }
            return 0;
        }

        public bool IsLikedSong( int songId, string username ) 
        {
            if ( username == "" )
            {
                return DataProvider.Ins.DB.CLIENT_LOVE_SONG.Where(like => like.songId == songId && like.clientId == null).Count() > 0;
            }
            int userId = DataProvider.Ins.DB.CLIENT.Where(client => client.userName == username).First().id;
            return DataProvider.Ins.DB.CLIENT_LOVE_SONG.Where(like => like.songId == songId && like.clientId == userId).Count() > 0;
        }

        public int InsertLikeSong(int songId, string username)
        {
            try
            {
                if (username == "")
                {
                    DataProvider.Ins.DB.Database.ExecuteSqlCommand($"INSERT INTO CLIENT_LOVE_SONG (songId, loveDate) VALUES ({songId}, '{DateTime.Now.ToString("yyyy/MM/dd")}')");
                }
                else
                {
                    int clientId = DataProvider.Ins.DB.CLIENT.Where(client => client.userName == username).First().id;
                    DataProvider.Ins.DB.Database.ExecuteSqlCommand($"INSERT INTO CLIENT_LOVE_SONG (songId, clientId, loveDate) VALUES ({songId}, {clientId}, '{DateTime.Now.ToString("yyyy/MM/dd")}')");
                }
            }
            catch
            {
                return 0;
            }
            return 1;
        }

        public int RemoveLikeSong( int songId, string username )
        {
            try
            {
                if ( username == "" )
                {
                    DataProvider.Ins.DB.Database.ExecuteSqlCommand($"DELETE FROM CLIENT_LOVE_SONG WHERE songId={songId} AND clientId=null)");
                }
                else
                {
                    int clientId = DataProvider.Ins.DB.CLIENT.Where(client => client.userName == username).First().id;
                    DataProvider.Ins.DB.Database.ExecuteSqlCommand($"DELETE FROM CLIENT_LOVE_SONG WHERE songId={songId} AND clientId={clientId}");
                }
            }
            catch
            {
                return 0;
            }
            return 1;
        }

        public List<CLIENT_LOVE_SONG> GetLikeSongList()
        {
            return DataProvider.Ins.DB.CLIENT_LOVE_SONG.ToList();
        }

        public List<CLIENT_VIEW_SONG> GetViewSongList()
        {
            return DataProvider.Ins.DB.CLIENT_VIEW_SONG.ToList();
        }

        public int GetViewsOfSong(SONG song)
        {
            return DataProvider.Ins.DB.CLIENT_VIEW_SONG.Where(View => View.songId == song.id ).Count();
        }
    }
}
