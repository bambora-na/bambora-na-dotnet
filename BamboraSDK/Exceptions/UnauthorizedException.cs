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
/// Http status codes:
///  401 - Unauthorized - No valid authentication provided.
/// 
/// The merchant API key may be wrong or improperly formatted and encoded.
/// 
/// This error should not occur while in a production environment. If it occurs the developer
/// has done something wrong and the cardholder or merchant getting this message should contact the developer
/// of the software.
/// </summary>
namespace Bambora.SDK.Exceptions
{
	public class UnauthorizedException : BaseApiException
	{
		public UnauthorizedException(HttpStatusCode statusCode, string response, string message, int category, int code)
			: base(statusCode, response, message, category, code)
		{ }
	}
}