// The MIT License (MIT)
//
// Copyright (c) 2014 Beanstream Internet Commerce Corp, Digital River, Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

using System;
using System.Net;

namespace Beanstream.Api.SDK.Data
{
	/// <summary>
	/// Represents a web command, i.e. An HTTP web request and its response.
	/// This lives so we can create mock objects for unit testing.
	/// </summary>
	/// <typeparam name="T">The type representing the response from the request.</typeparam>
	public interface IWebCommandSpec<out T>
	{
		/// <summary>
		/// The URL of the web command.
		/// </summary>
		Uri Url { get; }

		/// <summary>
		/// Prepares the web request for executing.
		/// </summary>
		/// <param name="request">The request to prepare.</param>
		void PrepareRequest(WebRequest request);

		/// <summary>
		/// Maps the web response to an instance of type {T}.
		/// </summary>
		/// <param name="response">The response to map.</param>
		/// <returns>An instance of type {T} representing the data from the web response.</returns>
		T MapResponse(WebResponse response);
	}
}