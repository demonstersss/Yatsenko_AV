using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Yatsenko_AV.TagHelpers
{
	[HtmlTargetElement("greeting")]
	public class GreetingTagHelper : TagHelper
	{
		public string Name { get; set; } = "Гость";

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "h2";
			output.Attributes.Add("class", "text-center text-primary mb-4");
			output.Content.SetHtmlContent($"👋 Добро пожаловать, <strong>{Name}</strong>!");
		}
	}
}
