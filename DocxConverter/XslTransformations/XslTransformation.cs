using System;
using System.Diagnostics.Contracts;
using System.Xml;
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

    private readonly XDocument _xslt;
    private static readonly Processor s_processor = new Processor ();

    public XslTransformation (XDocument xslt)
    {
      ArgumentUtility.CheckNotNull ("xslt", xslt);

      _xslt = xslt;
    }

    public XDocument Transform (XDocument source)
    {
      ArgumentUtility.CheckNotNull ("source", source);

      // Load the source document.
      XdmNode input = s_processor.NewDocumentBuilder ().Build (source.CreateReader());

      // Create a transformer for the stylesheet.
      XsltTransformer transformer = s_processor.NewXsltCompiler().Compile (_xslt.CreateReader()).Load();

      // Set the root node of the source document to be the initial context node.
      transformer.InitialContextNode = input;

      // BaseOutputUri is only necessary for xsl:result-document.
      //transformer.BaseOutputUri = new Uri (xsltUri);

      // transformer.SetParameter(new QName("", "", "a-param"), new XdmAtomicValue("hello to you!"));
      // transformer.SetParameter(new QName("", "", "b-param"), new XdmAtomicValue(someVariable));

      // Create a serializer.
      //Serializer serializer = new Serializer ();
      //serializer.SetOutputWriter (Response.Output); //for screen
      // serializer.SetOutputFile(Server.MapPath("test.html")); //for file

      // Transform the source XML to System.out.
      var destination = new XdmDestination();
      transformer.Run (destination);
      var result = new XDocument();
      using (var resultWriter = result.CreateWriter ())
      {
        destination.XdmNode.WriteTo (resultWriter);
      }
      return result;
    }
  }
}