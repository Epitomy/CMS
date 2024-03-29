﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Sites/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<Kooboo.CMS.Sites.Models.PagePosition>>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%
        ViewContext.RequestContext.RouteData.Values["siteName"] = ViewContext.RequestContext.GetRequestValue("siteName");
        ViewContext.RequestContext.RouteData.Values["fullName"] = ViewContext.RequestContext.GetRequestValue("fullName"); 
        %>
    <h2>
        Positions for "<%: ViewContext.RequestContext.GetRequestValue("fullName") %>"</h2>
        <%:Html.Partial("ViewStepNavigation") %>
    <div class="command">
        <%:Html.ActionLink("Create".Localize(), "CreatePosition", "Page", ViewContext.RequestContext.AllRouteValues(), new RouteValueDictionary(new { @class = "button create"}))%>
        
        
    </div>
    <%: Html.GridForModel() %>
    <p class="buttons">
        <%:Html.ActionLink("<< Previous", "Index", ViewContext.RequestContext.AllRouteValues(), new RouteValueDictionary(new {@class="button" }))%>
        <%:Html.ActionLink("Next >>", "Index", "DataRule" , ViewContext.RequestContext.AllRouteValues(), new RouteValueDictionary(new {@class="button" }))%>
    </p>
        <script language="javascript" type="text/javascript">
            adminJs.pageJs.initPopup();
    </script>
</asp:Content>
