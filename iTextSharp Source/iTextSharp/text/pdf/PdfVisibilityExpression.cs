using System;
using iTextSharp.text.error_messages;

/*
 * $Id: PdfLayerMembership.java 4242 2010-01-02 23:22:20Z xlv $
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

namespace iTextSharp.text.pdf {

    /**
     * An array specifying a visibility expression, used to compute visibility
     * of content based on a set of optional content groups.
     * @since 5.0.2
     */
    public class PdfVisibilityExpression : PdfArray {

        /** A boolean operator. */
        public const int OR = 0;
        /** A boolean operator. */
        public const int AND = 1;
        /** A boolean operator. */
        public const int NOT = -1;
        
        /**
         * Creates a visibility expression.
         * @param type should be AND, OR, or NOT
         */
        public PdfVisibilityExpression(int type) : base() {
            switch(type) {
            case OR:
                base.Add(PdfName.OR);
                break;
            case AND:
                base.Add(PdfName.AND);
                break;
            case NOT:
                base.Add(PdfName.NOT);
                break;
            default:
                throw new ArgumentException(MessageLocalization.GetComposedMessage("illegal.ve.value"));    
            } 
        }

        /**
         * @see com.itextpdf.text.pdf.PdfArray#add(int, com.itextpdf.text.pdf.PdfObject)
         */
        public override void Add(int index, PdfObject element) {
            throw new ArgumentException(MessageLocalization.GetComposedMessage("illegal.ve.value"));
        }

        /**
         * @see com.itextpdf.text.pdf.PdfArray#add(com.itextpdf.text.pdf.PdfObject)
         */
        public override bool Add(PdfObject obj) {
            if (obj is PdfLayer)
                return base.Add(((PdfLayer)obj).Ref);
            if (obj is PdfVisibilityExpression)
                return base.Add(obj);
            throw new ArgumentException(MessageLocalization.GetComposedMessage("illegal.ve.value"));
        }

        /**
         * @see com.itextpdf.text.pdf.PdfArray#addFirst(com.itextpdf.text.pdf.PdfObject)
         */
        public override void AddFirst(PdfObject obj) {
            throw new ArgumentException(MessageLocalization.GetComposedMessage("illegal.ve.value"));
        }

        /**
         * @see com.itextpdf.text.pdf.PdfArray#add(float[])
         */
        public override bool Add(float[] values) {
            throw new ArgumentException(MessageLocalization.GetComposedMessage("illegal.ve.value"));
        }

        /**
         * @see com.itextpdf.text.pdf.PdfArray#add(int[])
         */
        public override bool Add(int[] values) {
            throw new ArgumentException(MessageLocalization.GetComposedMessage("illegal.ve.value"));
        }
        
    }
}
