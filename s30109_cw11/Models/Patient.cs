﻿using System.Collections;

namespace s30109_cw11.Models;

public class Patient
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Email { get; set; }
    
    public ICollection<Prescription> Prescriptions { get; set; }
}