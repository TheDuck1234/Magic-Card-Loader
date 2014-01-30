using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using MTGLoadingPicFromWebsite.Core.Image;
using MTGLoadingPicFromWebsite.Core.Worker;
using MTGLoadingPicFromWebsite.Core.Xml;

namespace MTGLoadingPicFromWebsite.Frames
{
    /// <summary>
    /// Interaction logic for LoadingFrame.xaml
    /// </summary>
    public partial class DownLoadingFrame
    {
        readonly BackgroundWorker _bgw = new BackgroundWorker();

        private List<string> CardList { get; set; }

        private List<string> ErrorList { get; set; }

        public DownLoadingFrame(List<string> cardList)
        {
            InitializeComponent();
            ErrorList = new List<string>();
            CardList = cardList;
        }

        public void RunProcess()
        {
            Show();
            _bgw.DoWork += bgw_DoWork;
            _bgw.ProgressChanged += bgw_ProgressChanged;
            _bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            _bgw.WorkerReportsProgress = true;
            _bgw.RunWorkerAsync();
        }
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            var total = CardList.Count;
            for (var i = 1; i <= total; i++)
            {

                var percents = (i * 100) / total;
                _bgw.ReportProgress(percents, i);
                if (!ImageLoader.DownloadeImage(CardList[i - 1]))
                {
                    ErrorList.Add(CardList[i - 1]);
                }
            }
        }

        void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar.Value = e.ProgressPercentage;
            ProgressLabel.Content = String.Format("Progress: {0} %", e.ProgressPercentage);
            Window.Title = String.Format("Total items cleanse: {0}", e.UserState);
        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                throw new Exception("backgroundworker Stop");
            }
            if (e.Error != null)
            {
                throw new Exception("Error in backgroundworker");
            }
            else
            {
                if (ErrorList.Count != 0)
                {
                    //TODO make error report
                }
                MessageBox.Show("Downloade them all");
                Close();
            }
        }
    }
}
