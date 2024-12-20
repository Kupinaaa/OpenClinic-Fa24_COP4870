using System;
using Api.HospitalSystem.Data;
using Api.HospitalSystem.Dtos.AppointmentDtos;
using Api.HospitalSystem.Interfaces;
using Api.HospitalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.HospitalSystem.Repositories;

public class AppointmentRepository: IAppointmentRepository
{
    private readonly ApplicationDbContext _context;
    public AppointmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Appointment?> Create(Appointment createAppointment)
    {
        await _context.Appointments.AddAsync(createAppointment);
        await _context.SaveChangesAsync();

        return await _context.Appointments
            .Include(a => a.Patient)
                .ThenInclude(at => at.InsurancePlan)
            .Include(a => a.Physician)
            .Include(a => a.AppointmentTreatments)
                .ThenInclude(at => at.Treatment)
            .Include(a => a.Bill)
            .FirstOrDefaultAsync(a => a.Id == createAppointment.Id);
    }

    public async Task<Appointment?> Delete(int id)
    {
        var deleteAppointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
        if (deleteAppointment == null) return null;

        _context.Appointments.Remove(deleteAppointment);
        await _context.SaveChangesAsync();

        return deleteAppointment;
    }

    public async Task<List<Appointment>> GetAll()
    {
        return await _context.Appointments
            .Include(a => a.Patient)
                .ThenInclude(at => at.InsurancePlan)
            .Include(a => a.Physician)
            .Include(a => a.AppointmentTreatments)
                .ThenInclude(at => at.Treatment)
            .Include(a => a.Bill)
            .ToListAsync();
    }

    public async Task<Appointment?> GetById(int id)
    {
        return await _context.Appointments
            .Include(a => a.Patient)
                .ThenInclude(at => at.InsurancePlan)
            .Include(a => a.Physician)
            .Include(a => a.AppointmentTreatments)
                .ThenInclude(at => at.Treatment)
            .Include(a => a.Bill)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<Appointment>> GetByPatientAndPhysicianId(int patientId, int physicianId)
    {
        var appointments = _context.Appointments
            .Include(a => a.Patient)
                .ThenInclude(at => at.InsurancePlan)
            .Include(a => a.Physician)
            .Include(a => a.AppointmentTreatments)
                .ThenInclude(at => at.Treatment)
            .Include(a => a.Bill)
            .Where(a => a.PatientId == patientId || a.PhysicianId == physicianId);
        return await appointments.ToListAsync();
    }

    public async Task<List<Appointment>> GetByPatientId(int patientId)
    {
        var appointments = _context.Appointments
            .Include(a => a.Patient)
                .ThenInclude(at => at.InsurancePlan)
            .Include(a => a.Physician)
            .Include(a => a.AppointmentTreatments)
                .ThenInclude(at => at.Treatment)
            .Include(a => a.Bill)
            .Where(a => a.PatientId == patientId);
        return await appointments.ToListAsync();
    }

    public async Task<List<Appointment>> GetByPhysicianId(int physicianId)
    {
        var appointments = _context.Appointments
            .Include(a => a.Patient)
                .ThenInclude(at => at.InsurancePlan)
            .Include(a => a.Physician)
            .Include(a => a.AppointmentTreatments)
                .ThenInclude(at => at.Treatment)
            .Include(a => a.Bill)
            .Where(a => a.PhysicianId == physicianId);
        return await appointments.ToListAsync();
    }

    public async Task<List<Appointment>> GetUpcomingByPatientId(int patientId, DateTime now)
    {
        var appointments = _context.Appointments
            .Include(a => a.Patient)
                .ThenInclude(at => at.InsurancePlan)
            .Include(a => a.Physician)
            .Include(a => a.AppointmentTreatments)
                .ThenInclude(at => at.Treatment)
            .Include(a => a.Bill)
            .Where(a => a.PatientId == patientId && a.DateTimeEnd >= now);
        return await appointments.ToListAsync();
    }

    public async Task<List<Appointment>> GetUpcomingByPhysicianId(int physicianId, DateTime now)
    {
        var appointments = _context.Appointments
            .Include(a => a.Patient)
                .ThenInclude(at => at.InsurancePlan)
            .Include(a => a.Physician)
            .Include(a => a.AppointmentTreatments)
                .ThenInclude(at => at.Treatment)
            .Include(a => a.Bill)
            .Where(a => a.PhysicianId == physicianId && a.DateTimeEnd >= now);
        return await appointments.ToListAsync();
    }

    public async void SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<Appointment?> Update(int id, Appointment updateBody)
    {
        var updateAppointment = await _context.Appointments
            .Include(a => a.Patient)
                .ThenInclude(at => at.InsurancePlan)
            .Include(a => a.Physician)
            .Include(a => a.AppointmentTreatments)
                .ThenInclude(at => at.Treatment)
            .Include(a => a.Bill)
            .FirstOrDefaultAsync(a => a.Id == id);
        if (updateAppointment == null) return null;

        if (updateAppointment.Bill != null) _context.Bills.Remove(updateAppointment.Bill);

        updateAppointment.AppointmentTreatments.ForEach(at => _context.AppointmentTreatments.Remove(at));

        updateAppointment.Title = updateBody.Title;
        updateAppointment.DateTimeStart = updateBody.DateTimeStart;
        updateAppointment.DateTimeEnd = updateBody.DateTimeEnd;
        updateAppointment.Description = updateBody.Description;
        updateAppointment.Physician = updateBody.Physician;
        updateAppointment.Patient = updateBody.Patient;
        updateAppointment.PhysicianId = updateBody.PhysicianId;
        updateAppointment.AppointmentTreatments = updateBody.AppointmentTreatments;
        updateAppointment.Bill = updateBody.Bill;


        await _context.SaveChangesAsync();

        return updateBody;
    }
}
