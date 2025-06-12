using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace GorevTakipSistemi.Utilities.AutoMapper
{
    public class MappingProfile:Profile
    {

            public MappingProfile()
            {
                
                CreateMap<UserDtoForCreate, User>()
                    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

                CreateMap<CompanyOwnerRegistrationDto, User>()
                    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

                CreateMap<User, UserDtoForGet>();

                CreateMap<UserDtoForUpdate, User>()
                    .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

                CreateMap<User, UserDtoForUpdate>().ReverseMap();

                CreateMap<CompanyDtoForCreate, Company>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.TaxNumber, opt => opt.MapFrom(src => src.TaxNumber))
                    .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId)) 
                    .ForMember(dest => dest.Id, opt => opt.Ignore());


                CreateMap<CompanyDtoForUpdate, Company>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.TaxNumber, opt => opt.MapFrom(src => src.TaxNumber))
                    .ForAllMembers(opt => opt.Ignore());

                CreateMap<CompanyOwnerRegistrationDto, Company>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CompanyName))
                    .ForMember(dest => dest.TaxNumber, opt => opt.MapFrom(src => src.TaxNumber))
                    .ForMember(dest => dest.OwnerId, opt => opt.Ignore())
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForAllMembers(opt => opt.Ignore());

                CreateMap<Company, CompanyDto>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.TaxNumber, opt => opt.MapFrom(src => src.TaxNumber))
                    .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId));
                CreateMap<TaskItemDtoForCreate, TaskItem>()
                    .ForMember(dest => dest.Assignments, opt => opt.Ignore())
                    .ForMember(dest => dest.CreatedById, opt => opt.Ignore())
                    .ForMember(dest => dest.CompanyId, opt => opt.Ignore());
                CreateMap<User, UserDto>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                    .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                    .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
                    .ForMember(dest => dest.UserDescription, opt => opt.MapFrom(src => src.UserDescription))
                    .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                    .ForMember(dest => dest.Roles, opt => opt.Ignore())
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

            CreateMap<TaskItem, TaskItemForGetDto>()
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Assigned,opt => opt.MapFrom(src =>src.Assignments.FirstOrDefault() != null? 
                src.Assignments.First().AssignedTo.FirstName + " " + src.Assignments.First().AssignedTo.LastName: null))
                .ForMember(dest => dest.CreatedByFullName, opt => opt.MapFrom(src => src.CreatedByFullName.ToString()));

            CreateMap<TaskReport, ReportDtoForGet>()
                .ForMember(dest => dest.CreatedByFullName,
                opt => opt.MapFrom(src => src.CreatedBy.FirstName + " " + src.CreatedBy.LastName));

            CreateMap<ReportDtoForCreate, TaskReport>()
                .ForMember(dest => dest.PdfFileData, opt => opt.Ignore())
                .ForMember(dest => dest.PdfContentType, opt => opt.Ignore())
                .ForMember(dest => dest.PdfFileName, opt => opt.Ignore());

            CreateMap<TaskItemForUpdateDto, TaskItem>();


        }
    }



    }
        
    

