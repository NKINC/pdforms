using System;
using System.Collections.Generic;
using iTextSharp.tool.xml.exceptions;
/*
 * $Id: DefaultTagProcessorFactory.java 151 2011-06-06 10:52:12Z redlab_b $
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
     * A Default implementation of the TagProcessorFactory that uses a map to store
     * the TagProcessors. Within the same {@link ClassLoader}s this Processor can
     * also load the processors when they are only stored with there fully qualified
     * class names.<br />
     * <strong>Note:</strong> this implementation does not use namespaces (yet)!
     *
     *
     * @author redlab_b
     *
     */
    public class DefaultTagProcessorFactory : ITagProcessorFactory {

        /**
         * Internal Object to keep TagProcessors.
         * @author redlab_b
         *
         */
        protected class FactoryObject {
            private String className;
            private ITagProcessor proc;
            private DefaultTagProcessorFactory parent;

            /**
             * @param className the fully qualified class name
             *
             */
            public FactoryObject(String className, DefaultTagProcessorFactory parent) {
                this.className = className;
                this.parent = parent;
            }

            /**
             * @param className the fully qualified class name
             * @param processor the processor object
             */
            public FactoryObject(String className, ITagProcessor processor, DefaultTagProcessorFactory parent) : this(className, parent) {
                this.proc = processor;

            }

            /**
             *
             * @return the className
             */
            virtual public String ClassName {
                get {
                    return this.className;
                }
            }

            /**
             * @return return the processor
             */
            virtual public ITagProcessor Processor {
                get {
                    if (null == this.proc) {
                        this.proc = parent.Load(this.className);
                    }
                    return proc;
                }
            }
        }

        private IDictionary<String, FactoryObject> map;

        /**
         *
         */
        public DefaultTagProcessorFactory() {
            this.map = new Dictionary<String, FactoryObject>();
        }

        /**
         * Tries to load given processor with Class.forName
         * @param className fully qualified className
         * @return the loaded tag processor
         * @throws NoTagProcessorException if ITagProcessor could not be loaded.
         */
        protected virtual ITagProcessor Load(String className) {
            try {
                return (ITagProcessor) Activator.CreateInstance(null, className).Unwrap();
            } catch (Exception e) {
                throw new NoTagProcessorException(String.Format(LocaleMessages.GetInstance().GetMessage(LocaleMessages.NO_TAGPROCESSOR), className), e);
            }
        }

        /**
         * @throws NoTagProcessorException when the processor was not found for the given tag.
         */
        virtual public ITagProcessor GetProcessor(String tag, String nameSpace) {
            FactoryObject fo;
            if (map.TryGetValue(tag.ToLower(), out fo) && fo != null) {
                return fo.Processor;
            }
            throw new NoTagProcessorException(tag);
        }

        /**
         * Add an unloaded ITagProcessor.
         *
         * @param tag the tag the processor with the given className maps to
         * @param className the fully qualified class name (class has to be found on classpath, will be loaded with Class.ForName()
         */
        virtual public void AddProcessor(String tag, String className) {
            map[tag] = new FactoryObject(className, this);
        }

        /**
         * Add a loaded ITagProcessor.
         *
         * @param tag the tag the processor with the given className maps to
         * @param processor the ITagProcessor
         */
        virtual public void AddProcessor(String tag, ITagProcessor processor) {
            map[tag] = new FactoryObject(processor.GetType().FullName, processor, this);

        }
        /**
         *
         */
        virtual public void AddProcessor( ITagProcessor processor, String[] tags) {
            foreach (String tag in tags) {
                AddProcessor(tag, processor);
            }
        }
        /**
         * Add one tag processor that handles multiple tags.
         * @param className the fully qualified class name (class has to be found on classpath)
         * @param tags list of tags this processor maps to.
         */
        virtual public void AddProcessor( String className, String[] tags) {
            foreach (String tag in tags) {
                AddProcessor(tag, className);
            }
        }

        /* (non-Javadoc)
         * @see com.itextpdf.tool.xml.html.TagProcessorFactory#removeProcessor(java.lang.String)
         */
        virtual public void RemoveProcessor(String tag) {
            map.Remove(tag);
        }
    }
}
