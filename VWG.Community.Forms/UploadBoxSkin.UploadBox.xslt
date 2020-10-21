<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:WC="wgcontrols">
  <xsl:template match="WC:UploadBox" mode="modContent">
    <xsl:attribute name="class">UploadBox-Control</xsl:attribute>

    <IFRAME id="TRG_{@Id}" src="[Skin.Path]UploadBox.html.wgx?id={@Id}" style="width: 100%; height: 100%; overflow: hidden;" class="Common-AllowTransparency Common-NoBorder" contentEditable="true" onload="mobjApp.Web_EnforceIFrameTheming('{@Attr.Id}');">
      <xsl:if test="$varBrowserObsoleteIE='1'">
        <xsl:attribute name ="frameborder">0</xsl:attribute>
        <xsl:attribute name ="allowtransparency">true</xsl:attribute>
      </xsl:if>
    </IFRAME>
  </xsl:template>
</xsl:stylesheet>
