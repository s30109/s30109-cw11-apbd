namespace s30109_cw11.DTOs;

public class PrescriptionRequestDto
{
    public PatientDto Patient { get; set; }
    public int DoctorId { get; set; }
    public List<MedicamentDto> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}

public class PatientDto
{
    public int IdPatient { get; set; }  // 0 jeśli nowy
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }
}

public class MedicamentDto
{
    public int IdMedicament { get; set; }
    public int Dose { get; set; }
    public string Description { get; set; }
}