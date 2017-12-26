namespace LearningSystem.Web.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public static class TempDataDictionaryExtentions
    {
        public static void AddSuccessMessage(this ITempDataDictionary tempData, string message)
        {
            tempData["SuccessMessage"] = message;
        }
    }
}
