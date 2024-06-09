import React from "react";
import styled from "styled-components";
import Table from "../../ui/Table";
import { Link } from "react-router-dom";
import Button from "../../ui/Button";
import Modal from "../../ui/Modal";
import { HiTrash, HiEye } from "react-icons/hi2";
import ConfirmDelete from "../../ui/ConfirmDelete";

const Patient = styled.div`
  font-size: 1.6rem;
  font-weight: 600;
  color: var(--color-grey-600);
  font-family: "Sono";
`;

function PatientRow({ patient }) {
  return (
    <Table.Row role="row">
      <Patient>{patient.displayName}</Patient>
      <Patient>{patient.createdAt.split("T")[0]}</Patient>

      {patient.medicalHistory ? (
        <Link to={`/update-medical-history/${patient.medicalHistory}`}>
          <Button variation="secondary">See Record</Button>
        </Link>
      ) : (
        <Link to={"/medical-histories?tab=createForm"}>
          <Button variation="secondary">Create Record</Button>
        </Link>
      )}

      <Patient>{patient.diagnosis}</Patient>
      <Patient>{patient.phoneNumber}</Patient>
      <div>
        <Modal>
          <Modal.Open opens='patient-details'>
            <Button
              onClick={async () => {
                // const doctorDetails = await getDoctorDetails(doctor.doctorId);
                // console.log('doctorDetails', doctorDetails);  
              }}
            >
              <HiEye />
            </Button>
          </Modal.Open>
          <Modal.Window name='patient-details'>
            {/* <DoctorDetails doctorDetails={doctorDetails} /> */}
          </Modal.Window>

          {/* <UpdateDoctor doctorData={doctor} handleUpdate={handleUpdate} /> */}

          <Modal.Open opens="delete-patient">
            <Button variation="danger">
              <HiTrash />
            </Button>
          </Modal.Open>
          <Modal.Window name="delete-patient">
            {/* <ConfirmDelete
              resourceName={doctor.displayName}
              disabled={isDeleting}
              onConfirm={() => {
                handleDeleteDoctor(doctor.doctorId);
              }}
            /> */}
          </Modal.Window>
        </Modal>
      </div>
    </Table.Row>
  );
}

export default PatientRow;
