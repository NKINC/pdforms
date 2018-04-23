using System;

/*
 * $Id$
 * 
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
     *
     * @author  Paulo Soares
     */
    public class CMYKColor : ExtendedColor {

        float ccyan;
        float cmagenta;
        float cyellow;
        float cblack;

        public CMYKColor(int intCyan, int intMagenta, int intYellow, int intBlack) :
            this((float)intCyan / 255f, (float)intMagenta / 255f, (float)intYellow / 255f, (float)intBlack / 255f) {}

        public CMYKColor(float floatCyan, float floatMagenta, float floatYellow, float floatBlack) :
            base(TYPE_CMYK, 1f - floatCyan - floatBlack, 1f - floatMagenta - floatBlack, 1f - floatYellow - floatBlack) {
            ccyan = Normalize(floatCyan);
            cmagenta = Normalize(floatMagenta);
            cyellow = Normalize(floatYellow);
            cblack = Normalize(floatBlack);
        }
    
        virtual public float Cyan {
            get {
                return ccyan;
            }
        }

        virtual public float Magenta {
            get {
                return cmagenta;
            }
        }

        virtual public float Yellow {
            get {
                return cyellow;
            }
        }

        virtual public float Black {
            get {
                return cblack;
            }
        }

        public override bool Equals(Object obj) {
            if (!(obj is CMYKColor))
                return false;
            CMYKColor c2 = (CMYKColor)obj;
            return (ccyan == c2.ccyan && cmagenta == c2.cmagenta && cyellow == c2.cyellow && cblack == c2.cblack);
        }
    
        public override int GetHashCode() {
            return ccyan.GetHashCode() ^ cmagenta.GetHashCode() ^ cyellow.GetHashCode() ^ cblack.GetHashCode();
        }
    }
}
