using System;
using System.IO;
using System.Xml.Linq;
using DocxConverter.Utilities;
using Saxon.Api;

namespace DocxConverter.XslTransformations
{
  public class XslTransformation
  {
    public static XslTransformation Create (string xsltPath)
    {
      ArgumentUtility.CheckNotNull ("xsltPath", xsltPath);

      var xDocument = XDocument.Load (xsltPath, LoadOptions.SetBaseUri | LoadOptions.SetLineInfo);
      return new XslTransformation (xDocument);
    }

    private static readonly Processor s_processor = new Processor();
    private readonly XsltTransformer _transformer;

    public XslTransformation (XDocument xslt)
    {
      ArgumentUtility.CheckNotNull ("xslt", xslt);

      var xsltCompiler = s_processor.NewXsltCompiler();
      var compiledXslt = xsltCompiler.Compile (xslt.CreateReader());

      _transformer = compiledXslt.Load();
      _transformer.SetParameter (new QName ("", "", "newline"), new XdmAtomicValue (Environment.NewLine));
    }

    public XDocument TransformXml (XDocument source)
    {
      ArgumentUtility.CheckNotNull ("source", source);

      var destination = new XdmDestination();
      try
      {
        Transform (source, destination);
        var result = new XDocument();
        using (var resultWriter = result.CreateWriter())
        {
          destination.XdmNode.WriteTo (resultWriter);
        }
        return result;
      }
      finally
      {
        destination.Close();
      }
    }

    public string TransformText (XDocument source)
    {
      ArgumentUtility.CheckNotNull ("source", source);

      var destination = new Serializer();
      try
      {
        using (var result = new StringWriter())
        {
          destination.SetOutputWriter (result);
          Transform (source, destination);
          return result.ToString();
        }
      }
      finally
      {
        destination.Close();
      }
    }

    private void Transform (XDocument source, XmlDestination destination)
    {
      try
      {
        var documentBuilder = s_processor.NewDocumentBuilder();
        using (var sourceReader = source.CreateReader())
        {
          _transformer.InitialContextNode = documentBuilder.Build (sourceReader);
        }

        // transformer.SetParameter(new QName("", "", "a-param"), new XdmAtomicValue("hello to you!"));
        // transformer.SetParameter(new QName("", "", "b-param"), new XdmAtomicValue(someVariable));

        _transformer.Run (destination);
      }
      finally
      {
        _transformer.InitialContextNode = null;
      }
    }
  }
}