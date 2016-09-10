using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure.Pagination;

namespace EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure.Helpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
            PageInfo pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("div");
               
                tag.InnerHtml = pageUrl(i);

                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("disabled");
                }
                tag.AddCssClass("btn-page");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }

    //    public static MvcHtmlString PageLinks(this HtmlHelper html,
    //      PageInfo pageInfo, Func<int, string> pageUrl)
    //    {
    //        StringBuilder result = new StringBuilder();
    //        for (int i = 1; i <= pageInfo.TotalPages; i++)
    //        {
    //            TagBuilder tag = new TagBuilder("div");
    //            tag.MergeAttribute("data-url", pageUrl(i));
    //            tag.InnerHtml = i.ToString();

    //            if (i == pageInfo.PageNumber)
    //            {
    //                tag.AddCssClass("selected");
    //            }
    //            tag.AddCssClass("btn btn-page");
    //            result.Append(tag.ToString());
    //        }
    //        return MvcHtmlString.Create(result.ToString());
    //    }
    }
}