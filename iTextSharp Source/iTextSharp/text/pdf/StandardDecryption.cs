using System;
using iTextSharp.text.pdf.crypto;
/*
 * $Id$
 *
 * This file is part of the iText project.
 * Copyright (c) 1998-2016 iText Group NV
 * Authors: Bruno Lowagie, Paulo Soares, et al.
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License version 3
 * as published by the Free Software Foundation with the addition of the
 * following permission added to Section 15 as permitted in Section 7(a):
 * FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
 * ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
 * OF THIRD PARTY RIGHTS
 *
 * This program is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU Affero General Public License for more details.
 * You should have received a copy of the GNU Affero General Public License
 * along with this program; if not, see http://www.gnu.org/licenses or write to
 * the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
 * Boston, MA, 02110-1301 USA, or download the license from the following URL:
 * http://itextpdf.com/terms-of-use/
 *
 * The interactive user interfaces in modified source and object code versions
 * of this program must display Appropriate Legal Notices, as required under
 * Section 5 of the GNU Affero General Public License.
 *
 * In accordance with Section 7(b) of the GNU Affero General Public License,
 * a covered work must retain the producer line in every PDF that is created
 * or manipulated using iText.
 *
 * You can be released from the requirements of the license by purchasing
 * a commercial license. Buying such a license is mandatory as soon as you
 * develop commercial activities involving the iText software without
 * disclosing the source code of your own applications.
 * These activities include: offering paid services to customers as an ASP,
 * serving PDFs on the fly in a web application, shipping iText with a closed
 * source product.
 *
 * For more information, please contact iText Software Corp. at this
 * address: sales@itextpdf.com
 */

namespace iTextSharp.text.pdf.crypto {

    public class StandardDecryption {
        protected ARCFOUREncryption arcfour;
        protected AESCipher cipher;
        private byte[] key;
        private const int AES_128 = 4;
        private const int AES_256 = 5;
        private bool aes;
        private bool initiated;
        private byte[] iv = new byte[16];
        private int ivptr;

        /** Creates a new instance of StandardDecryption */
        public StandardDecryption(byte[] key, int off, int len, int revision) {
            aes = (revision == AES_128 || revision == AES_256);
            if (aes) {
                this.key = new byte[len];
                System.Array.Copy(key, off, this.key, 0, len);
            }
            else {
                arcfour = new ARCFOUREncryption();
                arcfour.PrepareARCFOURKey(key, off, len);
            }
        }
        
        virtual public byte[] Update(byte[] b, int off, int len) {
            if (aes) {
                if (initiated)
                    return cipher.Update(b, off, len);
                else {
                    int left = Math.Min(iv.Length - ivptr, len);
                    System.Array.Copy(b, off, iv, ivptr, left);
                    off += left;
                    len -= left;
                    ivptr += left;
                    if (ivptr == iv.Length) {
                        cipher = new AESCipher(false, key, iv);
                        initiated = true;
                        if (len > 0)
                            return cipher.Update(b, off, len);
                    }
                    return null;
                }
            }
            else {
                byte[] b2 = new byte[len];
                arcfour.EncryptARCFOUR(b, off, len, b2, 0);
                return b2;
            }
        }
        
        virtual public byte[] Finish() {
            if (aes) {
                return cipher.DoFinal();
            }
            else
                return null;
        }
    }
}
