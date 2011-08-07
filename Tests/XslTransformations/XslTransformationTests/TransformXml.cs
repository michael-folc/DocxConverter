using System;
using System.IO;
using System.Reflection;
using DocxConverter.XslTransformations;
using NUnit.Framework;

namespace Tests.XslTransformations.XslTransformationTests
{
  [TestFixture]
  public class TransformXml : XslTransformationsTestBase
  {
    protected override XslTransformation CreateXslTransformation ()
    {
      var fullPath = Path.Combine ("XslTransformations", "XslTransformationTests", "XmlTransformation.xslt");
      return XslTransformation.Create (fullPath);
    }

    [Test]
    public void Xml ()
    {
      AssertTransformXml (MethodBase.GetCurrentMethod ());
    }
  }
}