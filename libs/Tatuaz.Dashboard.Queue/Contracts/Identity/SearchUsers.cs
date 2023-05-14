namespace Tatuaz.Dashboard.Queue.Contracts.Identity;

public record SearchUsers(string Query, int PageNumber, int PageSize, bool OnlyArtists);
