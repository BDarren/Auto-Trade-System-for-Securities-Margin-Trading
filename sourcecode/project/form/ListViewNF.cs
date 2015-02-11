using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Windows.Forms
{
    class ListViewNF : System.Windows.Forms.ListView
    {
        public ListViewNF()
        {
            // 开启双缓冲
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles(); 
        }
    }
}
