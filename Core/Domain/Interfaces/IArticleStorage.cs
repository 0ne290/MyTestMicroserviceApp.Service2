namespace Domain.Interfaces;

public interface IArticleStorage
{
    IEnumerable<Article> GetAllByManufacturerGuid(string guid);
}