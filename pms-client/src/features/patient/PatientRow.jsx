import React from "react";
import styled from "styled-components";
import Table from "../../ui/Table";

const Patient = styled.div`
  font-size: 1.6rem;
  font-weight: 600;
  color: var(--color-grey-600);
  font-family: "Sono";
`;

function PatientRow({patient}) {
  return (
    <Table.Row role="row">
        <Patient>{patient.displayName}</Patient>
        <Patient>{patient.email}</Patient>
        <Patient>{patient.phoneNumber}</Patient>
        <Patient>{patient.PatientLicenseId}</Patient>
        <Patient>{patient.appointmentCount}</Patient>
        {/* <div>
          <Modal>
          <Modal.Open opens='doctor-details'>
          <Button
              onClick={async () => {
                const doctorDetails = await getDoctorDetails(doctor.doctorId);
                console.log('doctorDetails', doctorDetails);  
              }}
            >
              <HiEye />
            </Button>
          </Modal.Open>
          <Modal.Window name='doctor-details'>
            <DoctorDetails doctorDetails={doctorDetails} />
          </Modal.Window>
            

            <UpdateDoctor doctorData={doctor} handleUpdate={handleUpdate} />

            <Modal.Open opens="delete-doctor">
              <Button>
                <HiTrash />
              </Button>
            </Modal.Open>
            <Modal.Window name="delete-doctor">
              <ConfirmDelete
                resourceName={doctor.displayName}
                disabled={isDeleting}
                onConfirm={() => {
                  handleDeleteDoctor(doctor.doctorId);
                }}
              />
            </Modal.Window>
          </Modal>
        </div> */}
      </Table.Row>
  )
}

export default PatientRow
