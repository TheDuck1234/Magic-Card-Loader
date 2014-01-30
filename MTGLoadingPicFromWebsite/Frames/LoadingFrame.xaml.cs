using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using MTGLoadingPicFromWebsite.Core.Image;
using MTGLoadingPicFromWebsite.Core.Worker;
using MTGLoadingPicFromWebsite.Core.Xml;
using MessageBox = System.Windows.MessageBox;

namespace MTGLoadingPicFromWebsite.Frames
{
    /// <summary>
    /// Interaction logic for LoadingFrame.xaml
    /// </summary>
    public partial class LoadingFrame : Window
    {

        public LoadingFrame()
        {
            InitializeComponent();

        }

        public static LoadingResult Load(Window window,string target)
        {
            var frame = new LoadingFrame {Owner = window, ProgressBar = {IsIndeterminate = false}};
            frame.ProgressBar.Value = 100/1;
            frame.ProgressLabel.Content = String.Format("Progress: {0} %", 100 / 1);
            frame.Window.Title = String.Format("Total items : {0}", 100 / 1); ;
            frame.Show();


            var loader = LoadingManager.GetLoader(target);
            if (loader == null) return null;
            var result = loader.GoLoadingResult();
            
            frame.Close();
            return result;
        }
        
    }
}
