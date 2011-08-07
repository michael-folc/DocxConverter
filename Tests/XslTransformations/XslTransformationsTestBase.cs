using System.IO;
using System.Reflection;
using System.Xml.Linq;
using DocxConverter.XslTransformations;
using Jolt.Testing.Assertions.NUnit.SyntaxHelpers;
using NUnit.Framework;

namespace Tests.XslTransformations
{
  public abstract class XslTransformationsTestBase : TestBase
  {
    private XslTransformation _transformation;

    protected abstract XslTransformation CreateXslTransformation ();

    [SetUp]
    public void SetUp ()
    {
      _transformation = CreateXslTransformation();
    }

    protected void AssertTransformXml (MethodBase currentMethod)
    {
      var source = GetSourceXml (currentMethod);
      var expected = GetResultXml (currentMethod);
      var actual = _transformation.TransformXml (source);
      using (var actualReader = actual.CreateReader())
      {
        using (var expectedReader = expected.CreateReader())
        {
          Assert.That (actualReader, IsXml.EquivalentTo (expectedReader));
        }
      }
    }

    protected void AssertTransformText (MethodBase currentMethod)
    {
      var source = GetSourceXml (currentMethod);
      var expected = GetResultText (currentMethod);
      var actual = _transformation.TransformText (source);

      Assert.That (actual, Is.EqualTo (expected));
    }

    protected XslTransformation CreateXslTransformation (string fileName)
    {
      var fullPath = Path.Combine ("XslTransformations", fileName);
      return XslTransformation.Create (fullPath);
    }

    private XDocument GetSourceXml (MethodBase methodBase)
    {
      return XDocument.Load (GetFileName (methodBase, "Source", "xml"), LoadOptions.SetBaseUri | LoadOptions.SetLineInfo);
    }

    private XDocument GetResultXml (MethodBase methodBase)
    {
      return XDocument.Load (GetFileName (methodBase, "Result", "xml"), LoadOptions.SetBaseUri | LoadOptions.SetLineInfo);
    }

    private string GetResultText (MethodBase methodBase)
    {
      return File.ReadAllText (GetFileName (methodBase, "Result", "txt"));
    }
  }
}