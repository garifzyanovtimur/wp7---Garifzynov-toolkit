using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Garifzyanov.Toolkit.Controls
{
	public class PullToUpdateControl : ItemsControl
	{
		/// <summary>
		/// Triggered when the user requested a refresh.
		/// </summary>
		public event EventHandler PullRefresh;

		private bool _isPulling = false;
		private ScrollViewer _scrollViewer;
		private UIElement _releaseElement;
		private double VerticalOffset
		{
			get
			{
				return _scrollViewer == null ? double.NaN : _scrollViewer.VerticalOffset;
			}
		}

		public PullToUpdateControl()
		{
			DefaultStyleKey = typeof(PullToUpdateControl);
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			if (_scrollViewer != null)
			{
				_scrollViewer.MouseMove -= viewer_MouseMove;
				_scrollViewer.ManipulationCompleted -= viewer_ManipulationCompleted;
			}
			_scrollViewer = GetTemplateChild("ScrollViewer") as ScrollViewer;
			if (_scrollViewer != null)
			{
				_scrollViewer.MouseMove += viewer_MouseMove;
				_scrollViewer.ManipulationCompleted += viewer_ManipulationCompleted;
			}
			_releaseElement = GetTemplateChild("ReleaseElement") as UIElement;
		}

		private void viewer_MouseMove(object sender, MouseEventArgs e)
		{
			if (VerticalOffset == 0.0)
			{
				var p = this.TransformToVisual(_releaseElement).Transform(new Point());
				if (p.Y < 5d)
				{
					if (!_isPulling)
					{
						_isPulling = true;
						VisualStateManager.GoToState(this, "Pulling", true);
					}
				}
				else if (_isPulling)
				{
					_isPulling = false;
					VisualStateManager.GoToState(this, "NotPulling", true);
				}
			}
		}

		
		private void viewer_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
		{
			var p = this.TransformToVisual(_releaseElement).Transform(new Point(0, 0));
			if (p.Y < 5d)
			{
				if (_isPulling)
				{
					_isPulling = false;
					VisualStateManager.GoToState(this, "NotPulling", true);
					if (PullRefresh != null)
						PullRefresh(this, new EventArgs());
				}
			}
		}
	}
}
