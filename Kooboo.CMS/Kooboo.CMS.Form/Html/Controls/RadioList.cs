﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kooboo.CMS.Form.Html.Controls
{
    public class RadioList : ControlBase
    {
        public override string Name
        {
            get { return "RadioList"; }
        }
        public override bool HasDataSource
        {
            get
            {
                return true;
            }
        }
        protected override string RenderInput(IColumn column)
        {
            StringBuilder sb = new StringBuilder(string.Format(@"@{{ var radioDefaultValue_{0} = @""{1}"";}}", column.Name, column.DefaultValue.EscapeQuote()));
            if (!string.IsNullOrEmpty(column.SelectionFolder))
            {
                sb.AppendFormat(@"
                        @{{
                            var textFolder_{0} = new TextFolder(Repository.Current, ""{1}"");
                            var query_{0} = textFolder_{0}.CreateQuery().DefaultOrder();
                        }}
                        @foreach (var item in query_{0})
                        {{                            
                           <input id=""{0}"" name=""{0}"" type=""radio"" value=""@item.UUID""  @((Model.{0} != null && Model.{0}.ToString().ToLower() == @item.UUID.ToLower()) || (Model.{0} == null && radioDefaultValue_{0}.ToLower() == @item.UUID.ToLower()) ? ""checked"" : """")/><label for=""{0}"">@item.GetSummary()</label>
                        }}
                        ", column.Name, column.SelectionFolder);
            }
            else
            {

                if (column.SelectionItems != null)
                {
                    var index = 0;
                    foreach (var item in column.SelectionItems)
                    {
                        var id = column.Name + "_" + index.ToString();
                        index++;
                        sb.AppendFormat(@"
<input id=""{0}"" name=""{1}"" type=""radio"" value=""@(@""{2}"")""  @((Model.{1} != null && Model.{1}.ToString().ToLower() == @""{2}"".ToLower()) || (Model.{1} == null && radioDefaultValue_{1}.ToLower() == @""{2}"".ToLower()) ? ""checked"" : """")/><label for=""{0}"">{3}</label>"
                            , id, column.Name, item.Value.EscapeQuote(), item.Text);
                    }
                }

            }
            return sb.ToString();
        }
    }
}
