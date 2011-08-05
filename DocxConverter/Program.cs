using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DocxConverter
{
  static class Program
  {
    [STAThread]
    static void Main ()
    {
      Application.EnableVisualStyles ();
      Application.SetCompatibleTextRenderingDefault (false);
      Application.Run (new Form1 ());

      /*
       * - DocX
       *  -- extract main document (/)
       *  -- merge changes
       *  -- transform to plain xml
       *        Suppert new lines <---- !!!
       * 
       * - ODT
       *  -- extract main document
       *  -- merge changes (?)
       *  -- transform to plain xml

       * - Generate UBB from plain xml
       *  -- merge adjacent nodes (/)
       *  -- optimize whitespace
       *  -- replace headings with paragraphs
       *  -- normalize section breaks
       *  -- generate UBB

       * - Generate TXT from plain xml
       *  -- merge adjacent nodes (/)
       *  -- optimize whitespace
       *  -- replace headings with paragraphs
       *  -- normalize section breaks
       *  -- replace special characters
       *   --- slanted quotes, dashes, elipses
       *  -- generate Plain Text
       *   --- Replace italic and bold with special characters
       *   --- Line wrap: http://xsltcookbook2.atw.hu/xsltckbk2-chp-7-sect-6.html
       *                  http://plasmasturm.org/log/xslwordwrap/
      
       * - Generate HTML from plain xml
       *  -- merge adjacent nodes 
       *  -- normalize section breaks
       *  -- generate HTML
       *  
       */

    }
  }
}
