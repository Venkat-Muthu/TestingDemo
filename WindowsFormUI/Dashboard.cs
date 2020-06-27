using System;
using System.Globalization;
using System.Windows.Forms;
using DemoLibrary;
using DemoLibrary.Models;
using Utils.FileSystemWrapper;

namespace WindowsFormUI
{
    public partial class Dashboard : Form
    {
        private readonly ICalculator _calculator;
        private readonly IDataAccess _dataAccess;
        private readonly IFileWrapper _fileWrapper;
        private readonly IPathWrapper _pathWrapper;
        public Dashboard()
        {
            InitializeComponent();

            _fileWrapper = new FileWrapper();
            _pathWrapper = new PathWrapper();
            _calculator = new Calculator();
            _dataAccess = new DataAccess(_fileWrapper, _pathWrapper);

            cbUsers.DataSource = _dataAccess.GetAllPeople();
            cbUsers.DisplayMember = "Name";
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
    }
}
