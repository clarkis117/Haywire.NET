namespace HaywireNet.Bindings
{
	public enum HttpMethod : byte
	{
		DELETE = 0,
		GET,
		HEAD,
		POST,
		PUT,
		/* pathological */
		CONNECT,
		OPTIONS,
		TRACE,
		/* webdav */
		COPY,
		LOCK,
		MKCOL,
		MOVE,
		PROPFIND,
		PROPPATCH,
		SEARCH,
		UNLOCK,
		/* subversion */
		REPORT,
		MKACTIVITY,
		CHECKOUT,
		MERGE,
		/* upnp */
		MSEARCH,
		NOTIFY,
		SUBSCRIBE,
		UNSUBSCRIBE,
		/* RFC-5789 */
		PATCH,
		PURGE
	}
}