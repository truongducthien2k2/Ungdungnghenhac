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

namespace music.View.Topic
{
    /// <summary>
    /// Interaction logic for TopicItemView.xaml
    /// </summary>
    public partial class TopicItemView : UserControl
    {
        TopicViewModel topicVM = new TopicViewModel();
        TOPIC topic;
        Image topicimage;
        TextBlock topicname;
        public TopicItemView()
        {
            InitializeComponent();
        }
        public TopicItemView(TOPIC topic, Image topicimage, TextBlock topicname)
        {
            InitializeComponent();
            this.topic = topic;
            this.topicimage = topicimage;
            this.topicname = topicname;
            LoadData();
        }

        private void LoadData()
        {
            topicImage.Source = new BitmapImage(new Uri(topic.topicImage));
            tbTopicName.Text = topic.topicName;
        }
    }
}
