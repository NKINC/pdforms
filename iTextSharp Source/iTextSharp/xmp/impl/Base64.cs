//Copyright (c) 2006, Adobe Systems Incorporated
//All rights reserved.
//
//        Redistribution and use in source and binary forms, with or without
//        modification, are permitted provided that the following conditions are met:
//        1. Redistributions of source code must retain the above copyright
//        notice, this list of conditions and the following disclaimer.
//        2. Redistributions in binary form must reproduce the above copyright
//        notice, this list of conditions and the following disclaimer in the
//        documentation and/or other materials provided with the distribution.
//        3. All advertising materials mentioning features or use of this software
//        must display the following acknowledgement:
//        This product includes software developed by the Adobe Systems Incorporated.
//        4. Neither the name of the Adobe Systems Incorporated nor the
//        names of its contributors may be used to endorse or promote products
//        derived from this software without specific prior written permission.
//
//        THIS SOFTWARE IS PROVIDED BY ADOBE SYSTEMS INCORPORATED ''AS IS'' AND ANY
//        EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
//        WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
//        DISCLAIMED. IN NO EVENT SHALL ADOBE SYSTEMS INCORPORATED BE LIABLE FOR ANY
//        DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
//        (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
//        LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
//        ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
//        (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//        SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
//        http://www.adobe.com/devnet/xmp/library/eula-xmp-library-java.html

using System;

namespace iTextSharp.xmp.impl {
    /// <summary>
    /// A utility class to perform base64 encoding and decoding as specified
    /// in RFC-1521. See also RFC 1421.
    /// 
    /// @version     $Revision: 1.4 $
    /// </summary>
    public class Base64 {
        /// <summary>
        /// marker for invalid bytes </summary>
        private const byte INVALID = 0xFF;

        /// <summary>
        /// marker for accepted whitespace bytes </summary>
        private const byte WHITESPACE = 0xFE;

        /// <summary>
        /// marker for an equal symbol </summary>
        private const byte EQUAL = 0xFD;

        private static readonly byte[] base64 = {
                                                    (byte) 'A', (byte) 'B', (byte) 'C', (byte) 'D', (byte) 'E',
                                                    (byte) 'F',
                                                    (byte) 'G', (byte) 'H', (byte) 'I', (byte) 'J', (byte) 'K',
                                                    (byte) 'L',
                                                    (byte) 'M', (byte) 'N', (byte) 'O', (byte) 'P', (byte) 'Q',
                                                    (byte) 'R',
                                                    (byte) 'S', (byte) 'T', (byte) 'U', (byte) 'V', (byte) 'W',
                                                    (byte) 'X',
                                                    (byte) 'Y', (byte) 'Z', (byte) 'a', (byte) 'b', (byte) 'c',
                                                    (byte) 'd',
                                                    (byte) 'e', (byte) 'f', (byte) 'g', (byte) 'h', (byte) 'i',
                                                    (byte) 'j',
                                                    (byte) 'k', (byte) 'l', (byte) 'm', (byte) 'n', (byte) 'o',
                                                    (byte) 'p',
                                                    (byte) 'q', (byte) 'r', (byte) 's', (byte) 't', (byte) 'u',
                                                    (byte) 'v',
                                                    (byte) 'w', (byte) 'x', (byte) 'y', (byte) 'z', (byte) '0',
                                                    (byte) '1',
                                                    (byte) '2', (byte) '3', (byte) '4', (byte) '5', (byte) '6',
                                                    (byte) '7',
                                                    (byte) '8', (byte) '9', (byte) '+', (byte) '/'
                                                };

        private static readonly byte[] Ascii = new byte[255];

        static Base64() {
            // not valid bytes
            for (int idx = 0; idx < 255; idx++) {
                Ascii[idx] = INVALID;
            }
            // valid bytes
            for (int idx = 0; idx < base64.Length; idx++) {
                Ascii[base64[idx]] = (byte) idx;
            }
            // whitespaces
            Ascii[0x09] = WHITESPACE;
            Ascii[0x0A] = WHITESPACE;
            Ascii[0x0D] = WHITESPACE;
            Ascii[0x20] = WHITESPACE;

            // trailing equals
            Ascii[0x3d] = EQUAL;
        }


        /// <summary>
        /// Encode the given byte[].
        /// </summary>
        /// <param name="src"> the source string. </param>
        /// <returns> the base64-encoded data. </returns>
        public static byte[] Encode(byte[] src) {
            return Encode(src, 0);
        }


        /// <summary>
        /// Encode the given byte[].
        /// </summary>
        /// <param name="src"> the source string. </param>
        /// <param name="lineFeed"> a linefeed is added after <code>linefeed</code> characters;
        ///            must be dividable by four; 0 means no linefeeds </param>
        /// <returns> the base64-encoded data. </returns>
        public static byte[] Encode(byte[] src, int lineFeed) {
            // linefeed must be dividable by 4
            lineFeed = lineFeed/4*4;
            if (lineFeed < 0) {
                lineFeed = 0;
            }

            // determine code length
            int codeLength = ((src.Length + 2)/3)*4;
            if (lineFeed > 0) {
                codeLength += (codeLength - 1)/lineFeed;
            }

            byte[] dst = new byte[codeLength];
            int bits24;
            int bits6;
            //
            // Do 3-byte to 4-byte conversion + 0-63 to ascii printable conversion
            //
            int didx = 0;
            int sidx = 0;
            int lf = 0;
            while (sidx + 3 <= src.Length) {
                bits24 = (src[sidx++] & 0xFF) << 16;
                bits24 |= (src[sidx++] & 0xFF) << 8;
                bits24 |= (src[sidx++] & 0xFF) << 0;
                bits6 = (bits24 & 0x00FC0000) >> 18;
                dst[didx++] = base64[bits6];
                bits6 = (bits24 & 0x0003F000) >> 12;
                dst[didx++] = base64[bits6];
                bits6 = (bits24 & 0x00000FC0) >> 6;
                dst[didx++] = base64[bits6];
                bits6 = (bits24 & 0x0000003F);
                dst[didx++] = base64[bits6];

                lf += 4;
                if (didx < codeLength && lineFeed > 0 && lf%lineFeed == 0) {
                    dst[didx++] = 0x0A;
                }
            }
            if (src.Length - sidx == 2) {
                bits24 = (src[sidx] & 0xFF) << 16;
                bits24 |= (src[sidx + 1] & 0xFF) << 8;
                bits6 = (bits24 & 0x00FC0000) >> 18;
                dst[didx++] = base64[bits6];
                bits6 = (bits24 & 0x0003F000) >> 12;
                dst[didx++] = base64[bits6];
                bits6 = (bits24 & 0x00000FC0) >> 6;
                dst[didx++] = base64[bits6];
                dst[didx] = (byte) '=';
            }
            else if (src.Length - sidx == 1) {
                bits24 = (src[sidx] & 0xFF) << 16;
                bits6 = (bits24 & 0x00FC0000) >> 18;
                dst[didx++] = base64[bits6];
                bits6 = (bits24 & 0x0003F000) >> 12;
                dst[didx++] = base64[bits6];
                dst[didx++] = (byte) '=';
                dst[didx] = (byte) '=';
            }
            return dst;
        }


        /// <summary>
        /// Encode the given string. </summary>
        /// <param name="src"> the source string. </param>
        /// <returns> the base64-encoded string. </returns>
        public static string Encode(string src) {
            return GetString(Encode(GetBytes(src)));
        }


        /// <summary>
        /// Decode the given byte[].
        /// </summary>
        /// <param name="src">
        ///            the base64-encoded data. </param>
        /// <returns> the decoded data. </returns>
        public static byte[] Decode(byte[] src) {
            //
            // Do ascii printable to 0-63 conversion.
            //
            int sidx;
            int srcLen = 0;
            for (sidx = 0; sidx < src.Length; sidx++) {
                byte val = Ascii[src[sidx]];
                if (val >= 0) {
                    src[srcLen++] = val;
                }
                else if (val == INVALID) {
                    throw new ArgumentException("Invalid base 64 string");
                }
            }

            //
            // Trim any padding.
            //
            while (srcLen > 0 && src[srcLen - 1] == EQUAL) {
                srcLen--;
            }
            byte[] dst = new byte[srcLen*3/4];

            //
            // Do 4-byte to 3-byte conversion.
            //
            int didx;
            for (sidx = 0, didx = 0; didx < dst.Length - 2; sidx += 4, didx += 3) {
                dst[didx] = (byte) (((src[sidx] << 2) & 0xFF) | (((int) ((uint) src[sidx + 1] >> 4)) & 0x03));
                dst[didx + 1] = (byte) (((src[sidx + 1] << 4) & 0xFF) | (((int) ((uint) src[sidx + 2] >> 2)) & 0x0F));
                dst[didx + 2] = (byte) (((src[sidx + 2] << 6) & 0xFF) | ((src[sidx + 3]) & 0x3F));
            }
            if (didx < dst.Length) {
                dst[didx] = (byte) (((src[sidx] << 2) & 0xFF) | (((int) ((uint) src[sidx + 1] >> 4)) & 0x03));
            }
            if (++didx < dst.Length) {
                dst[didx] = (byte) (((src[sidx + 1] << 4) & 0xFF) | (((int) ((uint) src[sidx + 2] >> 2)) & 0x0F));
            }
            return dst;
        }


        /// <summary>
        /// Decode the given string.
        /// </summary>
        /// <param name="src"> the base64-encoded string. </param>
        /// <returns> the decoded string. </returns>
        public static string Decode(string src) {
            return GetString(Decode(GetBytes(src)));
        }

        private static byte[] GetBytes(string str) {
            byte[] bytes = new byte[str.Length*sizeof (char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private static string GetString(byte[] bytes) {
            char[] chars = new char[bytes.Length/sizeof (char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}
