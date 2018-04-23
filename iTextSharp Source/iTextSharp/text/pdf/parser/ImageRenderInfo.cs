/*
 * $Id$
 *
 * This file is part of the iText project.
 * Copyright (c) 1998-2016 iText Group NV
 * Authors: Kevin Day, Bruno Lowagie, Paulo Soares, et al.
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
namespace iTextSharp.text.pdf.parser {

    /**
     * Represents image data from a PDF
     * @since 5.0.1
     */
    public class ImageRenderInfo {
        /** The graphics state that was in effect when the image was rendered */
        private GraphicsState gs;
        /** A reference to the image XObject */
        private PdfIndirectReference refi;
        /** A reference to an inline image */
        private InlineImageInfo inlineImageInfo;
        /** the color space associated with the image */
        private PdfDictionary colorSpaceDictionary;
        /** the image object to be rendered, if it has been parsed already.  Null otherwise. */
        private PdfImageObject imageObject = null;

        private ImageRenderInfo(GraphicsState gs, PdfIndirectReference refi, PdfDictionary colorSpaceDictionary)
        {
            this.gs = gs;
            this.refi = refi;
            this.inlineImageInfo = null;
            this.colorSpaceDictionary = colorSpaceDictionary;
        }

        private ImageRenderInfo(GraphicsState gs, InlineImageInfo inlineImageInfo, PdfDictionary colorSpaceDictionary)
        {
            this.gs = gs;
            this.refi = null;
            this.inlineImageInfo = inlineImageInfo;
            this.colorSpaceDictionary = colorSpaceDictionary;
        }
        
        /**
         * Create an ImageRenderInfo object based on an XObject (this is the most common way of including an image in PDF)
         * @param ctm the coordinate transformation matrix at the time the image is rendered
         * @param ref a reference to the image XObject
         * @return the ImageRenderInfo representing the rendered XObject
         * @since 5.0.1
         */
        public static ImageRenderInfo CreateForXObject(GraphicsState gs, PdfIndirectReference refi, PdfDictionary colorSpaceDictionary)
        {
            return new ImageRenderInfo(gs, refi, colorSpaceDictionary);
        }
        
        /**
         * Create an ImageRenderInfo object based on inline image data.  This is nowhere near completely thought through
         * and really just acts as a placeholder.
         * @param ctm the coordinate transformation matrix at the time the image is rendered
         * @param imageObject the image object representing the inline image
         * @return the ImageRenderInfo representing the rendered embedded image
         * @since 5.0.1
         */
        protected internal static ImageRenderInfo CreateForEmbeddedImage(GraphicsState gs, InlineImageInfo inlineImageInfo, PdfDictionary colorSpaceDictionary)
        {
            ImageRenderInfo renderInfo = new ImageRenderInfo(gs, inlineImageInfo, colorSpaceDictionary);
            return renderInfo;
        }
        
        /**
         * Gets an object containing the image dictionary and bytes.
         * @return an object containing the image dictionary and byte[]
         * @since 5.0.2
         */
        virtual public PdfImageObject GetImage() {
            PrepareImageObject();
            return imageObject;
        }
        
        private void PrepareImageObject() {
            if (imageObject != null)
                return;
            
            if (refi != null){
                PRStream stream = (PRStream)PdfReader.GetPdfObject(refi);
                imageObject = new PdfImageObject(stream, colorSpaceDictionary);
            } else if (inlineImageInfo != null){
                imageObject = new PdfImageObject(inlineImageInfo.ImageDictionary, inlineImageInfo.Samples, colorSpaceDictionary);
            }
        }
        
        /**
         * @return a vector in User space representing the start point of the xobject
         */
        virtual public Vector GetStartPoint(){ 
            return new Vector(0, 0, 1).Cross(gs.ctm); 
        }

        /**
         * @return The coordinate transformation matrix active when this image was rendered.  Coordinates are in User space.
         * @since 5.0.3
         */
        virtual public Matrix GetImageCTM(){
            return gs.ctm;
        }
        
        /**
         * @return the size of the image, in User space units
         */
        virtual public float GetArea(){
            // the image space area is 1, so we multiply that by the determinant of the CTM to get the transformed area
            return gs.ctm.GetDeterminant();
        }
        
        /**
         * @return an indirect reference to the image
         * @since 5.0.2
         */
        virtual public PdfIndirectReference GetRef() {
            return refi;
        }

        /**
        * @return the current fill color from the graphics state at the time this render operation occured
        * @since 5.5.7
        */
        public BaseColor GetCurrentFillColor() {
            return gs.fillColor;
        }

    }
}
