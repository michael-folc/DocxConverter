using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocxConverter.Utilities;

namespace DocxConverter.WordExtractor
{
  public class BodyExtractor
  {
    public BodyExtractor ()
    {
    }

    public XDocument GetBody (WordprocessingDocument wordprocessingDocument)
    {
      ArgumentUtility.CheckNotNull ("wordprocessingDocument", wordprocessingDocument);

      var document = GetDocument (wordprocessingDocument);

      HandleFormatChanges (document);
      HandleDeletions (document);
      HandleInsertions (document);

      return GetXDocument (document);
    }

    private Document GetDocument (WordprocessingDocument wordprocessingDocument)
    {
      var document = new Document ();
      document.Load (wordprocessingDocument.MainDocumentPart);
      return document;
    }

    private XDocument GetXDocument (Document document)
    {
      using (var writeStream = new MemoryStream())
      {
        document.Save (writeStream);
        using (var readStream = new MemoryStream (writeStream.GetBuffer()))
        {
          return XDocument.Load (readStream);
        }
      }
    }

    private void HandleFormatChanges (Document document)
    {
      var body = document.Body;
      var changes = new List<OpenXmlElement> ();
      changes.AddRange (body.Descendants<RunPropertiesChange>().Cast<OpenXmlElement>());
      changes.AddRange (body.Descendants<ParagraphPropertiesChange>().Cast<OpenXmlElement>());

      foreach (var change in changes)
        change.Remove();
    }

    private void HandleDeletions (Document document)
    {
      var body = document.Body;
      var deletions = new List<OpenXmlElement> ();
      //deletions.AddRange (body.Descendants<Deleted> ().Cast<OpenXmlElement> ());
      //deletions.AddRange (body.Descendants<DeletedRun> ().Cast<OpenXmlElement> ());

      foreach (var deletion in deletions)
        deletion.Remove ();
    }

    private void HandleInsertions (Document document)
    {
      var body = document.Body;
      var insertions = new List<OpenXmlElement>();
      insertions.AddRange (body.Descendants<Inserted>().Cast<OpenXmlElement>());
      insertions.AddRange (body.Descendants<InsertedRun>().Cast<OpenXmlElement>());

      foreach (var insertion in insertions)
      {
        foreach (var run in insertion.Elements<Run>())
          insertion.InsertBeforeSelf(run.CloneNode (true));

        insertion.Remove();
      }
    }
  }
}