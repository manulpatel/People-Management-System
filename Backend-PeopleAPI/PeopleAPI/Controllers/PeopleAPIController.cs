using Microsoft.AspNetCore.Mvc;
using PeopleAPI.EfCore;
using PeopleAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace peopleApi.Controllers
{
    
    [ApiController]
    public class peopleApiController : ControllerBase
    {
        private readonly DbHelper _db;
        public peopleApiController(EF_DataContext eF_DataContext)
        {
            _db = new DbHelper(eF_DataContext);
        }



        /// <summary>
        /// Returns details of all the people in database
        /// </summary>
        /// <returns></returns>

        /// <response code="201">People</response>
        /// <response code="400">First Name is empty</response>

        // GET: api/<PeopleAPIController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("[controller]/people")]
        public IActionResult Get()
        {
            ResponseType type = ResponseType.Success;
            try
            {
                IEnumerable<PersonModel> data = _db.GetPeople();

                if (!data.Any())
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        /// <summary>
        /// Returns specific person details by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        // GET api/<PeopleAPIController>/5
        [HttpGet]
        [Route("[controller]/people/{Id}")]
        public IActionResult Get(Guid Id)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                PersonModel data = _db.GetPersonById(Id);
                if (data == null)
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type,data));
            }
            catch (Exception ex)
            {
                type = ResponseType.Failure;
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));    
            }
        }

        /// <summary>
        /// Adds a new record in the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST api/<PeopleAPIController>
        [HttpPost]
        [Route("[controller]/people")]
        public IActionResult Post([FromBody] PersonModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.SavePerson(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        /// <summary>
        /// Updates details' of exisiting person in the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // PUT api/<PeopleAPIController>/5
        [HttpPut]
        [Route("[controller]/people")]
        public IActionResult Put([FromBody] PersonModel model)
        {

            try
            {
                ResponseType type = ResponseType.Success;
                _db.UpdatePerson(model, model.Id);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        /// <summary>
        /// Removes the person details' from the database
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        // DELETE api/<PeopleAPIController>/5
        [HttpDelete]
        [Route("[controller]/people/{Id}")]
        public IActionResult Delete(Guid Id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.DeletePerson(Id);
                return Ok(ResponseHandler.GetAppResponse(type, "Deleted Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}