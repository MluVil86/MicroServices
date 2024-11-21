using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Common.Events
{
    public class CommentUpdatedEvent :BaseEvent
    {
        public CommentUpdatedEvent() :base(nameof(CommentUpdatedEvent))
        {
            
        }

        public Guid CommentId { get; set; }
        public string Comment { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public DateTime EditDate { get; set; }
    }

    
}
