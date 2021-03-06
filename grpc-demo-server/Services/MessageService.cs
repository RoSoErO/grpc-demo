using demo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grpc_demo_server
{
    //public interface IMessageServcie

    public class MessageService //: IMessageService
    {
        private List<Message> messages;
        private object mylock;
        private int nextiID;
        

        public MessageService()
        {
            this.messages = new List<Message>();
            mylock = new object();
            nextiID = 1;
        }

        public List<Message> GetMessages(int latestId)
        {
            // To get all ids set latestId < 0
            lock (mylock)
            {
                return messages
                    .Where(e => e.Id > latestId)
                    .ToList();
            }
        }

        public void AddMessage(Message message)
        {
            lock (mylock)
            {
                message.Id = nextiID;
                nextiID++;
                messages.Add(message);
            }
        }
    }
}
