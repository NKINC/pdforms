using System;
/*
 * $Id: Logger.java 4863 2011-05-12 07:01:55Z redlab_b $
 *
 * This file is part of the iText (R) project. Copyright (c) 1998-2016 iText Group NV
 * BVBA Authors: Balder Van Camp, Emiel Ackermann, et al.
 *
 * This program is free software; you can redistribute it and/or modify it under
 * the terms of the GNU Affero General License version 3 as published by the
 * Free Software Foundation with the addition of the following permission added
 * to Section 15 as permitted in Section 7(a): FOR ANY PART OF THE COVERED WORK
 * IN WHICH THE COPYRIGHT IS OWNED BY ITEXT GROUP, ITEXT GROUP DISCLAIMS THE WARRANTY OF NON
 * INFRINGEMENT OF THIRD PARTY RIGHTS.
 *
 * This program is distributed in the hope that it will be useful, but WITHOUT
 * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
 * FOR A PARTICULAR PURPOSE. See the GNU Affero General License for more
 * details. You should have received a copy of the GNU Affero General License
 * along with this program; if not, see http://www.gnu.org/licenses or write to
 * the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
 * MA, 02110-1301 USA, or download the license from the following URL:
 * http://itextpdf.com/terms-of-use/
 *
 * The interactive user interfaces in modified source and object code versions
 * of this program must display Appropriate Legal Notices, as required under
 * Section 5 of the GNU Affero General License.
 *
 * In accordance with Section 7(b) of the GNU Affero General License, a covered
 * work must retain the producer line in every PDF that is created or
 * manipulated using iText.
 *
 * You can be released from the requirements of the license by purchasing a
 * commercial license. Buying such a license is mandatory as soon as you develop
 * commercial activities involving the iText software without disclosing the
 * source code of your own applications. These activities include: offering paid
 * services to customers as an ASP, serving PDFs on the fly in a web
 * application, shipping iText with a closed source product.
 *
 * For more information, please contact iText Software Corp. at this address:
 * sales@itextpdf.com
 */
namespace iTextSharp.text.log {

    /**
     * Logger interface
     * {@link LoggerFactory#setLogger(Logger)}.
     *
     * @author redlab_b
     *
     */
    public interface ILogger {

        /**
         * @param klass
         * @return the logger for the given klass
         */
        ILogger GetLogger(Type klass);

        ILogger GetLogger(String name);
        /**
         * @param level
         * @return true if there should be logged for the given level
         */
        bool IsLogging(Level level);
        /**
         * Log a warning message.
         * @param message
         */
        void Warn(String message);

        /**
         * Log a trace message.
         * @param message
         */
        void Trace(String message);

        /**
         * Log a debug message.
         * @param message
         */
        void Debug(String message);

        /**
         * Log an info message.
         * @param message
         */
        void Info(String message);
        /**
         * Log an error message.
         * @param message
         */
        void Error(String message);

        /**
         * Log an error message and exception.
         * @param message
         * @param e
         */
        void Error(String message, Exception e);
    }
}
