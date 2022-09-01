using DRYDemoLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormUI
{
    public partial class Dashboard2 : Form
    {
        private IEmployeeProcessor _processor;
        public Dashboard2(IEmployeeProcessor processor)
        {
            InitializeComponent();
            _processor = processor;
        }

        private void generateEmployeeIdButton_Click(object sender, EventArgs e)
        {
            employeeIdText.Text = _processor.GenerateEmployeeID(firstNameText.Text, lastNameText.Text);
            _processor.GenerateEmployeeID2(firstNameText.Text, lastNameText.Text);
        }
    }
}
