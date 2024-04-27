import React from "react";
import styled from "styled-components";
import Table from "../../ui/Table";
import Modal from "../../ui/Modal";
import Button from "../../ui/Button";
import { HiTrash, HiPencil } from "react-icons/hi2";
import ReadMedicalHistory from "./ReadMedicalHistory";
import UpdateMedicalHistory from "./UpdateMedicalHistory";
import ConfirmDelete from "../../ui/ConfirmDelete";
import { Link } from "react-router-dom";

const MedicalHistory = styled.div`
  font-size: 1.6rem;
  font-weight: 600;
  color: var(--color-grey-600);
  font-family: "Sono";
  width: 95%;
`;

const Problem = styled.div`
  background-color: var(--color-red-700);
  color: var(--color-red-100);
  text-align: center;
  padding: 0.1em 0.5em;
  border-radius: 100px;
  width: fit-content;
  text-transform: uppercase;
  font-size: 1.2rem;
  font-weight: 600;
`;

function MedicalHistoryRow({
  medicalHistory,
  handleDeleteMedicalHistory,
  isDeleting,
}) {
  return (
    <>
      <Table.Row role="row">
        <MedicalHistory>{medicalHistory.patientName}</MedicalHistory>
        <MedicalHistory>
          {medicalHistory.height} cm - {medicalHistory.weight} kg
        </MedicalHistory>
        <MedicalHistory>{medicalHistory.testsPerformed}</MedicalHistory>
        <MedicalHistory>{medicalHistory.diagnosis}</MedicalHistory>
        <MedicalHistory>
          <Problem>
            {medicalHistory.medicalProblems} &{" "}
            {medicalHistory.mentalHealthProblems}
          </Problem>
        </MedicalHistory>

        <div>
          <Modal>
            <ReadMedicalHistory medicalHistory={medicalHistory} />

            <Button>
              <Link
                to={`/update-medical-history/${medicalHistory.medicalHistoryId}`}
              >
                <HiPencil />
              </Link>
            </Button>

            <Modal.Open opens="delete-medical-history">
              <Button>
                <HiTrash />
              </Button>
            </Modal.Open>
            <Modal.Window name="delete-medical-history">
              <ConfirmDelete
                resourceName={medicalHistory.patientName}
                disabled={isDeleting}
                onConfirm={() =>
                  handleDeleteMedicalHistory(medicalHistory.medicalHistoryId)
                }
              />
            </Modal.Window>
          </Modal>
        </div>
      </Table.Row>
    </>
  );
}

export default MedicalHistoryRow;
