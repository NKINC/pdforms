using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.util.collections;
using iTextSharp.text.error_messages;

using iTextSharp.text;

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

    /** Reads a Truetype font
     *
     * @author Paulo Soares
     */
    public class TrueTypeFont : BaseFont {

        /** The code pages possible for a True Type font.
         */    
        internal static string[] codePages = {
                                        "1252 Latin 1",
                                        "1250 Latin 2: Eastern Europe",
                                        "1251 Cyrillic",
                                        "1253 Greek",
                                        "1254 Turkish",
                                        "1255 Hebrew",
                                        "1256 Arabic",
                                        "1257 Windows Baltic",
                                        "1258 Vietnamese",
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        "874 Thai",
                                        "932 JIS/Japan",
                                        "936 Chinese: Simplified chars--PRC and Singapore",
                                        "949 Korean Wansung",
                                        "950 Chinese: Traditional chars--Taiwan and Hong Kong",
                                        "1361 Korean Johab",
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        "Macintosh Character Set (US Roman)",
                                        "OEM Character Set",
                                        "Symbol Character Set",
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        null,
                                        "869 IBM Greek",
                                        "866 MS-DOS Russian",
                                        "865 MS-DOS Nordic",
                                        "864 Arabic",
                                        "863 MS-DOS Canadian French",
                                        "862 Hebrew",
                                        "861 MS-DOS Icelandic",
                                        "860 MS-DOS Portuguese",
                                        "857 IBM Turkish",
                                        "855 IBM Cyrillic; primarily Russian",
                                        "852 Latin 2",
                                        "775 MS-DOS Baltic",
                                        "737 Greek; former 437 G",
                                        "708 Arabic; ASMO 708",
                                        "850 WE/Latin 1",
                                        "437 US"};
 
        protected bool justNames = false;
        /** Contains the location of the several tables. The key is the name of
         * the table and the value is an <CODE>int[2]</CODE> where position 0
         * is the offset from the start of the file and position 1 is the length
         * of the table.
         */
        protected Dictionary<String, int[]> tables;
        /** The file in use.
         */
        protected RandomAccessFileOrArray rf;

        virtual public RandomAccessFileOrArray Rf { get { return rf; } }
        /** The file name.
         */
        protected string fileName;

        virtual public string FileName { get { return fileName; } }
    
        protected bool cff = false;

        virtual public bool Cff { get { return cff; } }
    
        protected int cffOffset;
    
        protected int cffLength;
    
        /** The offset from the start of the file to the table directory.
         * It is 0 for TTF and may vary for TTC depending on the chosen font.
         */    
        protected int directoryOffset;

        virtual public int DirectoryOffset { get { return directoryOffset; } }

        /** The index for the TTC font. It is an empty <CODE>string</CODE> for a
         * TTF file.
         */    
        protected string ttcIndex;
        /** The style modifier */
        protected string style = "";
        /** The content of table 'head'.
         */
        protected FontHeader head = new FontHeader();
        /** The content of table 'hhea'.
         */
        protected HorizontalHeader hhea = new HorizontalHeader();
        /** The content of table 'OS/2'.
         */
        protected WindowsMetrics os_2 = new WindowsMetrics();
        /** The width of the glyphs. This is essentially the content of table
         * 'hmtx' normalized to 1000 units.
         */
        protected int[] glyphWidthsByIndex;

        protected int[][] bboxes;

        /** The map containing the code information for the table 'cmap', encoding 1.0.
         * The key is the code and the value is an <CODE>int[2]</CODE> where position 0
         * is the glyph number and position 1 is the glyph width normalized to 1000
         * units.
         */
        protected Dictionary<int, int[]> cmap10;
        /** The map containing the code information for the table 'cmap', encoding 3.1
         * in Unicode.
         * <P>
         * The key is the code and the value is an <CODE>int</CODE>[2] where position 0
         * is the glyph number and position 1 is the glyph width normalized to 1000
         * units.
         */
        protected Dictionary<int, int[]> cmap31;

        /// <summary>
        /// By James for unicode Ext.B
        /// </summary>
        protected Dictionary<int, int[]> cmapExt;

        protected int[] glyphIdToChar;

        protected int maxGlyphId;

        /** The map containing the kerning information. It represents the content of
        * table 'kern'. The key is an <CODE>Integer</CODE> where the top 16 bits
        * are the glyph number for the first character and the lower 16 bits are the
        * glyph number for the second character. The value is the amount of kerning in
        * normalized 1000 units as an <CODE>Integer</CODE>. This value is usually negative.
        */
        protected IntHashtable kerning = new IntHashtable();

        /**
         * The font name.
         * This name is usually extracted from the table 'name' with
         * the 'Name ID' 6.
         */
        protected string fontName;

        /**
         * The font subfamily
         * This subFamily name is usually extracted from the table 'name' with
         * the 'Name ID' 2 or 'Name ID' 17.
         */
        protected String[][] subFamily;

        /** The full name of the font 'Name ID' 1 or 'Name ID' 16 */
        protected String[][] fullName;

        /** All the names auf the Names-Table
        */
        protected string[][] allNameEntries;
        
        /** The family name of the font
         */    
        protected string[][] familyName;
        /** The italic angle. It is usually extracted from the 'post' table or in it's
         * absence with the code:
         * <P>
         * <PRE>
         * -Math.Atan2(hhea.caretSlopeRun, hhea.caretSlopeRise) * 180 / Math.PI
         * </PRE>
         */
        protected double italicAngle;
        /** <CODE>true</CODE> if all the glyphs have the same width.
         */
        protected bool isFixedPitch = false;
    
        protected int underlinePosition;
        
        protected int underlineThickness;

        /** The components of table 'head'.
         */
        protected class FontHeader {
            /** A variable. */
            internal int flags;
            /** A variable. */
            internal int unitsPerEm;
            /** A variable. */
            internal short xMin;
            /** A variable. */
            internal short yMin;
            /** A variable. */
            internal short xMax;
            /** A variable. */
            internal short yMax;
            /** A variable. */
            internal int macStyle;
        }
    
        /** The components of table 'hhea'.
         */
        protected class HorizontalHeader {
            /** A variable. */
            internal short Ascender;
            /** A variable. */
            internal short Descender;
            /** A variable. */
            internal short LineGap;
            /** A variable. */
            internal int advanceWidthMax;
            /** A variable. */
            internal short minLeftSideBearing;
            /** A variable. */
            internal short minRightSideBearing;
            /** A variable. */
            internal short xMaxExtent;
            /** A variable. */
            internal short caretSlopeRise;
            /** A variable. */
            internal short caretSlopeRun;
            /** A variable. */
            internal int numberOfHMetrics;
        }
    
        /** The components of table 'OS/2'.
         */
        protected class WindowsMetrics {
            /** A variable. */
            internal short xAvgCharWidth;
            /** A variable. */
            internal int usWeightClass;
            /** A variable. */
            internal int usWidthClass;
            /** A variable. */
            internal short fsType;
            /** A variable. */
            internal short ySubscriptXSize;
            /** A variable. */
            internal short ySubscriptYSize;
            /** A variable. */
            internal short ySubscriptXOffset;
            /** A variable. */
            internal short ySubscriptYOffset;
            /** A variable. */
            internal short ySuperscriptXSize;
            /** A variable. */
            internal short ySuperscriptYSize;
            /** A variable. */
            internal short ySuperscriptXOffset;
            /** A variable. */
            internal short ySuperscriptYOffset;
            /** A variable. */
            internal short yStrikeoutSize;
            /** A variable. */
            internal short yStrikeoutPosition;
            /** A variable. */
            internal short sFamilyClass;
            /** A variable. */
            internal byte[] panose = new byte[10];
            /** A variable. */
            internal byte[] achVendID = new byte[4];
            /** A variable. */
            internal int fsSelection;
            /** A variable. */
            internal int usFirstCharIndex;
            /** A variable. */
            internal int usLastCharIndex;
            /** A variable. */
            internal short sTypoAscender;
            /** A variable. */
            internal short sTypoDescender;
            /** A variable. */
            internal short sTypoLineGap;
            /** A variable. */
            internal int usWinAscent;
            /** A variable. */
            internal int usWinDescent;
            /** A variable. */
            internal int ulCodePageRange1;
            /** A variable. */
            internal int ulCodePageRange2;
            /** A variable. */
            internal int sCapHeight;
        }
    
        /** This constructor is present to allow extending the class.
         */
        protected TrueTypeFont() {
        }
    
        /** Creates a new TrueType font.
         * @param ttFile the location of the font on file. The file must end in '.ttf' or
         * '.ttc' but can have modifiers after the name
         * @param enc the encoding to be applied to this font
         * @param emb true if the font is to be embedded in the PDF
         * @param ttfAfm the font as a <CODE>byte</CODE> array
         * @throws DocumentException the font is invalid
         * @throws IOException the font file could not be read
         */
        internal TrueTypeFont(string ttFile, string enc, bool emb, byte[] ttfAfm, bool justNames, bool forceRead) {
            this.justNames = justNames;
            string nameBase = GetBaseName(ttFile);
            string ttcName = GetTTCName(nameBase);
            if (nameBase.Length < ttFile.Length) {
                style = ttFile.Substring(nameBase.Length);
            }
            encoding = enc;
            embedded = emb;
            fileName = ttcName;
            FontType = FONT_TYPE_TT;
            ttcIndex = "";
            if (ttcName.Length < nameBase.Length)
                ttcIndex = nameBase.Substring(ttcName.Length + 1);
            if (fileName.ToLower(System.Globalization.CultureInfo.InvariantCulture).EndsWith(".ttf") || fileName.ToLower(System.Globalization.CultureInfo.InvariantCulture).EndsWith(".otf") || fileName.ToLower(System.Globalization.CultureInfo.InvariantCulture).EndsWith(".ttc")) {
                Process(ttfAfm, forceRead);
                if (!justNames && embedded && os_2.fsType == 2)
                    throw new DocumentException(MessageLocalization.GetComposedMessage("1.cannot.be.embedded.due.to.licensing.restrictions", fileName + style));
            }
            else
                throw new DocumentException(MessageLocalization.GetComposedMessage("1.is.not.a.ttf.otf.or.ttc.font.file", fileName + style));
            if (!encoding.StartsWith("#"))
                PdfEncodings.ConvertToBytes(" ", enc); // check if the encoding exists
            CreateEncoding();
        }
    
        /** Gets the name from a composed TTC file name.
         * If I have for input "myfont.ttc,2" the return will
         * be "myfont.ttc".
         * @param name the full name
         * @return the simple file name
         */    
        protected static string GetTTCName(string name) {
            int idx = name.ToLower(System.Globalization.CultureInfo.InvariantCulture).IndexOf(".ttc,");
            if (idx < 0)
                return name;
            else
                return name.Substring(0, idx + 4);
        }


        /**
         * Reads the tables 'head', 'hhea', 'OS/2', 'post' and 'maxp' filling several variables.
         * @throws DocumentException the font is invalid
         * @throws IOException the font file could not be read
         */
        internal void FillTables() {
            int[] table_location;
            tables.TryGetValue("head", out table_location);
            if (table_location == null)
                throw new DocumentException(MessageLocalization.GetComposedMessage("table.1.does.not.exist.in.2", "head", fileName + style));
            rf.Seek(table_location[0] + 16);
            head.flags = rf.ReadUnsignedShort();
            head.unitsPerEm = rf.ReadUnsignedShort();
            rf.SkipBytes(16);
            head.xMin = rf.ReadShort();
            head.yMin = rf.ReadShort();
            head.xMax = rf.ReadShort();
            head.yMax = rf.ReadShort();
            head.macStyle = rf.ReadUnsignedShort();
        
            tables.TryGetValue("hhea", out table_location);
            if (table_location == null)
                throw new DocumentException(MessageLocalization.GetComposedMessage("table.1.does.not.exist.in.2", "hhea", fileName + style));
            rf.Seek(table_location[0] + 4);
            hhea.Ascender = rf.ReadShort();
            hhea.Descender = rf.ReadShort();
            hhea.LineGap = rf.ReadShort();
            hhea.advanceWidthMax = rf.ReadUnsignedShort();
            hhea.minLeftSideBearing = rf.ReadShort();
            hhea.minRightSideBearing = rf.ReadShort();
            hhea.xMaxExtent = rf.ReadShort();
            hhea.caretSlopeRise = rf.ReadShort();
            hhea.caretSlopeRun = rf.ReadShort();
            rf.SkipBytes(12);
            hhea.numberOfHMetrics = rf.ReadUnsignedShort();
        
            tables.TryGetValue("OS/2", out table_location);

            if (table_location != null)
            {

                rf.Seek(table_location[0]);
                int version = rf.ReadUnsignedShort();
                os_2.xAvgCharWidth = rf.ReadShort();
                os_2.usWeightClass = rf.ReadUnsignedShort();
                os_2.usWidthClass = rf.ReadUnsignedShort();
                os_2.fsType = rf.ReadShort();
                os_2.ySubscriptXSize = rf.ReadShort();
                os_2.ySubscriptYSize = rf.ReadShort();
                os_2.ySubscriptXOffset = rf.ReadShort();
                os_2.ySubscriptYOffset = rf.ReadShort();
                os_2.ySuperscriptXSize = rf.ReadShort();
                os_2.ySuperscriptYSize = rf.ReadShort();
                os_2.ySuperscriptXOffset = rf.ReadShort();
                os_2.ySuperscriptYOffset = rf.ReadShort();
                os_2.yStrikeoutSize = rf.ReadShort();
                os_2.yStrikeoutPosition = rf.ReadShort();
                os_2.sFamilyClass = rf.ReadShort();
                rf.ReadFully(os_2.panose);
                rf.SkipBytes(16);
                rf.ReadFully(os_2.achVendID);
                os_2.fsSelection = rf.ReadUnsignedShort();
                os_2.usFirstCharIndex = rf.ReadUnsignedShort();
                os_2.usLastCharIndex = rf.ReadUnsignedShort();
                os_2.sTypoAscender = rf.ReadShort();
                os_2.sTypoDescender = rf.ReadShort();
                if (os_2.sTypoDescender > 0)
                    os_2.sTypoDescender = (short) (-os_2.sTypoDescender);
                os_2.sTypoLineGap = rf.ReadShort();
                os_2.usWinAscent = rf.ReadUnsignedShort();
                os_2.usWinDescent = rf.ReadUnsignedShort();
                os_2.ulCodePageRange1 = 0;
                os_2.ulCodePageRange2 = 0;
                if (version > 0)
                {
                    os_2.ulCodePageRange1 = rf.ReadInt();
                    os_2.ulCodePageRange2 = rf.ReadInt();
                }
                if (version > 1)
                {
                    rf.SkipBytes(2);
                    os_2.sCapHeight = rf.ReadShort();
                }
                else
                    os_2.sCapHeight = (int) (0.7 * head.unitsPerEm);
            } else if (tables["hhea"] != null && tables["head"] != null) {

                if (head.macStyle == 0) {
                    os_2.usWeightClass = 700;
                    os_2.usWidthClass = 5;
                }
                else if (head.macStyle == 5) {
                    os_2.usWeightClass = 400;
                    os_2.usWidthClass = 3;
                }
                else if (head.macStyle == 6) {
                    os_2.usWeightClass = 400;
                    os_2.usWidthClass = 7;
                }
                else {
                    os_2.usWeightClass = 400;
                    os_2.usWidthClass = 5;
                }
                os_2.fsType = 0;

                os_2.ySubscriptYSize = 0;
                os_2.ySubscriptYOffset = 0;
                os_2.ySuperscriptYSize = 0;
                os_2.ySuperscriptYOffset = 0;
                os_2.yStrikeoutSize = 0;
                os_2.yStrikeoutPosition = 0;
                os_2.sTypoAscender = (short)(hhea.Ascender - 0.21 * hhea.Ascender);
                os_2.sTypoDescender = (short)-(Math.Abs(hhea.Descender) - Math.Abs(hhea.Descender) * 0.07);
                os_2.sTypoLineGap = (short)(hhea.LineGap * 2);
                os_2.usWinAscent = hhea.Ascender;
                os_2.usWinDescent = hhea.Descender;
                os_2.ulCodePageRange1 = 0;
                os_2.ulCodePageRange2 = 0;
                os_2.sCapHeight = (int)(0.7 * head.unitsPerEm);
            }

            tables.TryGetValue("post", out table_location);
            if (table_location == null) {
                italicAngle = -Math.Atan2(hhea.caretSlopeRun, hhea.caretSlopeRise)*180/Math.PI;
            } else {
                rf.Seek(table_location[0] + 4);
                short mantissa = rf.ReadShort();
                int fraction = rf.ReadUnsignedShort();
                italicAngle = mantissa + fraction/16384.0d;
                underlinePosition = rf.ReadShort();
                underlineThickness = rf.ReadShort();
                isFixedPitch = rf.ReadInt() != 0;
            }

            tables.TryGetValue("maxp", out table_location);
            if (table_location == null) {
                maxGlyphId = 65536;
            } else {
                rf.Seek(table_location[0] + 4);
                maxGlyphId = rf.ReadUnsignedShort();
            }
        }
    
        /**
         * Gets the Postscript font name.
         * @throws DocumentException the font is invalid
         * @throws IOException the font file could not be read
         * @return the Postscript font name
         */
        internal string BaseFont {
            get {
                int[] table_location;
                tables.TryGetValue("name", out table_location);
                if (table_location == null)
                    throw new DocumentException(MessageLocalization.GetComposedMessage("table.1.does.not.exist.in.2", "name", fileName + style));
                rf.Seek(table_location[0] + 2);
                int numRecords = rf.ReadUnsignedShort();
                int startOfStorage = rf.ReadUnsignedShort();
                for (int k = 0; k < numRecords; ++k) {
                    int platformID = rf.ReadUnsignedShort();
                    int platformEncodingID = rf.ReadUnsignedShort();
                    int languageID = rf.ReadUnsignedShort();
                    int nameID = rf.ReadUnsignedShort();
                    int length = rf.ReadUnsignedShort();
                    int offset = rf.ReadUnsignedShort();
                    if (nameID == 6) {
                        rf.Seek(table_location[0] + startOfStorage + offset);
                        if (platformID == 0 || platformID == 3)
                            return ReadUnicodeString(length);
                        else
                            return ReadStandardString(length);
                    }
                }
                FileInfo file = new FileInfo(fileName);
                return file.Name.Replace(' ', '-');
            }
        }
    
        /** Extracts the names of the font in all the languages available.
         * @param id the name id to retrieve
         * @throws DocumentException on error
         * @throws IOException on error
         */    
        internal string[][] GetNames(int id) {
            int[] table_location;
            tables.TryGetValue("name", out table_location);
            if (table_location == null)
                throw new DocumentException(MessageLocalization.GetComposedMessage("table.1.does.not.exist.in.2", "name", fileName + style));
            rf.Seek(table_location[0] + 2);
            int numRecords = rf.ReadUnsignedShort();
            int startOfStorage = rf.ReadUnsignedShort();
            List<string[]> names = new List<string[]>();
            for (int k = 0; k < numRecords; ++k) {
                int platformID = rf.ReadUnsignedShort();
                int platformEncodingID = rf.ReadUnsignedShort();
                int languageID = rf.ReadUnsignedShort();
                int nameID = rf.ReadUnsignedShort();
                int length = rf.ReadUnsignedShort();
                int offset = rf.ReadUnsignedShort();
                if (nameID == id) {
                    int pos = (int)rf.FilePointer;
                    rf.Seek(table_location[0] + startOfStorage + offset);
                    string name;
                    if (platformID == 0 || platformID == 3 || (platformID == 2 && platformEncodingID == 1)){
                        name = ReadUnicodeString(length);
                    }
                    else {
                        name = ReadStandardString(length);
                    }
                    names.Add(new string[]{platformID.ToString(),
                                              platformEncodingID.ToString(), languageID.ToString(), name});
                    rf.Seek(pos);
                }
            }
            string[][] thisName = new string[names.Count][];
            for (int k = 0; k < names.Count; ++k)
                thisName[k] = names[k];
            return thisName;
        }
    
        /** Extracts all the names of the names-Table
        * @param id the name id to retrieve
        * @throws DocumentException on error
        * @throws IOException on error
        */    
        internal string[][] GetAllNames() {
            int[] table_location;
            tables.TryGetValue("name", out table_location);
            if (table_location == null)
                throw new DocumentException(MessageLocalization.GetComposedMessage("table.1.does.not.exist.in.2", "name", fileName + style));
            rf.Seek(table_location[0] + 2);
            int numRecords = rf.ReadUnsignedShort();
            int startOfStorage = rf.ReadUnsignedShort();
            List<string[]> names = new List<string[]>();
            for (int k = 0; k < numRecords; ++k) {
                int platformID = rf.ReadUnsignedShort();
                int platformEncodingID = rf.ReadUnsignedShort();
                int languageID = rf.ReadUnsignedShort();
                int nameID = rf.ReadUnsignedShort();
                int length = rf.ReadUnsignedShort();
                int offset = rf.ReadUnsignedShort();
                int pos = (int)rf.FilePointer;
                rf.Seek(table_location[0] + startOfStorage + offset);
                String name;
                if (platformID == 0 || platformID == 3 || (platformID == 2 && platformEncodingID == 1)){
                    name = ReadUnicodeString(length);
                }
                else {
                    name = ReadStandardString(length);
                }
                names.Add(new String[]{nameID.ToString(), platformID.ToString(),
                    platformEncodingID.ToString(), languageID.ToString(), name});
                rf.Seek(pos);
            }
            string[][] thisName = new string[names.Count][];
            for (int k = 0; k < names.Count; ++k)
                thisName[k] = names[k];
            return thisName;
        }
        
        internal void CheckCff() {
            int[] table_location;
            tables.TryGetValue("CFF ", out table_location);
            if (table_location != null) {
                cff = true;
                cffOffset = table_location[0];
                cffLength = table_location[1];
            }
        }

        /** Reads the font data.
         * @param ttfAfm the font as a <CODE>byte</CODE> array, possibly <CODE>null</CODE>
         * @throws DocumentException the font is invalid
         * @throws IOException the font file could not be read
         */
        internal void Process(byte[] ttfAfm, bool preload) {
            tables = new Dictionary<string,int[]>();
        
            try {
                if (ttfAfm == null)
                    rf = new RandomAccessFileOrArray(fileName, preload);
                else
                    rf = new RandomAccessFileOrArray(ttfAfm);

                if (ttcIndex.Length > 0) {
                    int dirIdx = int.Parse(ttcIndex);
                    if (dirIdx < 0)
                        throw new DocumentException(MessageLocalization.GetComposedMessage("the.font.index.for.1.must.be.positive", fileName));
                    string mainTag = ReadStandardString(4);
                    if (!mainTag.Equals("ttcf"))
                        throw new DocumentException(MessageLocalization.GetComposedMessage("1.is.not.a.valid.ttc.file", fileName));
                    rf.SkipBytes(4);
                    int dirCount = rf.ReadInt();
                    if (dirIdx >= dirCount)
                        throw new DocumentException(MessageLocalization.GetComposedMessage("the.font.index.for.1.must.be.between.0.and.2.it.was.3", fileName, (dirCount - 1), dirIdx));
                    rf.SkipBytes(dirIdx * 4);
                    directoryOffset = rf.ReadInt();
                }
                rf.Seek(directoryOffset);
                int ttId = rf.ReadInt();
                if (ttId != 0x00010000 && ttId != 0x4F54544F)
                    throw new DocumentException(MessageLocalization.GetComposedMessage("1.is.not.a.valid.ttf.or.otf.file", fileName));
                int num_tables = rf.ReadUnsignedShort();
                rf.SkipBytes(6);
                for (int k = 0; k < num_tables; ++k) {
                    string tag = ReadStandardString(4);
                    rf.SkipBytes(4);
                    int[] table_location = new int[2];
                    table_location[0] = rf.ReadInt();
                    table_location[1] = rf.ReadInt();
                    tables[tag] = table_location;
                }
                CheckCff();
                fontName = BaseFont;
                fullName = GetNames(4); //full name
                //author Daniel Lichtenberger, CHEMDOX
                String[][] otfFamilyName = GetNames(16);
                if (otfFamilyName.Length > 0) {
                    familyName = otfFamilyName;
                } else {
                    familyName = GetNames(1);
                }
                String[][] otfSubFamily = GetNames(17);
                if (otfFamilyName.Length > 0) {
                    subFamily = otfSubFamily;
                } else {
                    subFamily = GetNames(2);
                }
                allNameEntries = GetAllNames();
                if (!justNames) {
                    FillTables();
                    ReadGlyphWidths();
                    ReadCMaps();
                    ReadKerning();
                    ReadBbox();
                }
            }
            finally {
                //TODO: For embedded fonts, the underlying data source for the font will be left open until this TrueTypeFont object is collected by the Garbage Collector.  That may not be optimal.
                if (!embedded){
                    rf.Close();
                    rf = null;
                }
            }
        }
    
        /** Reads a <CODE>string</CODE> from the font file as bytes using the Cp1252
         *  encoding.
         * @param length the length of bytes to read
         * @return the <CODE>string</CODE> read
         * @throws IOException the font file could not be read
         */
        virtual protected string ReadStandardString(int length) {
            return rf.ReadString(length, "windows-1252");
        }
    
        /** Reads a Unicode <CODE>string</CODE> from the font file. Each character is
         *  represented by two bytes.
         * @param length the length of bytes to read. The <CODE>string</CODE> will have <CODE>length</CODE>/2
         * characters
         * @return the <CODE>string</CODE> read
         * @throws IOException the font file could not be read
         */
        virtual protected string ReadUnicodeString(int length) {
            StringBuilder buf = new StringBuilder();
            length /= 2;
            for (int k = 0; k < length; ++k) {
                buf.Append(rf.ReadChar());
            }
            return buf.ToString();
        }
    
        /** Reads the glyphs widths. The widths are extracted from the table 'hmtx'.
         *  The glyphs are normalized to 1000 units.
         * @throws DocumentException the font is invalid
         * @throws IOException the font file could not be read
         */
        virtual protected void ReadGlyphWidths() {
            int[] table_location;
            tables.TryGetValue("hmtx", out table_location);
            if (table_location == null)
                throw new DocumentException(MessageLocalization.GetComposedMessage("table.1.does.not.exist.in.2", "hmtx", fileName + style));
            rf.Seek(table_location[0]);
            glyphWidthsByIndex = new int[hhea.numberOfHMetrics];
            for (int k = 0; k < hhea.numberOfHMetrics; ++k) {
                glyphWidthsByIndex[k] = (rf.ReadUnsignedShort() * 1000) / head.unitsPerEm;
                int leftSideBearing = rf.ReadShort() * 1000 / head.unitsPerEm;
            }
        }
    
        /** Gets a glyph width.
         * @param glyph the glyph to get the width of
         * @return the width of the glyph in normalized 1000 units
         */
        virtual protected internal int GetGlyphWidth(int glyph) {
            if (glyph >= glyphWidthsByIndex.Length)
                glyph = glyphWidthsByIndex.Length - 1;
            return glyphWidthsByIndex[glyph];
        }
    
        private void ReadBbox() {
            int[] tableLocation;
            tables.TryGetValue("head", out tableLocation);
            if (tableLocation == null)
                throw new DocumentException(MessageLocalization.GetComposedMessage("table.1.does.not.exist.in.2", "head", fileName + style));
            rf.Seek(tableLocation[0] + TrueTypeFontSubSet.HEAD_LOCA_FORMAT_OFFSET);
            bool locaShortTable = (rf.ReadUnsignedShort() == 0);
            tables.TryGetValue("loca", out tableLocation);
            if (tableLocation == null)
                return;
            rf.Seek(tableLocation[0]);
            int[] locaTable;
            if (locaShortTable) {
                int entries = tableLocation[1] / 2;
                locaTable = new int[entries];
                for (int k = 0; k < entries; ++k)
                    locaTable[k] = rf.ReadUnsignedShort() * 2;
            }
            else {
                int entries = tableLocation[1] / 4;
                locaTable = new int[entries];
                for (int k = 0; k < entries; ++k)
                    locaTable[k] = rf.ReadInt();
            }
            tables.TryGetValue("glyf", out tableLocation);
            if (tableLocation == null)
                throw new DocumentException(MessageLocalization.GetComposedMessage("table.1.does.not.exist.in.2", "glyf", fileName + style));
            int tableGlyphOffset = tableLocation[0];
            bboxes = new int[locaTable.Length - 1][];
            for (int glyph = 0; glyph < locaTable.Length - 1; ++glyph) {
                int start = locaTable[glyph];
                if (start != locaTable[glyph + 1]) {
                    rf.Seek(tableGlyphOffset + start + 2);
                    bboxes[glyph] = new int[]{
                        (rf.ReadShort() * 1000) / head.unitsPerEm,
                        (rf.ReadShort() * 1000) / head.unitsPerEm,
                        (rf.ReadShort() * 1000) / head.unitsPerEm,
                        (rf.ReadShort() * 1000) / head.unitsPerEm};
                }
            }
        }

        /** Reads the several maps from the table 'cmap'. The maps of interest are 1.0 for symbolic
         *  fonts and 3.1 for all others. A symbolic font is defined as having the map 3.0.
         * @throws DocumentException the font is invalid
         * @throws IOException the font file could not be read
         */
        internal void ReadCMaps() {
            int[] table_location;
            tables.TryGetValue("cmap", out table_location);
            if (table_location == null)
                throw new DocumentException(MessageLocalization.GetComposedMessage("table.1.does.not.exist.in.2", "cmap", fileName + style));
            rf.Seek(table_location[0]);
            rf.SkipBytes(2);
            int num_tables = rf.ReadUnsignedShort();
            fontSpecific = false;
            int map10 = 0;
            int map31 = 0;
            int map30 = 0;

            //add by james for cmap Ext.b
            int mapExt = 0;

            for (int k = 0; k < num_tables; ++k) {
                int platId = rf.ReadUnsignedShort();
                int platSpecId = rf.ReadUnsignedShort();
                int offset = rf.ReadInt();
                if (platId == 3 && platSpecId == 0) {
                    fontSpecific = true;
                    map30 = offset;
                }
                else if (platId == 3 && platSpecId == 1) {
                    map31 = offset;
                }
                else if (platId == 3 && platSpecId == 10)
                {
                    mapExt = offset;
                }
 
                if (platId == 1 && platSpecId == 0) {
                    map10 = offset;
                }

                
            }
            if (map10 > 0) {
                rf.Seek(table_location[0] + map10);
                int format = rf.ReadUnsignedShort();
                switch (format) {
                    case 0:
                        cmap10 = ReadFormat0();
                        break;
                    case 4:
                        cmap10 = ReadFormat4();
                        break;
                    case 6:
                        cmap10 = ReadFormat6();
                        break;
                }
            }
            if (map31 > 0) {
                rf.Seek(table_location[0] + map31);
                int format = rf.ReadUnsignedShort();
                if (format == 4) {
                    cmap31 = ReadFormat4();
                }
            }
            if (map30 > 0) {
                rf.Seek(table_location[0] + map30);
                int format = rf.ReadUnsignedShort();
                if (format == 4) {
                    cmap10 = ReadFormat4();
                }
            }
            if (mapExt > 0) {
                rf.Seek(table_location[0] + mapExt);
                int format = rf.ReadUnsignedShort();
                switch (format) {
                    case 0:
                        cmapExt = ReadFormat0();
                        break;
                    case 4:
                        cmapExt = ReadFormat4();
                        break;
                    case 6:
                        cmapExt = ReadFormat6();
                        break;
                    case 12:
                        cmapExt = ReadFormat12();
                        break;
                }
            }
        }

        internal Dictionary<int,int[]> ReadFormat12() {
            Dictionary<int,int[]> h = new Dictionary<int,int[]>();
            rf.SkipBytes(2);
            int table_lenght = rf.ReadInt();
            rf.SkipBytes(4);
            int nGroups = rf.ReadInt();
            for (int k = 0; k < nGroups; k++) {
                int startCharCode = rf.ReadInt();
                int endCharCode = rf.ReadInt();
                int startGlyphID = rf.ReadInt();
                for (int i = startCharCode; i <= endCharCode; i++) {
                    int[] r = new int[2];
                    r[0] = startGlyphID;
                    r[1] = GetGlyphWidth(r[0]);
                    h[i] = r;
                    startGlyphID++;
                }
            }
            return h;
        }

        /** The information in the maps of the table 'cmap' is coded in several formats.
         *  Format 0 is the Apple standard character to glyph index mapping table.
         * @return a <CODE>Hashtable</CODE> representing this map
         * @throws IOException the font file could not be read
         */
        internal Dictionary<int,int[]> ReadFormat0() {
            Dictionary<int,int[]> h = new Dictionary<int,int[]>();
            rf.SkipBytes(4);
            for (int k = 0; k < 256; ++k) {
                int[] r = new int[2];
                r[0] = rf.ReadUnsignedByte();
                r[1] = GetGlyphWidth(r[0]);
                h[k] = r;
            }
            return h;
        }
    
        /** The information in the maps of the table 'cmap' is coded in several formats.
         *  Format 4 is the Microsoft standard character to glyph index mapping table.
         * @return a <CODE>Hashtable</CODE> representing this map
         * @throws IOException the font file could not be read
         */
        internal Dictionary<int,int[]> ReadFormat4() {
            Dictionary<int,int[]> h = new Dictionary<int,int[]>();
            int table_lenght = rf.ReadUnsignedShort();
            rf.SkipBytes(2);
            int segCount = rf.ReadUnsignedShort() / 2;
            rf.SkipBytes(6);
            int[] endCount = new int[segCount];
            for (int k = 0; k < segCount; ++k) {
                endCount[k] = rf.ReadUnsignedShort();
            }
            rf.SkipBytes(2);
            int[] startCount = new int[segCount];
            for (int k = 0; k < segCount; ++k) {
                startCount[k] = rf.ReadUnsignedShort();
            }
            int[] idDelta = new int[segCount];
            for (int k = 0; k < segCount; ++k) {
                idDelta[k] = rf.ReadUnsignedShort();
            }
            int[] idRO = new int[segCount];
            for (int k = 0; k < segCount; ++k) {
                idRO[k] = rf.ReadUnsignedShort();
            }
            int[] glyphId = new int[table_lenght / 2 - 8 - segCount * 4];
            for (int k = 0; k < glyphId.Length; ++k) {
                glyphId[k] = rf.ReadUnsignedShort();
            }
            for (int k = 0; k < segCount; ++k) {
                int glyph;
                for (int j = startCount[k]; j <= endCount[k] && j != 0xFFFF; ++j) {
                    if (idRO[k] == 0) {
                        glyph = (j + idDelta[k]) & 0xFFFF;
                    }
                    else {
                        int idx = k + idRO[k] / 2 - segCount + j - startCount[k];
                        if (idx >= glyphId.Length)
                            continue;
                        glyph = (glyphId[idx] + idDelta[k]) & 0xFFFF;
                    }
                    int[] r = new int[2];
                    r[0] = glyph;
                    r[1] = GetGlyphWidth(r[0]);
                    h[fontSpecific ? ((j & 0xff00) == 0xf000 ? j & 0xff : j) : j] = r;
                }
            }
            return h;
        }
    
        /** The information in the maps of the table 'cmap' is coded in several formats.
         *  Format 6 is a trimmed table mapping. It is similar to format 0 but can have
         *  less than 256 entries.
         * @return a <CODE>Hashtable</CODE> representing this map
         * @throws IOException the font file could not be read
         */
        internal Dictionary<int,int[]> ReadFormat6() {
            Dictionary<int,int[]> h = new Dictionary<int,int[]>();
            rf.SkipBytes(4);
            int start_code = rf.ReadUnsignedShort();
            int code_count = rf.ReadUnsignedShort();
            for (int k = 0; k < code_count; ++k) {
                int[] r = new int[2];
                r[0] = rf.ReadUnsignedShort();
                r[1] = GetGlyphWidth(r[0]);
                h[k + start_code] = r;
            }
            return h;
        }
    
        /** Reads the kerning information from the 'kern' table.
         * @throws IOException the font file could not be read
         */
        internal void ReadKerning() {
            int[] table_location;
            tables.TryGetValue("kern", out table_location);
            if (table_location == null)
                return;
            rf.Seek(table_location[0] + 2);
            int nTables = rf.ReadUnsignedShort();
            int checkpoint = table_location[0] + 4;
            int length = 0;
            for (int k = 0; k < nTables; ++k) {
                checkpoint += length;
                rf.Seek(checkpoint);
                rf.SkipBytes(2);
                length = rf.ReadUnsignedShort();
                int coverage = rf.ReadUnsignedShort();
                if ((coverage & 0xfff7) == 0x0001) {
                    int nPairs = rf.ReadUnsignedShort();
                    rf.SkipBytes(6);
                    for (int j = 0; j < nPairs; ++j) {
                        int pair = rf.ReadInt();
                        int value = ((int)rf.ReadShort() * 1000) / head.unitsPerEm;
                        kerning[pair] = value;
                    }
                }
            }
        }
    
        /** Gets the kerning between two Unicode chars.
         * @param char1 the first char
         * @param char2 the second char
         * @return the kerning to be applied
         */
        public override int GetKerning(int char1, int char2) {
            int[] metrics = GetMetricsTT(char1);
            if (metrics == null)
                return 0;
            int c1 = metrics[0];
            metrics = GetMetricsTT(char2);
            if (metrics == null)
                return 0;
            int c2 = metrics[0];
            return kerning[(c1 << 16) + c2];
        }
    
        /** Gets the width from the font according to the unicode char <CODE>c</CODE>.
         * If the <CODE>name</CODE> is null it's a symbolic font.
         * @param c the unicode char
         * @param name the glyph name
         * @return the width of the char
         */
        internal override int GetRawWidth(int c, string name) {
            int[] metric = GetMetricsTT(c);
            if (metric == null)
                return 0;
            return metric[1];
        }
    
        /** Generates the font descriptor for this font.
         * @return the PdfDictionary containing the font descriptor or <CODE>null</CODE>
         * @param subsetPrefix the subset prefix
         * @param fontStream the indirect reference to a PdfStream containing the font or <CODE>null</CODE>
         * @throws DocumentException if there is an error
         */
        virtual public PdfDictionary GetFontDescriptor(PdfIndirectReference fontStream, string subsetPrefix, PdfIndirectReference cidset) {
            PdfDictionary dic = new PdfDictionary(PdfName.FONTDESCRIPTOR);
            dic.Put(PdfName.ASCENT, new PdfNumber((int)os_2.sTypoAscender * 1000 / head.unitsPerEm));
            dic.Put(PdfName.CAPHEIGHT, new PdfNumber((int)os_2.sCapHeight * 1000 / head.unitsPerEm));
            dic.Put(PdfName.DESCENT, new PdfNumber((int)os_2.sTypoDescender * 1000 / head.unitsPerEm));
            dic.Put(PdfName.FONTBBOX, new PdfRectangle(
            (int)head.xMin * 1000 / head.unitsPerEm,
            (int)head.yMin * 1000 / head.unitsPerEm,
            (int)head.xMax * 1000 / head.unitsPerEm,
            (int)head.yMax * 1000 / head.unitsPerEm));
            if (cidset != null)
                dic.Put(PdfName.CIDSET, cidset);
            if (cff) {
                if (encoding.StartsWith("Identity-"))
                    dic.Put(PdfName.FONTNAME, new PdfName(subsetPrefix + fontName+"-"+encoding));
                else
                    dic.Put(PdfName.FONTNAME, new PdfName(subsetPrefix + fontName + style));
            }
            else
                dic.Put(PdfName.FONTNAME, new PdfName(subsetPrefix + fontName + style));
            dic.Put(PdfName.ITALICANGLE, new PdfNumber(italicAngle));
            dic.Put(PdfName.STEMV, new PdfNumber(80));
            if (fontStream != null) {
                if (cff)
                    dic.Put(PdfName.FONTFILE3, fontStream);
                else
                    dic.Put(PdfName.FONTFILE2, fontStream);
            }
            int flags = 0;
            if (isFixedPitch)
                flags |= 1;
            flags |= fontSpecific ? 4 : 32;
            if ((head.macStyle & 2) != 0)
                flags |= 64;
            if ((head.macStyle & 1) != 0)
                flags |= 262144;
            dic.Put(PdfName.FLAGS, new PdfNumber(flags));
            
            return dic;
        }
    
        /** Generates the font dictionary for this font.
         * @return the PdfDictionary containing the font dictionary
         * @param subsetPrefix the subset prefx
         * @param firstChar the first valid character
         * @param lastChar the last valid character
         * @param shortTag a 256 bytes long <CODE>byte</CODE> array where each unused byte is represented by 0
         * @param fontDescriptor the indirect reference to a PdfDictionary containing the font descriptor or <CODE>null</CODE>
         * @throws DocumentException if there is an error
         */
        virtual protected PdfDictionary GetFontBaseType(PdfIndirectReference fontDescriptor, string subsetPrefix, int firstChar, int lastChar, byte[] shortTag) {
            PdfDictionary dic = new PdfDictionary(PdfName.FONT);
            if (cff) {
                dic.Put(PdfName.SUBTYPE, PdfName.TYPE1);
                dic.Put(PdfName.BASEFONT, new PdfName(fontName + style));
            }
            else {
                dic.Put(PdfName.SUBTYPE, PdfName.TRUETYPE);
                dic.Put(PdfName.BASEFONT, new PdfName(subsetPrefix + fontName + style));
            }
            dic.Put(PdfName.BASEFONT, new PdfName(subsetPrefix + fontName + style));
            if (!fontSpecific) {
                for (int k = firstChar; k <= lastChar; ++k) {
                    if (!differences[k].Equals(notdef)) {
                        firstChar = k;
                        break;
                    }
                }
            if (encoding.Equals(CP1252) || encoding.Equals(MACROMAN))
                    dic.Put(PdfName.ENCODING, encoding.Equals(CP1252) ? PdfName.WIN_ANSI_ENCODING : PdfName.MAC_ROMAN_ENCODING);
                else {
                    PdfDictionary enc = new PdfDictionary(PdfName.ENCODING);
                    PdfArray dif = new PdfArray();
                    bool gap = true;                
                    for (int k = firstChar; k <= lastChar; ++k) {
                        if (shortTag[k] != 0) {
                            if (gap) {
                                dif.Add(new PdfNumber(k));
                                gap = false;
                            }
                            dif.Add(new PdfName(differences[k]));
                        }
                        else
                            gap = true;
                    }
                    enc.Put(PdfName.DIFFERENCES, dif);
                    dic.Put(PdfName.ENCODING, enc);
                }
            }
            dic.Put(PdfName.FIRSTCHAR, new PdfNumber(firstChar));
            dic.Put(PdfName.LASTCHAR, new PdfNumber(lastChar));
            PdfArray wd = new PdfArray();
            for (int k = firstChar; k <= lastChar; ++k) {
                if (shortTag[k] == 0)
                    wd.Add(new PdfNumber(0));
                else
                    wd.Add(new PdfNumber(widths[k]));
            }
            dic.Put(PdfName.WIDTHS, wd);
            if (fontDescriptor != null)
                dic.Put(PdfName.FONTDESCRIPTOR, fontDescriptor);
            return dic;
        }
        
        virtual public byte[] GetFullFont() {
            lock (head) {
                RandomAccessFileOrArray rf2 = null;
                try {
                    rf2 = new RandomAccessFileOrArray(rf);
                    rf2.ReOpen();
                    byte[] b = new byte[rf2.Length];
                    rf2.ReadFully(b);
                    return b;
                } 
                finally {
                    try {if (rf2 != null) rf2.Close();} catch {}
                }
            }
        }
        
        virtual protected internal byte[] GetSubSet(HashSet2<int> glyphs, bool subsetp)  {
            lock (head) {
                TrueTypeFontSubSet sb = new TrueTypeFontSubSet(fileName, new RandomAccessFileOrArray(rf), glyphs, directoryOffset, true, !subsetp);
                return sb.Process();
            }
        }
        protected static int[] CompactRanges(List<int[]> ranges) {
            List<int[]> simp = new List<int[]>();
            for (int k = 0; k < ranges.Count; ++k) {
                int[] r = ranges[k];
                for (int j = 0; j < r.Length; j += 2) {
                    simp.Add(new int[]{Math.Max(0, Math.Min(r[j], r[j + 1])), Math.Min(0xffff, Math.Max(r[j], r[j + 1]))});
                }
            }
            for (int k1 = 0; k1 < simp.Count - 1; ++k1) {
                for (int k2 = k1 + 1; k2 < simp.Count; ++k2) {
                    int[] r1 = simp[k1];
                    int[] r2 = simp[k2];
                    if ((r1[0] >= r2[0] && r1[0] <= r2[1]) || (r1[1] >= r2[0] && r1[0] <= r2[1])) {
                        r1[0] = Math.Min(r1[0], r2[0]);
                        r1[1] = Math.Max(r1[1], r2[1]);
                        simp.RemoveAt(k2);
                        --k2;
                    }
                }
            }
            int[] s = new int[simp.Count * 2];
            for (int k = 0; k < simp.Count; ++k) {
                int[] r = simp[k];
                s[k * 2] = r[0];
                s[k * 2 + 1] = r[1];
            }
            return s;
        }
        
        virtual public void AddRangeUni(Dictionary<int, int[]> longTag, bool includeMetrics, bool subsetp) {
            if (!subsetp && (subsetRanges != null || directoryOffset > 0)) {
                int[] rg = (subsetRanges == null && directoryOffset > 0) ? new int[]{0, 0xffff} : CompactRanges(subsetRanges);
                Dictionary<int, int[]> usemap;
                if (!fontSpecific && cmap31 != null) 
                    usemap = cmap31;
                else if (fontSpecific && cmap10 != null) 
                    usemap = cmap10;
                else if (cmap31 != null) 
                    usemap = cmap31;
                else 
                    usemap = cmap10;
                foreach (KeyValuePair<int,int[]> e in usemap) {
                    int[] v = e.Value;
                    int gi = v[0];
                    if (longTag.ContainsKey(gi))
                        continue;
                    int c = e.Key;
                    bool skip = true;
                    for (int k = 0; k < rg.Length; k += 2) {
                        if (c >= rg[k] && c <= rg[k + 1]) {
                            skip = false;
                            break;
                        }
                    }
                    if (!skip)
                        longTag[gi] = includeMetrics ? new int[]{v[0], v[1], c} : null;
                }
            }
        }
        
        /** Outputs to the writer the font dictionaries and streams.
         * @param writer the writer for this document
         * @param ref the font indirect reference
         * @param params several parameters that depend on the font type
         * @throws IOException on error
         * @throws DocumentException error in generating the object
         */
        internal override void WriteFont(PdfWriter writer, PdfIndirectReference piref, Object[] parms) {
            int firstChar = (int)parms[0];
            int lastChar = (int)parms[1];
            byte[] shortTag = (byte[])parms[2];
            bool subsetp = (bool)parms[3] && subset;
            if (!subsetp) {
                firstChar = 0;
                lastChar = shortTag.Length - 1;
                for (int k = 0; k < shortTag.Length; ++k)
                    shortTag[k] = 1;
            }
            PdfIndirectReference ind_font = null;
            PdfObject pobj = null;
            PdfIndirectObject obj = null;
            string subsetPrefix = "";
            if (embedded) {
                if (cff) {
                    pobj = new StreamFont(ReadCffFont(), "Type1C", compressionLevel);
                    obj = writer.AddToBody(pobj);
                    ind_font = obj.IndirectReference;
                }
                else {
                    if (subsetp)
                        subsetPrefix = CreateSubsetPrefix();
                    Dictionary<int, int[]> glyphs = new Dictionary<int, int[]>();
                    for (int k = firstChar; k <= lastChar; ++k) {
                        if (shortTag[k] != 0) {
                            int[] metrics = null;
                            if (specialMap != null) {
                                int[] cd = GlyphList.NameToUnicode(differences[k]);
                                if (cd != null)
                                    metrics = GetMetricsTT(cd[0]);
                            }
                            else {
                                if (fontSpecific)
                                    metrics = GetMetricsTT(k);
                                else
                                    metrics = GetMetricsTT(unicodeDifferences[k]);
                            }
                            if (metrics != null)
                                glyphs[metrics[0]] = null;
                        }
                    }
                    AddRangeUni(new Dictionary<int, int[]>(glyphs), false, subsetp);
                    byte[] b = null;
                    if (subsetp || directoryOffset != 0 || subsetRanges != null) {
                        b = GetSubSet(new HashSet2<int>(glyphs.Keys), subsetp);
                    }
                    else {
                        b = GetFullFont();
                    }
                    int[] lengths = new int[]{b.Length};
                    pobj = new StreamFont(b, lengths, compressionLevel);
                    obj = writer.AddToBody(pobj);
                    ind_font = obj.IndirectReference;
                }
            }
            pobj = GetFontDescriptor(ind_font, subsetPrefix, null);
            if (pobj != null){
                obj = writer.AddToBody(pobj);
                ind_font = obj.IndirectReference;
            }
            pobj = GetFontBaseType(ind_font, subsetPrefix, firstChar, lastChar, shortTag);
            writer.AddToBody(pobj, piref);
        }
    
        /**
        * If this font file is using the Compact Font File Format, then this method
        * will return the raw bytes needed for the font stream. If this method is
        * ever made public: make sure to add a test if (cff == true).
        * @return  a byte array
        * @since   2.1.3
        */
        virtual public byte[] ReadCffFont() {
            RandomAccessFileOrArray rf2 = new RandomAccessFileOrArray(rf);
            byte[] b = new byte[cffLength];
            try {
                rf2.ReOpen();
                rf2.Seek(cffOffset);
                rf2.ReadFully(b);
            }
            finally {
                try {
                    rf2.Close();
                }
                catch {
                    // empty on purpose
                }
            }
            return b;
        }

        /**
        * Returns a PdfStream object with the full font program.
        * @return  a PdfStream with the font program
        * @since   2.1.3
        */
        public override PdfStream GetFullFontStream() {
            if (cff) {
                return new StreamFont(ReadCffFont(), "Type1C", compressionLevel);
            }
            else {
                byte[] b = GetFullFont();
                int[] lengths = new int[]{b.Length};
                return new StreamFont(b, lengths, compressionLevel);
            }
        }
        
        /** Gets the font parameter identified by <CODE>key</CODE>. Valid values
         * for <CODE>key</CODE> are <CODE>ASCENT</CODE>, <CODE>CAPHEIGHT</CODE>, <CODE>DESCENT</CODE>
         * and <CODE>ITALICANGLE</CODE>.
         * @param key the parameter to be extracted
         * @param fontSize the font size in points
         * @return the parameter in points
         */    
        public override float GetFontDescriptor(int key, float fontSize) {
            switch (key) {
                case ASCENT:
                    return (float)os_2.sTypoAscender * fontSize / (float)head.unitsPerEm;
                case CAPHEIGHT:
                    return (float)os_2.sCapHeight * fontSize / (float)head.unitsPerEm;
                case DESCENT:
                    return (float)os_2.sTypoDescender * fontSize / (float)head.unitsPerEm;
                case ITALICANGLE:
                    return (float)italicAngle;
                case BBOXLLX:
                    return fontSize * (int)head.xMin / head.unitsPerEm;
                case BBOXLLY:
                    return fontSize * (int)head.yMin / head.unitsPerEm;
                case BBOXURX:
                    return fontSize * (int)head.xMax / head.unitsPerEm;
                case BBOXURY:
                    return fontSize * (int)head.yMax / head.unitsPerEm;
                case AWT_ASCENT:
                    return fontSize * (int)hhea.Ascender / head.unitsPerEm;
                case AWT_DESCENT:
                    return fontSize * (int)hhea.Descender / head.unitsPerEm;
                case AWT_LEADING:
                    return fontSize * (int)hhea.LineGap / head.unitsPerEm;
                case AWT_MAXADVANCE:
                    return fontSize * (int)hhea.advanceWidthMax / head.unitsPerEm;
                case UNDERLINE_POSITION:
                    return (underlinePosition - underlineThickness / 2) * fontSize / head.unitsPerEm;
                case UNDERLINE_THICKNESS:
                    return underlineThickness * fontSize / head.unitsPerEm;
                case STRIKETHROUGH_POSITION:
                    return os_2.yStrikeoutPosition * fontSize / head.unitsPerEm;
                case STRIKETHROUGH_THICKNESS:
                    return os_2.yStrikeoutSize * fontSize / head.unitsPerEm;
                case SUBSCRIPT_SIZE:
                    return os_2.ySubscriptYSize * fontSize / head.unitsPerEm;
                case SUBSCRIPT_OFFSET:
                    return -os_2.ySubscriptYOffset * fontSize / head.unitsPerEm;
                case SUPERSCRIPT_SIZE:
                    return os_2.ySuperscriptYSize * fontSize / head.unitsPerEm;
                case SUPERSCRIPT_OFFSET:
                    return os_2.ySuperscriptYOffset * fontSize / head.unitsPerEm;
                case WEIGHT_CLASS:
                    return os_2.usWeightClass;
                case WIDTH_CLASS:
                    return os_2.usWidthClass;            }
            return 0;
        }
    
        /** Gets the glyph index and metrics for a character.
         * @param c the character
         * @return an <CODE>int</CODE> array with {glyph index, width}
         */    
        public virtual int[] GetMetricsTT(int c) {
            int[] ret = null;
            if (cmapExt != null)
                cmapExt.TryGetValue(c, out ret);
            else if (!fontSpecific && cmap31 != null)
                cmap31.TryGetValue(c, out ret);
            else if (fontSpecific && cmap10 != null) 
                cmap10.TryGetValue(c, out ret);
            else if (cmap31 != null) 
                cmap31.TryGetValue(c, out ret);
            else if (cmap10 != null) 
                cmap10.TryGetValue(c, out ret);
            return ret;
        }

        /** Gets the postscript font name.
         * @return the postscript font name
         */
        public override string PostscriptFontName {
            get {
                return fontName;
            }
            set {
                fontName = value;
            }
        }

        /** Gets the code pages supported by the font.
         * @return the code pages supported by the font
         */
        public override string[] CodePagesSupported {
            get {
                long cp = (((long)os_2.ulCodePageRange2) << 32) + ((long)os_2.ulCodePageRange1 & 0xffffffffL);
                int count = 0;
                long bit = 1;
                for (int k = 0; k < 64; ++k) {
                    if ((cp & bit) != 0 && codePages[k] != null)
                        ++count;
                    bit <<= 1;
                }
                string[] ret = new string[count];
                count = 0;
                bit = 1;
                for (int k = 0; k < 64; ++k) {
                    if ((cp & bit) != 0 && codePages[k] != null)
                        ret[count++] = codePages[k];
                    bit <<= 1;
                }
                return ret;
            }
        }
    
        /** Gets the full name of the font. If it is a True Type font
         * each array element will have {Platform ID, Platform Encoding ID,
         * Language ID, font name}. The interpretation of this values can be
         * found in the Open Type specification, chapter 2, in the 'name' table.<br>
         * For the other fonts the array has a single element with {"", "", "",
         * font name}.
         * @return the full name of the font
         */
        public override string[][] FullFontName {
            get {
                return fullName;
            }
        }

        /**
         * Gets the full subfamily name of the font. If it is a True Type font
         * each array element will have {Platform ID, Platform Encoding ID,
         * Language ID, subfamily}. The interpretation of this values can be
         * found in the Open Type specification, chapter 2, in the 'name' table.<br>
         * @return the full subfamily name of the font
         */
        public override string Subfamily {
            get {
                if (subFamily != null && subFamily.Length > 0) {
                    return subFamily[0][3];
                }
                return base.Subfamily;
            }
        }
    
        /** Gets all the entries of the Names-Table. If it is a True Type font
        * each array element will have {Name ID, Platform ID, Platform Encoding ID,
        * Language ID, font name}. The interpretation of this values can be
        * found in the Open Type specification, chapter 2, in the 'name' table.<br>
        * For the other fonts the array has a single element with {"", "", "",
        * font name}.
        * @return the full name of the font
        */
        public override string[][] AllNameEntries {
            get {
                return allNameEntries;
            }
        }
        
        /** Gets the family name of the font. If it is a True Type font
         * each array element will have {Platform ID, Platform Encoding ID,
         * Language ID, font name}. The interpretation of this values can be
         * found in the Open Type specification, chapter 2, in the 'name' table.<br>
         * For the other fonts the array has a single element with {"", "", "",
         * font name}.
         * @return the family name of the font
         */
        public override string[][] FamilyFontName {
            get {
                return familyName;
            }
        }
    
        /** Checks if the font has any kerning pairs.
        * @return <CODE>true</CODE> if the font has any kerning pairs
        */    
        public override bool HasKernPairs() {
            return kerning.Size > 0;
        }    
        
        /**
        * Sets the kerning between two Unicode chars.
        * @param char1 the first char
        * @param char2 the second char
        * @param kern the kerning to apply in normalized 1000 units
        * @return <code>true</code> if the kerning was applied, <code>false</code> otherwise
        */
        public override bool SetKerning(int char1, int char2, int kern) {
            int[] metrics = GetMetricsTT(char1);
            if (metrics == null)
                return false;
            int c1 = metrics[0];
            metrics = GetMetricsTT(char2);
            if (metrics == null)
                return false;
            int c2 = metrics[0];
            kerning[(c1 << 16) + c2] = kern;
            return true;
        }

        protected override int[] GetRawCharBBox(int c, String name) {
            Dictionary<int,int[]> map = null;
            if (name == null || cmap31 == null)
                map = cmap10;
            else
                map = cmap31;
            if (map == null)
                return null;
            int[] metric;
            map.TryGetValue(c, out metric);
            if (metric == null || bboxes == null)
                return null;
            return bboxes[metric[0]];
        }

        public int MaxGlyphId {
            get { return maxGlyphId; }
        }
    }
}
