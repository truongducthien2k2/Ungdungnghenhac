using music.Component;
using music.LocalStore;
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
using System.Windows.Shapes;

namespace music.View.Song
{
    /// <summary>
    /// Interaction logic for SetTimeToStopView.xaml
    /// </summary>
    public partial class SetTimeToStopView : Window
    {
        private bool IsClikeOneMs = false;
        private bool IsClikeTwoMs = false;
        private bool IsClikeThreeMs = false;
        public SetTimeToStopView()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized( EventArgs e )
        {
            IconHelper.RemoveIcon(this);
        }

        private void oneMs_MouseMove( object sender, MouseEventArgs e )
        {
            if (!IsClikeOneMs)
            {
                oneMs.Background = new SolidColorBrush(Colors.LightGray);
            }
        }

        private void oneMs_MouseLeave( object sender, MouseEventArgs e )
        {
            if ( !IsClikeOneMs )
            {
                oneMs.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void twoMs_MouseMove( object sender, MouseEventArgs e )
        {
            if ( !IsClikeTwoMs )
            {
                twoMs.Background = new SolidColorBrush(Colors.LightGray);
            }
        }

        private void twoMs_MouseLeave( object sender, MouseEventArgs e )
        {
            if ( !IsClikeTwoMs )
            {
                twoMs.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void threeMs_MouseMove( object sender, MouseEventArgs e )
        {
            if ( !IsClikeThreeMs )
            {
                threeMs.Background = new SolidColorBrush(Colors.LightGray);
            }
        }

        private void threeMs_MouseLeave( object sender, MouseEventArgs e )
        {
            if ( !IsClikeThreeMs )
            {
                threeMs.Background = new SolidColorBrush(Colors.Transparent);
            }
        }

        private void oneMs_PreviewMouseLeftButtonDown( object sender, MouseButtonEventArgs e )
        {
            IsClikeOneMs = true;
            IsClikeTwoMs = false;
            IsClikeThreeMs = false;
            oneMs.Background = new SolidColorBrush(Colors.LightGray);
            twoMs.Background = new SolidColorBrush(Colors.Transparent);
            threeMs.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void twoMs_PreviewMouseLeftButtonDown( object sender, MouseButtonEventArgs e )
        {
            IsClikeOneMs = false;
            IsClikeTwoMs = true;
            IsClikeThreeMs = false;
            oneMs.Background = new SolidColorBrush(Colors.Transparent);
            twoMs.Background = new SolidColorBrush(Colors.LightGray);
            threeMs.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void threeMs_PreviewMouseLeftButtonDown( object sender, MouseButtonEventArgs e )
        {
            IsClikeOneMs = false;
            IsClikeTwoMs = false;
            IsClikeThreeMs = true;
            oneMs.Background = new SolidColorBrush(Colors.Transparent);
            twoMs.Background = new SolidColorBrush(Colors.Transparent);
            threeMs.Background = new SolidColorBrush(Colors.LightGray);
        }

        private void btnSubmit_Click( object sender, RoutedEventArgs e )
        {
            if (!IsClikeOneMs && !IsClikeTwoMs && !IsClikeThreeMs)
            {
                MessageBox.Show("Vui lòng chọn thời gian hẹn tắt nhạc");
            }
            else
            {
                if (IsClikeOneMs)
                {
                    TimeToStop.Instance.minute = 1;
                }
                else if (IsClikeTwoMs)
                {
                    TimeToStop.Instance.minute = 2;
                }
                else if (IsClikeThreeMs)
                {
                    TimeToStop.Instance.minute = 3;
                }
                this.Close();
            }    
        }
    }
}
