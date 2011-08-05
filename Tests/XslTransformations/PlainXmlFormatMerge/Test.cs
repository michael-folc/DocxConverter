using System;
using System.Reflection;
using DocxConverter.XslTransformations;
using NUnit.Framework;

namespace Tests.XslTransformations.PlainXmlFormatMerge
{
  [TestFixture]
  public class Test : TestBase
  {
    protected override XslTransformation CreateXslTransformation ()
    {
      return CreateXslTransformation ("PlainXmlFormatMerge.xslt");
    }

    [Test]
    public void Text ()
    {
      RunAssertion (MethodBase.GetCurrentMethod ());
    }

    [Test]
    public void Italic ()
    {
      RunAssertion (MethodBase.GetCurrentMethod ());
    }

    [Test]
    public void Bold ()
    {
      RunAssertion (MethodBase.GetCurrentMethod ());
    }

    [Test]
    public void ItalicAndBold ()
    {
      RunAssertion (MethodBase.GetCurrentMethod ());
    }
  }
}