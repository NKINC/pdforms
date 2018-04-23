using System;
using System.Collections.Generic;
using System.Text;
using System.util;
using iTextSharp.text.pdf;
using iTextSharp.text.error_messages;

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
    /// A Rectangle is the representation of a geometric figure.
    /// </summary>
    /// <seealso cref="T:iTextSharp.text.Element"/>
    /// <seealso cref="T:iTextSharp.text.Table"/>
    /// <seealso cref="T:iTextSharp.text.Cell"/>
    /// <seealso cref="T:iTextSharp.text.HeaderFooter"/>
    public class Rectangle : Element, IElement {
    
        // static membervariables (concerning the presence of borders)
    
        ///<summary> This is the value that will be used as <VAR>undefined</VAR>. </summary>
        public const int UNDEFINED = -1;
    
        ///<summary> This represents one side of the border of the Rectangle. </summary>
        public const int TOP_BORDER = 1;
    
        ///<summary> This represents one side of the border of the Rectangle. </summary>
        public const int BOTTOM_BORDER = 2;
    
        ///<summary> This represents one side of the border of the Rectangle. </summary>
        public const int LEFT_BORDER = 4;
    
        ///<summary> This represents one side of the border of the Rectangle. </summary>
        public const int RIGHT_BORDER = 8;
    
        ///<summary> This represents a rectangle without borders. </summary>
        public const int NO_BORDER = 0;
    
        ///<summary> This represents a type of border. </summary>
        public const int BOX = TOP_BORDER + BOTTOM_BORDER + LEFT_BORDER + RIGHT_BORDER;
    
        // membervariables
    
        ///<summary> the lower left x-coordinate. </summary>
        protected float llx;
    
        ///<summary> the lower left y-coordinate. </summary>
        protected float lly;
    
        ///<summary> the upper right x-coordinate. </summary>
        protected float urx;
    
        ///<summary> the upper right y-coordinate. </summary>
        protected float ury;
    
        ///<summary> This represents the status of the 4 sides of the rectangle. </summary>
        protected int border = UNDEFINED;
    
        ///<summary> This is the width of the border around this rectangle. </summary>
        protected float borderWidth = UNDEFINED;
    
        ///<summary> This is the color of the border of this rectangle. </summary>
        protected BaseColor borderColor = null;
    
        /** The color of the left border of this rectangle. */
        protected BaseColor borderColorLeft = null;

        /** The color of the right border of this rectangle. */
        protected BaseColor borderColorRight = null;

        /** The color of the top border of this rectangle. */
        protected BaseColor borderColorTop = null;

        /** The color of the bottom border of this rectangle. */
        protected BaseColor borderColorBottom = null;

        /** The width of the left border of this rectangle. */
        protected float borderWidthLeft = UNDEFINED;

        /** The width of the right border of this rectangle. */
        protected float borderWidthRight = UNDEFINED;

        /** The width of the top border of this rectangle. */
        protected float borderWidthTop = UNDEFINED;

        /** The width of the bottom border of this rectangle. */
        protected float borderWidthBottom = UNDEFINED;

        /** Whether variable width borders are used. */
        protected bool useVariableBorders = false;

        ///<summary> This is the color of the background of this rectangle. </summary>
        protected BaseColor backgroundColor = null;
    
        ///<summary> This is the rotation value of this rectangle. </summary>
        protected int rotation = 0;

        // constructors
    
        /// <summary>
        /// Constructs a Rectangle-object.
        /// </summary>
        /// <param name="llx">lower left x</param>
        /// <param name="lly">lower left y</param>
        /// <param name="urx">upper right x</param>
        /// <param name="ury">upper right y</param>
        public Rectangle(float llx, float lly, float urx, float ury) {
            this.llx = llx;
            this.lly = lly;
            this.urx = urx;
            this.ury = ury;
        }
    
        /**
         * Constructs a <CODE>Rectangle</CODE>-object.
         *
         * @param llx   lower left x
         * @param lly   lower left y
         * @param urx   upper right x
         * @param ury   upper right y
         * @param rotation the rotation (0, 90, 180, or 270)
         * @since iText 5.0.6
         */
        public Rectangle(float llx, float lly, float urx, float ury, int rotation) : this(llx, lly, urx, ury) {
            Rotation = rotation;
        }

        /// <summary>
        /// Constructs a Rectangle-object starting from the origin (0, 0).
        /// </summary>
        /// <param name="urx">upper right x</param>
        /// <param name="ury">upper right y</param>
        public Rectangle(float urx, float ury) : this(0, 0, urx, ury) {}
    
        /**
         * Constructs a <CODE>Rectangle</CODE>-object starting from the origin
         * (0, 0) and with a specific rotation (valid values are 0, 90, 180, 270).
         *
         * @param urx   upper right x
         * @param ury   upper right y
         * @param rotation the rotation of the rectangle
         * @since iText 5.0.6
         */
        public Rectangle(float urx, float ury, int rotation) : this(0, 0, urx, ury, rotation) {
        }
            
        /// <summary>
        /// Constructs a Rectangle-object.
        /// </summary>
        /// <param name="rect">another Rectangle</param>
        public Rectangle(Rectangle rect) : this(rect.llx, rect.lly, rect.urx, rect.ury) {
            CloneNonPositionParameters(rect);
        }

        /**
         * Constructs a <CODE>Rectangle</CODE>-object based on a <CODE>com.itextpdf.awt.geom.Rectangle</CODE> object
         * @param rect com.itextpdf.awt.geom.Rectangle
         */
        public Rectangle(RectangleJ rect): this(rect.X, rect.Y, rect.X + rect.Width, rect.Y + rect.Height) {
        }

        /**
        * Copies all of the parameters from a <CODE>Rectangle</CODE> object
        * except the position.
        * 
        * @param rect
        *            <CODE>Rectangle</CODE> to copy from
        */

        public virtual void CloneNonPositionParameters(Rectangle rect) {
            this.rotation = rect.rotation;
            this.border = rect.border;
            this.borderWidth = rect.borderWidth;
            this.borderColor = rect.borderColor;
            this.backgroundColor = rect.backgroundColor;
            this.borderColorLeft = rect.borderColorLeft;
            this.borderColorRight = rect.borderColorRight;
            this.borderColorTop = rect.borderColorTop;
            this.borderColorBottom = rect.borderColorBottom;
            this.borderWidthLeft = rect.borderWidthLeft;
            this.borderWidthRight = rect.borderWidthRight;
            this.borderWidthTop = rect.borderWidthTop;
            this.borderWidthBottom = rect.borderWidthBottom;
            this.useVariableBorders = rect.useVariableBorders;
        }

        /**
        * Copies all of the parameters from a <CODE>Rectangle</CODE> object
        * except the position.
        * 
        * @param rect
        *            <CODE>Rectangle</CODE> to copy from
        */

        public virtual void SoftCloneNonPositionParameters(Rectangle rect) {
            if (rect.rotation != 0)
                this.rotation = rect.rotation;
            if (rect.border != UNDEFINED)
                this.border = rect.border;
            if (rect.borderWidth != UNDEFINED)
                this.borderWidth = rect.borderWidth;
            if (rect.borderColor != null)
                this.borderColor = rect.borderColor;
            if (rect.backgroundColor != null)
                this.backgroundColor = rect.backgroundColor;
            if (rect.borderColorLeft != null)
                this.borderColorLeft = rect.borderColorLeft;
            if (rect.borderColorRight != null)
                this.borderColorRight = rect.borderColorRight;
            if (rect.borderColorTop != null)
                this.borderColorTop = rect.borderColorTop;
            if (rect.borderColorBottom != null)
                this.borderColorBottom = rect.borderColorBottom;
            if (rect.borderWidthLeft != UNDEFINED)
                this.borderWidthLeft = rect.borderWidthLeft;
            if (rect.borderWidthRight != UNDEFINED)
                this.borderWidthRight = rect.borderWidthRight;
            if (rect.borderWidthTop != UNDEFINED)
                this.borderWidthTop = rect.borderWidthTop;
            if (rect.borderWidthBottom != UNDEFINED)
                this.borderWidthBottom = rect.borderWidthBottom;
            if (useVariableBorders)
                this.useVariableBorders = rect.useVariableBorders;
        }

        // implementation of the Element interface
    
        /// <summary>
        /// Processes the element by adding it (or the different parts) to an
        /// IElementListener.
        /// </summary>
        /// <param name="listener">an IElementListener</param>
        /// <returns>true if the element was processed successfully</returns>
        public virtual bool Process(IElementListener listener) {
            try {
                return listener.Add(this);
            }
            catch (DocumentException) {
                return false;
            }
        }
    
        /// <summary>
        /// Gets the type of the text element.
        /// </summary>
        /// <value>a type</value>
        public virtual int Type {
            get {
                return Element.RECTANGLE;
            }
        }
    
        /// <summary>
        /// Gets all the chunks in this element.
        /// </summary>
        /// <value>an ArrayList</value>
        public virtual IList<Chunk> Chunks {
            get {
                return new List<Chunk>();
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
        public virtual bool IsNestable() {
            return false;
        }

        // methods
    
        /**
        * Switches lowerleft with upperright
        */
        public virtual void Normalize() {
            if (llx > urx) {
                float a = llx;
                llx = urx;
                urx = a;
            }
            if (lly > ury) {
                float a = lly;
                lly = ury;
                ury = a;
            }
        }

        /// <summary>
        /// Gets a Rectangle that is altered to fit on the page.
        /// </summary>
        /// <param name="top">the top position</param>
        /// <param name="bottom">the bottom position</param>
        /// <returns>a Rectangle</returns>
        virtual public Rectangle GetRectangle(float top, float bottom) {
            Rectangle tmp = new Rectangle(this);
            if (this.Top > top) {
                tmp.Top = top;
                tmp.Border = border - (border & TOP_BORDER);
            }
            if (Bottom < bottom) {
                tmp.Bottom = bottom;
                tmp.Border = border - (border & BOTTOM_BORDER);
            }
            return tmp;
        }
    
        /// <summary>
        /// Swaps the values of urx and ury and of lly and llx in order to rotate the rectangle.
        /// </summary>
        /// <returns>a Rectangle</returns>
        virtual public Rectangle Rotate() {
            Rectangle rect = new Rectangle(lly, llx, ury, urx);
            rect.Rotation = rotation + 90;
            return rect;
        }
    
        // methods to set the membervariables
    
        /// <summary>
        /// Get/set the upper right y-coordinate. 
        /// </summary>
        /// <value>a float</value>
        public virtual float Top {
            get {
                return ury;
            }

            set {
                ury = value;
            }
        }

        /**
        * Enables the border on the specified side.
        * 
        * @param side
        *            the side to enable. One of <CODE>LEFT, RIGHT, TOP, BOTTOM
        *            </CODE>
        */
        public virtual void EnableBorderSide(int side) {
            if (border == UNDEFINED) {
                border = 0;
            }
            border |= side;
        }

        /**
        * Disables the border on the specified side.
        * 
        * @param side
        *            the side to disable. One of <CODE>LEFT, RIGHT, TOP, BOTTOM
        *            </CODE>
        */
        public virtual void DisableBorderSide(int side) {
            if (border == UNDEFINED) {
                border = 0;
            }
            border &= ~side;
        }


        /// <summary>
        /// Get/set the border
        /// </summary>
        /// <value>a int</value>
        public virtual int Border {
            get {
                return this.border;
            }

            set {
                border = value;
            }
        }
    
        /// <summary>
        /// Get/set the grayscale of the rectangle.
        /// </summary>
        /// <value>a float</value>
        public virtual float GrayFill {
            get {
                if (backgroundColor is GrayColor)
                    return ((GrayColor)backgroundColor).Gray;
                else
                    return 0;
            }
            set {
                backgroundColor = new GrayColor(value);            }
        }
    
        // methods to get the membervariables
    
        /// <summary>
        /// Get/set the lower left x-coordinate.
        /// </summary>
        /// <value>a float</value>
        public virtual float Left {
            get {
                return llx;
            }

            set {
                llx = value;
            }
        }
    
        /// <summary>
        /// Get/set the upper right x-coordinate.
        /// </summary>
        /// <value>a float</value>
        public virtual float Right {
            get {
                return urx;
            }
        
            set {
                urx = value;
            }
        }
    
        /// <summary>
        /// Get/set the lower left y-coordinate.
        /// </summary>
        /// <value>a float</value>
        public virtual float Bottom {
            get {
                return lly;
            }
            set {
                lly = value;
            }
        }
    
        public virtual BaseColor BorderColorBottom {
            get {
                if (borderColorBottom == null) return borderColor;
                return borderColorBottom;
            }
            set {
                borderColorBottom = value;
            }
        }
    
        public virtual BaseColor BorderColorTop {
            get {
                if (borderColorTop == null) return borderColor;
                return borderColorTop;
            }
            set {
                borderColorTop = value;
            }
        }
    
        public virtual BaseColor BorderColorLeft {
            get {
                if (borderColorLeft == null) return borderColor;
                return borderColorLeft;
            }
            set {
                borderColorLeft = value;
            }
        }
    
        public virtual BaseColor BorderColorRight {
            get {
                if (borderColorRight == null) return borderColor;
                return borderColorRight;
            }
            set {
                borderColorRight = value;
            }
        }
    
        /// <summary>
        /// Returns the lower left x-coordinate, considering a given margin.
        /// </summary>
        /// <param name="margin">a margin</param>
        /// <returns>the lower left x-coordinate</returns>
        public virtual float GetLeft(float margin) {
            return llx + margin;
        }
    
        /// <summary>
        /// Returns the upper right x-coordinate, considering a given margin.
        /// </summary>
        /// <param name="margin">a margin</param>
        /// <returns>the upper right x-coordinate</returns>
        public virtual float GetRight(float margin) {
            return urx - margin;
        }
    
        /// <summary>
        /// Returns the upper right y-coordinate, considering a given margin.
        /// </summary>
        /// <param name="margin">a margin</param>
        /// <returns>the upper right y-coordinate</returns>
        public virtual float GetTop(float margin) {
            return ury - margin;
        }
    
        /// <summary>
        /// Returns the lower left y-coordinate, considering a given margin.
        /// </summary>
        /// <param name="margin">a margin</param>
        /// <returns>the lower left y-coordinate</returns>
        public virtual float GetBottom(float margin) {
            return lly + margin;
        }
    
        /// <summary>
        /// Returns the width of the rectangle.
        /// </summary>
        /// <value>a width</value>
        public virtual float Width {
            get {
                return urx - llx;
            }
            set {
                throw new InvalidOperationException(MessageLocalization.GetComposedMessage("the.width.cannot.be.set"));
            }
        }
    
        /// <summary>
        /// Returns the height of the rectangle.
        /// </summary>
        /// <value>a height</value>
        virtual public float Height {
            get {
                return ury - lly;
            }
        }
    
        /// <summary>
        /// Indicates if the table has borders.
        /// </summary>
        /// <returns>a bool</returns>
        virtual public bool HasBorders() {
            switch (border) {
                case UNDEFINED:
                case NO_BORDER:
                    return false;
                default:
                    return borderWidth > 0 || borderWidthLeft > 0
                        || borderWidthRight > 0 || borderWidthTop > 0 || borderWidthBottom > 0;
            }
        }
    
        /// <summary>
        /// Indicates if the table has a some type of border.
        /// </summary>
        /// <param name="type">the type of border</param>
        /// <returns>a bool</returns>
        virtual public bool HasBorder(int type) {
            if (border == UNDEFINED)
                return false;
            return (border & type) == type;
        }
    
        /// <summary>
        /// Get/set the borderwidth.
        /// </summary>
        /// <value>a float</value>
        public virtual float BorderWidth {
            get {
                return borderWidth;
            }

            set {
                borderWidth = value;
            }
        }
    
        /**
         * Gets the color of the border.
         *
         * @return    a value
         */
        /// <summary>
        /// Get/set the color of the border.
        /// </summary>
        /// <value>a BaseColor</value>
        public virtual BaseColor BorderColor {
            get {
                return borderColor;
            }

            set {
                borderColor = value;
            }
        }
    
        /**
         * Gets the backgroundcolor.
         *
         * @return    a value
         */
        /// <summary>
        /// Get/set the backgroundcolor.
        /// </summary>
        /// <value>a BaseColor</value>
        public virtual BaseColor BackgroundColor {
            get {
                return backgroundColor;
            }

            set {
                backgroundColor = value;
            }
        }

        /// <summary>
        /// Set/gets the rotation
        /// </summary>
        /// <value>a int</value>    
        public virtual int Rotation {
            get {
                return rotation;
            }
            set {
                rotation = value % 360;
                switch (rotation) {
                    case 90:
                    case 180:
                    case 270:
                        break;
                    default:
                        rotation = 0;
                        break;
                }
            }
        }
    
        public virtual float BorderWidthLeft {
            get {
                return GetVariableBorderWidth(borderWidthLeft, LEFT_BORDER);
            }
            set {
                borderWidthLeft = value;
                UpdateBorderBasedOnWidth(value, LEFT_BORDER);
            }
        }

        public virtual float BorderWidthRight {
            get {
                return GetVariableBorderWidth(borderWidthRight, RIGHT_BORDER);
            }
            set {
                borderWidthRight = value;
                UpdateBorderBasedOnWidth(value, RIGHT_BORDER);
            }
        }

        public virtual float BorderWidthTop {
            get {
                return GetVariableBorderWidth(borderWidthTop, TOP_BORDER);
            }
            set {
                borderWidthTop = value;
                UpdateBorderBasedOnWidth(value, TOP_BORDER);
            }
        }

        public virtual float BorderWidthBottom {
            get {
                return GetVariableBorderWidth(borderWidthBottom, BOTTOM_BORDER);
            }
            set {
                borderWidthBottom = value;
                UpdateBorderBasedOnWidth(value, BOTTOM_BORDER);
            }
        }

        /**
        * Updates the border flag for a side based on the specified width. A width
        * of 0 will disable the border on that side. Any other width enables it.
        * 
        * @param width
        *            width of border
        * @param side
        *            border side constant
        */

        private void UpdateBorderBasedOnWidth(float width, int side) {
            useVariableBorders = true;
            if (width > 0) {
                EnableBorderSide(side);
            } else {
                DisableBorderSide(side);
            }
        }

        private float GetVariableBorderWidth(float variableWidthValue, int side) {
            if ((border & side) != 0)
                return variableWidthValue != UNDEFINED ? variableWidthValue : borderWidth;
            return 0;
        }

        /**
        * Sets a parameter indicating if the rectangle has variable borders
        * 
        * @param useVariableBorders
        *            indication if the rectangle has variable borders
        */
        public virtual bool UseVariableBorders{
            get {
                return useVariableBorders;
            }
            set {
                useVariableBorders = value;
            }
        }

	    public override String ToString() {
		    StringBuilder buf = new StringBuilder("Rectangle: ");
		    buf.Append(Width);
		    buf.Append('x');
		    buf.Append(Height);
		    buf.Append(" (rot: ");
		    buf.Append(rotation);
		    buf.Append(" degrees)");
		    return buf.ToString();
	    }

        public override bool Equals(Object obj)
        {
            if (obj is Rectangle) {
                Rectangle other = (Rectangle) obj;
                // should we normalize here?
                // normalization changes the structure and coordinates of the rectangle, so I'm inclined not to call normalize()
                // but it needs to be considered ~ Michael D.
                return other.llx == this.llx && other.lly == this.lly && other.urx == this.urx && other.ury == this.ury &&
                       other.rotation == this.rotation;
            }
            else {
                return false;
            }
        }
    }
}
