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
    /// Interaction logic for ActistItemView.xaml
    /// </summary>
    public partial class ActistItemView : UserControl
    {
            TopicViewModel topicVM = new TopicViewModel();
            SINGER topic;
            Image songImage;
            TextBlock songName;
            TextBlock singerName;
            MediaPlayer player;
            Frame MainContent;
            public ActistItemView()
            {
                InitializeComponent();
            }
            public ActistItemView(SINGER topic, Image songImage, TextBlock songName, TextBlock singerName, MediaPlayer player, Frame mainContent)
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
                topicImage.Source = new BitmapImage(new Uri(topic.singerImage));
                tbTopicName.Text = topic.singerName;
            }

            private void controlTopic_PreviewMouseDown(object sender, MouseButtonEventArgs e)
            {
                MainContent.Navigate(new ActistItem(songImage, songName, singerName, player, topic, MainContent));
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
