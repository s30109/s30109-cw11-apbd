using Microsoft.EntityFrameworkCore;
using s30109_cw11.Data;
using s30109_cw11.Services.Interfaces;

namespace s30109_cw11.Services.Implementations;

public class PatientService : IPatientService
{
    private readonly AppDBContext _context;

    public PatientService(AppDBContext context)
    {
        _context = context;
    }

    public async Task<object?> GetPatientDetailsAsync(int id)
    {
        var patient = await _context.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.Doctor)
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .FirstOrDefaultAsync(p => p.IdPatient == id);

        if (patient == null)
            return null;

        return new
        {
            patient.IdPatient,
            patient.FirstName,
            patient.LastName,
            patient.Email,
            Prescriptions = patient.Prescriptions
                .OrderBy(p => p.DueDate)
                .Select(p => new
                {
                    p.IdPrescription,
                    p.Date,
                    p.DueDate,
                    Doctor = new
                    {
                        p.Doctor.IdDoctor,
                        p.Doctor.FirstName,
                        p.Doctor.LastName
                    },
                    Medicaments = p.PrescriptionMedicaments.Select(pm => new
                    {
                        pm.Medicament.IdMedicament,
                        pm.Medicament.Name,
                        pm.Dose,
                        pm.Description
                    })
                })
        };
    }
}