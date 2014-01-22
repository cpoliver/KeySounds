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

namespace KeySounds.UI.Controls
{
    public partial class VolumeSlider : UserControl
    {
        public static DependencyProperty IsMutedProperty = DependencyProperty.Register("IsMuted", typeof (bool),
            typeof (VolumeSlider), new FrameworkPropertyMetadata(false));
        public static DependencyProperty VolumeProperty = DependencyProperty.Register("Volume", typeof(float),
            typeof(VolumeSlider), new FrameworkPropertyMetadata(0F));
        
        
        public VolumeSlider()
        {
            InitializeComponent();
        }

        public bool IsMuted
        {
            get { return (bool) GetValue(IsMutedProperty); }
            set { SetValue(IsMutedProperty, value); }
        }

        public float Volume
        {
            get { return (float)GetValue(VolumeProperty); }
            set { SetValue(VolumeProperty, value); }
        }
    }
}
