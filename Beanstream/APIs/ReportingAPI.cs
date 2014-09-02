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
using Beanstream.Api.SDK.Data;
using Beanstream.Api.SDK.Requests;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;

namespace Beanstream.Api.SDK
{
	public class ReportingAPI
	{
		private Configuration _configuration;
		private IWebCommandExecuter _webCommandExecuter = new WebCommandExecuter ();

		public Configuration Configuration {
			set { _configuration = value; }
		}

		public IWebCommandExecuter WebCommandExecuter {
			set { _webCommandExecuter = value; }
		}

		public Transaction GetTransaction(string paymentId) {

			string url = BeanstreamUrls.GetPaymentUrl
				.Replace("{v}", String.IsNullOrEmpty(_configuration.Version) ? "v1" : "v"+_configuration.Version)
				.Replace("{p}", String.IsNullOrEmpty(_configuration.Platform) ? "www" : _configuration.Platform)
				.Replace("{id}", paymentId);


			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.ApiPasscode,
				WebCommandExecutor = _webCommandExecuter
			};


			string response = req.ProcessTransaction (HttpMethod.Get, url);

			return JsonConvert.DeserializeObject<Transaction>(response);
		}

		public IList<TransactionRecord> Query(string reportName, DateTime startDate, DateTime endDate, int startRow, int endRow, params Criteria[] criteria) {

			string url = BeanstreamUrls.GetPaymentUrl
				.Replace ("{v}", String.IsNullOrEmpty (_configuration.Version) ? "v1" : "v" + _configuration.Version)
				.Replace ("{p}", String.IsNullOrEmpty (_configuration.Platform) ? "www" : _configuration.Platform);


			HttpsWebRequest req = new HttpsWebRequest () {
				MerchantId = _configuration.MerchantId,
				Passcode = _configuration.ApiPasscode,
				WebCommandExecutor = _webCommandExecuter
			};

			var query = new 
			{
				name = reportName,
				start_date = startDate,
				nd_date = endDate,   
				start_row = startRow,
				end_row = endRow,
				criteria = criteria
			};

			string response = req.ProcessTransaction (HttpMethod.Post, url);
			Console.WriteLine ("\n\n"+response+"\n\n");
			return null;
			//return JsonConvert.DeserializeObject<Transaction>(response);
		}



	}
}

