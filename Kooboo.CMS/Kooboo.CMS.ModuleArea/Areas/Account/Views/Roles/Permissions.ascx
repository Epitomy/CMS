﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Kooboo.CMS.Web.Areas.Account.Models.CreateRoleModel>" %>
<% var index = 0;
   foreach (var area in Model.GetAreas())
   {%>
<fieldset>
    <legend>
        <%:area.Localize()%></legend>
    <table>
        <%foreach (var group in Model.GetGroups(area))
          {%>
        <tr>
            <th>
                <label>
                    <%: group.Localize()  %></label>
            </th>
            <td>
                <%foreach (var permission in Model.GetPermission(area, group))
                  {
                      var indexName = string.Format("AllPermissions[{0}]", index);
                      index++;
                %>
                <p class="left" style="min-width: 120px;">
                    <%:Html.CheckBox(indexName + ".Checked", permission.Checked, new { id=indexName + ".CheckedId"})%>
                    <label for="<%:indexName+".CheckedId" %>">
                        <%:permission.DisplayName.Localize() %>
                    </label>
                    <%: Html.Hidden(indexName+".AreaName",permission.AreaName) %>
                    <%: Html.Hidden(indexName + ".Group", permission.Group)%>
                    <%: Html.Hidden(indexName + ".Name", permission.Name)%>
                </p>
                <%} %>
            </td>
        </tr>
        <%} %>
    </table>
</fieldset>
<%} %>