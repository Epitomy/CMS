﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%
    var controller = ViewContext.RequestContext.GetRequestValue("controller").ToLower();
    var action = ViewContext.RequestContext.GetRequestValue("action").ToLower();
    var IsNew = action == "create";
    ViewContext.RequestContext.RouteData.Values["from"] = controller.ToString().ToLower();
    ViewContext.RequestContext.RouteData.Values["fromName"] = ViewContext.RequestContext.AllRouteValues()["name"];
    var prevController = ((ViewContext.RequestContext.AllRouteValues()["prevController"] ?? Request["prevController"]) ?? ViewContext.RequestContext.AllRouteValues()["controller"]).ToString().ToLower();
%>
<ul class="step clearfix">
    <li class="<%=(action=="create"||action=="edit")&&(controller=="view"||controller=="page")?"current":" " %>">
        <%if (IsNew || ((action == "create" || action == "edit") && (controller == "view" || controller == "page")))
          {%>
        <%=IsNew?"Create".Localize(): "Edit".Localize() %>
        <%}else{%>
        <%:Html.ActionLink("Edit".Localize(),"Edit",prevController.ToString(),ViewContext.RequestContext.AllRouteValues(),new RouteValueDictionary()) %>
        <%} %>
    </li>

    <%if (prevController == "page") {%>
    <li class="<%:action=="positions" ?"current":""%>">
        <%if (IsNew || action == "positions")
          { %>
          <%="Positions".Localize() %>
          <%}
          else {%>
          <%:Html.ActionLink("Positions".Localize(), "Positions", "Page", ViewContext.RequestContext.AllRouteValues(), new RouteValueDictionary() { })%>
          <% } %>
    </li>
    <% } %>

    <li class="<%=action=="index"&&controller=="datarule"?" current":"" %>">
        <%
            if (IsNew ||( action == "index" && controller == "datarule"))
            {%><%="DataRules".Localize() %><%}else{%>
        <%:Html.ActionLink("DataRules".Localize(), "Index", "DataRule", ViewContext.RequestContext.AllRouteValues(), new RouteValueDictionary() { })%>
        <%} %>
    </li>
</ul>
