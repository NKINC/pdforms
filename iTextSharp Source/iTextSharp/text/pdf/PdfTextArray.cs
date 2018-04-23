using System;
using System.Collections.Generic;

/*
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

namespace iTextSharp.text.pdf {

    /**
    * <CODE>PdfTextArray</CODE> defines an array with displacements and <CODE>PdfString</CODE>-objects.
    * <P>
    * A <CODE>TextArray</CODE> is used with the operator <VAR>TJ</VAR> in <CODE>PdfText</CODE>.
    * The first object in this array has to be a <CODE>PdfString</CODE>;
    * see reference manual version 1.3 section 8.7.5, pages 346-347.
    *       OR
    * see reference manual version 1.6 section 5.3.2, pages 378-379.
    */

    public class PdfTextArray{
        List<Object> arrayList = new List<Object>();
        
        // To emit a more efficient array, we consolidate
        // repeated numbers or strings into single array entries.
        // "add( 50 ); Add( -50 );" will REMOVE the combined zero from the array.
        // the alternative (leaving a zero in there) was Just Weird.
        // --Mark Storer, May 12, 2008
        private String lastStr;
        private float lastNum = float.NaN;
        
        // constructors
        public PdfTextArray(String str) {
            Add(str);
        }
        
        public PdfTextArray() {
        }
        
        /**
        * Adds a <CODE>PdfNumber</CODE> to the <CODE>PdfArray</CODE>.
        *
        * @param  number   displacement of the string
        */
        virtual public void Add(PdfNumber number) {
            Add((float)number.DoubleValue);
        }
        
        virtual public void Add(float number) {
            if (number != 0) {
                if (!float.IsNaN(lastNum)) {
                    lastNum += number;
                    if (lastNum != 0) {
                        ReplaceLast(lastNum);
                    } else {
                        arrayList.RemoveAt(arrayList.Count - 1);
                    }
                } else {
                    lastNum = number;
                    arrayList.Add(lastNum);
                }
                lastStr = null;
            }
            // adding zero doesn't modify the TextArray at all
        }
        
        virtual public void Add(String str) {
            if (str.Length > 0) {
                if (lastStr != null) {
                    lastStr = lastStr + str;
                    ReplaceLast(lastStr);
                } else {
                    lastStr = str;
                    arrayList.Add(lastStr);
                }
                lastNum = float.NaN;
            }
            // adding an empty string doesn't modify the TextArray at all
        }
        
        internal List<Object> ArrayList {
            get {
                return arrayList;
            }
        }
        
        private void ReplaceLast(Object obj) {
            // deliberately throw the IndexOutOfBoundsException if we screw up.
            arrayList[arrayList.Count - 1] = obj;
        }
    }
}
