<?xml version="1.0" encoding="UTF-8"?>
<?altova_samplexml C:\Development\Temp\_Docx2Other\document.xml?>
<xsl:stylesheet version="2.0" 
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
    xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main">
  <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>
  <xsl:strip-space elements="*"/>
  
  <!-- render document body -->
  <xsl:template match="w:document/w:body">
    <document>
      <xsl:apply-templates select="w:p"/>
    </document>
  </xsl:template>  

  <!-- render each paragraph -->
  <xsl:template match="w:p">
    <xsl:choose>
      <xsl:when test="./w:pPr/w:pStyle/@w:val = 'Heading1'">
        <h1>
          <xsl:apply-templates />
        </h1>
      </xsl:when>
      <xsl:when test="./w:pPr/w:pStyle/@w:val = 'Heading2'">
        <h2>
          <xsl:apply-templates />
        </h2>
      </xsl:when>
      <xsl:when test="./w:pPr/w:pStyle/@w:val = 'Heading3'">
        <h3>
          <xsl:apply-templates />
        </h3>
      </xsl:when>
      <xsl:otherwise>
        <p>
          <xsl:apply-templates />
        </p>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <!-- insert italic tag -->
  <xsl:template match="w:r" priority="3">
    <xsl:choose>
      <xsl:when test="./w:rPr/w:i">
        <i xml:space="preserve"><xsl:next-match/></i>
      </xsl:when>
      <xsl:otherwise>
        <xsl:next-match/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>   
  
  <!-- insert bold tag -->
  <xsl:template match="w:r" priority="2">
    <xsl:choose>
      <xsl:when test="./w:rPr/w:b">
        <b xml:space="preserve"><xsl:next-match/></b>
      </xsl:when>
      <xsl:otherwise>
         <xsl:next-match/>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template> 
    
  <!-- just pass to text template -->
  <xsl:template match="w:r" priority="1">
    <xsl:apply-templates select="./w:t"/>
  </xsl:template>  
   
  <!-- render text -->
  <xsl:template match="w:t">
    <xsl:value-of select="./text()"/>
  </xsl:template>  
</xsl:stylesheet>
