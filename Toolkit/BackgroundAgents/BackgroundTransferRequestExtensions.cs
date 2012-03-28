using Microsoft.Phone.BackgroundTransfer;

namespace Garifzyanov.Toolkit.Linq
{
	public static class BackgroundTransferRequestExtensions
	{
		public static double PercentProgress(this BackgroundTransferRequest request)
		{
			return ((double)request.BytesReceived / (double)request.TotalBytesToReceive) * 100;
		}
	}
}
