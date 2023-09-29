using System;

namespace Clinic.Shared.Models
{
    public class DialogContext
    {
        public string Type { get; set; } = "";
        public DialogResult Result { get; set; } = DialogResult.None;
        public string Title { get; set; } = "Dialog Box";
        public string Message { get; set; } = "No Dialog Message";
        public Guid TagId { get; set; }

        public DialogContext()
        {
        }

        public DialogContext(string message, string title, Guid tagId, string type = "")
        {
            Message = message;
            Title = title;
            TagId = tagId;
            Type = type;
        }
    }

    public enum DialogResult
    {
        None,
        Accept,
        Cancel
    }
}
