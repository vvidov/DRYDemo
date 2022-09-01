using DRYDemoLibrary;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormUI.ViewModels;

namespace WinFormUI
{
    public partial class DashboardRUI : Form, IViewFor<TestViewModel>
    {
        public DashboardRUI(IEmployeeProcessor processor)
        {
            InitializeComponent();

            VM = new TestViewModel(processor);

            // Bind the view to the ReactiveUI viewmodel
            this.Bind(VM, x => x.FirstName, x => x.firstNameText.Text);
            this.Bind(VM, x => x.LastName, x => x.lastNameText.Text);
            this.Bind(VM, x => x.EmployeeId, x => x.employeeIdText.Text);
            this.BindCommand(VM, x => x.GenerateIdCmd, x => x.generateEmployeeIdButton);

        }

        public TestViewModel VM { get; set; }

        object IViewFor.ViewModel
        {
            get { return VM; }
            set { VM = (TestViewModel)value; }
        }

        TestViewModel IViewFor<TestViewModel>.ViewModel
        {
            get { return VM; }
            set { VM = value; }
        }
    }
}
