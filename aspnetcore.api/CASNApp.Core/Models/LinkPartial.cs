namespace CASNApp.Core.Models
{
    public partial class Link
    {
        public Link(Entities.Link link)
        {
            Id = link.Id;
            Title = link.Title;
            Url = link.Url;
            Target = link.Target;
            DisplayOrdinal = link.DisplayOrdinal;
        }

    }
}
