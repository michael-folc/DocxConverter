<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>
  <xsl:strip-space elements="*"/>

  <xsl:output indent="yes"/>

  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()"/>
    </xsl:copy>
  </xsl:template>

<xsl:template match="p">
    <xsl:copy>
      <xsl:for-each-group select="node()" group-adjacent="self::i or self::b">
        <xsl:choose>
          <xsl:when test="current-grouping-key() and (name() = 'i')">
            <i xml:space="preserve"><xsl:apply-templates select="current-group()/node()"/></i>
          </xsl:when>
          <xsl:when test="current-grouping-key() and (name() = 'b')">
            <b xml:space="preserve"><xsl:apply-templates select="current-group()/node()"/></b>
          </xsl:when>
          <xsl:otherwise>
            <xsl:apply-templates select="current-group()"/>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:for-each-group>
    </xsl:copy>
  </xsl:template>

</xsl:stylesheet>
