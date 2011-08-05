using System;
using System.Reflection;
using DocxConverter.XslTransformations;
using NUnit.Framework;

namespace Tests.XslTransformations.PlainXmlToUbbTests
{
  [TestFixture]
  public class Test : TestBase
  {
    protected override XslTransformation CreateXslTransformation ()
    {
      return CreateXslTransformation ("PlainXmlToUbb.xslt");
    }

    [Test]
    public void Paragraph ()
    {
      AssertTransformText (MethodBase.GetCurrentMethod());
    }

    [Test]
    public void Text ()
    {
      AssertTransformText (MethodBase.GetCurrentMethod());
    }

    [Test]
    public void Italic ()
    {
      AssertTransformText (MethodBase.GetCurrentMethod());
    }

    [Test]
    public void Bold ()
    {
      AssertTransformText (MethodBase.GetCurrentMethod());
    }

    [Test]
    public void ItalicAndBold ()
    {
      AssertTransformText (MethodBase.GetCurrentMethod());
    }

    [Test]
    public void Whitespace ()
    {
      AssertTransformText (MethodBase.GetCurrentMethod());
    }

    [Test]
    public void EmptyTags ()
    {
      AssertTransformText (MethodBase.GetCurrentMethod());
    }
  }
}