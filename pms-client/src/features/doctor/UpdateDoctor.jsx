import React from "react";
import UpdateDoctorForm from "./UpdateDoctorForm";
import Button from "../../ui/Button";
import Modal from "../../ui/Modal";
import { HiMiniPencilSquare } from "react-icons/hi2";

function UpdateDoctor({ doctorData, handleUpdate }) {

  return (
    <Modal>
      <Modal.Open opens='doctor-update-form'>
        <Button>
          <HiMiniPencilSquare />
        </Button>
      </Modal.Open>

      <Modal.Window name='doctor-update-form'>
        <UpdateDoctorForm
          doctorData={doctorData}
          handleUpdate={handleUpdate}
        />
      </Modal.Window>
    </Modal>
  );
}


export default UpdateDoctor;
