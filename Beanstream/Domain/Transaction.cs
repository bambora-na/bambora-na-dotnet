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
using Newtonsoft.Json;

/// <summary>
/// A query result representing a single transaction.
/// </summary>
namespace Beanstream
{
	public class Transaction
	{
		[JsonProperty(PropertyName = "row_id")]
		public int RowId {get; set;}

		[JsonProperty(PropertyName = "trn_id")]
		public int TransactionId {get; set;}

		[JsonProperty(PropertyName = "trn_date_time")]
		public String DateTime {get; set;}

		[JsonProperty(PropertyName = "trn_type")]
		public String Type {get; set;}

		[JsonProperty(PropertyName = "trn_order_number")]
		public String OrderNumber {get; set;}

		[JsonProperty(PropertyName = "trn_masked_card")]
		public int MaskedMard {get; set;}

		[JsonProperty(PropertyName = "trn_amount")]
		public double Amount {get; set;}

		[JsonProperty(PropertyName = "trn_returns")]
		public double Returns {get; set;}

		[JsonProperty(PropertyName = "trn_completions")]
		public double Completions {get; set;}

		[JsonProperty(PropertyName = "trn_voided")]
		public String Voided {get; set;}

		[JsonProperty(PropertyName = "trn_response")]
		public int Response{get; set;}

		[JsonProperty(PropertyName = "trn_card_type")]
		public String CardType {get; set;}

		[JsonProperty(PropertyName = "message_id")]
		public String MessageId {get; set;}

		[JsonProperty(PropertyName = "message_text")]
		public String MessageText {get; set;}

		[JsonProperty(PropertyName = "trn_card_owner")]
		public String CardOwner {get; set;}

		[JsonProperty(PropertyName = "b_name")]
		public String BillingName {get; set;}

		[JsonProperty(PropertyName = "b_email")]
		public String BillingEmail {get; set;}

		[JsonProperty(PropertyName = "b_phone")]
		public String BillingPhone {get; set;}

		[JsonProperty(PropertyName = "b_address1")]
		public String BillingAddress1  {get; set;}

		[JsonProperty(PropertyName = "b_address2")]
		public String b_address2 {get; set;}

		[JsonProperty(PropertyName = "b_city")]
		public String BillingCity {get; set;}

		[JsonProperty(PropertyName = "b_province")]
		public String BillingProvince {get; set;}

		[JsonProperty(PropertyName = "b_postal")]
		public String BillingPostal {get; set;}

		[JsonProperty(PropertyName = "b_country")]
		public String BillingCountry {get; set;}

		[JsonProperty(PropertyName = "s_name")]
		public String ShippingName {get; set;}

		[JsonProperty(PropertyName = "s_email")]
		public String ShippingEmail {get; set;}

		[JsonProperty(PropertyName = "s_phone")]
		public String ShippingPhone {get; set;}

		[JsonProperty(PropertyName = "s_address1")]
		public String ShippingAddress1 {get; set;}

		[JsonProperty(PropertyName = "s_address2")]
		public String ShippingAddress2 {get; set;}

		[JsonProperty(PropertyName = "s_city")]
		public String ShippingCity {get; set;}

		[JsonProperty(PropertyName = "s_province")]
		public String ShippingProvince {get; set;}

		[JsonProperty(PropertyName = "s_postal")]
		public String ShippingPostal {get; set;}

		[JsonProperty(PropertyName = "s_postal")]
		public String ShippingCountry {get; set;}

	}
}

