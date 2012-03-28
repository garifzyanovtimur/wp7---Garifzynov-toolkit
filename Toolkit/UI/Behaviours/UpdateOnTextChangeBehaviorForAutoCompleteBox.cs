using System.Windows;
using System.Windows.Interactivity;
using Microsoft.Phone.Controls;

namespace Mailru.Common.UI
{
	public class UpdateOnTextChangeBehaviorForAutoCompleteBox : Behavior<AutoCompleteBox>
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

		private void AssociatedObject_TextChanged(object sender, RoutedEventArgs e)
		{
			((AutoCompleteBox)sender).GetBindingExpression(AutoCompleteBox.TextProperty).UpdateSource();
		}
	}
}
