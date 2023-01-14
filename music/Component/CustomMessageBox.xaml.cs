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

namespace music.Component
{
    /// <summary>
    /// Interaction logic for CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox( string Message, string Type )
        {
            InitializeComponent();
            txtMessage.Text = Message;
            var converter = new System.Windows.Media.BrushConverter();
            switch ( Type )
            {
                case "Info":
                    txtTitle.Text = "Thông báo";
                    break;
                case "Success":
                    txtTitle.Text = "Thành công";
                    var brush = (Brush) converter.ConvertFromString("#0BDA51");
                    cardHeader.Background = brush;
                    break;
                case"Warning":
                    txtTitle.Text = "Cảnh báo";
                    brush = (Brush) converter.ConvertFromString("#FFBF00");
                    cardHeader.Background = brush;
                    break;
                case "Error":
                    txtTitle.Text = "Lỗi";
                    brush = (Brush) converter.ConvertFromString("#FF4433");
                    cardHeader.Background = brush;
                    break;
            }
        }

        private void btnCancel_Click( object sender, RoutedEventArgs e )
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnOk_Click( object sender, RoutedEventArgs e )
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnClose_Click( object sender, RoutedEventArgs e )
        {
            this.DialogResult = false;
            this.Close();
        }

    }
}
