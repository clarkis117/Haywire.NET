using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HaywireNet.RequestServer
{
	public class HaywireResponseFeature : IHttpResponseFeature
	{
		private Func<Task> _requestCompleted = () => Task.FromResult(0);

		public Stream Body { get; set; } = new MemoryStream();

		public bool HasStarted { get; }

		public IHeaderDictionary Headers { get; set; } = new HeaderDictionary();

		public string ReasonPhrase { get; set; }

		public int StatusCode { get; set; }

		public void OnCompleted(Func<object, Task> callback, object state)
		{
			var prior = _requestCompleted;

			_requestCompleted = async () =>

			{
				await prior();

				await callback(state);
			};
		}

		public void OnStarting(Func<object, Task> callback, object state)
		{
		}

		public Task RequestFinished()
		{
			return _requestCompleted?.Invoke();
		}
	}
}