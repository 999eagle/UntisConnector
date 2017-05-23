using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace UntisLibrary
{
	class Helper
	{
		public static int CharToBase32Value(char c)
		{
			if ('A' <= c && c <= 'Z')
				return c - 'A';
			if ('2' <= c && c <= '7')
				return c - '2' + 26;
			if ('a' <= c && c <= 'z')
				return c - 'a';
			throw new Exception("");
		}

		public static byte[] Base32StringToBytes(string str)
		{
			str = str.TrimEnd('=');
			int length = str.Length * 5 / 8;
			byte[] data = new byte[length];
			byte currentByte = 0, bitsRemaining = 8;
			int idx = 0;
			for (int i = 0; i < str.Length; i++)
			{
				int b32 = CharToBase32Value(str[i]);
				if (bitsRemaining > 5)
				{
					currentByte |= (byte)(b32 << (bitsRemaining - 5));
					bitsRemaining -= 5;
				}
				else
				{
					currentByte |= (byte)(b32 >> (5 - bitsRemaining));
					data[idx++] = currentByte;
					currentByte = (byte)(b32 << (3 + bitsRemaining));
					bitsRemaining += 3;
				}
			}
			if (idx != length)
			{
				data[idx] = bitsRemaining;
			}
			return data;
		}

		public static int CalcOTP(string key, long time)
		{
			time /= 30;
			var keyData = Base32StringToBytes(key);
			var timeData = BitConverter.GetBytes(time).Reverse().ToArray();

			var result = new HMACSHA1(keyData).ComputeHash(timeData);

			int i = (int)(result.Last() & 15);
			return ((int)(result[i] & 127) << 24 | (int)(result[i + 1] & 255) << 16 | (int)(result[i + 2] & 255) << 8 | (int)(result[i + 3] & 255)) % 1000000;
		}
	}
}
