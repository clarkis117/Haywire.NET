using HaywireNet.Bindings.Unsafe.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace HaywireNet.Bindings.Extensions
{
	public static class String
	{
		/*
		public static byte* ToAsciiNullTerm(this string clrString)
		{
			var nullTermString = clrString + "\0";
			var length = nullTermString.Length;

			byte* bytes = (byte*)Marshal.AllocHGlobal(length);

			fixed (char* chars = nullTermString)
			{
				System.Text.Encoding.ASCII.GetBytes(chars, length, bytes, length);
			}

			return bytes;
		}
		*/

		public static IntPtr ToAsciiString(this string clrString)
		{
			return Marshal.StringToHGlobalAnsi(clrString);
		}

		/*
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ConvertToStack(byte* stackarray, byte[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				*stackarray = array[i];

				stackarray++;
			}
		}
		*/

		public static byte[] ToAsciiArray(this string clrString)
		{
			return System.Text.Encoding.ASCII.GetBytes(clrString);
		}

		public static byte[] ToAsciiNullArray(this string clrString)
		{
			return System.Text.Encoding.ASCII.GetBytes(clrString);
		}

		/*
		public static byte* ToUtf8NullTerm(this string clrString)
		{
			var nullTermString = clrString + "\0";
			var length = nullTermString.Length;

			byte* bytes = (byte*)Marshal.AllocHGlobal(length);

			fixed (char* chars = nullTermString)
			{
				System.Text.Encoding.UTF8.GetBytes(chars, length, bytes, length);
			}

			return bytes;
		}
		*/

		/*
		public static HaywireString* GetHwStringASCII(this string clrString)
		{
			var length = clrString.Length;

			byte* bytes = (byte*)Marshal.AllocHGlobal(length);

			fixed (char* chars = clrString)
			{
				System.Text.Encoding.ASCII.GetBytes(chars, length, bytes, length);
			}

			var hwpointer = (HaywireString*)Marshal.AllocHGlobal(sizeof(HaywireString));

			hwpointer->length = (uint)length;
			hwpointer->value = bytes;

			return hwpointer;
		}

		public static void SetStringASCII(this string clrString, out HaywireString hwString)
		{
			var length = clrString.Length;

			byte* bytes = (byte*)Marshal.AllocHGlobal(length);

			fixed (char* chars = clrString)
			{
				System.Text.Encoding.ASCII.GetBytes(chars, length, bytes, length);
			}

			hwString.length = (uint)length;
			hwString.value = bytes;
		}

		public static void SetStringASCII(this string clrString, out Safe.HaywireString hwString)
		{
			var length = clrString.Length;

			var bytes = System.Text.Encoding.ASCII.GetBytes(clrString);

			hwString.length = (uint)length; // + 1;
			hwString.value = bytes;


			var length = clrString.Length;

			byte* bytes = (byte*)Marshal.AllocHGlobal(length);

			fixed (char* chars = clrString)
			{
				System.Text.Encoding.ASCII.GetBytes(chars, length, bytes, length);
			}

			hwString.length = (uint)length;
			hwString.value = bytes;
		}

		public static void SetStringUTF8(this string clrString, out HaywireString hwString)
		{
			var length = clrString.Length;

			byte* bytes = (byte*)Marshal.AllocHGlobal(length);

			fixed (char* chars = clrString)
			{
				System.Text.Encoding.UTF8.GetBytes(chars, length, bytes, length);
			}

			hwString.length = (uint)length;
			hwString.value = bytes;
		}
*/
	}
}
