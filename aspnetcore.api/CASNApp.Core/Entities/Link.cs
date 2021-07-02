using System;

namespace CASNApp.Core.Entities
{
    public partial class Link
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Target { get; set; }
        public int DisplayOrdinal { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
