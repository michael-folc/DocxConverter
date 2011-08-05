<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>
  <xsl:strip-space elements="*"/>

  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()"/>
    </xsl:copy>
  </xsl:template>

 <xsl:template match="node()" priority="2">
    <xsl:copy>
      <xsl:for-each-group select="node()" group-adjacent="boolean(self::i)">
        <xsl:choose>
          <xsl:when test="current-grouping-key()">
            <i xml:space="preserve"><xsl:apply-templates select="current-group()/node()"/></i>
          </xsl:when>
          <xsl:otherwise>
            <xsl:apply-templates select="current-group()"/>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:for-each-group>
    </xsl:copy>
  </xsl:template>

  <xsl:template match="node()" priority="1">
    <xsl:copy>
      <xsl:for-each-group select="node()" group-adjacent="boolean(self::b)">
        <xsl:choose>
          <xsl:when test="current-grouping-key()">
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
