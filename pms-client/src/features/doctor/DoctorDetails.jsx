import React from 'react'
import Modal from '../../ui/Modal'
import { HiMiniPencilSquare } from 'react-icons/hi2'
import Button from '../../ui/Button'
import DetailsDoctorForm from './DetailsDoctorForm'

function DoctorDetails({doctorDetails}) {
  return (
    <Modal>
      <Modal.Open opens='doctor-details-form'>
        <Button>
          <HiMiniPencilSquare />
        </Button>
      </Modal.Open>

      <Modal.Window name='doctor-details-form'>
        <DetailsDoctorForm
          doctorData={doctorDetails}
        />
      </Modal.Window>
    </Modal>
  )
}

export default DoctorDetails
