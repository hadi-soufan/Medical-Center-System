import React from 'react';
import Button from "../../ui/Button";
import Modal from "../../ui/Modal";
import { HiEye } from "react-icons/hi2";
import DetailsMedicalHistoryForm from './DetailsMedicalHistoryForm';

function ReadMedicalHistory({medicalHistory}) {
  return (
    <>
        <Modal>
      <Modal.Open opens='doctor-update-form'>
        <Button>
          <HiEye />
        </Button>
      </Modal.Open>

      <Modal.Window name='doctor-update-form'>
        <DetailsMedicalHistoryForm
          medicalHistoryData={medicalHistory}
        />
      </Modal.Window>
    </Modal>
    </>
  )
}

export default ReadMedicalHistory
