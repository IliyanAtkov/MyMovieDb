using System;
using System.Collections.Generic;
using System.Text;

namespace MyMovieDb.Services.TheMovieDb.Implementations
{
    public abstract class BaseListService
    {
        public BaseListService()
        {
            Parameters = new Dictionary<string, string>();
        }

        public Dictionary<string ,string> Parameters { get; private set; }

        public void AddPageParameter(int? page)
        {
            if (page.HasValue && page.Value > 0)
            {
                Parameters.Add("page", page.Value.ToString());
            }
        }

        public void AddLanguageParameter(string language)
        {
            if (!string.IsNullOrWhiteSpace(language))
            {
                Parameters.Add("language", language);
            }
        }
    }
}