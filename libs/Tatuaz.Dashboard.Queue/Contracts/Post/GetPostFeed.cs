namespace Tatuaz.Dashboard.Queue.Contracts.Post;

public record GetPostFeed(int PageNumber, int PageSize, bool Posts, bool Photos);
