﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<string>" %>
<% ViewData.TemplateInfo.HtmlFieldPrefix = ViewData.TemplateInfo.HtmlFieldPrefix.Replace(ViewData.ModelMetadata.PropertyName, "").Trim('.'); %>
<tr>
    <th>
        <label for="<%: ViewData.ModelMetadata.PropertyName%>">
            <%: ViewData.ModelMetadata.GetDisplayName().Localize()%></label>
    </th>
    <td>
        <%: Html.TextBox(ViewData.ModelMetadata.PropertyName,Model) %>
        <% if (!string.IsNullOrEmpty(ViewData.ModelMetadata.Description))
           {%>
        <a href="#" class="tooltip-link" title="<%: ViewData.ModelMetadata.Description %>">
        </a>
        <%} %>
        <%: Html.ValidationMessage(ViewData.ModelMetadata,null) %>
        <script type="text/javascript">
            jQuery(function ($) {
                $("#<%: ViewData.ModelMetadata.PropertyName %>").mask("9.9.9.9");
            });
        </script>
    </td>
</tr>
