using Microsoft.AspNetCore.Mvc;
using PlcBase.Base.Filter;

namespace PlcBase.Base.Controller;

[ApiController]
[Route("api/[controller]")]
[ServiceFilter(typeof(ValidateModelFilter))]
public class BaseController : ControllerBase { }
