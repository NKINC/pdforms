using System;
using System.IO;
/*
 * $Id$
 *
 * This file is part of the iText (R) project.
 * Copyright (c) 1998-2016 iText Group NV
 * BVBA Authors: Kevin Day, Bruno Lowagie, et al.
 *
 * This program is free software; you can redistribute it and/or modify it under
 * the terms of the GNU Affero General License version 3 as published by the
 * Free Software Foundation with the addition of the following permission added
 * to Section 15 as permitted in Section 7(a): FOR ANY PART OF THE COVERED WORK
 * IN WHICH THE COPYRIGHT IS OWNED BY ITEXT GROUP, ITEXT GROUP DISCLAIMS THE WARRANTY OF NON
 * INFRINGEMENT OF THIRD PARTY RIGHTS.
 *
 * This program is distributed in the hope that it will be useful, but WITHOUT
 * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
 * FOR A PARTICULAR PURPOSE. See the GNU Affero General License for more
 * details. You should have received a copy of the GNU Affero General License
 * along with this program; if not, see http://www.gnu.org/licenses or write to
 * the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
 * MA, 02110-1301 USA, or download the license from the following URL:
 * http://itextpdf.com/terms-of-use/
 *
 * The interactive user interfaces in modified source and object code versions
 * of this program must display Appropriate Legal Notices, as required under
 * Section 5 of the GNU Affero General License.
 *
 * In accordance with Section 7(b) of the GNU Affero General License, a covered
 * work must retain the producer line in every PDF that is created or
 * manipulated using iText.
 *
 * You can be released from the requirements of the license by purchasing a
 * commercial license. Buying such a license is mandatory as soon as you develop
 * commercial activities involving the iText software without disclosing the
 * source code of your own applications. These activities include: offering paid
 * services to customers as an ASP, serving PDFs on the fly in a web
 * application, shipping iText with a closed source product.
 *
 * For more information, please contact iText Software Corp. at this address:
 * sales@itextpdf.com
 */
namespace iTextSharp.text.io {

    /**
     * An input stream that uses a RandomAccessSource as it's underlying source 
     * @since 5.3.5
     */
    public class RASInputStream : Stream {
        /**
         * The source
         */
        private readonly IRandomAccessSource source;
        /**
         * The current position in the source
         */
        private long position = 0;

        /**
         * Creates an input stream based on the source
         * @param source the source
         */
        public RASInputStream(IRandomAccessSource source) {
            this.source = source;
        }

        public override bool CanRead {
            get { return true; }
        }

        public override bool CanSeek {
            get { return true; }
        }

        public override bool CanWrite {
            get { return false; }
        }

        public override void Flush() {
        }

        public override long Length {
            get { return source.Length; }
        }

        public override long Position {
            get {
                return position; ;
            }
            set {
                position = value;
            }
        }

        public override int Read(byte[] buffer, int offset, int len) {
            int count = source.Get(position, buffer, offset, len);
            if (count == -1)
                return 0;
            position += count;
            return count;
        }

        public override int ReadByte() {
            int c = source.Get(position);
            if (c >= 0)
                ++position;
            return c;
        }

        public override long Seek(long offset, SeekOrigin origin) {
            switch (origin) {
                case SeekOrigin.Begin:
                    position = offset;
                    break;
                case SeekOrigin.Current:
                    position += offset;
                    break;
                default:
                    position = offset + source.Length;
                    break;
            }
            return position;
        }

        public override void SetLength(long value) {
            throw new Exception("Not supported.");
        }

        public override void Write(byte[] buffer, int offset, int count) {
            throw new Exception("Not supported.");
        }
    }
}
