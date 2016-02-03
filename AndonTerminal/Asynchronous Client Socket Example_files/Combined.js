epx=window.epx||{};epx.communityContent=function(){function loadComments(){var $communityComments=$("#CommunityComments"),communityContentAddLink;$communityComments&&$communityComments.length!==0&&(communityContentAddLink=$(".communityContentAddLink"),$communityComments.html(""),window.epx.utilities.log("Loading comments..."),$.ajax({type:"GET",async:!0,url:$communityComments.attr("data-url"),dataType:"json",cache:!1,success:function(r){if(!r||r.length<1){window.epx.utilities.log("Comment web service returned 0 comments.");communityContentAddLink.length<1&&$(".communityContentHeaderTitle").css("display","none");return}window.epx.utilities.log("Comment web service returned "+r.length+" comment(s)...");$("#CommentTemplate").tmpl(r).appendTo($communityComments)}}))}return{loadComments:loadComments}}();$("#CommunityComments").ready(function(){epx.communityContent.loadComments()});;
epx=window.epx||{};epx.library=window.epx.library||{};epx.library.memberFilter=function(){function init(){if(showInherited=epx.utilities.getCookie(cookieInherited,"true")=="true",showProtected=epx.utilities.getCookie(cookieProtected,"true")=="true",tryFilterMembers()){var $filterHtml=$(".libraryMemberFilter"),$checkBoxInherited=$filterHtml.find("input.libraryFilterInherited"),$checkBoxProtected=$filterHtml.find("input.libraryFilterProtected");showInherited||$checkBoxInherited.prop("checked",!1);showProtected||$checkBoxProtected.prop("checked",!1);$checkBoxInherited.click(function(){var prevShow=showInherited;showInherited=$(this).prop("checked");memberFilterChanged(showInherited,cookieInherited,"input.libraryFilterInherited",prevShow)});$checkBoxProtected.click(function(){var prevShow=showProtected;showProtected=$(this).prop("checked");memberFilterChanged(showProtected,cookieProtected,"input.libraryFilterProtected",prevShow)});$filterHtml.show();$("table.members[id^='memberList']").before($filterHtml)}}function tryFilterMembers(){var containsMemberToFilter=!1;return $("tr[data]").each(function(){var dataValue=$(this).attr("data");dataValue.indexOf(dataInherited)>=0&&(showInherited||$(this).hide(),containsMemberToFilter=!0);dataValue.indexOf(dataProtected)>=0&&(showProtected||$(this).hide(),containsMemberToFilter=!0);(showInherited||showProtected)&&dataValue.indexOf(dataInherited)>=0&&dataValue.indexOf(dataProtected)>=0&&$(this).show()}),showInherited&&showProtected||toggleEmptyWarning(),containsMemberToFilter}function memberFilterChanged(showMembers,cookie,filterSelector,prevShow){epx.utilities.setCookie(cookie,showMembers,365,"/",".microsoft.com",null);$(filterSelector).prop("checked",showMembers);showMembers!=prevShow&&toggleMemberFilter()}function toggleMemberFilter(){$("tr[data]").each(function(){var dataValue=$(this).attr("data");dataValue.indexOf(dataInherited)>=0&&$(this).toggle(showInherited);dataValue.indexOf(dataProtected)>=0&&$(this).toggle(showProtected);(showInherited||showProtected)&&dataValue.indexOf(dataInherited)>=0&&dataValue.indexOf(dataProtected)>=0&&$(this).toggle(showInherited||showProtected)});toggleEmptyWarning()}function toggleEmptyWarning(){$("tr.emptyWarning").hide();showInherited&&showProtected||$("table.members[id^='memberList']").each(function(){var $memberTable=$(this),$warning;containsVisibleMember($memberTable)||($warning=$memberTable.find("tr.emptyWarning"),$warning.length>0?$warning.show():$memberTable.find("tbody").append('<tr class="emptyWarning"><td colspan="3">'+$("#libraryMemberFilterEmptyWarning").val()+"<\/td><\/tr>"))})}function containsVisibleMember($table){var result=!1;return $table.find("tr[data]").each(function(){if($(this).is(":visible")||$(this).css("display")!="none")return result=!0,!1}),result}var dataInherited="inherited;",dataProtected="protected;",cookieInherited="libraryShowInherited",cookieProtected="libraryShowProtected",showInherited=!0,showProtected=!0;return{init:init,tryFilterMembers:tryFilterMembers,memberFilterChanged:memberFilterChanged}}();$(document).ready(function(){epx.library.memberFilter.init()});;
(function(){var tocFixedModule=function(w,d,$){function init(){if(!window.epx||!window.epx.topic||window.epx.topic.isPrintExperience()!==!0){var $parameters=$("#fixLeftNavParameters"),param=$parameters.length>0?$parameters.val().split(","):null;($leftNav=param!=null&&param[1]!=null?$(param[1]):$("#leftNav"),$toc=param!=null&&param[2]!=null?$(param[2]):$("#tocnav"),$footer=$("#ux-footer"),w&&d&&$leftNav.length!==0&&$toc.length!==0&&$footer.length!==0)&&(param!=null&&param[0]!=null&&(fixedTocTop=Number(param[0])),$(w).scroll(function(){setPosition();var documentScrollLeft=$(d).scrollLeft();lastScrollLeft!=documentScrollLeft&&(lastScrollLeft=documentScrollLeft,$toc.css("left",-documentScrollLeft))}),$(w).resize(function(){setPosition()}))}}function setPosition(){var windowHeight=$(w).height(),documentHeight=$(d).height(),tocHeight=$toc.height(),nonFooterViewable,visibleContentHeight;windowHeight>tocHeight+fixedTocTop&&$leftNav.next().height()>tocHeight+fixedTocTop&&$(w).scrollTop()+fixedTocTop>$leftNav.offset().top?($toc.width($leftNav.width()).css("position","fixed"),$leftNav.css("height",tocHeight),nonFooterViewable=documentHeight-$footer.height()-$(w).scrollTop()-60,nonFooterViewable>=tocHeight+fixedTocTop?$toc.css("top",fixedTocTop+"px"):$toc.css("top",(tocHeight-nonFooterViewable)*-1)):windowHeight<tocHeight+fixedTocTop&&$leftNav.next().height()>tocHeight+fixedTocTop&&$(w).scrollTop()+windowHeight>tocHeight+$leftNav.offset().top?($toc.width($leftNav.width()).css("position","fixed"),$leftNav.css("height",tocHeight),visibleContentHeight=documentHeight-$footer.height()-$(w).scrollTop()-60,visibleContentHeight>=windowHeight?$toc.css("top",(tocHeight-windowHeight)*-1):$toc.css("top",(tocHeight-visibleContentHeight)*-1)):($toc.css("width","").css("position","").css("top",""),$leftNav.css("height","auto"))}var $leftNav,$toc,$footer,fixedTocTop=60,lastScrollLeft=0;return $(document).ready(function(){init()}),{init:init,setPosition:setPosition}};typeof define=="function"?define("tocFixed",["jquery"],function($){return tocFixedModule(window,document,$)}):(window.epx=window.epx||{},window.epx.library=window.epx.library||{},window.epx.library.tocFixed=tocFixedModule(window,document,$))})();;
epx=window.epx||{};epx.codeSnippetModule=function(w,d){function init(){scrollOnLoad();initCopyLinks()}function initCopyLinks(){w.clipboardData&&$("a[name=CodeSnippetCopyLink]").css("display","block")}function copyCode(id){if(w.clipboardData){var obj=d.getElementById(id);w.clipboardData.setData("Text",obj.innerText)}}function scrollOnLoad(){var hash=location.hash,hashY;hash.length>1&&hash.substr(1,1)==="Y"&&(hashY=Number(hash.substr(2)),isNaN(hashY)||w.scrollTo(0,hashY))}return{init:init,initCopyLinks:initCopyLinks,copyCode:copyCode,scrollOnLoad:scrollOnLoad}};epx.codeSnippet=epx.codeSnippetModule(window,document);$(document).ready(function(){epx.codeSnippet.init()});;
function TopicNotInScope_ShowPicker(){var topicNotInScopePicker=document.getElementById("topicNotInScopeCollectionPicker");topicNotInScopePicker&&(topicNotInScopePicker.style.display=topicNotInScopePicker.style.display!="block"?"block":"none")}function TopicNotInScope_HidePicker(e){var topicNotInScopePicker=document.getElementById("topicNotInScopeCollectionPicker"),src,e;topicNotInScopePicker&&(e=e||window.event,e.target?src=e.target:e.srcElement&&(src=e.srcElement),src.tagName=="A"&&src.parentNode.parentNode&&src.parentNode.parentNode.id=="topicNotInScopeCollectionPicker"||src.id=="topicNotInScopeSwitchCollection"||src.id=="topicNotInScopeDropdownImage"||src.id=="topicNotInScopeSwitchCollectionContainer"||topicNotInScopePicker.style.display!="block"||(topicNotInScopePicker.style.display="none"))}document.addEventListener?document.addEventListener("mouseup",TopicNotInScope_HidePicker,!1):document.attachEvent&&document.attachEvent("onmouseup",TopicNotInScope_HidePicker);;
epx=window.epx||{};epx.collapsibleArea=function(){function init(){$expandCollapseAllButton=$("a#expandCollapseAll");$titleAnchor=$("a.LW_CollapsibleArea_TitleAhref");$titleAnchor.length===0&&(isEnhanced=!1);$expandCollapseAllButton.length===0&&(isEnhanced=!1);isEnhanced||$expandCollapseAllButton.hide();expandedCaretClass="cl_CollapsibleArea_expanding LW_CollapsibleArea_Img";collapsedCaretClass="cl_CollapsibleArea_collapsing LW_CollapsibleArea_Img";expandedCaretSelector="span.cl_CollapsibleArea_expanding.LW_CollapsibleArea_Img";collapsedCaretSelector="span.cl_CollapsibleArea_collapsing.LW_CollapsibleArea_Img";var stringsAvailable=typeof window.MTPS!="undefined"&&typeof window.MTPS.LocalizedStrings!="undefined";stringsAvailable&&(isEnhanced?(expandTooltip=window.MTPS.LocalizedStrings.EnhancedExpandTooltip,collapseTooltip=window.MTPS.LocalizedStrings.EnhancedCollapseTooltip):(expandTooltip=window.MTPS.LocalizedStrings.ExpandButtonTooltip,collapseTooltip=window.MTPS.LocalizedStrings.CollapseButtonTooltip),$(".cl_CollapsibleArea_expanding").parent().attr("title",collapseTooltip),$(".cl_CollapsibleArea_collapsing").parent().attr("title",expandTooltip),$(collapsedCaretSelector).length===0&&$expandCollapseAllButton.text(window.MTPS.LocalizedStrings.CollapseAllButtonTooltip));setupEventHandlers()}function titleAnchorClicked(){var $this=$(this),$image=$this.children("span").first(),$content=$this.parent().parent().next();$image&&$content&&($this.removeAttr("title"),$content.attr("class")==="sectionblock"?($image.attr("class",collapsedCaretClass),$content.attr("class","sectionnone"),$this.attr("title",expandTooltip),$image.attr("title",expandTooltip)):($image.attr("class",expandedCaretClass),$content.attr("class","sectionblock"),$this.attr("title",collapseTooltip),$image.attr("title",collapseTooltip)),setECAButtonText())}function expandCollapseAllButtonClicked(){$(this).text()===window.MTPS.LocalizedStrings.ExpandAllButtonTooltip?expandAll():collapseAll()}function collapseAll(){$(expandedCaretSelector).parent().click();$expandCollapseAllButton.text(window.MTPS.LocalizedStrings.ExpandAllButtonTooltip)}function expandAll(){$(collapsedCaretSelector).parent().click();$expandCollapseAllButton.text(window.MTPS.LocalizedStrings.CollapseAllButtonTooltip)}function expandCollapseAll(){if(isEnhanced){var $eca=$(this);$eca.hasClass("LW_CollapsibleArea_Title")&&($eca=$($eca.parent().children().first()));$eca.hasClass("cl_CollapsibleArea_collapsing")?expandAll():$eca.hasClass("cl_CollapsibleArea_expanding")&&collapseAll();$("html, body").scrollTop($eca.offset().top-10)}}function setupEventHandlers(){$titleAnchor.click(titleAnchorClicked);$expandCollapseAllButton.click(expandCollapseAllButtonClicked);$("span.LW_CollapsibleArea_Img").dblclick(expandCollapseAll);$("span.LW_CollapsibleArea_Title").dblclick(expandCollapseAll)}function setECAButtonText(){$(collapsedCaretSelector).length===0?$expandCollapseAllButton.text(window.MTPS.LocalizedStrings.CollapseAllButtonTooltip):$(expandedCaretSelector).length===0&&$expandCollapseAllButton.text(window.MTPS.LocalizedStrings.ExpandAllButtonTooltip)}var isEnhanced=!0,expandTooltip="",collapseTooltip="",expandedCaretClass="",collapsedCaretClass="",expandedCaretSelector="",collapsedCaretSelector="",$expandCollapseAllButton,$titleAnchor;return{init:init}}();$(document).ready(function(){epx.collapsibleArea.init()});;
epx=window.epx||{};epx.versionSelector=function(){function init(){$link&&$arrow&&$list&&($link.live("click",function(){epx.versionSelector.open()}),$arrow.live("click",function(){epx.versionSelector.open()}),$(document).live("mouseup",function(){epx.versionSelector.close()}))}function open(){$list.show()}function close(){$list.hide()}var $link=$("#vsLink"),$arrow=$("#vsArrow"),$list=$("#vsPanel");return{init:init,open:open,close:close}}();$(document).ready(function(){epx.versionSelector.init()});;
var isMetroIE10=navigator.userAgent.indexOf("MSIE 10.0")!=-1&&window.innerWidth==screen.width&&window.innerHeight==screen.height,brokerScript;isMetroIE10||(brokerScript=document.createElement("script"),brokerScript.src=window.location.protocol=="https:"?"https://www.microsoft.com/library/svy/sto/https/broker.js":"//js.microsoft.com/library/svy/sto/broker.js",document.getElementsByTagName("head")[0].appendChild(brokerScript));;
