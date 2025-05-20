using Microsoft.EntityFrameworkCore;
using s30109_cw11.Data;
using s30109_cw11.DTOs;
using s30109_cw11.Models;
using s30109_cw11.Services.Interfaces;

namespace s30109_cw11.Services.Implementations;

public class PrescriptionService : IPrescriptionService
{
    private readonly AppDBContext _context;

    public PrescriptionService(AppDBContext context)
    {
        _context = context;
    }

    public async Task<string> AddPrescriptionAsync(PrescriptionRequestDto request)
    {
        if (request.Medicaments.Count > 10)
            throw new ArgumentException("Recepta może zawierać maksymalnie 10 leków.");

        if (request.DueDate < request.Date)
            throw new ArgumentException("DueDate musi być większe lub równe Date.");

        var doctor = await _context.Doctors.FindAsync(request.DoctorId);
        if (doctor == null)
            throw new KeyNotFoundException("Lekarz nie istnieje.");

        var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.IdPatient == request.Patient.IdPatient);

        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = request.Patient.FirstName,
                LastName = request.Patient.LastName,
                Email = request.Patient.Email
            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        var prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            Doctor = doctor,
            Patient = patient,
            PrescriptionMedicaments = new List<PrescriptionMedicament>()
        };

        foreach (var med in request.Medicaments)
        {
            var medicament = await _context.Medicaments.FindAsync(med.IdMedicament);
            if (medicament == null)
                throw new KeyNotFoundException($"Lek o ID {med.IdMedicament} nie istnieje.");

            prescription.PrescriptionMedicaments.Add(new PrescriptionMedicament
            {
                Medicament = medicament,
                Dose = med.Dose,
                Description = med.Description
            });
        }

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        return "Recepta dodana.";
    }
}