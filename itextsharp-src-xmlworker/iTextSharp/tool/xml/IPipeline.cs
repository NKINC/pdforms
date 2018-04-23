using System;
/*
 * $Id: IPipeline.java 160 2011-06-07 09:34:57Z redlab_b $
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
namespace iTextSharp.tool.xml {

    /**
     * @author redlab_b
     * @param <T> the type of CustomContext
     *
     */
    public interface IPipeline {

        /**
         * @param context
         * @return the next pipeline in line
         * @throws PipelineException
         */
        IPipeline Init(IWorkerContext context);
        /**
         * Called when an opening tag has been encountered.
         * @param context the IWorkerContext
         * @param t the Tag
         * @param po a processObject to put {@link Writable}s in
         * @return the next pipeline in line
         * @throws PipelineException can be thrown to indicate that something went wrong.
         */
        IPipeline Open(IWorkerContext context, Tag t, ProcessObject po);

        /**
         * Called when content has been encountered.
         * @param context the IWorkerContext
         * @param t the Tag
         * @param content the content
         * @param po a processObject to put {@link Writable}s in
         * @return the next pipeline in line
         * @throws PipelineException can be thrown to indicate that something went wrong.
         */
        IPipeline Content(IWorkerContext context, Tag t, string content, ProcessObject po);

        /**
         * Called when a closing tag has been encountered.
         * @param context the IWorkerContext
         * @param t the Tag
         * @param po a processObject to put {@link Writable}s in
         * @return the next pipeline in line
         * @throws PipelineException  can be thrown to indicate that something went wrong.
         */
        IPipeline Close(IWorkerContext context, Tag t, ProcessObject po);

        /**
         * Returns the next pipeline in line.
         * @return the next pipeline
         */
        IPipeline GetNext();
    }
}
