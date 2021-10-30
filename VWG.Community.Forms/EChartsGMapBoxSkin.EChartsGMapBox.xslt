<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:WC="wgcontrols">

  <xsl:template match="WC:EChartsGMapBox" mode="modContent">
    <xsl:attribute name="Class">EChartsGMapBox-Control</xsl:attribute>
    <IFRAME id="TRG_{@Id}" src="[Skin.Path]EChartsGMapBox.html.wgx?id={@Id}" style="width: 100%; height: 100%; overflow: hidden;" class="Common-AllowTransparency Common-NoBorder" contentEditable="true" onload="mobjApp.Web_EnforceIFrameTheming('{@Attr.Id}');">
      &#160;
    </IFRAME>
  </xsl:template>
</xsl:stylesheet>