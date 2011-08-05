<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes"/>
  
  <xsl:template match="/">
    <xsl:variable name="i-nodes-merged">
      <xsl:apply-templates select="." mode="i-nodes"/>
    </xsl:variable>
    
    <xsl:variable name="b-nodes-merged">
      <xsl:apply-templates select="$i-nodes-merged" mode="b-nodes"/>
    </xsl:variable>
    
    <xsl:apply-templates select="$b-nodes-merged" mode="t-nodes"/>
  </xsl:template>
 
  <!-- i-nodes merge -->

  <xsl:template match="node()|@*" mode="i-nodes">
    <xsl:copy>
      <xsl:apply-templates select="node()|@*" mode="i-nodes"/>
    </xsl:copy>
  </xsl:template>

 <xsl:template match="node()" mode="i-nodes">
    <xsl:copy>
      <xsl:for-each-group select="node()|@*" group-adjacent="boolean(self::i)">
        <xsl:choose>
          <xsl:when test="current-grouping-key()">
            <i>
              <xsl:apply-templates select="current-group()/node()" mode="i-nodes"/>
            </i>
          </xsl:when>
          <xsl:otherwise>
            <xsl:apply-templates select="current-group()" mode="i-nodes" />
          </xsl:otherwise>
        </xsl:choose>
      </xsl:for-each-group>
    </xsl:copy>
  </xsl:template>

  <!-- b-nodes merge -->

  <xsl:template match="node()|@*" mode="b-nodes">
    <xsl:copy>
      <xsl:apply-templates select="node()|@*" mode="b-nodes"/>
    </xsl:copy>
  </xsl:template>
  
 <xsl:template match="node()" mode="b-nodes">
    <xsl:copy>
      <xsl:for-each-group select="node()|@*" group-adjacent="boolean(self::b)">
        <xsl:choose>
          <xsl:when test="current-grouping-key()">
            <b>
              <xsl:apply-templates select="current-group()/node()" mode="b-nodes"/>
            </b>
          </xsl:when>
          <xsl:otherwise>
            <xsl:apply-templates select="current-group()" mode="b-nodes" />
          </xsl:otherwise>
        </xsl:choose>
      </xsl:for-each-group>
    </xsl:copy>
  </xsl:template>

  <!-- t-nodes merge -->

  <xsl:template match="node()|@*" mode="t-nodes">
    <xsl:copy>
      <xsl:apply-templates select="node()|@*" mode="t-nodes"/>
    </xsl:copy>
  </xsl:template>
  
 <xsl:template match="node()" mode="t-nodes">
    <xsl:copy>
      <xsl:for-each-group select="node()|@*" group-adjacent="boolean(self::t)">
        <xsl:choose>
          <xsl:when test="current-grouping-key()">
            <t xml:space="preserve"><xsl:apply-templates select="current-group()/node()" mode="t-nodes"/></t>
          </xsl:when>
          <xsl:otherwise>
            <xsl:apply-templates select="current-group()" mode="t-nodes" />
          </xsl:otherwise>
        </xsl:choose>
      </xsl:for-each-group>
    </xsl:copy>
  </xsl:template>
  
</xsl:stylesheet>
