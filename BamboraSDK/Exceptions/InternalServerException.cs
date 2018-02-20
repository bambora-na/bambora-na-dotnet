// The MIT License (MIT)
//
// Copyright (c) 2018 Bambora, Inc.
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

using System.Net;

/// <summary>
/// An error occurred on the Bambora servers.
/// 
/// Http status codes:
///  500 - Internal Server Error - Application error on Bambora's end
///  503 - Service Unavailable - Bambora service is unavailable
///  504 - Gateway Timeout - Bambora service timed out
/// 
/// This error should be handled neatly and ask the user to try their request again in a moment.
/// 
/// If the Bambora network is down it will redirect to one of several failover data centres. Each failover
/// has its own IP address that you will need to obtain by calling Bambora's technical support. You will 
/// also have to make sure your firewall will allow a connection out to these new IP addresses.
/// </summary>
namespace Bambora.NA.SDK.Exceptions
{
	public class InternalServerException : BaseApiException
	{
		public InternalServerException(HttpStatusCode statusCode, string response, string message, int category, int code)
			: base(statusCode, response, message, category, code)
		{ }
	}
}