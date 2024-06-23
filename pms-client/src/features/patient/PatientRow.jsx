import React from "react";
import styled from "styled-components";
import Table from "../../ui/Table";
import { Link } from "react-router-dom";
import Button from "../../ui/Button";
import Modal from "../../ui/Modal";
import { HiTrash, HiEye } from "react-icons/hi2";

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
              }}
            >
              <HiEye />
            </Button>
          </Modal.Open>
          <Modal.Window name='patient-details'>
          </Modal.Window>


          <Modal.Open opens="delete-patient">
            <Button variation="danger">
              <HiTrash />
            </Button>
          </Modal.Open>
          <Modal.Window name="delete-patient">
            
          </Modal.Window>
        </Modal>
      </div>
    </Table.Row>
  );
}

export default PatientRow;
