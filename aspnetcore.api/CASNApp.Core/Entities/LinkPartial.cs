using CASNApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CASNApp.Core.Entities
{
    [ModelMetadataType(typeof(LinkMetadata))]
    public partial class Link : ISoftDelete, ICreatedDate, IUpdatedDate
    {
    }
}
