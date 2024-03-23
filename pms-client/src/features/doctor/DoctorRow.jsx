import React from "react";
import styled from "styled-components";
import UpdateDoctor from "./UpdateDoctor";
import Button from "../../ui/Button";
import { HiTrash, HiEye } from "react-icons/hi2";
import Modal from "../../ui/Modal";
import ConfirmDelete from "../../ui/ConfirmDelete";
import Table from "../../ui/Table";

const Doctor = styled.div`
  font-size: 1.6rem;
  font-weight: 600;
  color: var(--color-grey-600);
  font-family: "Sono";
`;

function DoctorRow({
  doctor,
  handleDeleteDoctor,
  handleUpdate,
  isDeleting
}) {
  const { displayName, email, phoneNumber, doctorLicenseId, appointmentCount } =
    doctor;


  return (
    <>
      <Table.Row role="row">
        <Doctor>{displayName}</Doctor>
        <Doctor>{email}</Doctor>
        <Doctor>{phoneNumber}</Doctor>
        <Doctor>{doctorLicenseId}</Doctor>
        <Doctor>{appointmentCount}</Doctor>
        <div>
          <Modal>
            <Button>
              <HiEye />
            </Button>

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

          


        </div>
      </Table.Row>
    </>
  );
}

export default DoctorRow;
