using RestWithASPNET.Hypermedia.Abstract;

namespace RestWithASPNET.Hypermedia.Filters
{
    public class HyperMidiaFilterOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
