using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using DocxConverter.XslTransformations;
using Jolt.Testing.Assertions.NUnit.SyntaxHelpers;
using NUnit.Framework;

namespace Tests.XslTransformations.Docx2PlainXml
{
  [TestFixture]
  public class Test
  {
    private XslTransformation _transformation;

    [SetUp]
    public void SetUp ()
    {
      _transformation = CreateXslTransformation ("Docx2PlainXml.xslt");
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
    public void Whitespace ()
    {
      RunAssertion (MethodBase.GetCurrentMethod());
    }

    [Test]
    public void EmptyTags ()
    {
      RunAssertion (MethodBase.GetCurrentMethod ());
    }

    [Test]
    public void Integration ()
    {
      RunAssertion (MethodBase.GetCurrentMethod());
    }

    private void RunAssertion (MethodBase currentMethod)
    {
      var source = GetSourceXml (currentMethod);
      var expected = GetResultXml (currentMethod);
      var actual = _transformation.Transform (source);
      using (var actualReader = actual.CreateReader())
      {
        using (var expectedReader = expected.CreateReader())
        {
          Assert.That (actualReader, IsXml.EquivalentTo (expectedReader));
        }
      }
    }

    private XslTransformation CreateXslTransformation (string fileName)
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

    private string GetFileName (MethodBase methodBase, string suffix, string extension)
    {
      var assemblyName = methodBase.DeclaringType.Assembly.GetName().Name;
      var namespaceName = methodBase.DeclaringType.Namespace;
      Debug.Assert (namespaceName != null);

      var folderName = namespaceName.Remove (0, assemblyName.Length).Split (new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
      var fileName = methodBase.Name + suffix + "." + extension;

      var fullPath = Path.Combine (folderName.Concat (new[] { fileName }).ToArray());
      Assert.That (File.Exists (fullPath), "File not found: {0}", fullPath);

      return fullPath;
    }
  }
}