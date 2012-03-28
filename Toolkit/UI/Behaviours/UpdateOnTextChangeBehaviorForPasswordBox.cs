using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Mailru.Common.UI
{
	public class UpdateOnTextChangeBehaviorForPasswordBox : Behavior<PasswordBox>
	{
		protected override void OnAttached()
		{
			base.OnAttached();
			AssociatedObject.PasswordChanged += AssociatedObject_PasswordChanged;
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			AssociatedObject.PasswordChanged -= AssociatedObject_PasswordChanged;
		}

		private void AssociatedObject_PasswordChanged(object sender, RoutedEventArgs e)
		{
			((PasswordBox) sender).GetBindingExpression(PasswordBox.PasswordProperty).UpdateSource();
		}
	}
}
