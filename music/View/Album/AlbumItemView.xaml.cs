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

namespace music.View.Album
{
    /// <summary>
    /// Interaction logic for AlbumItemView.xaml
    /// </summary>
    public partial class AlbumItemView : UserControl
    {
        TopicViewModel topicVM = new TopicViewModel();
        ALBUM topic;
        Image songImage;
        TextBlock songName;
        TextBlock singerName;
        MediaPlayer player;
        Frame MainContent;
        public AlbumItemView()
        {
            InitializeComponent();
        }
        public AlbumItemView(ALBUM topic, Image songImage, TextBlock songName, TextBlock singerName, MediaPlayer player, Frame mainContent)
        {
            InitializeComponent();
            this.topic = topic;
            this.songImage = songImage;
            this.songName = songName;
            this.singerName = singerName;
            this.player = player;
            this.MainContent = mainContent;
            LoadData();
        }

        private void LoadData()
        {
            topicImage.Source = new BitmapImage(new Uri(topic.albumImage));
            tbTopicName.Text = topic.albumName;
        }

        private void controlTopic_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            MainContent.Navigate(new AlbumItem(songImage, songName, singerName, player, topic, MainContent));
        }

        private void controlTopic_MouseMove(object sender, MouseEventArgs e)
        {
            controlTopic.Background = new SolidColorBrush(Colors.LightGray);
        }

        private void controlTopic_MouseLeave(object sender, MouseEventArgs e)
        {
            controlTopic.Background = new SolidColorBrush(Colors.White);
        }
    }
}
