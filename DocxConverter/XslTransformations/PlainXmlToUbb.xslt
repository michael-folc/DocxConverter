<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:output method="text" encoding="UTF-8"/>

  <xsl:param name="newline">
    <xsl:text>&#10;</xsl:text>
  </xsl:param>

  <xsl:template match="p">
    <xsl:apply-templates select="./*" />

    <xsl:if test="position() != last()">
      <xsl:value-of select="$newline"/>
      <xsl:value-of select="$newline"/>
    </xsl:if>
  </xsl:template>

  <xsl:template match="i">
    <xsl:text>[i]</xsl:text>
    <xsl:apply-templates select="./*" />
    <xsl:text>[/i]</xsl:text>
  </xsl:template>

  <xsl:template match="b">
    <xsl:text>[b]</xsl:text>
    <xsl:apply-templates select="./*" />
    <xsl:text>[/b]</xsl:text>
  </xsl:template>

  <xsl:template match="t">
    <xsl:value-of select="./text()"/>
  </xsl:template>

</xsl:stylesheet>
