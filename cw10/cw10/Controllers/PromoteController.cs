using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cw10.Models;

namespace cw10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromoteController : ControllerBase
    {
        private readonly s16767Context _context;

        public PromoteController(s16767Context context)
        {
            _context = context;
        }

        
    }
}
