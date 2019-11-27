using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace CoalMemory
{
	public class CMemory
	{
		public Process Process { get; private set; }

		private IntPtr _processHandle;

		[DllImport("kernel32.dll")]
		private static extern bool WriteProcessMemory(IntPtr ProcessHandle, IntPtr Address, byte[] Buffer, int nSize, IntPtr NumberOfBytesWritten);
		[DllImport("kernel32.dll")]
		private static extern bool ReadProcessMemory(IntPtr ProcessHandle, IntPtr Address, byte[] Buffer, int Size, IntPtr NumberOfBytesRead);

		public CMemory(Process process)
		{
			Process = process;
			_processHandle = Process.Handle;
		}

		private byte[] ReadMemory(IntPtr address, int size)
		{
			byte[] buffer = new byte[size];
			ReadProcessMemory(_processHandle, address, buffer, size, IntPtr.Zero);
			return buffer;
		}

		private bool WriteMemory(IntPtr address, byte[] data)
		{
			return WriteProcessMemory(_processHandle, address, data, data.Length, IntPtr.Zero);
		}

		public IntPtr GetLibraryAddress(string libraryName)
		{
			foreach (ProcessModule module in Process.Modules)
			{
				if (module.ModuleName == libraryName)
					return module.BaseAddress;
			}

			return IntPtr.Zero;
		}

		public double ReadDouble(IntPtr address)
		{
			var bytes = ReadMemory(address, sizeof(float));
			return BitConverter.ToDouble(bytes, 0);
		}

		public int ReadInt(IntPtr address)
		{
			var bytes = ReadMemory(address, sizeof(int));
			return BitConverter.ToInt32(bytes, 0);
		}

		public bool ReadBool(IntPtr address)
		{
			var bytes = ReadMemory(address, sizeof(bool));
			return BitConverter.ToBoolean(bytes, 0);
		}

		public string ReadStringASCII(IntPtr address, int strLen)
		{
			var bytes = ReadMemory(address, strLen * sizeof(char));
			return Encoding.ASCII.GetString(bytes);
		}

		public string ReadStringUTF8(IntPtr address, int strLen)
		{
			var bytes = ReadMemory(address, strLen * sizeof(char));
			return Encoding.UTF8.GetString(bytes);
		}

		public bool WriteMemory(IntPtr address, int value)
		{
			var data = BitConverter.GetBytes(value);
			return WriteMemory(address, data);
		}

		public bool WriteMemory(IntPtr address, bool value)
		{
			var data = BitConverter.GetBytes(value);
			return WriteMemory(address, data);
		}

		public bool WriteMemory(IntPtr address, double value)
		{
			var data = BitConverter.GetBytes(value);
			return WriteMemory(address, data);
		}

		public bool WriteStringASCII(IntPtr address, string value)
		{
			var data = Encoding.ASCII.GetBytes(value);
			return WriteMemory(address, data);
		}

		public bool WriteStringUTF8(IntPtr address, string value)
		{
			var data = Encoding.UTF8.GetBytes(value);
			return WriteMemory(address, data);
		}
	}
}
