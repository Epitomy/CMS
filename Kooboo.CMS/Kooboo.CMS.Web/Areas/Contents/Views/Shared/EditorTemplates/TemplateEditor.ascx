﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<object>" %>
<% ViewData.TemplateInfo.HtmlFieldPrefix = ViewData.TemplateInfo.HtmlFieldPrefix.Replace(ViewData.ModelMetadata.PropertyName, "").Trim('.'); %>
<tr>
    <th>
        <label for="<%: ViewData.ModelMetadata.PropertyName%>">
            <%: ViewData.ModelMetadata.GetDisplayName().Localize()%></label>
    </th>
    <td>
        <%: Html.TextArea(ViewData.ModelMetadata.PropertyName, Model == null ? "" : Model.ToString(), new { rows = 30, style = "width:600px;min-width:400px;max-width:700px" })%>
        <% 
            if (!string.IsNullOrEmpty(ViewData.ModelMetadata.Description))
           {%>
        <a href="#" class="tooltip-link" title="<%: ViewData.ModelMetadata.Description %>">
        </a>
        <%} %>
        <%: Html.ValidationMessage(ViewData.ModelMetadata,null) %>
    </td>
</tr>
