using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            //source -> distination
            CreateMap<Command, CommandReadDto>();
            //distination -> source 
            CreateMap<CommandCreateDto , Command>();
            //
            CreateMap<CommandUpdateDto , Command>();
            //
            CreateMap<Command , CommandUpdateDto>();
            

        }
        
    }
}