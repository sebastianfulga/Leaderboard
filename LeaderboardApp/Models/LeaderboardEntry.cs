namespace LeaderboardApp.Models;

public sealed record LeaderboardEntry(
    int Rank,
    string Name,
    string Team,
    double Rating,
    int Score,
    string Record,
    int Change,
    string Form)
{
    public string Initials => string.Join("", Name.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(part => part[0]));

    public int RatingPercent => Math.Clamp((int)Math.Round(Rating), 0, 100);
}
