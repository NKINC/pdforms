/*
 * $Id$
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

using System;
using System.IO;

namespace iTextSharp.testutils {
    public abstract class ITextTest {
        public virtual void RunTest() {
            Console.WriteLine("Starting test.");
            String outPdf = GetOutPdf();
            if (outPdf == null || outPdf.Length == 0)
                throw new InvalidOperationException("outPdf cannot be empty!");
            MakePdf(outPdf);
            AssertPdf(outPdf);
            ComparePdf(outPdf, GetCmpPdf());
            Console.WriteLine("Test complete.");
        }

        protected abstract void MakePdf(String outPdf);

        /**
         * Gets the name of the resultant PDF file.
         * This name will be passed to <code>makePdf</code>, <code>assertPdf</code> and <code>comparePdf</code> methods.
         * @return
         */
        protected abstract String GetOutPdf();

        protected virtual void AssertPdf(String outPdf) {
        }

        protected virtual void ComparePdf(String outPdf, String cmpPdf) {
        }

        /**
         * Gets the name of the compare PDF file.
         * This name will be passed to <code>comparePdf</code> method.
         * @return
         */
        protected virtual String GetCmpPdf() {
            return "";
        }

        protected virtual void DeleteDirectory(string path) {
            if (path == null)
                return;
            if (Directory.Exists(path)) {
                foreach (string d in Directory.GetDirectories(path)) {
                    DeleteDirectory(d);
                    Directory.Delete(d);
                }
                foreach (string f in Directory.GetFiles(path)) {
                    File.Delete(f);
                }
                Directory.Delete(path);
            }
        }

        protected virtual void DeleteFiles(String path) {
            if (path != null && Directory.Exists(path)) {
                foreach (String f in Directory.GetFiles(path)) {
                    File.Delete(f);
                }
            }
        }
    }
}
