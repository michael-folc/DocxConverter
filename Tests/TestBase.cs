using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace Tests
{
  public abstract class TestBase
  {
    protected string GetFileName (MethodBase methodBase, string suffix, string extension)
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