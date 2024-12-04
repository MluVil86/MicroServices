using Post.Query.Domain.Entities;

namespace Post.Query.Domain.Repostories
{
    public interface IPostRespository
    {
        Task CreateAsync(PostEntity post);
        Task UpdateAsync(PostEntity post);        
        Task DeleteAsync(Guid postID);
        Task<PostEntity> GetByIdAsync(Guid postID);
        Task<List<PostEntity>> ListAllAsync();
        Task<List<PostEntity>> ListByAuthorAsync(string author); 
        Task<List<PostEntity>> ListWithLikesAsync(int numberOfLikes);
        Task<List<PostEntity>> ListWithCommentsAsync();

    }
}
