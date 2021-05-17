using Common.Messages;
using Newtonsoft.Json.Linq;

namespace Payment.Domain
{
    public class StageLog
    {
        public string Id { get; set; }
        public int StepNumber { get; }
        public string FunctionToCall { get; set; }
        public string FunctionToRestore { get; set; }
        public IMessage Input { get; set; } ///JObject Input { get; set; }
        public IMessage Output { get; set; }

        public ProcessState ProcessState { get; set; }
    }
}