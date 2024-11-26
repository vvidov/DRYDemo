using System.Windows.Forms;

namespace ModelsLib
{
    public sealed class MessageBoxInput
    {
        public string Message;
        public string Caption = "Question";
        public MessageBoxButtons Buttons = MessageBoxButtons.YesNo;
        public MessageBoxIcon Icon = MessageBoxIcon.Question;
    }
}
