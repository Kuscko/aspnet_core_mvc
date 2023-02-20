namespace Bookstore.Models
{
    public static class Operation
    {
        public static bool IsAdd(string action) => action.EqualsNoCase("add");
        public static bool IsDelete(string action) => action.EqualsNoCase("delete");
    }
}
