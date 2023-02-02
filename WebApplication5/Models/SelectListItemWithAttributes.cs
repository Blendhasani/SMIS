using System.Web.WebPages.Html;

namespace WebApplication5.Models
{
	public class SelectListItemWithAttributes : SelectListItem
	{
		public IDictionary<string, string> HtmlAttributes { get; set; }
		public SelectListItemWithAttributes()
		{
			HtmlAttributes = new Dictionary<string, string>();
		}
	}
}
