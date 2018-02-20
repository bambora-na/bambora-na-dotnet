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
using System.Reflection;
using System.ComponentModel;

/// <summary>
/// Writes out the Operator Enum's attribute descriptions instead of the default value.
/// We Want to write a URL encoded version of the operators.
/// </summary>
namespace Bambora.SDK
{
	public class OperatorsStringEnumConverter : Newtonsoft.Json.Converters.StringEnumConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value is Operators)
			{
				FieldInfo fi = value.GetType().GetField(value.ToString());

				DescriptionAttribute[] attributes =
					(DescriptionAttribute[])fi.GetCustomAttributes(
						typeof(DescriptionAttribute),
						false);

				if (attributes != null &&
					attributes.Length > 0)
					writer.WriteValue( attributes[0].Description );
				else
					writer.WriteValue( value.ToString() );
			}
			else
				base.WriteJson(writer, value, serializer);
		}
	}
}

