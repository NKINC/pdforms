using System.Collections;
using System.Text;

//Copyright (c) 2006, Adobe Systems Incorporated
//All rights reserved.
//
//        Redistribution and use in source and binary forms, with or without
//        modification, are permitted provided that the following conditions are met:
//        1. Redistributions of source code must retain the above copyright
//        notice, this list of conditions and the following disclaimer.
//        2. Redistributions in binary form must reproduce the above copyright
//        notice, this list of conditions and the following disclaimer in the
//        documentation and/or other materials provided with the distribution.
//        3. All advertising materials mentioning features or use of this software
//        must display the following acknowledgement:
//        This product includes software developed by the Adobe Systems Incorporated.
//        4. Neither the name of the Adobe Systems Incorporated nor the
//        names of its contributors may be used to endorse or promote products
//        derived from this software without specific prior written permission.
//
//        THIS SOFTWARE IS PROVIDED BY ADOBE SYSTEMS INCORPORATED ''AS IS'' AND ANY
//        EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
//        WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
//        DISCLAIMED. IN NO EVENT SHALL ADOBE SYSTEMS INCORPORATED BE LIABLE FOR ANY
//        DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
//        (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
//        LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
//        ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
//        (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//        SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
//        http://www.adobe.com/devnet/xmp/library/eula-xmp-library-java.html

namespace iTextSharp.xmp.impl.xpath {
    /// <summary>
    /// Representates an XMP XmpPath with segment accessor methods.
    /// 
    /// @since   28.02.2006
    /// </summary>
    public class XmpPath {
        // Bits for XPathStepInfo options.

        /// <summary>
        /// Marks a struct field step , also for top level nodes (schema "fields"). </summary>
        public const uint STRUCT_FIELD_STEP = 0x01;

        /// <summary>
        /// Marks a qualifier step. 
        ///  Note: Order is significant to separate struct/qual from array kinds! 
        /// </summary>
        public const uint QUALIFIER_STEP = 0x02;

        /// <summary>
        /// Marks an array index step </summary>
        public const uint ARRAY_INDEX_STEP = 0x03;

        public const uint ARRAY_LAST_STEP = 0x04;
        public const uint QUAL_SELECTOR_STEP = 0x05;
        public const uint FIELD_SELECTOR_STEP = 0x06;
        public const uint SCHEMA_NODE = 0x80000000;
        public const uint STEP_SCHEMA = 0;
        public const uint STEP_ROOT_PROP = 1;


        /// <summary>
        /// stores the segments of an XmpPath </summary>
        private readonly IList _segments = new ArrayList(5);


        /// <summary>
        /// Append a path segment
        /// </summary>
        /// <param name="segment"> the segment to add </param>
        public virtual void Add(XmpPathSegment segment) {
            _segments.Add(segment);
        }


        /// <param name="index"> the index of the segment to return </param>
        /// <returns> Returns a path segment. </returns>
        public virtual XmpPathSegment GetSegment(int index) {
            return (XmpPathSegment) _segments[index];
        }


        /// <returns> Returns the size of the xmp path.  </returns>
        public virtual int Size() {
            return _segments.Count;
        }


        /// <summary>
        /// Serializes the normalized XMP-path. </summary>
        /// <seealso cref= Object#toString() </seealso>
        public override string ToString() {
            StringBuilder result = new StringBuilder();
            int index = 1;
            while (index < Size()) {
                result.Append(GetSegment(index));
                if (index < Size() - 1) {
                    uint kind = GetSegment(index + 1).Kind;
                    if (kind == STRUCT_FIELD_STEP || kind == QUALIFIER_STEP) {
                        // all but last and array indices
                        result.Append('/');
                    }
                }
                index++;
            }

            return result.ToString();
        }
    }
}
