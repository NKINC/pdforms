using System;
using System.Collections.Generic;
using System.Text;
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

namespace iTextSharp.text.pdf
{
    public abstract class BaseField {
        
        /** A thin border with 1 point width. */    
        public const float BORDER_WIDTH_THIN = 1;
        /** A medium border with 2 point width. */    
        public const float BORDER_WIDTH_MEDIUM = 2;
        /** A thick border with 3 point width. */    
        public const float BORDER_WIDTH_THICK = 3;
        /** The field is visible. */    
        public const int VISIBLE = 0;
        /** The field is hidden. */    
        public const int HIDDEN = 1;
        /** The field is visible but does not print. */    
        public const int VISIBLE_BUT_DOES_NOT_PRINT = 2;
        /** The field is hidden but is printable. */    
        public const int HIDDEN_BUT_PRINTABLE = 3;
        /** The user may not change the value of the field. */    
        public const int READ_ONLY = PdfFormField.FF_READ_ONLY;
        /** The field must have a value at the time it is exported by a submit-form
        * action.
        */    
        public const int REQUIRED = PdfFormField.FF_REQUIRED;
        /** The field may contain multiple lines of text.
        * This flag is only meaningful with text fields.
        */    
        public const int MULTILINE = PdfFormField.FF_MULTILINE;
        /** The field will not scroll (horizontally for single-line
        * fields, vertically for multiple-line fields) to accommodate more text
        * than will fit within its annotation rectangle. Once the field is full, no
        * further text will be accepted.
        */    
        public const int DO_NOT_SCROLL = PdfFormField.FF_DONOTSCROLL;
        /** The field is intended for entering a secure password that should
        * not be echoed visibly to the screen.
        */    
        public const int PASSWORD = PdfFormField.FF_PASSWORD;
        /** The text entered in the field represents the pathname of
        * a file whose contents are to be submitted as the value of the field.
        */    
        public const int FILE_SELECTION = PdfFormField.FF_FILESELECT;
        /** The text entered in the field will not be spell-checked.
        * This flag is meaningful only in text fields and in combo
        * fields with the <CODE>EDIT</CODE> flag set.
        */    
        public const int DO_NOT_SPELL_CHECK = PdfFormField.FF_DONOTSPELLCHECK;
        /** If set the combo box includes an editable text box as well as a drop list; if
        * clear, it includes only a drop list.
        * This flag is only meaningful with combo fields.
        */    
        public const int EDIT = PdfFormField.FF_EDIT;
    
        /** whether or not a list may have multiple selections.  Only applies to /CH LIST
        * fields, not combo boxes.
        */
        public const int MULTISELECT = PdfFormField.FF_MULTISELECT;

        /**
        * combo box flag.
        */
        public const int COMB = PdfFormField.FF_COMB;

        protected float borderWidth = BORDER_WIDTH_THIN;
        protected int borderStyle = PdfBorderDictionary.STYLE_SOLID;
        protected BaseColor borderColor;
        protected BaseColor backgroundColor;
        protected BaseColor textColor;
        protected BaseFont font;
        protected float fontSize = 0;
        protected int alignment = Element.ALIGN_LEFT;
        protected PdfWriter writer;
        protected String text;
        protected Rectangle box;
        
        /** Holds value of property rotation. */
        protected int rotation = 0;
        
        /** Holds value of property visibility. */
        protected int visibility;
        
        /** Holds value of property fieldName. */
        protected String fieldName;
        
        /** Holds value of property options. */
        protected int options;
        
        /** Holds value of property maxCharacterLength. */
        protected int maxCharacterLength;
        
        private static Dictionary<PdfName,int> fieldKeys = new Dictionary<PdfName,int>();
     
        static BaseField() {
            foreach (KeyValuePair<PdfName,int> entry in PdfCopyFieldsImp.fieldKeys)
                fieldKeys[entry.Key] = entry.Value;
            fieldKeys[PdfName.T] = 1;
        }
        /** Creates a new <CODE>TextField</CODE>.
        * @param writer the document <CODE>PdfWriter</CODE>
        * @param box the field location and dimensions
        * @param fieldName the field name. If <CODE>null</CODE> only the widget keys
        * will be included in the field allowing it to be used as a kid field.
        */
        public BaseField(PdfWriter writer, Rectangle box, String fieldName) {
            this.writer = writer;
            Box = box;
            this.fieldName = fieldName;
        }
        
        protected virtual BaseFont RealFont {
            get {
                if (font == null)
                    return BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, false);
                else
                    return font;
            }
        }
        
        virtual protected PdfAppearance GetBorderAppearance() {
            PdfAppearance app = PdfAppearance.CreateAppearance(writer, box.Width, box.Height);
            switch (rotation) {
                case 90:
                    app.SetMatrix(0, 1, -1, 0, box.Height, 0);
                    break;
                case 180:
                    app.SetMatrix(-1, 0, 0, -1, box.Width, box.Height);
                    break;
                case 270:
                    app.SetMatrix(0, -1, 1, 0, 0, box.Width);
                    break;
            }
            app.SaveState();
            // background
            if (backgroundColor != null) {
                app.SetColorFill(backgroundColor);
                app.Rectangle(0, 0, box.Width, box.Height);
                app.Fill();
            }
            // border
            if (borderStyle == PdfBorderDictionary.STYLE_UNDERLINE) {
                if (borderWidth != 0 && borderColor != null) {
                    app.SetColorStroke(borderColor);
                    app.SetLineWidth(borderWidth);
                    app.MoveTo(0, borderWidth / 2);
                    app.LineTo(box.Width, borderWidth / 2);
                    app.Stroke();
                }
            }
            else if (borderStyle == PdfBorderDictionary.STYLE_BEVELED) {
                if (borderWidth != 0 && borderColor != null) {
                    app.SetColorStroke(borderColor);
                    app.SetLineWidth(borderWidth);
                    app.Rectangle(borderWidth / 2, borderWidth / 2, box.Width - borderWidth, box.Height - borderWidth);
                    app.Stroke();
                }
                // beveled
                BaseColor actual = backgroundColor;
                if (actual == null)
                    actual = BaseColor.WHITE;
                app.SetGrayFill(1);
                DrawTopFrame(app);
                app.SetColorFill(actual.Darker());
                DrawBottomFrame(app);
            }
            else if (borderStyle == PdfBorderDictionary.STYLE_INSET) {
                if (borderWidth != 0 && borderColor != null) {
                    app.SetColorStroke(borderColor);
                    app.SetLineWidth(borderWidth);
                    app.Rectangle(borderWidth / 2, borderWidth / 2, box.Width - borderWidth, box.Height - borderWidth);
                    app.Stroke();
                }
                // inset
                app.SetGrayFill(0.5f);
                DrawTopFrame(app);
                app.SetGrayFill(0.75f);
                DrawBottomFrame(app);
            }
            else {
                if (borderWidth != 0 && borderColor != null) {
                    if (borderStyle == PdfBorderDictionary.STYLE_DASHED)
                        app.SetLineDash(3, 0);
                    app.SetColorStroke(borderColor);
                    app.SetLineWidth(borderWidth);
                    app.Rectangle(borderWidth / 2, borderWidth / 2, box.Width - borderWidth, box.Height - borderWidth);
                    app.Stroke();
                    if ((options & COMB) != 0 && maxCharacterLength > 1) {
                        float step = box.Width / maxCharacterLength;
                        float yb = borderWidth / 2;
                        float yt = box.Height - borderWidth / 2;
                        for (int k = 1; k < maxCharacterLength; ++k) {
                            float x = step * k;
                            app.MoveTo(x, yb);
                            app.LineTo(x, yt);
                        }
                        app.Stroke();
                    }
                }
            }
            app.RestoreState();
            return app;
        }
        
        protected static List<string> GetHardBreaks(String text) {
            List<string> arr = new List<string>();
            char[] cs = text.ToCharArray();
            int len = cs.Length;
            StringBuilder buf = new StringBuilder();
            for (int k = 0; k < len; ++k) {
                char c = cs[k];
                if (c == '\r') {
                    if (k + 1 < len && cs[k + 1] == '\n')
                        ++k;
                    arr.Add(buf.ToString());
                    buf = new StringBuilder();
                }
                else if (c == '\n') {
                    arr.Add(buf.ToString());
                    buf = new StringBuilder();
                }
                else
                    buf.Append(c);
            }
            arr.Add(buf.ToString());
            return arr;
        }
        
        protected static void TrimRight(StringBuilder buf) {
            int len = buf.Length;
            while (true) {
                if (len == 0)
                    return;
                if (buf[--len] != ' ')
                    return;
                buf.Length = len;
            }
        }
        
        protected static List<string> BreakLines(List<string> breaks, BaseFont font, float fontSize, float width) {
            List<string> lines = new List<string>();
            StringBuilder buf = new StringBuilder();
            for (int ck = 0; ck < breaks.Count; ++ck) {
                buf.Length = 0;
                float w = 0;
                char[] cs = breaks[ck].ToCharArray();
                int len = cs.Length;
                // 0 inline first, 1 inline, 2 spaces
                int state = 0;
                int lastspace = -1;
                char c = (char)0;
                int refk = 0;
                for (int k = 0; k < len; ++k) {
                    c = cs[k];
                    switch (state) {
                        case 0:
                            w += font.GetWidthPoint(c, fontSize);
                            buf.Append(c);
                            if (w > width) {
                                w = 0;
                                if (buf.Length > 1) {
                                    --k;
                                    buf.Length = buf.Length - 1;
                                }
                                lines.Add(buf.ToString());
                                buf.Length = 0;
                                refk = k;
                                if (c == ' ')
                                    state = 2;
                                else
                                    state = 1;
                            }
                            else {
                                if (c != ' ')
                                    state = 1;
                            }
                            break;
                        case 1:
                            w += font.GetWidthPoint(c, fontSize);
                            buf.Append(c);
                            if (c == ' ')
                                lastspace = k;
                            if (w > width) {
                                w = 0;
                                if (lastspace >= 0) {
                                    k = lastspace;
                                    buf.Length = lastspace - refk;
                                    TrimRight(buf);
                                    lines.Add(buf.ToString());
                                    buf.Length = 0;
                                    refk = k;
                                    lastspace = -1;
                                    state = 2;
                                }
                                else {
                                    if (buf.Length > 1) {
                                        --k;
                                        buf.Length = buf.Length - 1;
                                    }
                                    lines.Add(buf.ToString());
                                    buf.Length = 0;
                                    refk = k;
                                    if (c == ' ')
                                        state = 2;
                                }
                            }
                            break;
                        case 2:
                            if (c != ' ') {
                                w = 0;
                                --k;
                                state = 1;
                            }
                            break;
                    }
                }
                TrimRight(buf);
                lines.Add(buf.ToString());
            }
            return lines;
        }
            
        private void DrawTopFrame(PdfAppearance app) {
            app.MoveTo(borderWidth, borderWidth);
            app.LineTo(borderWidth, box.Height - borderWidth);
            app.LineTo(box.Width - borderWidth, box.Height - borderWidth);
            app.LineTo(box.Width - 2 * borderWidth, box.Height - 2 * borderWidth);
            app.LineTo(2 * borderWidth, box.Height - 2 * borderWidth);
            app.LineTo(2 * borderWidth, 2 * borderWidth);
            app.LineTo(borderWidth, borderWidth);
            app.Fill();
        }
        
        private void DrawBottomFrame(PdfAppearance app) {
            app.MoveTo(borderWidth, borderWidth);
            app.LineTo(box.Width - borderWidth, borderWidth);
            app.LineTo(box.Width - borderWidth, box.Height - borderWidth);
            app.LineTo(box.Width - 2 * borderWidth, box.Height - 2 * borderWidth);
            app.LineTo(box.Width - 2 * borderWidth, 2 * borderWidth);
            app.LineTo(2 * borderWidth, 2 * borderWidth);
            app.LineTo(borderWidth, borderWidth);
            app.Fill();
        }
        
        /** Sets the border width in points. To eliminate the border
        * set the border color to <CODE>null</CODE>.
        * @param borderWidth the border width in points
        */
        virtual public float BorderWidth {
            set {
                this.borderWidth = value;
            }
            get {
                return borderWidth;
            }
        }
        
        /** Sets the border style. The styles are found in <CODE>PdfBorderDictionary</CODE>
        * and can be <CODE>STYLE_SOLID</CODE>, <CODE>STYLE_DASHED</CODE>,
        * <CODE>STYLE_BEVELED</CODE>, <CODE>STYLE_INSET</CODE> and
        * <CODE>STYLE_UNDERLINE</CODE>.
        * @param borderStyle the border style
        */
        virtual public int BorderStyle {
            set {
                this.borderStyle = value;
            }
            get {
                return borderStyle;
            }
        }
        
        /** Sets the border color. Set to <CODE>null</CODE> to remove
        * the border.
        * @param borderColor the border color
        */
        virtual public BaseColor BorderColor {
            set {
                this.borderColor = value;
            }
            get {
                return borderColor;
            }
        }
        
        /** Sets the background color. Set to <CODE>null</CODE> for
        * transparent background.
        * @param backgroundColor the background color
        */
        virtual public BaseColor BackgroundColor {
            set {
                this.backgroundColor = value;
            }
            get {
                return backgroundColor;
            }
        }
        
        /** Sets the text color. If <CODE>null</CODE> the color used
        * will be black.
        * @param textColor the text color
        */
        virtual public BaseColor TextColor {
            set {
                this.textColor = value;
            }
            get {
                return textColor;
            }
        }
        
        /** Sets the text font. If <CODE>null</CODE> then Helvetica
        * will be used.
        * @param font the text font
        */
        virtual public BaseFont Font {
            set {
                this.font = value;
            }
            get {
                return font;
            }
        }
        
        /** Sets the font size. If 0 then auto-sizing will be used but
        * only for text fields.
        * @param fontSize the font size
        */
        virtual public float FontSize {
            set {
                this.fontSize = value;
            }
            get {
                return fontSize;
            }
        }
        
        /** Sets the text horizontal alignment. It can be <CODE>Element.ALIGN_LEFT</CODE>,
        * <CODE>Element.ALIGN_CENTER</CODE> and <CODE>Element.ALIGN_RIGHT</CODE>.
        * @param alignment the text horizontal alignment
        */
        virtual public int Alignment {
            set {
                this.alignment = value;
            }
            get {
                return alignment;
            }
        }
        
        /** Sets the text for text fields.
        * @param text the text
        */
        virtual public string Text {
            set {
                this.text = value;
            }
            get {
                return text;
            }
        }
        
        /** Sets the field dimension and position.
        * @param box the field dimension and position
        */
        virtual public Rectangle Box {
            set {
                if (value == null)
                    box = null;
                else {
                    box = new Rectangle(value);
                    box.Normalize();
                }
            }
            get {
                return box;
            }
        }
        
        /** Sets the field rotation. This value should be the same as
        * the page rotation where the field will be shown.
        * @param rotation the field rotation
        */
        virtual public int Rotation {
            set {
                if (value % 90 != 0)
                    throw new ArgumentException(MessageLocalization.GetComposedMessage("rotation.must.be.a.multiple.of.90"));
                rotation = (value % 360);
                if (rotation < 0)
                    rotation += 360;
            }
            get {
                return rotation;
            }
        }
        
        /** Convenience method to set the field rotation the same as the
        * page rotation.
        * @param page the page
        */    
        virtual public void SetRotationFromPage(Rectangle page) {
            Rotation = page.Rotation;
        }
        
        /** Sets the field visibility flag. This flags can be one of
        * <CODE>VISIBLE</CODE>, <CODE>HIDDEN</CODE>, <CODE>VISIBLE_BUT_DOES_NOT_PRINT</CODE>
        * and <CODE>HIDDEN_BUT_PRINTABLE</CODE>.
        * @param visibility field visibility flag
        */
        virtual public int Visibility {
            set {
                this.visibility = value;
            }
            get {
                return visibility;
            }
        }
        
        /** Sets the field name.
        * @param fieldName the field name. If <CODE>null</CODE> only the widget keys
        * will be included in the field allowing it to be used as a kid field.
        */
        virtual public string FieldName {
            set {
                this.fieldName = value;
            }
            get {
                return fieldName;
            }
        }
        
        /** Sets the option flags. The option flags can be a combination by oring of
        * <CODE>READ_ONLY</CODE>, <CODE>REQUIRED</CODE>,
        * <CODE>MULTILINE</CODE>, <CODE>DO_NOT_SCROLL</CODE>,
        * <CODE>PASSWORD</CODE>, <CODE>FILE_SELECTION</CODE>,
        * <CODE>DO_NOT_SPELL_CHECK</CODE> and <CODE>EDIT</CODE>.
        * @param options the option flags
        */
        virtual public int Options {
            set {
                this.options = value;
            }
            get {
                return options;
            }
        }
        
        /** Sets the maximum length of the field�s text, in characters.
        * It is only meaningful for text fields.
        * @param maxCharacterLength the maximum length of the field�s text, in characters
        */
        virtual public int MaxCharacterLength {
            set {
                this.maxCharacterLength = value;
            }
            get {
                return maxCharacterLength;
            }
        }
        
        virtual public PdfWriter Writer {
            get {
                return writer;
            }
            set {
                writer = value;
            }
        }
        
        /**
        * Moves the field keys from <CODE>from</CODE> to <CODE>to</CODE>. The moved keys
        * are removed from <CODE>from</CODE>.
        * @param from the source
        * @param to the destination. It may be <CODE>null</CODE>
        */    
        public static void MoveFields(PdfDictionary from, PdfDictionary to) {
            PdfName[] keys = new PdfName[from.Size];
            from.Keys.CopyTo(keys, 0);
            foreach (PdfName key in keys) {
                if (fieldKeys.ContainsKey(key)) {
                    if (to != null)
                        to.Put(key, from.Get(key));
                    from.Remove(key);
                }
            }
        }
    }
}
