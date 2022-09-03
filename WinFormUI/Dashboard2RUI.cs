using DRYDemoLibrary;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormUI.ViewModels;

namespace WinFormUI
{
    public partial class Dashboard2RUI : Form, IViewFor<TestViewModel>
    {
        public Dashboard2RUI(IEmployeeProcessor processor)
        {
            InitializeComponent();

            VM = new TestViewModel(processor);

            // Bind the view to the ReactiveUI viewmodel
            this.Bind(VM, x => x.FirstName, x => x.firstNameText.Text);
            this.Bind(VM, x => x.LastName, x => x.lastNameText.Text);
            this.Bind(VM, x => x.EmployeeId, x => x.employeeIdText.Text);
            this.BindCommand(VM, x => x.GenerateIdCmdWithMessage, x => x.generateEmployeeIdButton);
            this.BindCommand(VM, x => x.ClearCmdWithMessage, x => x.btnClear);

            this.WhenActivated(
            d =>
            {
                d(this
                    .VM
                    .Confirm
                    .RegisterHandler(
                        async interaction =>
                        {
                            var res = await DisplayAlert(interaction.Input);

                            interaction.SetOutput(res);
                            //interaction.SetOutput(await Task.FromResult(MessageBox.Show(interaction.Input, "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes));
                        }));
            });
        }

        private Task<bool> DisplayAlert(string input)
        {
            return Task.FromResult(MessageBox.Show(input, "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
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
