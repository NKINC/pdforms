using System;
using System.IO;
using iTextSharp.text;
/*
 * $Id:  $
 *
 * This file is part of the iText (R) project.
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
namespace iTextSharp.text.pdf {

    /**
     * Wrapper class for PdfCopy and PdfSmartCopy.
     * Allows you to concatenate existing PDF documents with much less code.
     */
    public class PdfConcatenate {
        /** The Document object for PdfCopy. */
        protected internal Document document;
        /** The actual PdfWriter */
        protected internal PdfCopy copy;
        
        /**
         * Creates an instance of the concatenation class.
         * @param os    the Stream for the PDF document
         */
        public PdfConcatenate(Stream os) : this(os, false) {
        }

        /**
         * Creates an instance of the concatenation class.
         * @param os    the Stream for the PDF document
         * @param smart do we want PdfCopy to detect redundant content?
         */
        public PdfConcatenate(Stream os, bool smart) {
            document = new Document();
            if (smart)
                copy = new PdfSmartCopy(document, os);
            else
                copy = new PdfCopy(document, os);   
        }
        
        /**
         * Adds the pages from an existing PDF document.
         * @param reader    the reader for the existing PDF document
         * @return          the number of pages that were added
         * @throws DocumentException
         * @throws IOException
         */
        virtual public int AddPages(PdfReader reader) {
            Open();
            int n = reader.NumberOfPages;
            for (int i = 1; i <= n; i++) {
                copy.AddPage(copy.GetImportedPage(reader, i));
            }
            copy.FreeReader(reader);
            reader.Close();
            return n;
        }
        
        /**
         * Gets the PdfCopy instance so that you can add bookmarks or change preferences before you close PdfConcatenate.
         */
        virtual public PdfCopy Writer {
            get {
                return copy;
            }
        }
        
        /**
         * Opens the document (if it isn't open already).
         * Opening the document is done implicitly.
         */
        virtual public void Open() {
            if (!document.IsOpen()) {
                document.Open();
            }
        }
        
        /**
         * We've finished writing the concatenated document.
         */
        virtual public void Close() {
            document.Close();
        }
    }
}
