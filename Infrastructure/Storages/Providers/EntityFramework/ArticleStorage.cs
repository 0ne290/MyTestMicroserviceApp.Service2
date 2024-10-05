using Domain.Entities;
using Domain.Interfaces;

namespace Storages.Providers.EntityFramework;

public class ArticleStorage(DbContext dbContext) : IArticleStorage
{
    public IEnumerable<Article> GetAllByManufacturerGuid(string guid)
    {
        return Map(dbContext.Articles.Where(a => a.Guid == guid);
    }

    private IEnumerable<Domain.Models.Article> Map(IEnumerable<Article> articles)
    {
        return articles.Select(article => new Domain.Models.Article
        {
            // ...
            Manufacturer = new Lazy<Manufacturer>(() => dbContext.Entry(article)
                .Reference(a => a.Manufacturer)
                .Load())
        });
    }
}