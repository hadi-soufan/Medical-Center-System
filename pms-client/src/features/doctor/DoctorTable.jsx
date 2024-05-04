import React from "react";
import DoctorRow from "./DoctorRow";
import Table from "../../ui/Table";
import Menus from "../../ui/Menus";
import Pagination from "../../ui/Pagination";

/**
 * Renders a table of doctors.
 *
 * @param {Object} props - The component props.
 * @param {Array} props.doctors - The array of doctors to display in the table.
 * @param {Function} props.handleDeleteDoctor - The function to handle deleting a doctor.
 * @param {Function} props.handleUpdate - The function to handle updating a doctor.
 * @param {boolean} props.isDeleting - Indicates whether a doctor is currently being deleted.
 * @param {Function} props.setIsDeleting - The function to set the isDeleting state.
 * @returns {JSX.Element} The rendered DoctorTable component.
 */
function DoctorTable({
  doctors,
  handleDeleteDoctor,
  handleUpdate,
  isDeleting,
  setIsDeleting,
  getDoctorDetails,
}) {


  return (
    <Menus>
      <Table columns="repeat(6, 1fr)">
        <Table.Header>
          <div>Name</div>
          <div>Email</div>
          <div>Phone</div>
          <div>Doctor Licence</div>
          <div>Appointments Count</div>
          <div>Actions</div>
        </Table.Header>

        <Table.Body
          data={doctors}
          render={(doctor) => (
            doctor && (
              <DoctorRow
                key={doctor.doctorId}
                doctor={doctor}
                handleDeleteDoctor={handleDeleteDoctor}
                handleUpdate={handleUpdate}
                isDeleting={isDeleting}
                setIsDeleting={setIsDeleting}
                getDoctorDetails={getDoctorDetails}
              />
            )
          )}
        />

        <Table.Footer>
          <Pagination />
        </Table.Footer>
        
      </Table>
    </Menus>
  );
}

export default DoctorTable;
