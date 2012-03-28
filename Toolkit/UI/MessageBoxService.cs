using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace Garifzyanov.Toolkit.UI
{
	public class MessageBoxService<T, K>
		where T : UserControl, IMessageBox<K>, new()
		where K : class, new()
	{
		private Action<K> _callback;
		private T _control;
		private PhoneApplicationFrame _frame;

		public void Show(PhoneApplicationFrame rootFrame, Action<K> callback)
			
		{
			_frame = rootFrame;
			var page = _frame.Content as PhoneApplicationPage;
			page.BackKeyPress += PageOnBackKeyPress_Callback;

			_control = new T();
			_control.OnResultGet += ControlGetResult_Callback;
			_callback = callback;

			var grid = System.Windows.Media.VisualTreeHelper.GetChild(page, 0) as Grid;
			Canvas.SetZIndex(_control, 999);
			grid.Children.Add(_control);

			var transitionIn = new SwivelTransition
			                   	{
			                   		Mode = SwivelTransitionMode.BackwardIn
			                   	};

			var transition = transitionIn.GetTransition(_control);
			transition.Completed += (s, e) => transition.Stop();
			transition.Begin();

			if (page.ApplicationBar != null)
			{
				page.ApplicationBar.IsVisible = false;
			}
		}

		private void ControlGetResult_Callback(K obj)
		{
			_control.OnResultGet -= ControlGetResult_Callback;
			Remove();
			_callback(obj);
		}

		private void PageOnBackKeyPress_Callback(object sender, CancelEventArgs e)
		{
			_callback(null);
			Remove();
			e.Cancel = true;
		}

		private void Remove()
		{
			var page = _frame.Content as PhoneApplicationPage;
			var grid = System.Windows.Media.VisualTreeHelper.GetChild(page, 0) as Grid;

			page.BackKeyPress -= PageOnBackKeyPress_Callback;

			// Create a transition like the regular MessageBox
			var transitionOut = new SwivelTransition
			                    	{
			                    		Mode = SwivelTransitionMode.BackwardOut
			                    	};

			var transition = transitionOut.GetTransition(_control);
			transition.Completed += (s, e) =>
			                        	{
			                        		transition.Stop();
			                        		grid.Children.Remove(_control);
			                        		if (page.ApplicationBar != null)
			                        		{
			                        			page.ApplicationBar.IsVisible = true;
			                        		}
			                        	};
			transition.Begin();
		}
	}
}