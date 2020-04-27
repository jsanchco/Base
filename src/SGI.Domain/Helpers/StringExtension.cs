namespace SGI.Domain.Helpers
{
    #region Using

    using System.Globalization;
    using System.Text;

    #endregion

    public static class StringExtension
    {
        public static string RemoveAccentsWithNormalization(this string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
                return string.Empty;

            var normalizedString = inputString.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            for (int i = 0; i < normalizedString.Length; i++)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(normalizedString[i]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(normalizedString[i]);
                }
            }
            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string GetSearcher(this string filter)
        {
            if (filter != null)
            {
                var newfiltersplits = filter;
                var filtersplits = newfiltersplits.Split('(', ')', ' ');
                var filterfield = filtersplits[1];

                if (filtersplits.Length == 5)
                {
                    if (filtersplits[1] == "tolower")
                    {
                        filterfield = filter.Split('(', ')', '\'')[2];
                    }
                }
                if (filtersplits.Length != 5)
                {
                    filterfield = filter.Split('(', ')', '\'')[3];
                }

                return filterfield.RemoveAccentsWithNormalization();
            }

            return null;
        }
    }
}
