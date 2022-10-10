using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SampleOptimizelyAddon.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleOptimizelyAddon.Controllers
{
    public class SampleOptimizelyAddOnController : Controller
    {
        private SampleOptimizelyAddonOptions _options;
        public SampleOptimizelyAddOnController(IOptions<SampleOptimizelyAddonOptions> options)
        {
            _options = options.Value;
        }
    }
}
