using HaywireNet.Bindings.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace HaywireNet.Bindings
{
	//concurrent state machine for managing memory associations between string values
	//and their unmanaged blocks of memory
	[StructLayout(LayoutKind.Sequential)]
	public struct HaywireString
	{
		public static readonly ConcurrentDictionary<string, IntPtr> MemoryStrings = new ConcurrentDictionary<string, IntPtr>();

		/// <summary>
		/// normally byte*
		/// </summary>
		private readonly IntPtr value;

		/*
		public string Value
		{
			get { return MemoryStrings. }
		}
		*/

		//this is size_t
		[MarshalAs(UnmanagedType.SysUInt)]
		private readonly UIntPtr length;

		public HaywireString(string stringVal)
		{
			if(!MemoryStrings.ContainsKey(stringVal))
			{
				value = stringVal.ToAsciiString();
			}
			else
			{
				value = MemoryStrings[stringVal];
			}

			length = new UIntPtr((uint)stringVal.Length);
		}
	}
}
