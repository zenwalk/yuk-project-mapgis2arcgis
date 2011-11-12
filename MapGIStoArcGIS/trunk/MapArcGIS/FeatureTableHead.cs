using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapArcGIS
{
    internal struct FeatureTableHead
    {
        internal string headName;
        internal byte itemType;
        internal int offset;
        internal short lengthInBytes;
        internal short tableItemCharLength;
    }
}
