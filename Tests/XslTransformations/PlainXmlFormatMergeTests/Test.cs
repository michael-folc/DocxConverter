using System.Reflection;
using DocxConverter.XslTransformations;
using NUnit.Framework;

namespace Tests.XslTransformations.PlainXmlFormatMergeTests
{
  [TestFixture]
  public class Test : XslTransformationsTestBase
  {
    protected override XslTransformation CreateXslTransformation ()
    {
      return CreateXslTransformation ("PlainXmlFormatMerge.xslt");
    }

    [Test]
    public void Text ()
    {
      AssertTransformXml (MethodBase.GetCurrentMethod ());
    }

    [Test]
    public void Italic ()
    {
      AssertTransformXml (MethodBase.GetCurrentMethod ());
    }

    [Test]
    public void Bold ()
    {
      AssertTransformXml (MethodBase.GetCurrentMethod ());
    }

    [Test]
    public void ItalicAndBold ()
    {
      AssertTransformXml (MethodBase.GetCurrentMethod ());
    }
  }
}