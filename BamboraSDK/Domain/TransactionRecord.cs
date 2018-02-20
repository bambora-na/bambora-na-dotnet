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
using System;
using Newtonsoft.Json;

/// <summary>
/// A query result representing a single transaction.
/// </summary>
namespace Bambora.SDK
{
	public class TransactionRecord
	{
		[JsonProperty(PropertyName = "row_id")]
		public int RowId {get; set;}

		[JsonProperty(PropertyName = "trn_id")]
		public int TransactionId {get; set;}

		[JsonProperty(PropertyName = "trn_date_time")]
		public DateTime DateTime {get; set;}

		[JsonProperty(PropertyName = "trn_type")]
		public string Type {get; set;}

		[JsonProperty(PropertyName = "trn_order_number")]
		public string OrderNumber {get; set;}

		[JsonProperty(PropertyName = "trn_payment_method")]
		public string PaymentMethod {get; set;}

		[JsonProperty(PropertyName = "trn_comments")]
		public string Comments {get; set;}

		[JsonProperty(PropertyName = "trn_masked_card")]
		public string MaskedCard {get; set;}

		[JsonProperty(PropertyName = "trn_amount")]
		public decimal Amount {get; set;}

		[JsonProperty(PropertyName = "trn_returns")]
		public decimal Returns {get; set;}

		[JsonProperty(PropertyName = "trn_completions")]
		public decimal Completions {get; set;}

		[JsonProperty(PropertyName = "trn_voided")]
		public string Voided {get; set;}

		[JsonProperty(PropertyName = "trn_response")]
		public int Response{get; set;}

		[JsonProperty(PropertyName = "trn_card_type")]
		public string CardType {get; set;}

		[JsonProperty(PropertyName = "trn_batch_no")]
		public int BatchNumber {get; set;}

		[JsonProperty(PropertyName = "trn_avs_result")]
		public string AVSResult {get; set;}

		[JsonProperty(PropertyName = "trn_cvd_result")]
		public int CVDResult {get; set;}

		[JsonProperty(PropertyName = "trn_card_expiry")]
		public string CardExpiry {get; set;}

		[JsonProperty(PropertyName = "message_id")]
		public string MessageId {get; set;}

		[JsonProperty(PropertyName = "message_text")]
		public string MessageText {get; set;}

		[JsonProperty(PropertyName = "trn_card_owner")]
		public string CardOwner {get; set;}

		[JsonProperty(PropertyName = "trn_ip")]
		public string IPAddress {get; set;}

		[JsonProperty(PropertyName = "trn_approval_code")]
		public string ApprovalCode {get; set;}

		[JsonProperty(PropertyName = "trn_reference")]
		public int Reference {get; set;}

		[JsonProperty(PropertyName = "b_name")]
		public string BillingName {get; set;}

		[JsonProperty(PropertyName = "b_email")]
		public string BillingEmail {get; set;}

		[JsonProperty(PropertyName = "b_phone")]
		public string BillingPhone {get; set;}

		[JsonProperty(PropertyName = "b_address1")]
		public string BillingAddress1  {get; set;}

		[JsonProperty(PropertyName = "b_address2")]
		public string b_address2 {get; set;}

		[JsonProperty(PropertyName = "b_city")]
		public string BillingCity {get; set;}

		[JsonProperty(PropertyName = "b_province")]
		public string BillingProvince {get; set;}

		[JsonProperty(PropertyName = "b_postal")]
		public string BillingPostal {get; set;}

		[JsonProperty(PropertyName = "b_country")]
		public string BillingCountry {get; set;}

		[JsonProperty(PropertyName = "s_name")]
		public string ShippingName {get; set;}

		[JsonProperty(PropertyName = "s_email")]
		public string ShippingEmail {get; set;}

		[JsonProperty(PropertyName = "s_phone")]
		public string ShippingPhone {get; set;}

		[JsonProperty(PropertyName = "s_address1")]
		public string ShippingAddress1 {get; set;}

		[JsonProperty(PropertyName = "s_address2")]
		public string ShippingAddress2 {get; set;}

		[JsonProperty(PropertyName = "s_city")]
		public string ShippingCity {get; set;}

		[JsonProperty(PropertyName = "s_province")]
		public string ShippingProvince {get; set;}

		[JsonProperty(PropertyName = "s_postal")]
		public string ShippingPostal {get; set;}

		[JsonProperty(PropertyName = "s_country")]
		public string ShippingCountry {get; set;}

		[JsonProperty(PropertyName = "ref1")]
		public string Ref1 {get; set;}

		[JsonProperty(PropertyName = "ref2")]
		public string Ref2 {get; set;}

		[JsonProperty(PropertyName = "ref3")]
		public string Ref3 {get; set;}

		[JsonProperty(PropertyName = "ref4")]
		public string Ref4 {get; set;}

		[JsonProperty(PropertyName = "product_name")]
		public string ProductName {get; set;}

		[JsonProperty(PropertyName = "product_id")]
		public string ProductId {get; set;}

		[JsonProperty(PropertyName = "customer_code")]
		public string CustomerCode {get; set;}
	}
}

