using Post.Common.Events;
using Post.Query.Domain.Entities;
using Post.Query.Domain.Repostories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Query.Infrastructure.Handlers
{
    public class EventHandler : IEventHandler
    {

        private readonly IPostRespository _postRespository;
        private readonly ICommentRepository _commentRepository;

        public EventHandler(IPostRespository postRespository, ICommentRepository commentRepository)
        {
            _postRespository = postRespository;
            _commentRepository = commentRepository;
        }

        public async Task On(PostCreatedEvent @event)
        {
            var post = new PostEntity 
            { 
                PostId =  @event.Id,
                Author = @event.Author,
                DatePosted = @event.DatePosted,
                Message = @event.Message                        
            };

            await _postRespository.CreateAsync(post);
        }

        public async Task On(MessageUpdatedEvent @event)
        {
            var post = await _postRespository.GetByIdAsync(@event.Id);

            if (post == null) return;

            post.Message = @event.Message;
            await _postRespository.UpdateAsync(post);
        }

        public async Task On(PostLikedEvent @event)
        {
            var post = await _postRespository.GetByIdAsync(@event.Id);

            if (post == null) return;

            post.Likes++;
            await _postRespository.UpdateAsync(post);
        }

        public async Task On(CommentAddedEvent @event)
        {
            var comment = new CommentEntity
            {
                PostId = @event.Id,
                CommentId = @event.Id,
                CommentDate = @event.CommentDate,
                Comment = @event.Comment,
                Username = @event.Username,
                Edited = false
            };

            await _commentRepository.CreateAsync(comment);
        }

        public async Task On(CommentUpdatedEvent @event)
        {
            var comment = await _commentRepository.GetByIdAsync(@event.CommentId);

            if (comment == null) return;    

            comment.Comment = @event.Comment;
            comment.Edited = true;
            comment.CommentDate = @event.EditDate;

            await _commentRepository.UpdateAsync(comment);
        }

        public async Task On(CommentRemovedEvent @event)
        {
            await _commentRepository.DeleteCommentAsync(@event.CommentId);
        }

        public async Task On(PostRemovedEvent @event)
        {
            await _postRespository.DeleteAsync(@event.Id);
        }
    }
}
