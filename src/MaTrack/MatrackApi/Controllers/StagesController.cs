using MaTrack.Core.Entities;
using MaTrack.Shared.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MatrackApi.Controllers
{
    [Route("api/[controller]")]
    public class StagesController : BaseController<StageEntity>
    {
        public StagesController(IStageRepository stageRepository):base(stageRepository)
        {

        }

    }
}
