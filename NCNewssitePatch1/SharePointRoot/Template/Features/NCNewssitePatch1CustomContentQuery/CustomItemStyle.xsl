<xsl:stylesheet
  version="1.0"
  exclude-result-prefixes="x d xsl msxsl cmswrt"
  xmlns:x="http://www.w3.org/2001/XMLSchema"
  xmlns:d="http://schemas.microsoft.com/sharepoint/dsp"
  xmlns:cmswrt="http://schemas.microsoft.com/WebParts/v3/Publishing/runtime"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt">
  <xsl:param name="ItemsHaveStreams">
    <xsl:value-of select="'False'" />
  </xsl:param>
  <xsl:variable name="OnClickTargetAttribute" select="string('javascript:this.target=&quot;_blank&quot;')" />
  <xsl:variable name="ImageWidth" />
  <xsl:variable name="ImageHeight" />
  <xsl:template name="TopStory" match="Row[@Style='TopStory']" mode="itemstyle">
    <xsl:variable name="SafeLinkUrl">
      <xsl:call-template name="OuterTemplate.GetSafeLink">
        <xsl:with-param name="UrlColumnName" select="'LinkUrl'"/>
      </xsl:call-template>
    </xsl:variable>
    <xsl:variable name="SafeImageUrl">
      <xsl:call-template name="OuterTemplate.GetSafeStaticUrl">
        <xsl:with-param name="UrlColumnName" select="'ImageUrl'"/>
      </xsl:call-template>
    </xsl:variable>
    <xsl:variable name="DisplayTitle">
      <xsl:call-template name="OuterTemplate.GetTitle">
        <xsl:with-param name="Title" select="@Title"/>
        <xsl:with-param name="Url" select="@LinkUrl"/>
      </xsl:call-template>
    </xsl:variable>
    <div class="span-3 frontpageTopStory">
      <div class="boxed bottomborder">
        <xsl:call-template name="OuterTemplate.CallPresenceStatusIconTemplate"/>
        <xsl:if test="string-length(@EnglishUrl) != 0">
          <a class="language-en" title="Change to english" href="{@EnglishUrl}"></a>
        </xsl:if>
        <xsl:if test="string-length(@DanishUrl) != 0">
          <a class="language-dan" title="Skift til dansk" href="{@DanishUrl}"></a>
        </xsl:if>
        <xsl:if test="string-length(@NorwegianUrl) != 0">
          <a class="language-no" title="Bytt til norsk" href="{@NorwegianUrl}"></a>
        </xsl:if>
        <a href="{$SafeLinkUrl}" title="{@LinkToolTip}" class="newsHeader">
          <xsl:if test="$ItemsHaveStreams = 'True'">
            <xsl:attribute name="onclick">
              <xsl:value-of select="@OnClickForWebRendering"/>
            </xsl:attribute>
          </xsl:if>
          <xsl:if test="$ItemsHaveStreams != 'True' and @OpenInNewWindow = 'True'">
            <xsl:attribute name="onclick">
              <xsl:value-of disable-output-escaping="yes" select="$OnClickTargetAttribute"/>
            </xsl:attribute>
          </xsl:if>
          <h1 class="articleTopStoryHeader">
            <xsl:value-of select="$DisplayTitle"/>
          </h1>
        </a>
        <p class="byline">
          &nbsp;<xsl:value-of select="@ArticleAuthor" />
        </p>
        <xsl:if test="string-length($SafeImageUrl) != 0">
          <div class="frontpageNewsImage">
            <xsl:if test="$ItemsHaveStreams = 'True'">
              <xsl:attribute name="onclick">
                <xsl:value-of select="@OnClickForWebRendering"/>
              </xsl:attribute>
            </xsl:if>
            <xsl:if test="$ItemsHaveStreams != 'True' and @OpenInNewWindow = 'True'">
              <xsl:attribute name="onclick">
                <xsl:value-of disable-output-escaping="yes" select="$OnClickTargetAttribute"/>
              </xsl:attribute>
            </xsl:if>
            <img class="image" src="{$SafeImageUrl}" title="{@ImageUrlAltText}">
              <xsl:if test="$ImageWidth != ''">
                <xsl:attribute name="width">
                  <xsl:value-of select="$ImageWidth" />
                </xsl:attribute>
              </xsl:if>
              <xsl:if test="$ImageHeight != ''">
                <xsl:attribute name="height">
                  <xsl:value-of select="$ImageHeight" />
                </xsl:attribute>
              </xsl:if>
            </img>
          </div>
        </xsl:if>
        <p class="newsDescription">
          <xsl:value-of select="@Description" />
        </p>
        <div class="more">
          <a href="{$SafeLinkUrl}" title="{@LinkToolTip}">Read more</a>
        </div>
      </div>
    </div>
  </xsl:template>
  <xsl:template name="ProfileNews" match="Row[@Style='ProfileNews']" mode="itemstyle">
    <xsl:variable name="SafeLinkUrl">
      <xsl:call-template name="OuterTemplate.GetSafeLink">
        <xsl:with-param name="UrlColumnName" select="'LinkUrl'"/>
      </xsl:call-template>
    </xsl:variable>
    <xsl:variable name="SafeImageUrl">
      <xsl:call-template name="OuterTemplate.GetSafeStaticUrl">
        <xsl:with-param name="UrlColumnName" select="'ImageUrl'"/>
      </xsl:call-template>
    </xsl:variable>
    <xsl:variable name="DisplayTitle">
      <xsl:call-template name="OuterTemplate.GetTitle">
        <xsl:with-param name="Title" select="@Title"/>
        <xsl:with-param name="UrlColumnName" select="'LinkUrl'"/>
      </xsl:call-template>
    </xsl:variable>
    <div class="span-2 border frontpageProfileNews">
      <div class="boxed">
        <xsl:if test="string-length($SafeImageUrl) != 0">
          <div class="frontpageNewsImage">
            <xsl:if test="$ItemsHaveStreams = 'True'">
              <xsl:attribute name="onclick">
                <xsl:value-of select="@OnClickForWebRendering"/>
              </xsl:attribute>
            </xsl:if>
            <xsl:if test="$ItemsHaveStreams != 'True' and @OpenInNewWindow = 'True'">
              <xsl:attribute name="onclick">
                <xsl:value-of disable-output-escaping="yes" select="$OnClickTargetAttribute"/>
              </xsl:attribute>
            </xsl:if>
            <img class="image" src="{$SafeImageUrl}" title="{@ImageUrlAltText}">
              <xsl:if test="$ImageWidth != ''">
                <xsl:attribute name="width">
                  <xsl:value-of select="$ImageWidth" />
                </xsl:attribute>
              </xsl:if>
              <xsl:if test="$ImageHeight != ''">
                <xsl:attribute name="height">
                  <xsl:value-of select="$ImageHeight" />
                </xsl:attribute>
              </xsl:if>
            </img>
          </div>
        </xsl:if>
        <xsl:call-template name="OuterTemplate.CallPresenceStatusIconTemplate"/>
        <xsl:if test="string-length(@EnglishUrl) != 0">
          <a class="language-en" title="Change to english" href="{@EnglishUrl}"></a>
        </xsl:if>
        <xsl:if test="string-length(@DanishUrl) != 0">
          <a class="language-dan" title="Skift til dansk" href="{@DanishUrl}"></a>
        </xsl:if>
        <xsl:if test="string-length(@NorwegianUrl) != 0">
          <a class="language-no" title="Bytt til norsk" href="{@NorwegianUrl}"></a>
        </xsl:if>
        <a href="{$SafeLinkUrl}" title="{@LinkToolTip}" class="newsHeader">
          <xsl:if test="$ItemsHaveStreams = 'True'">
            <xsl:attribute name="onclick">
              <xsl:value-of select="@OnClickForWebRendering"/>
            </xsl:attribute>
          </xsl:if>
          <xsl:if test="$ItemsHaveStreams != 'True' and @OpenInNewWindow = 'True'">
            <xsl:attribute name="onclick">
              <xsl:value-of disable-output-escaping="yes" select="$OnClickTargetAttribute"/>
            </xsl:attribute>
          </xsl:if>
          <h3>
            <xsl:value-of select="$DisplayTitle"/>
          </h3>
        </a>

        <div class="newsDescription">
          <xsl:value-of disable-output-escaping="yes" select="@Description" />
        </div>
        <div class="more">
          <a href="{$SafeLinkUrl}" title="{@LinkToolTip}">Read more</a>
        </div>
      </div>
    </div>
  </xsl:template>
  <xsl:template name="NewsList" match="Row[@Style='NewsList']" mode="itemstyle">
    <xsl:variable name="SafeLinkUrl">
      <xsl:call-template name="OuterTemplate.GetSafeLink">
        <xsl:with-param name="UrlColumnName" select="'LinkUrl'"/>
      </xsl:call-template>
    </xsl:variable>
    <xsl:variable name="DisplayTitle">
      <xsl:call-template name="OuterTemplate.GetTitle">
        <xsl:with-param name="Title" select="@Title"/>
        <xsl:with-param name="UrlColumnName" select="'LinkUrl'"/>
      </xsl:call-template>
    </xsl:variable>
    <div class="span-1 last frontpageNewsList">
      <div class="boxed">
      <xsl:call-template name="OuterTemplate.CallPresenceStatusIconTemplate"/>
        <p>
          <xsl:value-of select="@Created" />
          <br/>
          <strong>
            <a href="{$SafeLinkUrl}" title="{@LinkToolTip}">
              <xsl:if test="$ItemsHaveStreams = 'True'">
                <xsl:attribute name="onclick">
                  <xsl:value-of select="@OnClickForWebRendering"/>
                </xsl:attribute>
              </xsl:if>
              <xsl:if test="$ItemsHaveStreams != 'True' and @OpenInNewWindow = 'True'">
                <xsl:attribute name="onclick">
                  <xsl:value-of disable-output-escaping="yes" select="$OnClickTargetAttribute"/>
                </xsl:attribute>
              </xsl:if>
              <xsl:value-of select="$DisplayTitle"/>
            </a>
          </strong>
        </p>
      </div>
    </div>
  </xsl:template>
</xsl:stylesheet>
