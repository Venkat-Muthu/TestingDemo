using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using DemoLibrary;
using DemoLibrary.Models;
using Reactive.EventAggregator;
using Utils.FileSystemWrapper;

namespace WindowsFormUI
{
    public partial class Dashboard : Form
    {
        private delegate void SafeCallDelegate(Ticker ticker);
        private readonly ICalculator _calculator;
        private readonly IDataAccess _dataAccess;
        private readonly IFileWrapper _fileWrapper;
        private readonly IPathWrapper _pathWrapper;
        private readonly IEventAggregator _eventAggregator;
        private readonly ITickerMonitor _tickerMonitor;
        private IDisposable disposable;

        public Dashboard()
        {
            InitializeComponent();
            InitializeBackgroundWorker();

            _eventAggregator = new EventAggregator();
            _fileWrapper = new FileWrapper();
            _pathWrapper = new PathWrapper();
            _calculator = new Calculator();
            _dataAccess = new DataAccess(_fileWrapper, _pathWrapper);
            _tickerMonitor = new TickerMonitor(_eventAggregator);
            _tickerMonitor.Start();
            disposable = _eventAggregator.GetEvent<Ticker>().Subscribe(OnNext);

            cbUsers.DataSource = _dataAccess.GetAllPeople();
            cbUsers.DisplayMember = "Name";

            backgroundWorker1.WorkerReportsProgress = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void OnNext(Ticker ticker)
        {
            if (txtSymbol.InvokeRequired)
            {
                try
                {
                    SafeCallDelegate d = OnNext;
                    Invoke(d, ticker);
                }
                catch (ObjectDisposedException)
                {
                }
            }
            else
            {
                txtSymbol.Text = ticker.Symbol;
                txtBidPrice.Text = ticker.BidPrice.ToString(CultureInfo.InvariantCulture);
                txtAskPrice.Text = ticker.AskPrice.ToString(CultureInfo.InvariantCulture);
            }

        }

        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.DoWork +=
                backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged +=
                backgroundWorker1_ProgressChanged;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundWorker1.ReportProgress(0, DateTime.Now);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            txtClock.Text = e.UserState.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtResult.Text = _calculator.Add((double) numericUpDown1.Value, (double) numericUpDown2.Value)
                .ToString(CultureInfo.InvariantCulture);
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            txtResult.Text = _calculator.Subtract((double) numericUpDown1.Value, (double) numericUpDown2.Value)
                .ToString(CultureInfo.InvariantCulture);
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            txtResult.Text = _calculator.Multiply((double) numericUpDown1.Value, (double) numericUpDown2.Value)
                .ToString(CultureInfo.InvariantCulture);
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            txtResult.Text = _calculator.Divide((double) numericUpDown1.Value, (double) numericUpDown2.Value)
                .ToString(CultureInfo.InvariantCulture);
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            var personModel = new PersonModel {FirstName = txtFirstName.Text, LastName = txtLastName.Text};
            _dataAccess.AddNewPerson(personModel);
            cbUsers.DataSource = _dataAccess.GetAllPeople();
        }

        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            disposable?.Dispose();
            disposable = null;
        }

        private void chkMonitor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMonitor.Checked)
            {
                _tickerMonitor.Start();
            }
            else
            {
                _tickerMonitor.Stop();
                txtSymbol.Text = string.Empty;
                txtBidPrice.Text = string.Empty;
                txtAskPrice.Text = string.Empty;
            }
        }
    }
}
