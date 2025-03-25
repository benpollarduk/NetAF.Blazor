namespace NetAF.Blazor.Classes
{
    /// <summary>
    /// Provides settings for command categories.
    /// </summary>
    /// <param name="show">If the category should be shown.</param>
    /// <param name="htmlColor">The HTML color.</param>
    /// <param name="order">The order, relative to other categories. Lower numbers will be displayed first, higher numbers last.</param>
    public class CommandCategorySettings(bool show, string htmlColor, int order)
    {
        #region Properties

        /// <summary>
        /// Get or set if the category should be shown.
        /// </summary>
        public bool Show { get; set; } = show;

        /// <summary>
        /// Get or set the HTML color.
        /// </summary>
        public string HtmlColor { get; set; } = htmlColor;

        /// <summary>
        /// Get or set the order, relative to other categories. Lower numbers will be displayed first, higher numbers last.
        /// </summary>
        public int Order { get; set; } = order;

        #endregion
    }
}
