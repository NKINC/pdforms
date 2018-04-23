using System;
using System.Collections.Generic;
using System.util;
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

namespace iTextSharp.text {

    /**
    * Wrapper that allows to add properties to 'basic building block' objects.
    * Before iText 1.5 every 'basic building block' implemented the MarkupAttributes interface.
    * By setting attributes, you could add markup to the corresponding XML and/or HTML tag.
    * This functionality was hardly used by anyone, so it was removed, and replaced by
    * the MarkedObject functionality.
    *
    * @deprecated since 5.5.9. This class is no longer used.
    */

    [Obsolete]
    public class MarkedObject : IElement {

        /** The element that is wrapped in a MarkedObject. */
        protected internal IElement element;

        /** Contains extra markupAttributes */
        protected internal Properties markupAttributes = new Properties();
            
        /**
        * This constructor is for internal use only.
        */
        protected MarkedObject() {
            element = null;
        }
        
        /**
        * Creates a MarkedObject.
        */
        public MarkedObject(IElement element) {
            this.element = element;
        }
        
        /**
        * Gets all the chunks in this element.
        *
        * @return  an <CODE>ArrayList</CODE>
        */
        public virtual IList<Chunk> Chunks {
            get {
                return element.Chunks;
            }
        }

        /**
        * Processes the element by adding it (or the different parts) to an
        * <CODE>ElementListener</CODE>.
        *
        * @param       listener        an <CODE>ElementListener</CODE>
        * @return <CODE>true</CODE> if the element was processed successfully
        */
        public virtual bool Process(IElementListener listener) {
            try {
                return listener.Add(element);
            }
            catch (DocumentException) {
                return false;
            }
        }
        
        /**
        * Gets the type of the text element.
        *
        * @return  a type
        */
        public virtual int Type {
            get {
                return Element.MARKED;
            }
        }

        /**
        * @see com.lowagie.text.Element#isContent()
        * @since   iText 2.0.8
        */
        virtual public bool IsContent() {
            return true;
        }

        /**
        * @see com.lowagie.text.Element#isNestable()
        * @since   iText 2.0.8
        */
        virtual public bool IsNestable() {
            return true;
        }

        /**
        * @return the markupAttributes
        */
        public virtual Properties MarkupAttributes {
            get {
                return markupAttributes;
            }
        }
        
        public virtual void SetMarkupAttribute(String key, String value) {
            markupAttributes.Add(key, value);
        }

    }
}
