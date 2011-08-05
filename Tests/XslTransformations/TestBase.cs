using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using DocxConverter.XslTransformations;
using Jolt.Testing.Assertions.NUnit.SyntaxHelpers;
using NUnit.Framework;

namespace Tests.XslTransformations
{
  public abstract class TestBase
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
      var actual = _transformation.Transform (source);
      using (var actualReader = actual.CreateReader())
      {
        using (var expectedReader = expected.CreateReader())
        {
          Assert.That (actualReader, IsXml.EquivalentTo (expectedReader));
        }
      }
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