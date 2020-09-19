using System.Collections.Generic;
using System.Linq;

namespace MyMovieDb.Services.Models
{
    public class ResultModel
    {
        private HashSet<string> messages = new HashSet<string>();
        private const string genericErrorMessage = "Error! Please try again!";

        public ResultModel()
        {
            IsOneMessage = true;
            IsSucess = false;
        }

        public ResultModel(string message)
            : this()
        {
            AddMessage(message);
        }

        public ResultModel(ResultModel model)
        {
            messages = new HashSet<string>();
            InitializeResultModel(model);
        }

        public bool IsOneMessage { get; set; }

        public bool IsSucess { get; set; }

        public bool HasMessage { get; set; }


        public void AddMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            messages.Add(message);

            if (messages.Count > 1)
            {
                IsOneMessage = false;
            }
        }

        public void AddMessages(IEnumerable<string> messages)
        {
            if (messages == null)
            {
                return;
            }

            foreach (var message in messages)
            {
                AddMessage(message);
            }
        }

        public ResultModel SetDefaultMessageIfFailedAndEmpty()
        {
            if (IsSucess == false && HasMessage == false)
            {
                AddMessage(genericErrorMessage);
            }

            return this;
        }

        public string GetFirstMessage()
        {
            if (messages != null)
            {
                return messages.First();
            }

            return string.Empty;
        }

        public ICollection<string> GetMessages()
        {
            return messages;
        }

        private void InitializeResultModel(ResultModel oldResultModel)
        {
            if (oldResultModel == null)
            {
                return;
            }

            IsOneMessage = oldResultModel.IsOneMessage;
            AddMessages(oldResultModel.GetMessages());
        }
    }
}
