using music.Model;
using music.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace music.View.Actist
{
    /// <summary>
    /// Interaction logic for ActistItem.xaml
    /// </summary>
        public partial class ActistItem : Page
        {
            SongViewModel topicVM = new SongViewModel();
            Image songImage;
            TextBlock songName;
            TextBlock singerName;
            MediaPlayer player;
            SINGER topic;
            Frame MainContent;
            public ActistItem()
            {
                InitializeComponent();
            }
            public ActistItem(Image songImage, TextBlock songName, TextBlock singerName, MediaPlayer player, SINGER topic, Frame MainContent)
            {
                InitializeComponent();
                this.songImage = songImage;
                this.songName = songName;
                this.singerName = singerName;
                this.player = player;
                this.topic = topic;
                this.MainContent = MainContent;
                LoadUI();
            }
            private void LoadUI()
            {
                if (topic == null)
                {
                    List<SINGER> topic = topicVM.GetAllSinger();
                    foreach (SINGER topicDataItem in topic)
                    {
                        plTOPIC.Children.Add(new ActistItemView(topicDataItem, songImage, songName, singerName, player, MainContent));
                    }
                }
                else
                {
                    List<SONG> songs = topicVM.GetAllSong().Where(song => song.topicId == topic.id).ToList();
                    if (songs.Count == 0)
                    {
                        TextBlock notice = new TextBlock();
                        notice.Text = "Không có bài hát có sẵn trong chủ đề này";
                        notice.HorizontalAlignment = HorizontalAlignment.Center;
                        notice.Margin = new Thickness(0, 30, 0, 0);
                        notice.FontSize = 14;
                        plTOPIC.Children.Add(notice);
                    }
                    else
                    {
                        foreach (SONG song in songs)
                        {
                            plTOPIC.Children.Add(new SongItemView(song, songImage, songName, singerName, player));
                        }
                    }
                }
            }
        }
    }

