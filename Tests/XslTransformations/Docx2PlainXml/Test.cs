using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using DocxConverter.XslTransformations;
using NUnit.Framework;
using Jolt.Testing.Assertions.NUnit.SyntaxHelpers;

namespace Tests.XslTransformations.Docx2PlainXml
{
  [TestFixture]
  public class Test
  {
    [Test]
    public void Test2 ()
    {
      var expected = XDocument.Parse (
          @"<?xml version=""1.0"" encoding=""UTF-8""?>
<root>
</root>");
      //var transformation = CreateXslTransformation ("Docx2PlainXml.xslt");
      var actual = XDocument.Parse (
          @"<?xml version=""1.0"" encoding=""UTF-8""?>

<root>
</root>");
      Console.WriteLine ("---");
      Console.WriteLine (actual.ToString ());
      Console.WriteLine ("---");
      Console.WriteLine (expected.ToString ());
      Console.WriteLine ("---");
      Assert.That (actual.CreateReader (), IsXml.EquivalentTo (expected.CreateReader ()));
    }

    [Test]
    public void IntegrationTest ()
    {
      var source = GetSourceXml (MethodBase.GetCurrentMethod());
      var expected = GetResultXml (MethodBase.GetCurrentMethod());

      var transformation = CreateXslTransformation("Docx2PlainXml.xslt");
      var actual = transformation.Transform (source);
      Console.WriteLine ("---");
      Console.WriteLine (actual.ToString ());
      Console.WriteLine ("---");
      Console.WriteLine (expected.ToString ());
      Console.WriteLine ("---");
      Assert.That (actual.CreateReader (), IsXml.EquivalentTo (expected.CreateReader ()));
    }

    private XslTransformation CreateXslTransformation (string fileName)
    {
      var fullPath = Path.Combine ("XslTransformations", fileName);
      return XslTransformation.Create (fullPath);
    }

    private XDocument GetSourceXml (MethodBase methodBase)
    {
      return XDocument.Load (GetFileName (methodBase, "Source", "xml"), LoadOptions.SetBaseUri | LoadOptions.SetLineInfo | LoadOptions.PreserveWhitespace);
    }

    private XDocument GetResultXml (MethodBase methodBase)
    {
      return XDocument.Load (GetFileName (methodBase, "Result", "xml"), LoadOptions.SetBaseUri | LoadOptions.SetLineInfo | LoadOptions.PreserveWhitespace);
    }

    private string GetFileName (MethodBase methodBase, string suffix, string extension)
    {
      var assemblyName = methodBase.DeclaringType.Assembly.GetName().Name;
      var namespaceName = methodBase.DeclaringType.Namespace;
      Debug.Assert (namespaceName != null);

      var folderName = namespaceName.Remove (0, assemblyName.Length).Split (new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
      var fileName = methodBase.Name + suffix + "." + extension;

      var fullPath = Path.Combine (folderName.Concat (new[] { fileName }).ToArray());
      Assert.That (File.Exists (fullPath));

      return fullPath;
    }
  }
}