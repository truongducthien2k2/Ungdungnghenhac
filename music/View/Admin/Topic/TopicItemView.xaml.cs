using music.Model;
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

namespace music.View.Admin.Topic
{
    /// <summary>
    /// Interaction logic for TopicItemView.xaml
    /// </summary>
    public partial class TopicItemView : UserControl
    {
        Frame MainContent;
        TOPIC topic;
        public TopicItemView()
        {
            InitializeComponent();
        }

        public TopicItemView(TOPIC topic, Frame MainContent)
        {
            InitializeComponent();
            this.MainContent = MainContent;
            this.topic = topic;
            LoadData();
        }

        private void LoadData()
        {
            topicImage.Source = new BitmapImage(new Uri(topic.topicImage));
            tbTopicName.Text = topic.topicName;
        }

        private void btnAdjustTopic_Click( object sender, RoutedEventArgs e )
        {
            NewTopicView newTopic = new NewTopicView(topic, "adjust", MainContent);
            newTopic.Show();
        }

        private void btnRemoveTopic_Click( object sender, RoutedEventArgs e )
        {

        }
    }
}
