﻿//
// ArgumentExceptionTests.cs
//
// Author: Jeffrey Stedfast <jestedfa@microsoft.com>
//
// Copyright (c) 2013-2018 Xamarin Inc. (www.xamarin.com)
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
using System.IO;
using System.Collections.Generic;

using NUnit.Framework;

using MimeKit;
using MailKit;
using MailKit.Search;

namespace UnitTests
{
	[TestFixture]
	public class ArgumentExceptionTests
	{
		[Test]
		public void TestArgumentExceptions ()
		{
			Assert.Throws<ArgumentNullException> (() => new FolderNamespace ('.', null));

			var namespaces = new FolderNamespaceCollection ();
			FolderNamespace ns;

			Assert.Throws<ArgumentNullException> (() => namespaces.Add (null));
			Assert.Throws<ArgumentNullException> (() => namespaces.Contains (null));
			Assert.Throws<ArgumentNullException> (() => namespaces.Remove (null));
			Assert.Throws<ArgumentOutOfRangeException> (() => ns = namespaces[-1]);
			Assert.Throws<ArgumentOutOfRangeException> (() => namespaces[-1] = new FolderNamespace ('.', ""));

			namespaces.Add (new FolderNamespace ('.', ""));
			Assert.Throws<ArgumentNullException> (() => namespaces[0] = null);

			Assert.Throws<ArgumentNullException> (() => new FolderNotFoundException (null));
			Assert.Throws<ArgumentNullException> (() => new FolderNotFoundException ("message", null));
			Assert.Throws<ArgumentNullException> (() => new FolderNotFoundException ("message", null, new Exception ("message")));

			Assert.Throws<ArgumentNullException> (() => new FolderNotOpenException (null, FolderAccess.ReadOnly));
			Assert.Throws<ArgumentNullException> (() => new FolderNotOpenException (null, FolderAccess.ReadOnly, "message"));
			Assert.Throws<ArgumentNullException> (() => new FolderNotOpenException (null, FolderAccess.ReadOnly, "message", new Exception ("message")));

			Assert.Throws<ArgumentNullException> (() => new MetadataCollection (null));

			var metadataOptions = new MetadataOptions ();
			Assert.Throws<ArgumentOutOfRangeException> (() => metadataOptions.Depth = 500);

			Assert.Throws<ArgumentOutOfRangeException> (() => new OrderBy (OrderByType.To, SortOrder.None));

			Assert.Throws<ArgumentNullException> (() => new ProtocolLogger ((string) null));
			Assert.Throws<ArgumentNullException> (() => new ProtocolLogger ((Stream) null));
			using (var logger = new ProtocolLogger (new MemoryStream ())) {
				var buffer = new byte[1024];

				Assert.Throws<ArgumentNullException> (() => logger.LogConnect (null));
				Assert.Throws<ArgumentNullException> (() => logger.LogClient (null, 0, 0));
				Assert.Throws<ArgumentNullException> (() => logger.LogServer (null, 0, 0));
				Assert.Throws<ArgumentOutOfRangeException> (() => logger.LogClient (buffer, -1, 0));
				Assert.Throws<ArgumentOutOfRangeException> (() => logger.LogServer (buffer, -1, 0));
				Assert.Throws<ArgumentOutOfRangeException> (() => logger.LogClient (buffer, 0, -1));
				Assert.Throws<ArgumentOutOfRangeException> (() => logger.LogServer (buffer, 0, -1));
			}

			Assert.Throws<ArgumentNullException> (() => new UniqueIdMap (null, new [] { UniqueId.MinValue }));
			Assert.Throws<ArgumentNullException> (() => new UniqueIdMap (new [] { UniqueId.MinValue }, null));
		}
	}
}
