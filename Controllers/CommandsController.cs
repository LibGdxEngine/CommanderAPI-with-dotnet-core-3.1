using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    //api/commands
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository , IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        //GET api/commands
        [HttpGet]
        public ActionResult <IEnumerable <CommandReadDto>> GelAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        //GET api/commands/{Id}
        [HttpGet("{Id}" , Name = "GetCommandById")]
        public ActionResult  <CommandReadDto> GetCommandById(int Id)
        {
            var commandItem = _repository.GetCommandById(Id);
            if(commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }
            return NotFound();//return 404 NOT FOUND
        }

        //POST api/commands
        [HttpPost]
        public ActionResult <CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            //convert from createDto to model
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreatCommand(commandModel);
            _repository.SaveChanges();
            //convert from model to readDto 
            var commandReadModel = _mapper.Map<CommandReadDto>(commandModel);
            return CreatedAtRoute(nameof(GetCommandById) , new {Id = commandReadModel.Id } , commandReadModel);
        }

        //PUT api/command/{Id}
        [HttpPut("{Id}")]
        public ActionResult UpdateCommand(int Id , CommandUpdateDto commandUpdateDto) 
        {
            var commandModelFromRepo = _repository.GetCommandById(Id);
            if(commandModelFromRepo  == null)
            {
                return NotFound();
            }
            //map from updateDto to model // mapping is just exist but we don't do any thing with it right now
            _mapper.Map(commandUpdateDto , commandModelFromRepo);

            _repository.UpdateCommand(commandModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //PATCH api/comaands/{Id}
        [HttpPatch("{Id}")]
        public ActionResult PartialCommandUpdate(int Id , JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            
            var commandModelFromRepo = _repository.GetCommandById(Id);
            if(commandModelFromRepo  == null)
            {
                return NotFound();
            }
            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);

            patchDoc.ApplyTo(commandToPatch , ModelState);

            if(!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, commandModelFromRepo);

            _repository.UpdateCommand(commandModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/commands/{Id}
        [HttpDelete("{Id}")]
        public ActionResult DeleteCommand(int Id) 
        {
            var commandModelFromRepo = _repository.GetCommandById(Id);
            if(commandModelFromRepo == null )
            {
                return NotFound();
            }
            _repository.DeleteCommand(commandModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
       
    }

}
