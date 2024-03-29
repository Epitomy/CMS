﻿<%@ Import Namespace="Kooboo.CMS.Web" %>

<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="<%:Url.Action("Localization", "StyleEditing", new { siteName = ViewBag.SiteName, _draft_ = ViewBag._draft_ })%>"></script>
    <%: Html.ExternalResources("styleEditingCss") %>
    <%: Html.ExternalResources("styleEditingJs") %>
    <script type="text/javascript">
        $(function () {

            var timeoutId;
            var showMessage = function (message) {
                clearTimeout(timeoutId);
                $('.header span.message').html(message).show();
                timeoutId = setTimeout(function () { $('.header span.message').hide(); }, 2000);
            };

            var editor = visualstyle.create({
                renderTo: $('#container'),
                btnUndo: $('.btnUndo'),
                btnRedo: $('.btnRedo'),
                effectWin: top.effectWin
            });

            editor.onError.add(function () { showMessage('Load failure!'); });
            var effect = editor.getComponent('effect');
            var lastSelectedText, lastSelectedValue;
            var loadCurrent = function () {
                lastSelectedText = $('.cssfiles').find('option:selected').text();
                lastSelectedValue = $('.cssfiles').find('option:selected').val();
                editor.loadFile(lastSelectedValue);
            };

            $('input.btnSave').click(function () {
                $.ajax({
                    url: '<%:Url.Action("SaveFile", "StyleEditing", new { siteName = ViewBag.SiteName, _draft_ = ViewBag._draft_ })%>',
                    data: {
                        filePath: $('.cssfiles').val(),
                        fileContent: editor.getValue()
                    },
                    type: 'post', dataType: 'json', timeout: 30000,
                    beforeSend: function (request) { editor.applyLock(); },
                    complete: function (request, state) { },
                    success: function (d, state) {
                        if (d.Success) {
                            showMessage('<%:"Save success!".Localize()%>');
                            if (effect.ruleManager) { effect.ruleManager.persistenceChanges(); }
                            setTimeout(loadCurrent, 16);
                        } else {
                            showMessage('<%:"Save failure!".Localize()%>');
                            editor.releaseLock();
                        }
                    }
                });
            });

            $('.cssfiles').change(function () {
                if (editor.isChanged() && confirm(String.format('<%:"[{0}] has been changed, do you want to save?".Localize()%>', lastSelectedText.trim()))) {
                    $(this).val(lastSelectedValue);
                    $('input.btnSave').triggerHandler('click');
                } else {
                    loadCurrent();
                }
            });

            $('input.btnReload').click(function () {
                if (!editor.isChanged()) {
                    loadCurrent();
                } else if (confirm(String.format('<%:"[{0}] has been changed, are you sure you want to reload?".Localize()%>', lastSelectedText.trim()))) {
                    loadCurrent();
                }
            });

            loadCurrent();
        });
    </script>
</head>
<body style="background-color: #fff;">
    <div class="header">
        <span style="color: #000; font-weight: bold; font-size: 14px;">
            <%:string.Format("CSS files of site [{0}]:".Localize(), ViewBag.SiteName)%></span>
        <select class="cssfiles">
            <%foreach (var item in ViewBag.CssFiles)
              {%>
            <option value="<%=Url.Content(item)%>">
                <%:System.IO.Path.GetFileName(item)%></option>
            <%}%>
        </select>
        <input type="button" class="btnSave" value="<%:"Save".Localize()%>" style="margin-left: 40px;" />
        <input type="button" class="btnReload" value="<%:"Reload".Localize()%>" />
        <input type="button" class="btnUndo" value="<%:"Undo".Localize()%>" />
        <input type="button" class="btnRedo" value="<%:"Redo".Localize()%>" />
        <span class="message" style="color: Red; font-weight: bold; font-size: 14px;"></span>
    </div>
    <div id="container" style="margin-top: 6px;">
    </div>
    <div class="footer">
    </div>
</body>
</html>
