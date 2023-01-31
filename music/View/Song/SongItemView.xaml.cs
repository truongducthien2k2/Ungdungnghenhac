using music.LocalStore;
using music.Model;
using music.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace music.View.Song
{
    /// <summary>
    /// Interaction logic for SongItemView.xaml
    /// </summary>
    public partial class SongItemView : System.Windows.Controls.UserControl
    {
        SongViewModel songVM = new SongViewModel();
        SONG songDataItem;
        Image songImage;
        TextBlock songName;
        TextBlock singerName;
        MediaPlayer player;
        public SongItemView()
        {
            InitializeComponent();
        }

        public SongItemView( SONG songDataItem, Image songImage, TextBlock songName, TextBlock singerName, MediaPlayer player )
        {
            InitializeComponent();
            this.songDataItem = songDataItem;
            this.songImage = songImage;
            this.songName = songName;
            this.singerName = singerName;
            this.player = player;
            LoadSong();
        }

        private void LoadSong()
        {
            tbSongName.Text = songDataItem.songName;
            tbSingerName.Text = songVM.GetAllSinger().Where(singer => singer.id == songDataItem.singerId).Select(singer => singer.singerName).First();
            tbViewsOfSong.Text = songVM.GetViewsOfSong(songDataItem).ToString();
        }

        private void Grid_MouseLeftButtonDown( object sender, MouseButtonEventArgs e )
        {
            songImage.Source = new BitmapImage(new Uri(songDataItem.songImage));
            songName.Text = tbSongName.Text;
            singerName.Text = tbSingerName.Text;
            player.Open(new Uri(songDataItem.songCode));
            BasicSong.Instance.name = songName.Text;
        }

        private void btnShare_Click( object sender, RoutedEventArgs e )
        {
            ShareSongView shareSong = new ShareSongView();
            shareSong.Show();
        }

        public static byte [] ReadToEnd( System.IO.Stream stream )
        {
            long originalPosition = 0;

            if ( stream.CanSeek )
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte [] readBuffer = new byte [4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ( (bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0 )
                {
                    totalBytesRead += bytesRead;

                    if ( totalBytesRead == readBuffer.Length )
                    {
                        int nextByte = stream.ReadByte();
                        if ( nextByte != -1 )
                        {
                            byte [] temp = new byte [readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte) nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte [] buffer = readBuffer;
                if ( readBuffer.Length != totalBytesRead )
                {
                    buffer = new byte [totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if ( stream.CanSeek )
                {
                    stream.Position = originalPosition;
                }
            }
        }

        private void btnDownLoad_Click( object sender, RoutedEventArgs e )
        {
            using (var file = new FileStream(songDataItem.songCode, FileMode.OpenOrCreate)) 
            {
                SaveFileDialog save = new SaveFileDialog();
                save.FileName = songDataItem.songName;
                save.Filter = "Mp3 Files|*.Mp3|Wma Files|*.wma|All Files|*.*";
                save.AddExtension = true;
                save.RestoreDirectory = true;
                save.Title = "Where do you want to save the file?";
                if ( save.ShowDialog() == DialogResult.OK )
                {
                    byte [] songBytes = ReadToEnd(file);
                    file.Write(songBytes, 0, songBytes.Length);
                    file.Close();
                }
            }
        }
    }
}
