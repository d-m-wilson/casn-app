using System;
using System.Collections.Generic;

namespace CASNApp.Core.Entities
{
    public partial class MessageType
    {
        public MessageType()
        {
            Messages = new HashSet<Message>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
