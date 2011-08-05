using System;
using System.IO;
using System.Reflection;
using DocxConverter.XslTransformations;
using NUnit.Framework;

namespace Tests.XslTransformations.XslTransformationTests
{
  [TestFixture]
  public class TransformText : TestBase
  {
    protected override XslTransformation CreateXslTransformation ()
    {
      var fullPath = Path.Combine ("XslTransformations", "XslTransformationTests", "TextTransformation.xslt");
      return XslTransformation.Create (fullPath);
    }

    [Test]
    public void Text ()
    {
      AssertTransformText (MethodBase.GetCurrentMethod ());
    }
  }
}