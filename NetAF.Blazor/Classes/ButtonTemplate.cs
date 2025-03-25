using Microsoft.AspNetCore.Components;

namespace NetAF.Blazor.Classes
{
    /// <summary>
    /// Provides a template for a button.
    /// </summary>
    /// <param name="Text">The text to display on the button.</param>
    /// <param name="CssClass">The CSS class.</param>
    /// <param name="Style">Styling to apply to the button.</param>
    /// <param name="OnClick">A callback to invoke when the button is clicked.</param>
    internal record ButtonTemplate(string Text, string CssClass, string Style, EventCallback OnClick);
}
