using com.itextpdf.text.pdf;
using iTextSharp.text.pdf;

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
    /// <summary>
    /// A ListItem is a Paragraph
    /// that can be added to a List.
    /// </summary>
    /// <example>
    /// <B>Example 1:</B>
    /// <code>
    /// List list = new List(true, 20);
    /// list.Add(<strong>new ListItem("First line")</strong>);
    /// list.Add(<strong>new ListItem("The second line is longer to see what happens once the end of the line is reached. Will it start on a new line?")</strong>);
    /// list.Add(<strong>new ListItem("Third line")</strong>);
    /// </code>
    /// 
    /// The result of this code looks like this:
    /// <OL>
    ///        <LI>
    ///            First line
    ///        </LI>
    ///        <LI>
    ///            The second line is longer to see what happens once the end of the line is reached. Will it start on a new line?
    ///        </LI>
    ///        <LI>
    ///            Third line
    ///        </LI>
    ///    </OL>
    ///    
    /// <B>Example 2:</B>
    /// <code>
    /// List overview = new List(false, 10);
    /// overview.Add(<strong>new ListItem("This is an item")</strong>);
    /// overview.Add("This is another item");
    /// </code>
    /// 
    /// The result of this code looks like this:
    /// <UL>
    ///        <LI>
    ///            This is an item
    ///        </LI>
    ///        <LI>
    ///            This is another item
    ///        </LI>
    ///    </UL>
    /// </example>
    /// <seealso cref="T:iTextSharp.text.Element"/>
    /// <seealso cref="T:iTextSharp.text.List"/>
    /// <seealso cref="T:iTextSharp.text.Paragraph"/>
    public class ListItem : Paragraph {
    
        // membervariables
    
        /// <summary> this is the symbol that wil proceed the listitem. </summary>
        protected Chunk symbol;

        private ListBody listBody = null;
        private ListLabel listLabel = null;

        // constructors
    
        /// <summary>
        /// Constructs a ListItem.
        /// </summary>
        public ListItem()
            : base()
        {
            Role = PdfName.LI;
        }
    
        /// <summary>
        ///    Constructs a ListItem with a certain leading.
        /// </summary>
        /// <param name="leading">the leading</param>
        public ListItem(float leading)
            : base(leading)
        {
            Role = PdfName.LI;
        }
    
        /// <summary>
        /// Constructs a ListItem with a certain Chunk.
        /// </summary>
        /// <param name="chunk">a Chunk</param>
        public ListItem(Chunk chunk)
            : base(chunk)
        {
            Role = PdfName.LI;
        }
    
        /// <summary>
        /// Constructs a ListItem with a certain string.
        /// </summary>
        /// <param name="str">a string</param>
        public ListItem(string str)
            : base(str)
        {
            Role = PdfName.LI;
        }
    
        /// <summary>
        /// Constructs a ListItem with a certain string
        /// and a certain Font.
        /// </summary>
        /// <param name="str">a string</param>
        /// <param name="font">a string</param>
        public ListItem(string str, Font font)
            : base(str, font)
        {
            Role = PdfName.LI;
        }
    
        /// <summary>
        /// Constructs a ListItem with a certain Chunk
        /// and a certain leading.
        /// </summary>
        /// <param name="leading">the leading</param>
        /// <param name="chunk">a Chunk</param>
        public ListItem(float leading, Chunk chunk)
            : base(leading, chunk)
        {
            Role = PdfName.LI;
        }
    
        /// <summary>
        /// Constructs a ListItem with a certain string
        /// and a certain leading.
        /// </summary>
        /// <param name="leading">the leading</param>
        /// <param name="str">a string</param>
        public ListItem(float leading, string str)
            : base(leading, str)
        {
            Role = PdfName.LI;
        }
    
        /**
         * Constructs a ListItem with a certain leading, string
         * and Font.
         *
         * @param    leading        the leading
         * @param    string        a string
         * @param    font        a Font
         */
        /// <summary>
        /// Constructs a ListItem with a certain leading, string
        /// and Font.
        /// </summary>
        /// <param name="leading">the leading</param>
        /// <param name="str">a string</param>
        /// <param name="font">a Font</param>
        public ListItem(float leading, string str, Font font)
            : base(leading, str, font)
        {
            Role = PdfName.LI;
        }
    
        /// <summary>
        /// Constructs a ListItem with a certain Phrase.
        /// </summary>
        /// <param name="phrase">a Phrase</param>
        public ListItem(Phrase phrase)
            : base(phrase)
        {
            Role = PdfName.LI;
        }
    
        // implementation of the Element-methods
    
        /// <summary>
        /// Gets the type of the text element.
        /// </summary>
        /// <value>a type</value>
        public override int Type {
            get {
                return Element.LISTITEM;
            }
        }
    
        // methods

        public override Paragraph CloneShallow(bool spacingBefore) {
            ListItem copy = new ListItem();
            PopulateProperties(copy, spacingBefore);
            return copy;
        }
    
        // methods to retrieve information
    
        /// <summary>
        /// Get/set the listsymbol.
        /// </summary>
        /// <value>a Chunk</value>
        virtual public Chunk ListSymbol {
            get {
                return symbol;
            }

            set {
                if (this.symbol == null) {
                    this.symbol = value;
                    if (this.symbol.Font.IsStandardFont()) {
                        this.symbol.Font = font;
                    }
                }
            }
        }
    
        /**
        * Sets the indentation of this paragraph on the left side.
        *
        * @param	indentation		the new indentation
        */        
        virtual public void SetIndentationLeft(float indentation, bool autoindent) {
            if (autoindent) {
            	IndentationLeft = ListSymbol.GetWidthPoint();
            }
            else {
            	IndentationLeft = indentation;
            }
        }

        /**
         * Changes the font of the list symbol to the font of the first chunk
         * in the list item.
         * @since 5.0.6
         */
        virtual public void AdjustListSymbolFont() {
            System.Collections.Generic.IList<Chunk> cks = Chunks;
            if (cks.Count != 0 && symbol != null)
                symbol.Font = cks[0].Font;
        }

        virtual public ListBody ListBody {
            get
            {
                if (listBody == null)
                    listBody = new ListBody(this);
                return listBody;
            }
        }

        virtual public ListLabel ListLabel {
            get
            {
                if (listLabel == null)
                    listLabel = new ListLabel(this);
                return listLabel;
            }
        }
    }
}
