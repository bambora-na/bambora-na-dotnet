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

using Beanstream.Data;
using Beanstream.Repositories;

namespace Beanstream
{
	public static class Beanstream
	{
		/// <summary>
		/// The Beanstream merchant ID 
		/// </summary>
		public static int? MerchantId { get; set; }

		/// <summary>
		/// The API Key (Passcode) for accessing the API.
		/// </summary>
		public static string ApiKey { get; set; }

		/// <summary>
		/// The username for accessing the API.
		/// </summary>
		public static string Username { get; set; }

		/// <summary>
		/// The password for accessing the API.
		/// </summary>
		public static string Password { get; set; }
		
		/// <summary>
		/// The api version to use
		/// </summary>
		public static string ApiVersion { get; set; }

		/// <summary>
		/// The Beanstream platform to use. e.g www or payments
		/// </summary>
		public static string Platform { get; set; }

		public static Payments Payments() 
		{
			return new Payments();
		}


		public static Profiles Profiles()
		{
			return new Profiles();
		}


		public static void ThrowIfNullArgument(object value, string name)
		{
			if (value == null)
			{
				throw new System.ArgumentNullException(name);
			}
		}

	}

}
