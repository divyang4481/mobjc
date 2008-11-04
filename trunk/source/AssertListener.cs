// Copyright (C) 2008 Jesse Jones
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

using System;
using System.Diagnostics;

internal class AssertException : Exception
{
	public AssertException(string text) : base(text) 
	{
	}
}

// Trace.Assert and Debug.Assert work well on Windows where they pop up a
// dialog. They don't work so well on Mono where all they do is write a message
// (and even that has to be explicitly enabled). We want asserts to be very
// visible events so what we do is use this listener to throw an exception
// on asserts.
internal sealed class AssertListener : TraceListener
{
	public override void Fail(string message)
	{
		throw new AssertException(message);
	}

	public override void Fail(string message, string details)
	{
		if (string.IsNullOrEmpty(details))
			throw new AssertException(message);
		else
			throw new AssertException(message + " --- " + details);
	}

	public override void Write(string message)
	{
		// need to override this, but we don't want to do anything
	}

	public override void WriteLine(string message)
	{
		// need to override this, but we don't want to do anything
	}
}