using ClinicTool.Domain.Dto;

namespace ClinicTool.Domain.Services.Interfaces
{
    public interface IDoctorService
    {
        List<DoctorDto> GetAll();

        void Add(DoctorDto doctor);

        void Update(DoctorDto doctor);

        bool Remove(DoctorDto doctor);
    }
}


