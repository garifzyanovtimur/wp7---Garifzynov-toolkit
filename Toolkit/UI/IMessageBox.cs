using System;

namespace Garifzyanov.Toolkit.UI
{
	public interface IMessageBox<TResult>
	{
		event Action<TResult> OnResultGet;
	}
}
