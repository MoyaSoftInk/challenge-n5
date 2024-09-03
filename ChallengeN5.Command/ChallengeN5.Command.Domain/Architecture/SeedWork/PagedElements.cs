namespace ChallengeN5.Command.Domain.Architecture.SeedWork;

public class PagedElements<TEntity>
    where TEntity : class
{
    public PagedElements(IEnumerable<TEntity> elements, int totalElements)
    {
        Elements = elements;
        TotalElements = totalElements;
    }

    public IEnumerable<TEntity> Elements
    {
        get;
        private set;
    }

    public int TotalElements
    {
        get;
        private set;
    }

    public int TotalPages(int pageSize)
    {
        return (int)Math.Ceiling(Convert.ToDouble(TotalElements) / pageSize);
    }
}