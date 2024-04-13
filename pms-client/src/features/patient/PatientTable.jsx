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
        <div>Doctor Assgined</div>
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
              patient={patient}
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
