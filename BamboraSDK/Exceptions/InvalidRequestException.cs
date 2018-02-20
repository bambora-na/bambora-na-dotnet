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
/// The request can be invalid for several reasons:
/// Http status codes:
///  400 - Bad Request - Often missing a required parameter
///  405 - Method not allowed - Sending the wrong HTTP Method
///  415 - Unsupported Media Type - Sending an incorrect Content-Type
/// 
/// This error can occur while in a production environment. It will be thrown if a card is declined or if
/// invalid card info is entered. It can also be thrown by developer-induced formatting errors. The
/// UserFacingMessage will tell the card holder what happened.
/// </summary>
namespace Bambora.NA.SDK.Exceptions
{
	public class InvalidRequestException : BaseApiException
	{
		public InvalidRequestException(HttpStatusCode statusCode, string response, string message, int category, int code)
			: base(statusCode, response, message, category, code)
		{ }

		new public bool IsUserError() {
			if (Category == 1)
				return true;
			else if (Category == 3 && Code == 52)
				return true;
			else
				return false;
		}

		new public string UserFacingMessage ()
		{
			if (IsUserError())
				return ResponseMessage;
			else
				return base.UserFacingMessage();
		}
	}
}