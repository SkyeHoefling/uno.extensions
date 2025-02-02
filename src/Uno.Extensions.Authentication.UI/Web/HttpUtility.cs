﻿// 
// System.Web.HttpUtility
//
// Authors:
//   Patrik Torstensson (Patrik.Torstensson@labs2.com)
//   Wictor Wilén (decode/encode functions) (wictor@ibizkit.se)
//   Tim Coleman (tim@timcoleman.com)
//   Gonzalo Paniagua Javier (gonzalo@ximian.com)
//
// Copyright (C) 2005-2010 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

namespace System.Web
{
	internal sealed class AuthHttpUtility
	{
		sealed class HttpQSCollection : NameValueCollection
		{
			public override string ToString()
			{
				int count = Count;
				if (count == 0)
				{
					return "";
				}

				StringBuilder sb = new StringBuilder();
				string?[] keys = AllKeys;
				for (int i = 0; i < count; i++)
				{
					string? key = keys[i];
					string[]? values = GetValues(key);
					if (values != null)
					{
						foreach (string value in values)
						{
							if (!string.IsNullOrEmpty(key))
							{
								sb.Append(key).Append('=');
							}
							sb.Append(WebUtility.UrlEncode(value)).Append('&');
						}
					}
				}

				return sb.Length > 0 ? sb.ToString(0, sb.Length - 1) : "";
			}
		}

		#region Constructors

		public AuthHttpUtility()
		{
		}

		#endregion // Constructors

		#region Methods

		public static NameValueCollection ParseQueryString(string query)
		{
			return ParseQueryString(query, Encoding.UTF8);
		}

		public static NameValueCollection ParseQueryString(string query, Encoding encoding)
		{
			if (query == null)
				throw new ArgumentNullException("query");
			if (encoding == null)
				throw new ArgumentNullException("encoding");
			if (query.Length == 0 || (query.Length == 1 && query[0] == '?'))
				return new HttpQSCollection();
			if (query[0] == '?')
				query = query.Substring(1);

			var result = new HttpQSCollection();
			ParseQueryString(query, encoding, result);
			return result;
		}

		internal static void ParseQueryString(string query, Encoding encoding, NameValueCollection result)
		{
			if (query.Length == 0)
				return;

			string decoded = System.Net.WebUtility.HtmlDecode(query);
			int decodedLength = decoded.Length;
			int namePos = 0;
			bool first = true;
			while (namePos <= decodedLength)
			{
				int valuePos = -1, valueEnd = -1;
				for (int q = namePos; q < decodedLength; q++)
				{
					if (valuePos == -1 && decoded[q] == '=')
					{
						valuePos = q + 1;
					}
					else if (decoded[q] == '&')
					{
						valueEnd = q;
						break;
					}
				}

				if (first)
				{
					first = false;
					if (decoded[namePos] == '?')
						namePos++;
				}

				string? name, value;
				if (valuePos == -1)
				{
					name = null;
					valuePos = namePos;
				}
				else
				{
					name = System.Net.WebUtility.UrlDecode(decoded.Substring(namePos, valuePos - namePos - 1));
				}
				if (valueEnd < 0)
				{
					namePos = -1;
					valueEnd = decoded.Length;
				}
				else
				{
					namePos = valueEnd + 1;
				}
				value = System.Net.WebUtility.UrlDecode(decoded.Substring(valuePos, valueEnd - valuePos));

				result.Add(name, value);
				if (namePos == -1)
					break;
			}
		}

		#endregion // Methods
	}
}
