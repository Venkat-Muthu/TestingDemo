using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DemoLibrary;
using DemoLibrary.Models;

namespace WindowsFormUI
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            
            cbUsers.DataSource = DataAccess.GetAllPeople();
            cbUsers.DisplayMember = "Name";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtResult.Text = Calculator.Add((double) numericUpDown1.Value, (double) numericUpDown2.Value)
                .ToString(CultureInfo.InvariantCulture);
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            txtResult.Text = Calculator.Subtract((double) numericUpDown1.Value, (double) numericUpDown2.Value)
                .ToString(CultureInfo.InvariantCulture);
        }

        private void btnMultiply_Click(object sender, EventArgs e)
        {
            txtResult.Text = Calculator.Multiply((double) numericUpDown1.Value, (double) numericUpDown2.Value)
                .ToString(CultureInfo.InvariantCulture);
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            txtResult.Text = Calculator.Divide((double) numericUpDown1.Value, (double) numericUpDown2.Value)
                .ToString(CultureInfo.InvariantCulture);
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            var personModel = new PersonModel {FirstName = txtFirstName.Text, LastName = txtLastName.Text};
            DataAccess.AddNewPerson(personModel);
            cbUsers.DataSource = DataAccess.GetAllPeople();
        }
    }
}
