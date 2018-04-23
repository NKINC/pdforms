using System;
using iTextSharp.text.error_messages;

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
    /** This class takes 2 barcodes, an EAN/UPC and a supplemental
     * and creates a single barcode with both combined in the
     * expected layout. The UPC/EAN should have a positive text
      * baseline and the supplemental a negative one (in the supplemental
     * the text is on the top of the barcode.<p>
     * The default parameters are:
     * <pre>
     *n = 8; // horizontal distance between the two barcodes
     * </pre>
     *
     * @author Paulo Soares
     */
    public class BarcodeEANSUPP : Barcode {
    
        /** The barcode with the EAN/UPC.
         */    
        protected Barcode ean;
        /** The barcode with the supplemental.
         */    
        protected Barcode supp;
    
        /** Creates new combined barcode.
         * @param ean the EAN/UPC barcode
         * @param supp the supplemental barcode
         */
        public BarcodeEANSUPP(Barcode ean, Barcode supp) {
            n = 8; // horizontal distance between the two barcodes
            this.ean = ean;
            this.supp = supp;
        }
    
        /** Gets the maximum area that the barcode and the text, if
         * any, will occupy. The lower left corner is always (0, 0).
         * @return the size the barcode occupies.
         */
        public override Rectangle BarcodeSize {
            get {
                Rectangle rect = ean.BarcodeSize;
                rect.Right = rect.Width + supp.BarcodeSize.Width + n;
                return rect;
            }
        }
    
        /** Places the barcode in a <CODE>PdfContentByte</CODE>. The
         * barcode is always placed at coodinates (0, 0). Use the
         * translation matrix to move it elsewhere.<p>
         * The bars and text are written in the following colors:<p>
         * <P><TABLE BORDER=1>
         * <TR>
         *   <TH><P><CODE>barColor</CODE></TH>
         *   <TH><P><CODE>textColor</CODE></TH>
         *   <TH><P>Result</TH>
         *   </TR>
         * <TR>
         *   <TD><P><CODE>null</CODE></TD>
         *   <TD><P><CODE>null</CODE></TD>
         *   <TD><P>bars and text painted with current fill color</TD>
         *   </TR>
         * <TR>
         *   <TD><P><CODE>barColor</CODE></TD>
         *   <TD><P><CODE>null</CODE></TD>
         *   <TD><P>bars and text painted with <CODE>barColor</CODE></TD>
         *   </TR>
         * <TR>
         *   <TD><P><CODE>null</CODE></TD>
         *   <TD><P><CODE>textColor</CODE></TD>
         *   <TD><P>bars painted with current color<br>text painted with <CODE>textColor</CODE></TD>
         *   </TR>
         * <TR>
         *   <TD><P><CODE>barColor</CODE></TD>
         *   <TD><P><CODE>textColor</CODE></TD>
         *   <TD><P>bars painted with <CODE>barColor</CODE><br>text painted with <CODE>textColor</CODE></TD>
         *   </TR>
         * </TABLE>
         * @param cb the <CODE>PdfContentByte</CODE> where the barcode will be placed
         * @param barColor the color of the bars. It can be <CODE>null</CODE>
         * @param textColor the color of the text. It can be <CODE>null</CODE>
         * @return the dimensions the barcode occupies
         */
        public override Rectangle PlaceBarcode(PdfContentByte cb, BaseColor barColor, BaseColor textColor) {
            if (supp.Font != null)
                supp.BarHeight = ean.BarHeight + supp.Baseline - supp.Font.GetFontDescriptor(BaseFont.CAPHEIGHT, supp.Size);
            else
                supp.BarHeight = ean.BarHeight;
            Rectangle eanR = ean.BarcodeSize;
            cb.SaveState();
            ean.PlaceBarcode(cb, barColor, textColor);
            cb.RestoreState();
            cb.SaveState();
            cb.ConcatCTM(1, 0, 0, 1, eanR.Width + n, eanR.Height - ean.BarHeight);
            supp.PlaceBarcode(cb, barColor, textColor);
            cb.RestoreState();
            return this.BarcodeSize;
        }

#if DRAWING
        public override System.Drawing.Image CreateDrawingImage(System.Drawing.Color foreground, System.Drawing.Color background) {
            throw new InvalidOperationException(MessageLocalization.GetComposedMessage("the.two.barcodes.must.be.composed.externally"));
        }
#endif// DRAWING
    }
}
