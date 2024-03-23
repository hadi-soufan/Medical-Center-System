import React from "react";
import DoctorRow from "./DoctorRow";
import Table from "../../ui/Table";
import Menus from "../../ui/Menus";

function DoctorTable({
  doctors,
  handleDeleteDoctor,
  handleUpdate,
  isDeleting,
  setIsDeleting,
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
            <DoctorRow
              key={doctor.doctorId}
              doctor={doctor}
              handleDeleteDoctor={handleDeleteDoctor}
              handleUpdate={handleUpdate}
              isDeleting={isDeleting}
              setIsDeleting={setIsDeleting}
            />
          )}
        />
      </Table>
    </Menus>
  );
}

export default DoctorTable;
