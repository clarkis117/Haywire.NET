using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HaywireNet.RequestServer
{
	public class HaywireRequestFeature : IHttpRequestFeature
	{
		//
		// Summary:
		//     A System.IO.Stream representing the request body, if any. Stream.Null may be
		//     used to represent an empty request body.
		public Stream Body { get; set; }
		//
		// Summary:
		//     Headers included in the request, aggregated by header name. The values are not
		//     split or merged across header lines. E.g. The following headers: HeaderA: value1,
		//     value2 HeaderA: value3 Result in Headers["HeaderA"] = { "value1, value2", "value3"
		//     }
		public IHeaderDictionary Headers { get; set; }
		//
		// Summary:
		//     The request method as defined in RFC 7230. E.g. "GET", "HEAD", "POST", etc..
		public string Method { get; set; }
		//
		// Summary:
		//     The portion of the request path that identifies the requested resource. The value
		//     is un-escaped. The value may be string.Empty if Microsoft.AspNetCore.Http.Features.IHttpRequestFeature.PathBase
		//     contains the full path.
		public string Path { get; set; }
		//
		// Summary:
		//     The first portion of the request path associated with application root. The value
		//     is un-escaped. The value may be string.Empty.
		public string PathBase { get; set; }

		//
		// Summary:
		//     The HTTP-version as defined in RFC 7230. E.g. "HTTP/1.1"
		public string Protocol { get; set; }

		//
		// Summary:
		//     The query portion of the request-target as defined in RFC 7230. The value may
		//     be string.Empty. If not empty then the leading '?' will be included. The value
		//     is in its original form, without un-escaping.
		public string QueryString { get; set; }

		//
		// Summary:
		//     The request target as it was sent in the HTTP request. This property contains
		//     the raw path and full query, as well as other request targets such as * for OPTIONS
		//     requests (https://tools.ietf.org/html/rfc7230#section-5.3).
		//
		// Remarks:
		//     This property is not used internally for routing or authorization decisions.
		//     It has not been UrlDecoded and care should be taken in its use.
		public string RawTarget { get; set; }

		//
		// Summary:
		//     The request uri scheme. E.g. "http" or "https". Note this value is not included
		//     in the original request, it is inferred by checking if the transport used a TLS
		//     connection or not.
		public string Scheme { get; set; } //= "http";
	}
}
