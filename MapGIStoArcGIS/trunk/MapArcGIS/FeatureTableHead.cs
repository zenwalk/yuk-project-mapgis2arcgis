using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapArcGIS
{
    internal struct FeatureTableHead
    {
        internal string headName;
        internal byte[] headNameBytes;
        internal FeatureType itemType;
        internal int offset;
        internal short lengthInBytes;
        internal short tableItemCharLength;
    }
    internal enum FeatureType
    {
        String =    1 << 0,
        Byte =      1 << 1,
        Short =     1 << 2,
        Int =       1 << 3,
        Float =     1 << 4,
        Double =    1 << 5,
        Date =      1 << 6,
        Time =      1 << 7,
        Bool =      1 << 8,
        Text =      1 << 9,
        Image =     1 << 10,
        Map =       1 << 11,
        Aminate =   1 << 12,
        PostCode =  1 << 13,
        Binary =    1 << 14,
        Table =     1 << 15
    }
}
