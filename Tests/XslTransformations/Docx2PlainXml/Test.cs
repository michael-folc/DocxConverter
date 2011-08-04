using System;
using System.Reflection;
using DocxConverter.XslTransformations;
using NUnit.Framework;

namespace Tests.XslTransformations.Docx2PlainXml
{
  [TestFixture]
  public class Test : TestBase
  {
    protected override XslTransformation CreateXslTransformation ()
    {
      return CreateXslTransformation ("Docx2PlainXml.xslt");
    }

    [Test]
    public void Paragraph ()
    {
      RunAssertion (MethodBase.GetCurrentMethod());
    }

    [Test]
    public void Italic ()
    {
      RunAssertion (MethodBase.GetCurrentMethod());
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

    [Test]
    public void Whitespace ()
    {
      RunAssertion (MethodBase.GetCurrentMethod());
    }

    [Test]
    public void EmptyTags ()
    {
      RunAssertion (MethodBase.GetCurrentMethod());
    }

    [Test]
    public void Integration ()
    {
      RunAssertion (MethodBase.GetCurrentMethod());
    }
  }
}