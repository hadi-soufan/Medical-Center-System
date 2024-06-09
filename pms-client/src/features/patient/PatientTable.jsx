import React from 'react';
import Table from "../../ui/Table";
import Menus from "../../ui/Menus";
import PatientRow from './PatientRow';

function PatientTable({patients}) {
  return (
    <Menus>
    <Table columns="repeat(6, 1fr)">
      <Table.Header>
        <div>Name</div>
        <div>Date Check In</div>
        <div>Medical Record</div>
        <div>Disease</div>
        <div>Phone</div>
        <div>Actions</div>
      </Table.Header>

      <Table.Body
        data={patients}
        render={(patient) => (
          patient && (
            <PatientRow
              key={patient.patientId}
              patient={{
                  displayName: patient.displayName,
                  createdAt: patient.createdAt,
                  medicalHistoryId: patient.medicalHistoryId,
                  phoneNumber: patient.phoneNumber,
                  patientLicenseId: patient.patientLicenseId,
                  appointmentCount: patient.appointmentCount,
                  medicalHistory: patient?.user?.patients?.$values[0]?.medicalHistory?.medicalHistoryId,
                  diagnosis: patient?.user?.patients?.$values[0]?.medicalHistory?.diagnosis || "N/A"
                }}
            //   handleDeleteDoctor={handleDeleteDoctor}
            //   isDeleting={isDeleting}
            //   setIsDeleting={setIsDeleting}
            //   getDoctorDetails={getDoctorDetails}
            />
          )
        )}
      />
      
    </Table>
  </Menus>
  )
}

export default PatientTable
