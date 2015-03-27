using System;

namespace Jess
{
	public class MissingHeaderException : Exception
	{
		public MissingHeaderException(string headerName)
			:base(string.Format("The request did not have an '{0}' header set.", headerName))
		{
		}
	}
}
