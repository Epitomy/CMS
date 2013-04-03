﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Kooboo.CMS.Web.Areas.Sites.Models.ImportSiteModel>" %>
<div id="import-site" style="display: none;">
        <form method="post" action="<%=Url.Action("ImportSite",ViewContext.RequestContext.AllRouteValues()) %>"
        enctype="multipart/form-data">
        <div class="common-form">
            <table>
                <%:Html.EditorFor(o=>o.Name) %>
                <%:Html.EditorFor(o => o.Parent, new {@class="long" })%>
                <tr>
                    <th>
                        <%:"Upload".Localize() %>
                    </th>
                    <td>
                        <input type="file" name="SitePackage" />
                    </td>
                </tr>
            </table>
        </div>
        <p class="buttons">
            <button type="submit">
                <%:"Save" %></button>
        </p>
        </form>
    </div>

