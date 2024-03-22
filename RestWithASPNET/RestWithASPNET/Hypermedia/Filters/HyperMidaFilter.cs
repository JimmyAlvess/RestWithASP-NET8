using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace RestWithASPNET.Hypermedia.Filters
{
    public class HyperMidaFilter : ResultFilterAttribute
    {
        private readonly HyperMidiaFilterOptions _hyperMidiaFilterOptions;
        public HyperMidaFilter(HyperMidiaFilterOptions hyperMidiaFilterOptions)
        {
            _hyperMidiaFilterOptions = hyperMidiaFilterOptions;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            tryEnrichResult(context);
            base.OnResultExecuting(context);
        }

        private void tryEnrichResult(ResultExecutingContext context)
        {
            if (context.Result is OkObjectResult objectResult)
            {
                var enricher = _hyperMidiaFilterOptions
                    .ContentResponseEnricherList.
                    FirstOrDefault(x => x.CanEnrich(context));
                if(enricher != null) Task.FromResult(enricher.Enrich(context));
            };
        }
    }
}
