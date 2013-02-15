/*
 * [The "BSD license"]
 *  Copyright (c) 2013 Terence Parr
 *  Copyright (c) 2013 Sam Harwell
 *  All rights reserved.
 *
 *  Redistribution and use in source and binary forms, with or without
 *  modification, are permitted provided that the following conditions
 *  are met:
 *
 *  1. Redistributions of source code must retain the above copyright
 *     notice, this list of conditions and the following disclaimer.
 *  2. Redistributions in binary form must reproduce the above copyright
 *     notice, this list of conditions and the following disclaimer in the
 *     documentation and/or other materials provided with the distribution.
 *  3. The name of the author may not be used to endorse or promote products
 *     derived from this software without specific prior written permission.
 *
 *  THIS SOFTWARE IS PROVIDED BY THE AUTHOR ``AS IS'' AND ANY EXPRESS OR
 *  IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
 *  OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
 *  IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT,
 *  INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
 *  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 *  DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 *  THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 *  (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 *  THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
using System.IO;
using Antlr4.Runtime;
using Sharpen;

namespace Antlr4.Runtime
{
	/// <summary>
	/// This is an ANTLRInputStream that is loaded from a file
	/// all at once when you construct the object.
	/// </summary>
	/// <remarks>
	/// This is an ANTLRInputStream that is loaded from a file
	/// all at once when you construct the object.  This is a special case
	/// since we know the exact size of the object to load.  We can avoid lots
	/// of data copying.
	/// </remarks>
	public class ANTLRFileStream : ANTLRInputStream
	{
		protected internal string fileName;

		/// <exception cref="System.IO.IOException"></exception>
		public ANTLRFileStream(string fileName) : this(fileName, null)
		{
		}

		/// <exception cref="System.IO.IOException"></exception>
		public ANTLRFileStream(string fileName, string encoding)
		{
			this.fileName = fileName;
			Load(fileName, encoding);
		}

		/// <exception cref="System.IO.IOException"></exception>
		public virtual void Load(string fileName, string encoding)
		{
			if (fileName == null)
			{
				return;
			}
			FilePath f = new FilePath(fileName);
			int size = (int)f.Length();
			InputStreamReader isr;
			FileInputStream fis = new FileInputStream(fileName);
			if (encoding != null)
			{
				isr = new InputStreamReader(fis, encoding);
			}
			else
			{
				isr = new InputStreamReader(fis);
			}
			try
			{
				data = new char[size];
				base.n = isr.Read(data);
			}
			finally
			{
				isr.Close();
			}
		}

		public override string GetSourceName()
		{
			return fileName;
		}
	}
}
