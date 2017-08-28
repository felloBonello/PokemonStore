using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Razor.TagHelpers;
using System;
using System.Text;
using PokemonStore.ViewModels;
using PokemonStore.Utils;
namespace PokemonStore.TagHelpers
{
    // You may need to install the Microsoft.AspNet.Razor.Runtime package into your project
    [HtmlTargetElement("catalogue", Attributes = BrandIdAttribute)]
    public class CatalogueHelper : TagHelper
    {
        private const string BrandIdAttribute = "Brand";
        [HtmlAttributeName(BrandIdAttribute)]
        public string BrandId { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public CatalogueHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (_session.GetObject<ProductViewModel[]>(SessionVars.Catalogue) != null)
            {
                var innerHtml = new StringBuilder();
                ProductViewModel[] catalogue = _session.GetObject<ProductViewModel[]>(SessionVars.Catalogue);
                innerHtml.Append("<div class=\"col-xs-12\" style=\"font-size:xlarge;\"><span>Catalogue</span></div>");
                foreach (ProductViewModel item in catalogue)
                {
                    if (item.BrandId == Convert.ToInt32(BrandId) || Convert.ToInt32(BrandId) == 0)
                    {
                        innerHtml.Append("<div id=\"item\" class=\"col-sm-3 col-xs-12 textcenter\" style=\"border:solid;background-color:white;\">");
                        innerHtml.Append("<span class=\"col-xs-12\"><img src=\"/img/"+ item.ProductName + ".png\"/ width =\"128\" height\"128\"></span> ");
                        innerHtml.Append("<p id=desc" + item.Id + " data-description=\"" + item.ProductName + "\">");
                        innerHtml.Append("<span style=\"font-size:large;\">" + item.ProductName + "</span></p><div>");
                        innerHtml.Append("<span>Click for Details</span></div> ");
                        innerHtml.Append("<div style=\"padding-bottom: 10px;\"><a href =\"#details_popup\" data-toggle=\"modal\" class=\"btn btn-default\"");
                        innerHtml.Append("<id=\"modalbtn\" data-id=\"" + item.Id + "\">Details</a>");
                        innerHtml.Append("<input type=\"hidden\" id=\"Description" + item.Id + "\" value =\"" + item.Description + "\"/>");
                        innerHtml.Append("<input type=\"hidden\" id=\"detailsGraphic" + item.Id + "\" value =\"..\\img\\" + item.GraphicName + "\"/>");
                        innerHtml.Append("<input type=\"hidden\" id=\"CostPrice" + item.Id + "\" value =\"" + item.CostPrice + "\"/>");
                        innerHtml.Append("<input type=\"hidden\" id=\"MSRP" + item.Id + "\" value =\"" + item.MSRP + "\"/>");
                        innerHtml.Append("<input type=\"hidden\" id=\"QtyOnHand" + item.Id + "\" value =\"" + item.QtyOnHand + "\"/>");
                        innerHtml.Append("<input type=\"hidden\" id=\"QtyOnBackOrder" + item.Id + "\" value =\"" + item.QtyOnBackOrder + "\"/>");
                        innerHtml.Append("<input type=\"hidden\" id=\"ProductName" + item.Id + "\" value =\"" + item.ProductName + "\"/>");
                        innerHtml.Append("<input type=\"hidden\" id=\"hp" + item.Id + "\" value =\"" + item.HP + "\"/>");
                        innerHtml.Append("<input type=\"hidden\" id=\"attack" + item.Id + "\" value =\"" + item.ATTACK + "\"/>");
                        innerHtml.Append("<input type=\"hidden\" id=\"defence" + item.Id + "\" value =\"" + item.DEFENCE + "\"/>");
                        innerHtml.Append("<input type=\"hidden\" id=\"specialAttack" + item.Id + "\" value =\"" + item.SPECIALATTACK + "\"/>");
                        innerHtml.Append("<input type=\"hidden\" id=\"specialDefence" + item.Id + "\" value =\"" + item.SPECIALDEFENCE + "\"/>");
                        innerHtml.Append("<input type=\"hidden\" id=\"speed" + item.Id + "\" value =\"" + item.SPEED + "\"/></div></div>");
                    }
                }
                output.Content.SetHtmlContent(innerHtml.ToString());
            }
        }
    }
}