using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure.Common;
using EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure.Pagination;

namespace EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure.Helpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
            PageInfo pageInfo, Func<int, string, string> pageLink)
        {
            var result = new StringBuilder();
            int paginationButtonOnPage = Constants.PageRange;

            //Make button 'first'
            if (pageInfo.PageNumber > paginationButtonOnPage / 2 + 1)
            {
                var first = new TagBuilder("div");
                first.InnerHtml = pageLink(1, "first");
                first.AddCssClass("btn-page");
                result.Append(first);
            }

            #region Generate button of pagination

            if (pageInfo.TotalPages <= paginationButtonOnPage)
            {
                for (int i = 1; i <= pageInfo.TotalPages; i++)
                {
                    GeneratePages(ref result, i, pageInfo, pageLink);
                }
            }
            else if (pageInfo.PageNumber <= (int)Math.Round((double)paginationButtonOnPage / 2, MidpointRounding.ToEven))
            {
                for (int i = 1; i <= paginationButtonOnPage; i++)
                {
                    GeneratePages(ref result, i, pageInfo, pageLink);
                }
            }
            else if (pageInfo.TotalPages - pageInfo.PageNumber < (int)Math.Round((double)paginationButtonOnPage / 2, MidpointRounding.ToEven))
            {
                for (int i = pageInfo.TotalPages - paginationButtonOnPage + 1; i <= pageInfo.TotalPages; ++i)
                {
                    GeneratePages(ref result, i, pageInfo, pageLink);
                }
            }
            else
            {
                for (int i = pageInfo.PageNumber - paginationButtonOnPage / 2; i <= pageInfo.PageNumber + paginationButtonOnPage / 2; i++)
                {
                    GeneratePages(ref result, i, pageInfo, pageLink);
                 }
            }

            #endregion
            //Make button 'last'
            if (pageInfo.PageNumber < pageInfo.TotalPages - paginationButtonOnPage / 2)
            {
                var last = new TagBuilder("div");
                last.InnerHtml = pageLink(pageInfo.TotalPages, "last");
                last.AddCssClass("btn-page");

                result.Append(last);
            }

            return MvcHtmlString.Create(result.ToString());
        }

        private static void GeneratePages(ref StringBuilder result, int i, PageInfo pageInfo, Func<int, string, string> pageLink)
        {
            var tag = new TagBuilder("div");
            tag.InnerHtml = pageLink(i, i.ToString());

            if (i == pageInfo.PageNumber)
            {
                tag.AddCssClass("disabled");
            }
            tag.AddCssClass("btn-page");
            result.Append(tag);
        }
    }
}