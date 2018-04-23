using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.log;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using iTextSharp.tool.xml.css;
using iTextSharp.tool.xml.exceptions;
using iTextSharp.tool.xml.net;
using iTextSharp.tool.xml.net.exc;
using iTextSharp.tool.xml.pipeline.html;
/*
 * $Id: Image.java 118 2011-05-27 11:10:19Z redlab_b $
 *
 * This file is part of the iText (R) project.
 * Copyright (c) 1998-2016 iText Group NV
 * Authors: Balder Van Camp, Emiel Ackermann, et al.
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
namespace iTextSharp.tool.xml.html {

    /**
     * @author redlab_b
     *
     */
    public class Image : AbstractTagProcessor {

        private CssUtils utils = CssUtils.GetInstance();
        private static ILogger logger = LoggerFactory.GetLogger(typeof(iTextSharp.tool.xml.html.Image));

        /*
         * (non-Javadoc)
         *
         * @see
         * com.itextpdf.tool.xml.ITagProcessor#endElement(com.itextpdf.tool.xml.Tag,
         * java.util.List, com.itextpdf.text.Document)
         */
        public override IList<IElement> End(IWorkerContext ctx, Tag tag, IList<IElement> currentContent) {
            IDictionary<String, String> attributes = tag.Attributes;
            String src;
            attributes.TryGetValue(HTML.Attribute.SRC, out src);
            iTextSharp.text.Image img = null;
            IList<IElement> l = new List<IElement>(1);
            if (!string.IsNullOrEmpty(src)) {
                src = XMLUtil.UnescapeXML(src);
                src = src.Trim();
                // check if the image was already added once
                try {
                    if (logger.IsLogging(Level.TRACE)) {
                        logger.Trace(String.Format(LocaleMessages.GetInstance().GetMessage(LocaleMessages.HTML_IMG_USE), src));
                    }
                    HtmlPipelineContext context = GetHtmlPipelineContext(ctx);
                    img = new ImageRetrieve(context.ResourcePath, context.GetImageProvider()).RetrieveImage(src);
                } catch (NoImageException e) {
                    if (logger.IsLogging(Level.ERROR)) {
                        logger.Error(string.Format(LocaleMessages.GetInstance().GetMessage(LocaleMessages.HTML_IMG_RETRIEVE_FAIL), src), e);
                    }
                } catch (NoCustomContextException e) {
                    throw new RuntimeWorkerException(LocaleMessages.GetInstance().GetMessage(LocaleMessages.NO_CUSTOM_CONTEXT), e);
                }
                if (null != img) {
                    try {
                        String alt;
                        attributes.TryGetValue(HTML.Attribute.ALT, out alt);

                        if (alt != null) {
                            img.SetAccessibleAttribute(PdfName.ALT, new PdfString(attributes[HTML.Attribute.ALT]));
                        }

                        HtmlPipelineContext htmlPipelineContext = GetHtmlPipelineContext(ctx);
                        l.Add(GetCssAppliers().Apply(new Chunk((iTextSharp.text.Image) GetCssAppliers().Apply(img, tag, htmlPipelineContext), 0, 0, true), tag, htmlPipelineContext));
                    } catch (NoCustomContextException e) {
                        throw new RuntimeWorkerException(e);
                    }
                }
            }
            return l;
        }


        /*
         * (non-Javadoc)
         *
         * @see com.itextpdf.tool.xml.ITagProcessor#isStackOwner()
         */
        public override bool IsStackOwner() {
            return false;
        }
    }
}
