using System;
using System.IO;
/*
 * $Id: RAFRandomAccessSource.java 5550 2012-11-21 13:26:06Z blowagie $
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
     * 
     * A RandomAccessSource that uses a {@link RandomAccessFile} as it's source
     * Note: Unlike most of the RandomAccessSource implementations, this class is not thread safe
     */
    class RAFRandomAccessSource : IRandomAccessSource {
	    /**
	     * The source
	     */
	    private readonly FileStream raf;
    	
	    /**
	     * The length of the underling RAF.  Note that the length is cached at construction time to avoid the possibility
	     * of IOExceptions when reading the length.
	     */
	    private readonly long length;
    	
	    /**
	     * Creates this object
	     * @param raf the source for this RandomAccessSource
	     * @throws IOException if the RAF can't be read
	     */
	    public RAFRandomAccessSource(FileStream raf) {
		    this.raf = raf;
		    length = raf.Length;
	    }

	    /**
	     * {@inheritDoc}
	     */
	    // TODO: test to make sure we are handling the length properly (i.e. is raf.length() the last byte in the file, or one past the last byte?)
	    public virtual int Get(long position) {
		    if (position > length)
			    return -1;
    		
		    // Not thread safe!
            if(raf.Position != position)
		        raf.Seek(position, SeekOrigin.Begin);
    		
		    return raf.ReadByte();
	    }

	    /**
	     * {@inheritDoc}
	     */
	    public virtual int Get(long position, byte[] bytes, int off, int len) {
		    if (position > length)
			    return -1;
    		
		    // Not thread safe!
            if (raf.Position != position)
		        raf.Seek(position, SeekOrigin.Begin);

		    int n = raf.Read(bytes, off, len);
            return n == 0 ? -1 : n; //in .NET Streams return 0 on EOF, not -1
	    }

	    /**
	     * {@inheritDoc}
	     * Note: the length is determined when the {@link RAFRandomAccessSource} is constructed.  If the file length changes
	     * after construction, that change will not be reflected in this call.
	     */
	    public virtual long Length {
            get {
		        return length;
            }
	    }

	    /**
	     * Closes the underlying RandomAccessFile
	     */
	    public virtual void Close() {
		    raf.Close();
	    }

        virtual public void Dispose() {
            Close();
        }
    }
}
