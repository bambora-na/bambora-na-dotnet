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

/// <summary>
/// The base exception for all exceptions that can happen. 
/// It holds the Http status code, the category, and the unique error code.
/// 
/// The Http status code is what determines which specific exception is thrown.
/// 
/// The 'code' is a unique error ID, an int.
/// 
/// The 'category' is one of 4 numeric values:
///   1	- The transaction was successfully processed
///       These can be successes or failures. They indicate that the message went to the bank and it returned a response. 
///       The response could be a "payment approved" or even a "declined" response, along with many others.
///       These errors should be sent to the card holder and merchant to indicate a problem with their card.
///   2	- Business rule violation
///       This is usually a problem with how your account is configured. Sometimes it could be duplicate order number errors or something
///       similar. These errors need to be worked out by the developer before the software moves to production.
///   3	- Input data problem
///       The requests are inproperly formatted or the data is wrong. Invalid card number errors (code 52) can also appear here.
///       Mopst of these errors need to be worked out by the developer before the software moves to production.
///   4	- Transaction failed due to technical problem
///       There was a problem on the Beanstream or bank servers that is out of your control. These will respond with an http status code
///       in the 500+ range. Card holders should wait a minute and try the transaction again.
/// 
/// </summary>
namespace Beanstream.Api.SDK.Exceptions
{
	public abstract class BaseApiException : Exception
	{
		private readonly string _response;
		private readonly HttpStatusCode _statusCode;
		private readonly int _category;
		private readonly int _code;

		protected BaseApiException(HttpStatusCode statusCode, string response, int category, int code)
			:base (statusCode.ToString()+":    "+response)
		{
			_response = response;
			_statusCode =  statusCode;
			_category = category;
			_code = code;
		}

		public string Response
		{
			get { return _response; }
		}

		public int StatusCode
		{
			get { return (int) _statusCode; }
		}

		public int Category {
			get { return _category; }
		}

		public int Code {
			get { return _code; }
		}
	}
}