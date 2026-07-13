using SMS.DTOS.StudentDtos;
using SMS.Models;
using SMS.Repositorys;
using StudentManagementSystem.Services.Interfaces;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    private readonly IUserRepository _userRepository;

    public StudentService(
        IStudentRepository studentRepository,
        IUserRepository userRepository)
    {
        _studentRepository = studentRepository;
        _userRepository = userRepository;
    }
    public async Task<StudentResponseDTO> CreateStudentAsync(
    CreateStudentRequestDTO dto)
    {
        var user = new User
        {
            Username = dto.Username,
            PasswordHash = dto.Password,
            Email = dto.Email,
            RoleId = 4,
            Status = "ACTIVE",
        };

        await _userRepository.CreateAsync(user);

        var student = new Student
        {
            StudentCode = dto.StudentCode,
            UserId = user.UserId,

            FirstName = dto.FirstName,
            LastName = dto.LastName,

            Dob = DateOnly.FromDateTime(dto.Dob),

            Gender = dto.Gender,
            Phone = dto.Phone,
            Email = dto.Email,
            Address = dto.Address,

            AdmissionDate = DateOnly.FromDateTime(DateTime.UtcNow),

            ClassId = dto.ClassId,
            Status = "ACTIVE"
        };

        await _studentRepository.CreateAsync(student);

        return new StudentResponseDTO
        {
            StudentId = student.StudentId,
            StudentCode = student.StudentCode,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email,
            Status = student.Status
        };
    }

    public async Task<List<StudentResponseDTO>>
        GetAllStudentsAsync()
    {
        var students = await _studentRepository
            .GetAllAsync();

        return students.Select(x => new StudentResponseDTO
        {
            StudentId = x.StudentId,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Email = x.Email
        }).ToList();
    }

    public async Task<StudentResponseDTO?>GetStudentByIdAsync(int studentId)
    {
        var student =await _studentRepository.GetByIdAsync(studentId);

        if (student == null)
            return null;

        return new StudentResponseDTO
        {
            StudentId = student.StudentId,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email
        };
    }

    public async Task<StudentResponseDTO>UpdateStudentAsync(
     int studentId,UpdateStudentRequestDTO dto)
    {
        var student =await _studentRepository.GetByIdAsync(studentId);

        if (student == null)
            throw new Exception("Student Not Found");

        student.FirstName = dto.FirstName;
        student.LastName = dto.LastName;
        student.Phone = dto.Phone;
        student.Address = dto.Address;

        await _studentRepository.UpdateAsync(student);

        return await GetStudentByIdAsync(studentId);
    }

    public async Task ChangeStudentStatusAsync(
        int studentId,
        string status)
    {
        var student =
            await _studentRepository.GetByIdAsync(studentId);

        if (student == null)
            throw new Exception("Student Not Found");

        student.Status = status;

        await _studentRepository.UpdateAsync(student);
    }
}