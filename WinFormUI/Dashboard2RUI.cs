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
using ModelsLib.ViewModels;

namespace WinFormUI
{
    public partial class Dashboard2RUI : Form, IViewFor<PersonViewModel>
    {
        public Dashboard2RUI(IEmployeeProcessor processor)
        {
            InitializeComponent();

            VM = new PersonViewModel(processor);

            // Bind the view to the ReactiveUI viewmodel
            this.Bind(VM, vm => vm.FirstName, v => v.firstNameText.Text);
            this.Bind(VM, wm => wm.LastName, v => v.lastNameText.Text);
            this.OneWayBind(VM, wm => wm.EmployeeId, v => v.employeeIdText.Text);
            this.OneWayBind(VM, wm => wm.FullName, v => v.tbFullName.Text);

            this.BindCommand(VM, wm => wm.GenerateIdCmdWithMessage, v => v.generateEmployeeIdButton);
            this.BindCommand(VM, wm => wm.ClearCmdWithMessage, v => v.btnClear);

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

        public PersonViewModel VM { get; set; }

        object IViewFor.ViewModel
        {
            get { return VM; }
            set { VM = (PersonViewModel)value; }
        }

        PersonViewModel IViewFor<PersonViewModel>.ViewModel
        {
            get { return VM; }
            set { VM = value; }
        }
    }
}
