using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Garifzyanov.Toolkit.UI.Behaviours
{
	public class UpdateOnTextChangeBehavior : Behavior<TextBox>
	{
		protected override void OnAttached()
		{
			base.OnAttached();
			AssociatedObject.TextChanged += AssociatedObject_TextChanged;
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			AssociatedObject.TextChanged -= AssociatedObject_TextChanged;
		}

		private void AssociatedObject_TextChanged(object sender, TextChangedEventArgs e)
		{
			((TextBox)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
		}
	}
}
