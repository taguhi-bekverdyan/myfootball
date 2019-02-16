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
using Microsoft.Win32;
using Image = System.Drawing.Image;

namespace MyFootballAdmin.Main.Views.AddTournament
{
    /// <summary>
    /// Interaction logic for AddTournamentView.xaml
    /// </summary>
    public partial class AddTournamentView : UserControl
    {
        public AddTournamentView()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var op = new OpenFileDialog
            {
                Title = "Select a logo",
                Filter = "All supported graphics|*.jpg;*.jpeg;*.png;*.svg|" +
                         "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                         "Portable Network Graphic (*.png)|*.png|" +
                         "SVG (*.svg)|*.svg"
            };
            if (op.ShowDialog() == true)
            {
                var bitmapImage = Image.FromFile(op.FileName);
                var viewModel = (AddTournamentViewModel)DataContext;
                viewModel.LogoFileName = op.FileName;
                ImgLogo.Source = MyFootballAdmin.Main.Helpers.ToBitmap(bitmapImage).ToBitmapImage();
            }
        }
    }
}
