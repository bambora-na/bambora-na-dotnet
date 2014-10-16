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

/// <summary>
/// The fields we can query in the search criteria. Order matters in this enum so do not change it.
/// Each field has a comment with what operators can be used with it.
/// </summary>
namespace Beanstream
{
	public enum QueryFields
	{
		TransactionId, 		// >=,<=,=,<>,>,<
		Amount, 			// >=,<=,=,<>,>,<
		MaskedCardNumber, 	// =
		CardOwner, 			// =,START WITH
		OrderNumber, 		// >=,<=,=,<>,>,<
		IPAddress, 			// =,START WITH
		AuthorizationCode, 	// =,START WITH
		TransType, 			// =
		CardType, 			// =
		Response, 			// =
		BillingName, 		// =,START WITH
		BillingEmail, 		// =,START WITH
		BillingPhone, 		// =,START WITH
		ProcessedBy, 		// =
		Ref1, 				// =,START WITH
		Ref2, 				// =,START WITH
		Ref3, 				// =,START WITH
		Ref4, 				// =,START WITH
		Ref5, 				// =,START WITH
		ProductName, 		// =,START WITH
		ProductID, 			// =,START WITH
		CustCode, 			// =,START WITH
		IDAdjustmentTo, 	// =
		IDAdjustedBy 		// =
	}
}

