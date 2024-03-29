﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%
    if (Model != null)
    {%>
<ul nodetype="<%:ViewData["NodeType"]!=null?ViewData["NodeType"].ToString():"tree-root" %>">
    <% foreach (var p in Model)
       {
           if (p.Value == null) {
               continue;
           }
           var page = new Kooboo.CMS.Sites.Models.Page(Kooboo.CMS.Sites.Models.Site.Current, PageHelper.SplitFullName(p.Value)).AsActual();
    %>
    <li class="clearfix">
        <input type="checkbox" value="<%:page.VirtualPath %>" text="<%: page.LinkText %>" name="selectedPage"
            class="selected-page" />
        <a href="javascript:;">
            <%:p.Text %></a>
        <%if (p.Items != null && ((IEnumerable<SelectListItemTree>)p.Items).Count() > 0)
          {%>
        <%:Html.Partial("PageTree", (object)p.Items, new ViewDataDictionary(new { NodeType ="tree-node-"+ Guid.NewGuid() }))%>
        <% } %>
    </li>
    <% }%>
</ul>
<%
    } %>