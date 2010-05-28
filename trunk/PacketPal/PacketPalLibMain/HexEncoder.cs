using System;
using System.Collections.Generic;
using System.Text;

namespace Kopf.PacketPal.Util
{
    /**
     * This class was adapted for use in this project from
     * http://www.codeproject.com/csharp/HexEncoding.asp
     * 
     * This class is a utility to convert between hexadecimal
     * strings and bytes.
     */

    public static class HexEncoder
    {
        /*
         * Determine the number of bytes required for converting
         * a hex string.
         */
        public static int GetByteCount(string hexString)
        {
            int numHexChars = 0;
            char c;
            // remove all none A-F, 0-9, characters
            for (int i = 0; i < hexString.Length; i++)
            {
                c = hexString[i];
                if (IsHexDigit(c))
                    numHexChars++;
            }
            // if odd number of characters, discard last character
            if (numHexChars % 2 != 0)
            {
                numHexChars--;
            }
            return numHexChars / 2; // 2 characters per byte
        }

        /*
         * Convert a hex string to a byte array.
         */
        public static byte[] GetBytes(string hexString, out int discarded)
        {
            discarded = 0;
            string newString = "";
            char c;
            // remove all none A-F, 0-9, characters
            for (int i = 0; i < hexString.Length; i++)
            {
                c = hexString[i];
                if (IsHexDigit(c))
                    newString += c;
                else
                    discarded++;
            }
            // if odd number of characters, discard last character
            if (newString.Length % 2 != 0)
            {
                discarded++;
                newString = newString.Substring(0, newString.Length - 1);
            }

            int byteLength = newString.Length / 2;
            byte[] bytes = new byte[byteLength];
            string hex;
            int j = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                hex = new String(new Char[] { newString[j], newString[j + 1] });
                bytes[i] = HexToByte(hex);
                j = j + 2;
            }
            return bytes;
        }


        /*
         * Convert a byte array to a hexadecimal string.
         */
        public static string ToString(byte[] bytes)
        {
            string hexString = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    hexString += bytes[i].ToString("X2");
                }
            }
            return hexString;
        }


        /*
         * Verify a string is in hex format.
         */
        public static bool InHexFormat(string hexString)
        {
            bool hexFormat = true;

            foreach (char digit in hexString)
            {
                if (!IsHexDigit(digit))
                {
                    hexFormat = false;
                    break;
                }
            }
            return hexFormat;
        }


        /*
         * Verify the character is within range.
         */
        public static bool IsHexDigit(Char c)
        {
            int numChar;
            int numA = Convert.ToInt32('A');
            int num1 = Convert.ToInt32('0');
            c = Char.ToUpper(c);
            numChar = Convert.ToInt32(c);
            if (numChar >= numA && numChar < (numA + 6))
                return true;
            if (numChar >= num1 && numChar < (num1 + 10))
                return true;
            return false;
        }

        /*
         * Converty a single byte.
         */
        private static byte HexToByte(string hex)
        {
            if (hex.Length > 2 || hex.Length <= 0)
                throw new ArgumentException("hex must be 1 or 2 characters in length");
            byte newByte = byte.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            return newByte;
        }

        public static string PrePadHexString(string inString, int minLength)
        {
            while (inString.Length < minLength)
            {
                inString = "0" + inString;
            }
            return inString;
        }

        public static string ToString(int number)
        {
            return number.ToString("X");
        }

        public static string ToString(int number, int length)
        {
            return number.ToString("X" + length.ToString());
        }

        public static byte[] GetBytes(int number, out int discarded)
        {
            return GetBytes(ToString(number), out discarded);
        }

        public static byte[] GetBytes(int number, int length, out int discarded)
        {
            return GetBytes(ToString(number, length), out discarded);
        }
    }
}
