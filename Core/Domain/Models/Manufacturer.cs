using FluentResults;

namespace Domain.Models;

public class Manufacturer(Lazy<IEnumerable<Product>> products, Lazy<IEnumerable<Article>> articles) : BaseModel
{
    public Result AddArticle(Article article) => _articles.Value.Add(article)
        ? Result.Ok()
        : Result.Fail($"The article {article.Guid} already belongs to the manufacturer {Guid}.");
    
    public Result RemoveArticle(Article article) => _articles.Value.Remove(article)
        ? Result.Ok()
        : Result.Fail($"Tthe article {article.Guid} does not belong to the manufacturer {Guid}.");

    public required string Address { get; set; }

    public required string Name { get; set; }

    public IReadOnlyCollection<Product> Products => _products.Value;

    public IReadOnlyCollection<Article> Articles => _articles.Value;

    private readonly Lazy<HashSet<Product>> _products = new(() => new HashSet<Product>(products.Value));

    private readonly Lazy<HashSet<Article>> _articles = new(() => new HashSet<Article>(articles.Value));
}