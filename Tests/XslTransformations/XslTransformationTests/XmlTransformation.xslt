<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  
  <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>

  <xsl:template match="body">
    <document>
      <xsl:apply-templates select="element"/>
    </document>
  </xsl:template>

  <xsl:template match="element">
    <p>
      <xsl:value-of select="./text()"/>
    </p>
  </xsl:template>

</xsl:stylesheet>
