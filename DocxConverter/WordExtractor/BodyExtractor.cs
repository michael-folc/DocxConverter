using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DocxConverter.WordExtractor
{
  public class BodyExtractor
  {
    public BodyExtractor ()
    {
    }

    public XDocument GetBody (WordprocessingDocument document)
    {
      HandleFormatChanges (document.MainDocumentPart);
      HandleDeletions (document.MainDocumentPart);
      HandleInsertions (document.MainDocumentPart);

      using (var xmlReader = XmlReader.Create (document.MainDocumentPart.GetStream()))
      {
        var xDocument = XDocument.Load (xmlReader);
        return xDocument;
      }
    }

    private void HandleFormatChanges (MainDocumentPart mainDocumentPart)
    {
      var body = mainDocumentPart.Document.Body;
      var changes = body.Descendants<RunPropertiesChange>().ToList();
      //var changes = body.Descendants<ParagraphPropertiesChange> ().ToList ();

      foreach (var change in changes)
        change.Remove();
    }

    private void HandleDeletions (MainDocumentPart mainDocumentPart)
    {
      var body = mainDocumentPart.Document.Body;
      var deletions = new List<OpenXmlElement> ();
      //deletions.AddRange (body.Descendants<Deleted> ().Cast<OpenXmlElement> ());
      //deletions.AddRange (body.Descendants<DeletedRun> ().Cast<OpenXmlElement> ());

      foreach (var deletion in deletions)
        deletion.Remove ();
    }

    private void HandleInsertions (MainDocumentPart mainDocumentPart)
    {
      var body = mainDocumentPart.Document.Body;
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