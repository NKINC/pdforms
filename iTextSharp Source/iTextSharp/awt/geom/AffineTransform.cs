/*
 * $Id$
 *
 * This file is part of the iText (R) project.
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

using System;

namespace iTextSharp.awt.geom
{
    public class AffineTransform : ICloneable {

        public const int TYPE_IDENTITY = 0;
        public const int TYPE_TRANSLATION = 1;
        public const int TYPE_UNIFORM_SCALE = 2;
        public const int TYPE_GENERAL_SCALE = 4;
        public const int TYPE_QUADRANT_ROTATION = 8;
        public const int TYPE_GENERAL_ROTATION = 16;
        public const int TYPE_GENERAL_TRANSFORM = 32;
        public const int TYPE_FLIP = 64;
        public const int TYPE_MASK_SCALE = TYPE_UNIFORM_SCALE | TYPE_GENERAL_SCALE;
        public const int TYPE_MASK_ROTATION = TYPE_QUADRANT_ROTATION | TYPE_GENERAL_ROTATION;

        /**
         * The <code>TYPE_UNKNOWN</code> is an initial type value
         */
        const int TYPE_UNKNOWN = -1;
    
        /**
         * The min value equivalent to zero. If absolute value less then ZERO it considered as zero.  
         */
        const double ZERO = 1E-10;
   
        /**
         * The values of transformation matrix
         */
        double m00;
        double m10;
        double m01;
        double m11;
        double m02;
        double m12;

        /**
         * The transformation <code>type</code> 
         */
        int type;

        public AffineTransform() {
            type = TYPE_IDENTITY;
            m00 = m11 = 1.0;
            m10 = m01 = m02 = m12 = 0.0;
        }

        public AffineTransform(AffineTransform t) {
            this.type = t.type;
            this.m00 = t.m00;
            this.m10 = t.m10;
            this.m01 = t.m01;
            this.m11 = t.m11;
            this.m02 = t.m02;
            this.m12 = t.m12;
        }

        public AffineTransform(float m00, float m10, float m01, float m11, float m02, float m12) {
            this.type = TYPE_UNKNOWN;
            this.m00 = m00;
            this.m10 = m10;
            this.m01 = m01;
            this.m11 = m11;
            this.m02 = m02;
            this.m12 = m12;
        }

        public AffineTransform(double m00, double m10, double m01, double m11, double m02, double m12) {
            this.type = TYPE_UNKNOWN;
            this.m00 = m00;
            this.m10 = m10;
            this.m01 = m01;
            this.m11 = m11;
            this.m02 = m02;
            this.m12 = m12;
        }

        public AffineTransform(float[] matrix) {
            this.type = TYPE_UNKNOWN;
            m00 = matrix[0];
            m10 = matrix[1];
            m01 = matrix[2];
            m11 = matrix[3];
            if (matrix.Length > 4) {
                m02 = matrix[4];
                m12 = matrix[5];
            }
        }

        public AffineTransform(double[] matrix) {
            this.type = TYPE_UNKNOWN;
            m00 = matrix[0];
            m10 = matrix[1];
            m01 = matrix[2];
            m11 = matrix[3];
            if (matrix.Length > 4) {
                m02 = matrix[4];
                m12 = matrix[5];
            }
        }

        /*
         * Method returns type of affine transformation.
         * 
         * Transform matrix is
         *   m00 m01 m02
         *   m10 m11 m12
         * 
         * According analytic geometry new basis vectors are (m00, m01) and (m10, m11), 
         * translation vector is (m02, m12). Original basis vectors are (1, 0) and (0, 1). 
         * Type transformations classification:  
         *   TYPE_IDENTITY - new basis equals original one and zero translation
         *   TYPE_TRANSLATION - translation vector isn't zero  
         *   TYPE_UNIFORM_SCALE - vectors length of new basis equals
         *   TYPE_GENERAL_SCALE - vectors length of new basis doesn't equal 
         *   TYPE_FLIP - new basis vector orientation differ from original one
         *   TYPE_QUADRANT_ROTATION - new basis is rotated by 90, 180, 270, or 360 degrees     
         *   TYPE_GENERAL_ROTATION - new basis is rotated by arbitrary angle
         *   TYPE_GENERAL_TRANSFORM - transformation can't be inversed
         */
        virtual public int Type {
            get {
                if (this.type != TYPE_UNKNOWN)
                {
                    return this.type;
                }

                int type = 0;

                if (m00*m01 + m10*m11 != 0.0)
                {
                    type |= TYPE_GENERAL_TRANSFORM;
                    return type;
                }

                if (m02 != 0.0 || m12 != 0.0)
                {
                    type |= TYPE_TRANSLATION;
                }
                else if (m00 == 1.0 && m11 == 1.0 && m01 == 0.0 && m10 == 0.0)
                {
                    type = TYPE_IDENTITY;
                    return type;
                }

                if (m00*m11 - m01*m10 < 0.0)
                {
                    type |= TYPE_FLIP;
                }

                double dx = m00*m00 + m10*m10;
                double dy = m01*m01 + m11*m11;
                if (dx != dy)
                {
                    type |= TYPE_GENERAL_SCALE;
                }
                else if (dx != 1.0)
                {
                    type |= TYPE_UNIFORM_SCALE;
                }

                if ((m00 == 0.0 && m11 == 0.0) ||
                    (m10 == 0.0 && m01 == 0.0 && (m00 < 0.0 || m11 < 0.0)))
                {
                    type |= TYPE_QUADRANT_ROTATION;
                }
                else if (m01 != 0.0 || m10 != 0.0)
                {
                    type |= TYPE_GENERAL_ROTATION;
                }

                return type;
            }
        }

        virtual public double GetScaleX() {
            return m00;
        }

        virtual public double GetScaleY() {
            return m11;
        }

        virtual public double GetShearX() {
            return m01;
        }

        virtual public double GetShearY() {
            return m10;
        }

        virtual public double GetTranslateX() {
            return m02;
        }

        virtual public double GetTranslateY() {
            return m12;
        }

        virtual public bool IsIdentity() {
            return Type == TYPE_IDENTITY;
        }

        virtual public void GetMatrix(double[] matrix) {
            matrix[0] = m00;
            matrix[1] = m10;
            matrix[2] = m01;
            matrix[3] = m11;
            if (matrix.Length > 4) {
                matrix[4] = m02;
                matrix[5] = m12;
            }
        }

        virtual public double GetDeterminant() {
            return m00 * m11 - m01 * m10;
        }

        virtual public void SetTransform(double m00, double m10, double m01, double m11, double m02, double m12) {
            this.type = TYPE_UNKNOWN;
            this.m00 = m00;
            this.m10 = m10;
            this.m01 = m01;
            this.m11 = m11;
            this.m02 = m02;
            this.m12 = m12;
        }

        virtual public void SetTransform(AffineTransform t) {
            type = t.type;
            SetTransform(t.m00, t.m10, t.m01, t.m11, t.m02, t.m12);
        }

        virtual public void SetToIdentity() {
            type = TYPE_IDENTITY;
            m00 = m11 = 1.0;
            m10 = m01 = m02 = m12 = 0.0;
        }

        virtual public void SetToTranslation(double mx, double my) {
            m00 = m11 = 1.0;
            m01 = m10 = 0.0;
            m02 = mx;
            m12 = my;
            if (mx == 0.0 && my == 0.0) {
                type = TYPE_IDENTITY;
            } else {
                type = TYPE_TRANSLATION;
            }
        }

        virtual public void SetToScale(double scx, double scy) {
            m00 = scx;
            m11 = scy;
            m10 = m01 = m02 = m12 = 0.0;
            if (scx != 1.0 || scy != 1.0) {
                type = TYPE_UNKNOWN;
            } else {
                type = TYPE_IDENTITY;
            }
        }

        virtual public void SetToShear(double shx, double shy) {
            m00 = m11 = 1.0;
            m02 = m12 = 0.0;
            m01 = shx;
            m10 = shy;
            if (shx != 0.0 || shy != 0.0) {
                type = TYPE_UNKNOWN;
            } else {
                type = TYPE_IDENTITY;
            }
        }

        virtual public void SetToRotation(double angle) {
            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);
            if (Math.Abs(cos) < ZERO) {
                cos = 0.0;
                sin = sin > 0.0 ? 1.0 : -1.0;
            } else
                if (Math.Abs(sin) < ZERO) {
                    sin = 0.0;
                    cos = cos > 0.0 ? 1.0 : -1.0;
                }
            m00 = m11 = cos;
            m01 = -sin;
            m10 = sin;
            m02 = m12 = 0.0;
            type = TYPE_UNKNOWN;
        }

        virtual public void SetToRotation(double angle, double px, double py) {
            SetToRotation(angle);
            m02 = px * (1.0 - m00) + py * m10;
            m12 = py * (1.0 - m00) - px * m10;
            type = TYPE_UNKNOWN;
        }

        public static AffineTransform GetTranslateInstance(double mx, double my) {
            AffineTransform t = new AffineTransform();
            t.SetToTranslation(mx, my);
            return t;
        }

        public static AffineTransform GetScaleInstance(double scx, double scY) {
            AffineTransform t = new AffineTransform();
            t.SetToScale(scx, scY);
            return t;
        }

        public static AffineTransform GetShearInstance(double shx, double shy) {
            AffineTransform m = new AffineTransform();
            m.SetToShear(shx, shy);
            return m;
        }

        public static AffineTransform GetRotateInstance(double angle) {
            AffineTransform t = new AffineTransform();
            t.SetToRotation(angle);
            return t;
        }

        public static AffineTransform GetRotateInstance(double angle, double x, double y) {
            AffineTransform t = new AffineTransform();
            t.SetToRotation(angle, x, y);
            return t;
        }

        virtual public void Translate(double mx, double my) {
            Concatenate(AffineTransform.GetTranslateInstance(mx, my));
        }

        virtual public void Scale(double scx, double scy) {
            Concatenate(AffineTransform.GetScaleInstance(scx, scy));
        }

        virtual public void Shear(double shx, double shy) {
            Concatenate(AffineTransform.GetShearInstance(shx, shy));
        }

        virtual public void Rotate(double angle) {
            Concatenate(AffineTransform.GetRotateInstance(angle));
        }

        virtual public void Rotate(double angle, double px, double py) {
            Concatenate(AffineTransform.GetRotateInstance(angle, px, py));
        }

        /** 
         * Multiply matrix of two AffineTransform objects 
         * @param t1 - the AffineTransform object is a multiplicand
         * @param t2 - the AffineTransform object is a multiplier
         * @return an AffineTransform object that is a result of t1 multiplied by matrix t2. 
         */
        AffineTransform Multiply(AffineTransform t1, AffineTransform t2) {
            return new AffineTransform(
                    t1.m00 * t2.m00 + t1.m10 * t2.m01,          // m00
                    t1.m00 * t2.m10 + t1.m10 * t2.m11,          // m01
                    t1.m01 * t2.m00 + t1.m11 * t2.m01,          // m10
                    t1.m01 * t2.m10 + t1.m11 * t2.m11,          // m11
                    t1.m02 * t2.m00 + t1.m12 * t2.m01 + t2.m02, // m02
                    t1.m02 * t2.m10 + t1.m12 * t2.m11 + t2.m12);// m12
        }

        virtual public void Concatenate(AffineTransform t) {
            SetTransform(Multiply(t, this));
        }

        virtual public void preConcatenate(AffineTransform t) {
            SetTransform(Multiply(this, t));
        }

        virtual public AffineTransform CreateInverse() {
            double det = GetDeterminant();
            if (Math.Abs(det) < ZERO) {
                // awt.204=Determinant is zero
                throw new InvalidOperationException("awt.204"); //$NON-NLS-1$
            }
            return new AffineTransform(
                     m11 / det, // m00
                    -m10 / det, // m10
                    -m01 / det, // m01
                     m00 / det, // m11
                    (m01 * m12 - m11 * m02) / det, // m02
                    (m10 * m02 - m00 * m12) / det  // m12
            );
        }

        virtual public Point2D Transform(Point2D src, Point2D dst) {
            if (dst == null) {
                if (src is Point2D.Double) {
                    dst = new Point2D.Double();
                } else {
                    dst = new Point2D.Float();
                }
            }

            double x = src.GetX();
            double y = src.GetY();

            dst.SetLocation(x * m00 + y * m01 + m02, x * m10 + y * m11 + m12);
            return dst;
        }

        virtual public void Transform(Point2D[] src, int srcOff, Point2D[] dst, int dstOff, int length) {
            while (--length >= 0) {
                Point2D srcPoint = src[srcOff++]; 
                double x = srcPoint.GetX();
                double y = srcPoint.GetY();
                Point2D dstPoint = dst[dstOff]; 
                if (dstPoint == null) {
                    if (srcPoint is Point2D.Double) {
                        dstPoint = new Point2D.Double();
                    } else {
                        dstPoint = new Point2D.Float();
                    }
                }
                dstPoint.SetLocation(x * m00 + y * m01 + m02, x * m10 + y * m11 + m12);
                dst[dstOff++] = dstPoint;
            }
        }
    
         virtual public void Transform(double[] src, int srcOff, double[] dst, int dstOff, int length) {
            int step = 2;
            if (src == dst && srcOff < dstOff && dstOff < srcOff + length * 2) {
                srcOff = srcOff + length * 2 - 2;
                dstOff = dstOff + length * 2 - 2;
                step = -2;
            }
            while (--length >= 0) {
                double x = src[srcOff + 0];
                double y = src[srcOff + 1];
                dst[dstOff + 0] = x * m00 + y * m01 + m02;
                dst[dstOff + 1] = x * m10 + y * m11 + m12;
                srcOff += step;
                dstOff += step;
            }
        }

        virtual public void Transform(float[] src, int srcOff, float[] dst, int dstOff, int length) {
            int step = 2;
            if (src == dst && srcOff < dstOff && dstOff < srcOff + length * 2) {
                srcOff = srcOff + length * 2 - 2;
                dstOff = dstOff + length * 2 - 2;
                step = -2;
            }
            while (--length >= 0) {
                float x = src[srcOff + 0];
                float y = src[srcOff + 1];
                dst[dstOff + 0] = (float)(x * m00 + y * m01 + m02);
                dst[dstOff + 1] = (float)(x * m10 + y * m11 + m12);
                srcOff += step;
                dstOff += step;
            }
        }
    
        virtual public void Transform(float[] src, int srcOff, double[] dst, int dstOff, int length) {
            while (--length >= 0) {
                float x = src[srcOff++];
                float y = src[srcOff++];
                dst[dstOff++] = x * m00 + y * m01 + m02;
                dst[dstOff++] = x * m10 + y * m11 + m12;
            }
        }

        virtual public void Transform(double[] src, int srcOff, float[] dst, int dstOff, int length) {
            while (--length >= 0) {
                double x = src[srcOff++];
                double y = src[srcOff++];
                dst[dstOff++] = (float)(x * m00 + y * m01 + m02);
                dst[dstOff++] = (float)(x * m10 + y * m11 + m12);
            }
        }

        virtual public Point2D DeltaTransform(Point2D src, Point2D dst) {
            if (dst == null) {
                if (src is Point2D.Double) {
                    dst = new Point2D.Double();
                } else {
                    dst = new Point2D.Float();
                }
            }

            double x = src.GetX();
            double y = src.GetY();

            dst.SetLocation(x * m00 + y * m01, x * m10 + y * m11);
            return dst;
        }

        virtual public void DeltaTransform(double[] src, int srcOff, double[] dst, int dstOff, int length) {
            while (--length >= 0) {
                double x = src[srcOff++];
                double y = src[srcOff++];
                dst[dstOff++] = x * m00 + y * m01;
                dst[dstOff++] = x * m10 + y * m11;
            }
        }

        virtual public Point2D InverseTransform(Point2D src, Point2D dst) {
            double det = GetDeterminant();
            if (Math.Abs(det) < ZERO) {
                // awt.204=Determinant is zero
                throw new InvalidOperationException("awt.204"); //$NON-NLS-1$
            }

            if (dst == null) {
                if (src is Point2D.Double) {
                    dst = new Point2D.Double();
                } else {
                    dst = new Point2D.Float();
                }
            }

            double x = src.GetX() - m02;
            double y = src.GetY() - m12;

            dst.SetLocation((x * m11 - y * m01) / det, (y * m00 - x * m10) / det);
            return dst;
        }

        virtual public void InverseTransform(double[] src, int srcOff, double[] dst, int dstOff, int length) {
            double det = GetDeterminant();
            if (Math.Abs(det) < ZERO) {
                // awt.204=Determinant is zero
                throw new InvalidOperationException("awt.204"); //$NON-NLS-1$
            }

            while (--length >= 0) {
                double x = src[srcOff++] - m02;
                double y = src[srcOff++] - m12;
                dst[dstOff++] = (x * m11 - y * m01) / det;
                dst[dstOff++] = (y * m00 - x * m10) / det;
            }
        }

        virtual public void InverseTransform(float[] src, int srcOff, float[] dst, int dstOff, int length) {
            float det = (float)GetDeterminant();
            if (Math.Abs(det) < ZERO) {
                // awt.204=Determinant is zero
                throw new InvalidOperationException("awt.204"); //$NON-NLS-1$
            }

            while (--length >= 0) {
                float x = src[srcOff++] - (float)m02;
                float y = src[srcOff++] - (float)m12;
                dst[dstOff++] = (x * (float)m11 - y * (float)m01) / det;
                dst[dstOff++] = (y * (float)m00 - x * (float)m10) / det;
            }
        }

        virtual public object Clone() {
            return new AffineTransform(this);
        }

    //    public Shape createTransformedShape(Shape src) {
    //        if (src == null) {
    //            return null;
    //        }
    //        if (src is GeneralPath) {
    //            return ((GeneralPath)src).createTransformedShape(this);
    //        }
    //        PathIterator path = src.GetPathIterator(this);
    //        GeneralPath dst = new GeneralPath(path.GetWindingRule());
    //        dst.append(path, false);
    //        return dst;
    //    }

        public override String ToString() {
            return "Transform: [[" + m00 + ", " + m01 + ", " + m02 + "], [" //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$ //$NON-NLS-4$
                    + m10 + ", " + m11 + ", " + m12 + "]]"; //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
        }

    //    @Override
    //    public int hashCode() {
    //        HashCode hash = new HashCode();
    //        hash.append(m00);
    //        hash.append(m01);
    //        hash.append(m02);
    //        hash.append(m10);
    //        hash.append(m11);
    //        hash.append(m12);
    //        return hash.hashCode();
    //    }
    //
    //    @Override
    //    public boolean equals(Object obj) {
    //        if (obj == this) {
    //            return true;
    //        }
    //        if (obj instanceof AffineTransform) {
    //            AffineTransform t = (AffineTransform)obj;
    //            return
    //                m00 == t.m00 && m01 == t.m01 &&
    //                m02 == t.m02 && m10 == t.m10 &&
    //                m11 == t.m11 && m12 == t.m12;
    //        }
    //        return false;
    //    }

    
    //    /**
    //     * Write AffineTrasform object to the output steam.
    //     * @param stream - the output stream
    //     * @throws IOException - if there are I/O errors while writing to the output strem
    //     */
    //    private void writeObject(java.io.ObjectOutputStream stream) throws IOException {
    //        stream.defaultWriteObject();
    //    }
    //
    //    
    //    /**
    //     * Read AffineTransform object from the input stream
    //     * @param stream - the input steam
    //     * @throws IOException - if there are I/O errors while reading from the input strem
    //     * @throws ClassNotFoundException - if class could not be found 
    //     */
    //    private void readObject(java.io.ObjectInputStream stream) throws IOException, ClassNotFoundException {
    //        stream.defaultReadObject();
    //        type = TYPE_UNKNOWN;
    //    }
    }
}
