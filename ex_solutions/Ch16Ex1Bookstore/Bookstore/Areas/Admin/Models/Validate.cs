using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Bookstore.Models
{
    public class Validate
    {
        private const string GenreKey = "validGenre";
        private const string AuthorKey = "validAuthor";
        private const string EmailKey = "validEmail";

        private ITempDataDictionary tempData { get; set; }
        public Validate(ITempDataDictionary temp) => tempData = temp;

        public bool IsValid { get; private set; }
        public string ErrorMessage { get; private set; }

        public void CheckGenre(string genreId, Repository<Genre> data)
        {
            Genre entity = data.Get(genreId);
            IsValid = (entity == null) ? true : false;
            ErrorMessage = (IsValid) ? "" : 
                $"Genre id {genreId} is already in the database.";
        }
        public void MarkGenreChecked() => tempData[GenreKey] = true;
        public void ClearGenre() => tempData.Remove(GenreKey);
        public bool IsGenreChecked => tempData.Keys.Contains(GenreKey);

        public void CheckAuthor(string firstName, string lastName, string operation, Repository<Author> data)
        {
            Author entity = null; 
            if (Operation.IsAdd(operation)) {
                entity = data.Get(new QueryOptions<Author> {
                    Where = a => a.FirstName == firstName && a.LastName == lastName });
            }
            IsValid = (entity == null) ? true : false;
            ErrorMessage = (IsValid) ? "" : 
                $"Author {entity.FullName} is already in the database.";
        }
        public void MarkAuthorChecked() => tempData[AuthorKey] = true;
        public void ClearAuthor() => tempData.Remove(AuthorKey);
        public bool IsAuthorChecked => tempData.Keys.Contains(AuthorKey);
    }
}
