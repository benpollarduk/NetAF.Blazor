using NetAF.Targets.Html;

namespace NetAF.Blazor.Classes
{
    /// <summary>
    /// Represents any Blazor component that directly interacts with NetAF.
    /// </summary>
    public interface INetAFComponent
    {
        /// <summary>
        /// Set the adapter.
        /// </summary>
        /// <param name="adapter">The adapter.</param>
        void SetAdapter(HtmlAdapter adapter);
    }
}
