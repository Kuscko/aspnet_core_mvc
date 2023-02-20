using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Bookstore.Models
{
    public class SearchData
    {
        private const string SearchKey = "search";
        private const string TypeKey = "searchtype";

        private ITempDataDictionary tempData { get; set; }
        public SearchData(ITempDataDictionary temp) =>
            tempData = temp;

        public string SearchTerm
        {
            get => tempData.Peek(SearchKey)?.ToString();
            set => tempData[SearchKey] = value;
        }
        public string Type
        {
            get => tempData.Peek(TypeKey)?.ToString();
            set => tempData[TypeKey] = value;
        }

        public bool HasSearchTerm => !string.IsNullOrEmpty(SearchTerm);
        public bool IsBook => Type.EqualsNoCase("book");
        public bool IsAuthor => Type.EqualsNoCase("author");
        public bool IsGenre => Type.EqualsNoCase("genre");

        public void Clear()
        {
            tempData.Remove(SearchKey);
            tempData.Remove(TypeKey);
        }
    }
}
