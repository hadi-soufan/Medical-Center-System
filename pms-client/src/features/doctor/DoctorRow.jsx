import React, { useEffect } from "react";
import styled from "styled-components";
import UpdateDoctor from "./UpdateDoctor";
import Button from "../../ui/Button";
import { HiTrash, HiEye } from "react-icons/hi2";
import Modal from "../../ui/Modal";
import ConfirmDelete from "../../ui/ConfirmDelete";
import Table from "../../ui/Table";
import { useDispatch, useSelector } from "react-redux";
import DoctorDetails from "./DoctorDetails";

const Doctor = styled.div`
  font-size: 1.6rem;
  font-weight: 600;
  color: var(--color-grey-600);
  font-family: "Sono";
`;

/**
 * Renders a row for a doctor in a table.
 *
 * @param {Object} props - The component props.
 * @param {Object} props.doctor - The doctor object to display.
 * @param {Function} props.handleDeleteDoctor - The function to handle deleting a doctor.
 * @param {Function} props.handleUpdate - The function to handle updating a doctor.
 * @param {boolean} props.isDeleting - Indicates whether a doctor is being deleted.
 * @returns {JSX.Element} The rendered DoctorRow component.
 */
function DoctorRow({
  doctor,
  handleDeleteDoctor,
  handleUpdate,
  isDeleting,
  getDoctorDetails,
}) {

  const dispatch = useDispatch();
  const doctorDetails = useSelector(state => state.doctor); 

  useEffect(() => {
    dispatch(getDoctorDetails(doctor.doctorId));
  }, [dispatch, getDoctorDetails, doctor.doctorId]);


  return (
      <Table.Row role="row">
        <Doctor>{doctor.displayName}</Doctor>
        <Doctor>{doctor.email}</Doctor>
        <Doctor>{doctor.phoneNumber}</Doctor>
        <Doctor>{doctor.doctorLicenseId}</Doctor>
        <Doctor>{doctor.appointmentCount}</Doctor>
        <div>
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
        </div>
      </Table.Row>
  );
}

export default DoctorRow;
